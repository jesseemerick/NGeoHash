using System;
using Xunit;

namespace NGeoHash.Tests.Unit
{
    public class GeoHashTests
    {
        private const double Latitude = 37.8324;
        private const double Longitude = 112.5584;
        private const string HashString = "ww8p1r4t8";
        private const long HashStringInt = 4064984913515641;

        [Fact]
        public void Encode_WhenTheNumberOfCharsIsNotProvided_ReturnsHashStringWithTheDefaultNumberOfChars()
        {
            const int defaultHashStringNumberOfChars = 9;

            var actualHashString = GeoHash.Encode(Latitude, Longitude);

            Assert.Equal(defaultHashStringNumberOfChars, actualHashString.Length);
            Assert.Equal(HashString, actualHashString);
        }

        [Fact]
        public void Encode_WhenTheNumberOfCharsIsProvided_ReturnsHashStringWithTheProvidedNumberOfChars()
        {
            const int hashStringNumberOfChars = 3;

            var actualHashString = GeoHash.Encode(32, 117, hashStringNumberOfChars);

            Assert.Equal(hashStringNumberOfChars, actualHashString.Length);
            Assert.Equal("wte", actualHashString);
        }

        [Fact]
        public void EncodeInt_WhenTheBitDepthIsNotProvided_ReturnsHashStringIntWithTheDefaultBitDepth()
        {
            var actualHashStringInt = GeoHash.EncodeInt(Latitude, Longitude);

            Assert.Equal(HashStringInt, actualHashStringInt);
        }

        [Fact]
        public void EncodeInt_WhenTheBitDepthIsProvided_ReturnsHashStringIntWithTheProvidedBitDepth()
        {
            var actualHashStringInt = GeoHash.EncodeInt(Latitude, Longitude, 26);

            Assert.Equal(60572995, actualHashStringInt);
        }

        [Fact]
        public void Decode_WhenHashStringHasDefaultLength_ReturnsCoordinatesWithinStandardPrecision()
        {
            var geoHashDecodeResult = GeoHash.Decode(HashString);

            Assert.True(Math.Abs(Latitude - geoHashDecodeResult.Coordinates.Lat) < 0.0001, "(37.8324 - " + geoHashDecodeResult.Coordinates.Lat + " was >= 0.0001");
            Assert.True(Math.Abs(Longitude - geoHashDecodeResult.Coordinates.Lon) < 0.0001, "(112.5584 - " + geoHashDecodeResult.Coordinates.Lon + " was >= 0.0001");
        }

        [Fact]
        public void DecodeInt_WhenHashStringIntHasDefaultBitDepth_ReturnsCoordinatesWithinStandardPrecision()
        {
            var geoHashDecodeResult = GeoHash.DecodeInt(HashStringInt);

            Assert.True(Math.Abs(Latitude - geoHashDecodeResult.Coordinates.Lat) < 0.0001, "(37.8324 - " + geoHashDecodeResult.Coordinates.Lat + " was >= 0.0001");
            Assert.True(Math.Abs(Longitude - geoHashDecodeResult.Coordinates.Lon) < 0.0001, "(112.5584 - " + geoHashDecodeResult.Coordinates.Lon + " was >= 0.0001");
        }

        [Theory]
        [InlineData("dqcjq", new[] { 1, 0 }, "dqcjw")]
        [InlineData("dqcjq", new[] { -1, -1 }, "dqcjj")]
        public void Neighbor_WhenDirectionIsProvided_ReturnsTheNeighborInTheProvidedDirection(string hashString, int[] direction, string expectedNeighborHashString)
        {
            var neighborHashString = GeoHash.Neighbor(hashString, direction);

            Assert.Equal(expectedNeighborHashString, neighborHashString);
        }

        [Theory]
        [InlineData(1702789509, new[] { 1, 0 }, 32, 1702789520)]
        [InlineData(27898503327470, new[] { -1, -1 }, 46, 27898503327465)]
        public void NeighborInt_WhenDirectionIsProvided_ReturnsTheNeighborInTheProvidedDirection(long hashInt, int[] direction, int bitDepth, long expectedNeighborHashInt)
        {
            var neighborHashInt = GeoHash.NeighborInt(hashInt, direction, bitDepth);

            Assert.Equal(expectedNeighborHashInt, neighborHashInt);
        }

        [Fact]
        public void Neighbors_ReturnsNeighborsInAllDirections()
        {
            var expectedNeighbors = new[] { "dqcjw", "dqcjx", "dqcjr", "dqcjp", "dqcjn", "dqcjj", "dqcjm", "dqcjt" };

            var neighbors = GeoHash.Neighbors("dqcjq");

            Assert.Equal(expectedNeighbors, neighbors);
        }

        [Fact]
        public void NeighborsInt_ReturnsNeighborsInAllDirections()
        {
            var expectedNeighbors = new long[] { 1702789520, 1702789522, 1702789511, 1702789510, 1702789508, 1702789422, 1702789423, 1702789434 };

            var neighbors = GeoHash.NeighborsInt(1702789509, 32);

            Assert.Equal(expectedNeighbors, neighbors);
        }

        [Fact]
        public void BBoxes_ReturnsHashStringsBetweenCoordinates()
        {
            var bBoxes = GeoHash.Bboxes(30, 120, 30.0001, 120.0001, 8);

            Assert.Equal(GeoHash.Encode(30.0001, 120.0001, 8), bBoxes[bBoxes.Length -1]);
        }

        [Fact]
        public void BboxesInt_ReturnsHashStringsIntBetweenCoordinates()
        {
            var bBoxes = GeoHash.BboxesInt(30, 120, 30.0001, 120.0001, 8);

            Assert.Equal(GeoHash.EncodeInt(30.0001, 120.0001, 8), bBoxes[bBoxes.Length - 1]);
        }
    }
}
