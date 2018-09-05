### NGeoHash
========
[![NuGet Version and Downloads count](https://buildstats.info/nuget/ngeohash)](https://www.nuget.org/packages/ngeohash)

C# Port of Node-Geohash 

This is a line for line port to C# of Node-Geohash https://github.com/sunng87/node-geohash

The project is available in nuget - https://www.nuget.org/packages/ngeohash/

#### Usage
```csharp

// Fort Worth
var location = new
{
	latitude = 32.768799,
	longitude = -97.309341,
};
var encoded = GeoHash.Encode(location.latitude, location.longitude); // "9vff3tms0"

var decoded = GeoHash.Decode("9vff3tms0");
var latitude = decoded.Coordinates.Lat;		// 32.768805027008057
var longitude = decoded.Coordinates.Lon;	// -97.309319972991943

var errorMarginLat = decoded.Error.Lat //	2.1457672119140625E-05
var errorMarginLon = decoded.Error.Lon //	2.1457672119140625E-05

```


#### Invalid Usage
```csharp

var bad = GeoHash.Decode("happy times"); // Throws exception
```
