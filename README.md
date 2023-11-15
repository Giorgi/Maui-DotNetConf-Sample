# Maui DotNetConf 2023 Spatial Data Sample App

## Overview

This is a sample app and backend code for my [Spatial Data with Entity Framework Core and .NET MAUI](https://www.dotnetconf.net/agenda#536258) session.

The code demonstrates how to work with spatial data with EF Core and ASP.NET and how to use MAUI Map control.

The projects use the following libraries:

 - [NetTopologySuite](https://github.com/NetTopologySuite/NetTopologySuite)
 - [NetTopologySuite.IO.GeoJSON](https://github.com/NetTopologySuite/NetTopologySuite.IO.GeoJSON)
 - [.NET MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)
 - [.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet)
 - [Refit](https://github.com/reactiveui/refit)

## Running the projects locally

There are several steps required to run the project locally

### Set up PostgreSQL database
The backend project uses PostgreSQL. Once you set up the PostgreSQL server, [create a spatial database](https://postgis.net/workshops/en/postgis-intro/creating_db.html) 
and [restore data.](https://postgis.net/workshops/en/postgis-intro/loading_data.html). Next, run [scripts.sql](https://github.com/Giorgi/Maui-DotNetConf-Sample/blob/main/SpatialData.Api/scripts.sql) from the project. Finally, update the connection string in the project.

### Get a Google Maps API key
To use the Map control follow the [Platform configuration](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/map?view=net-maui-8.0#platform-configuration) steps from the official documentation

### Set service endpoint
If you use a different port or want to run the app on a local device, you must update the service base url in `MauiProgram.cs`
