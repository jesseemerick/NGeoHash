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
var result = GeoHash.Encode(location.latitude, location.longitude); //

```
