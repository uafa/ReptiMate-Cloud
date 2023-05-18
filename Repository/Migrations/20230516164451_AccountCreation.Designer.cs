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
    [Migration("20230516164451_AccountCreation")]
    partial class AccountCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Model.Account", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Model.Measurements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Co2")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Model.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("DateTime");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Model.Terrarium", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<Guid>("measurementsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("terrariumBoundariesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("terrariumLimitsId")
                        .HasColumnType("uuid");

                    b.HasKey("name");

                    b.HasIndex("measurementsId");

                    b.HasIndex("terrariumBoundariesId");

                    b.HasIndex("terrariumLimitsId");

                    b.ToTable("Terrarium");
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

            modelBuilder.Entity("Model.TerrariumLimits", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("TemperatureLimitMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureLimitMin")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("TerrariumLimits");
                });

            modelBuilder.Entity("Model.Terrarium", b =>
                {
                    b.HasOne("Model.Measurements", "measurements")
                        .WithMany()
                        .HasForeignKey("measurementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.TerrariumBoundaries", "terrariumBoundaries")
                        .WithMany()
                        .HasForeignKey("terrariumBoundariesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.TerrariumLimits", "terrariumLimits")
                        .WithMany()
                        .HasForeignKey("terrariumLimitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("measurements");

                    b.Navigation("terrariumBoundaries");

                    b.Navigation("terrariumLimits");
                });
#pragma warning restore 612, 618
        }
    }
}
