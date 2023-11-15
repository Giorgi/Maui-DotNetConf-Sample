Alter table nyc_subway_stations add column "location" geography(Point,4326);

Update nyc_subway_stations set "location" = ST_Transform(geom, 4326)::geography;

Alter table nyc_neighborhoods add column "location" geography(MultiPolygon,4326);

Update nyc_neighborhoods set "location" = ST_Transform(geom, 4326)::geography;
