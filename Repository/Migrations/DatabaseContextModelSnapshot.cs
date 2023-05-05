﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Model.Measurements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Co2")
                        .HasColumnType("double precision");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Model.TerrariumBoundaries", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CO2BoundaryMax")
                        .HasColumnType("double precision");

                    b.Property<double>("CO2BoundaryMin")
                        .HasColumnType("double precision");

                    b.Property<double>("HumidityBoundaryMax")
                        .HasColumnType("double precision");

                    b.Property<double>("HumidityBoundaryMin")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureBoundaryMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureBoundaryMin")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("TerrariumBoundaries");
                });
#pragma warning restore 612, 618
        }
    }
}
