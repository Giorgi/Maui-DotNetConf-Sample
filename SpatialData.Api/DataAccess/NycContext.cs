using Microsoft.EntityFrameworkCore;
using SpatialData.Api.Models;

namespace SpatialData.Api.DataAccess;

public partial class NycContext : DbContext
{
    public NycContext()
    {
    }

    public NycContext(DbContextOptions<NycContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NycCensusBlock> NycCensusBlocks { get; set; }

    public virtual DbSet<NycHomicide> NycHomicides { get; set; }

    public virtual DbSet<NycNeighborhood> NycNeighborhoods { get; set; }

    public virtual DbSet<NycStreet> NycStreets { get; set; }

    public virtual DbSet<NycSubwayStation> NycSubwayStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<NycCensusBlock>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nyc_census_blocks_pkey");

            entity.ToTable("nyc_census_blocks");

            entity.HasIndex(e => e.Geom, "nyc_census_blocks_geom_idx").HasMethod("gist");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.Blkid)
                .HasMaxLength(15)
                .HasColumnName("blkid");
            entity.Property(e => e.Boroname)
                .HasMaxLength(32)
                .HasColumnName("boroname");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(MultiPolygon,26918)")
                .HasColumnName("geom");
            entity.Property(e => e.PopnAsian).HasColumnName("popn_asian");
            entity.Property(e => e.PopnBlack).HasColumnName("popn_black");
            entity.Property(e => e.PopnNativ).HasColumnName("popn_nativ");
            entity.Property(e => e.PopnOther).HasColumnName("popn_other");
            entity.Property(e => e.PopnTotal).HasColumnName("popn_total");
            entity.Property(e => e.PopnWhite).HasColumnName("popn_white");
        });

        modelBuilder.Entity<NycHomicide>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nyc_homicides_pkey");

            entity.ToTable("nyc_homicides");

            entity.HasIndex(e => e.Geom, "nyc_homicides_geom_idx").HasMethod("gist");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.Boroname)
                .HasMaxLength(13)
                .HasColumnName("boroname");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(Point,26918)")
                .HasColumnName("geom");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IncidentD).HasColumnName("incident_d");
            entity.Property(e => e.LightDark)
                .HasMaxLength(1)
                .HasColumnName("light_dark");
            entity.Property(e => e.NumVictim)
                .HasMaxLength(1)
                .HasColumnName("num_victim");
            entity.Property(e => e.PrimaryMo)
                .HasMaxLength(20)
                .HasColumnName("primary_mo");
            entity.Property(e => e.Weapon)
                .HasMaxLength(16)
                .HasColumnName("weapon");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<NycNeighborhood>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nyc_neighborhoods_pkey");

            entity.ToTable("nyc_neighborhoods");

            entity.HasIndex(e => e.Geom, "nyc_neighborhoods_geom_idx").HasMethod("gist");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.Boroname)
                .HasMaxLength(43)
                .HasColumnName("boroname");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(MultiPolygon,26918)")
                .HasColumnName("geom");
            entity.Property(e => e.Location)
                .HasColumnType("geography(MultiPolygon,4326)")
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<NycStreet>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nyc_streets_pkey");

            entity.ToTable("nyc_streets");

            entity.HasIndex(e => e.Geom, "nyc_streets_geom_idx").HasMethod("gist");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(MultiLineString,26918)")
                .HasColumnName("geom");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Oneway)
                .HasMaxLength(10)
                .HasColumnName("oneway");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<NycSubwayStation>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nyc_subway_stations_pkey");

            entity.ToTable("nyc_subway_stations");

            entity.HasIndex(e => e.Geom, "nyc_subway_stations_geom_idx").HasMethod("gist");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.AltName)
                .HasMaxLength(38)
                .HasColumnName("alt_name");
            entity.Property(e => e.Borough)
                .HasMaxLength(15)
                .HasColumnName("borough");
            entity.Property(e => e.Closed)
                .HasMaxLength(10)
                .HasColumnName("closed");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.CrossSt)
                .HasMaxLength(27)
                .HasColumnName("cross_st");
            entity.Property(e => e.Express)
                .HasMaxLength(10)
                .HasColumnName("express");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(Point,26918)")
                .HasColumnName("geom");
            entity.Property(e => e.Location)
                .HasColumnType("geography(Point,4326)")
                .HasColumnName("location");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasColumnName("label");
            entity.Property(e => e.LongName)
                .HasMaxLength(60)
                .HasColumnName("long_name");
            entity.Property(e => e.Name)
                .HasMaxLength(31)
                .HasColumnName("name");
            entity.Property(e => e.Nghbhd)
                .HasMaxLength(30)
                .HasColumnName("nghbhd");
            entity.Property(e => e.Objectid).HasColumnName("objectid");
            entity.Property(e => e.Routes)
                .HasMaxLength(20)
                .HasColumnName("routes");
            entity.Property(e => e.Transfers)
                .HasMaxLength(25)
                .HasColumnName("transfers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
