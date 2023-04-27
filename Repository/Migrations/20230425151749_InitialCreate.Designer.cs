﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230425151749_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
#pragma warning restore 612, 618
        }
    }
}