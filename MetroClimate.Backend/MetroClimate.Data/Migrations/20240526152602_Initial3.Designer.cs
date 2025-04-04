﻿// <auto-generated />
using System;
using MetroClimate.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetroClimate.Data.Migrations
{
    [DbContext(typeof(MetroClimateDbContext))]
    [Migration("20240526152602_Initial3")]
    partial class Initial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MetroClimate.Data.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("description");

                    b.Property<DateTime>("LastReceived")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_received");

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("name");

                    b.Property<int>("SensorTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("sensor_type_id");

                    b.Property<string>("StationId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("station_id");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_sensors");

                    b.HasIndex("SensorTypeId")
                        .HasDatabaseName("ix_sensors_sensor_type_id");

                    b.HasIndex("StationId")
                        .HasDatabaseName("ix_sensors_station_id");

                    b.ToTable("sensors", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.SensorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<string>("Formula")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("formula");

                    b.Property<int>("MaxValue")
                        .HasColumnType("integer")
                        .HasColumnName("max_value");

                    b.Property<int>("MinValue")
                        .HasColumnType("integer")
                        .HasColumnName("min_value");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int>("SensorTypeEnum")
                        .HasColumnType("integer")
                        .HasColumnName("sensor_type_enum");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("unit");

                    b.HasKey("Id")
                        .HasName("pk_sensor_types");

                    b.ToTable("sensor_types", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.Station", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<DateTime>("LastReceived")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_received");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("name");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_stations");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_stations_user_id");

                    b.ToTable("stations", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.StationReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int>("SensorId")
                        .HasColumnType("integer")
                        .HasColumnName("sensor_id");

                    b.Property<string>("StationId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("station_id");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<double>("Value")
                        .HasColumnType("double precision")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_station_readings");

                    b.HasIndex("SensorId")
                        .HasDatabaseName("ix_station_readings_sensor_id");

                    b.HasIndex("StationId")
                        .HasDatabaseName("ix_station_readings_station_id");

                    b.ToTable("station_readings", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_users_username");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.WeatherForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Summary")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("summary");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("integer")
                        .HasColumnName("temperature_c");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_weather_forecasts");

                    b.ToTable("weather_forecasts", (string)null);
                });

            modelBuilder.Entity("MetroClimate.Data.Models.Sensor", b =>
                {
                    b.HasOne("MetroClimate.Data.Models.SensorType", "SensorType")
                        .WithMany()
                        .HasForeignKey("SensorTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sensors_sensor_types_sensor_type_id");

                    b.HasOne("MetroClimate.Data.Models.Station", "Station")
                        .WithMany("Sensors")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sensors_stations_station_id");

                    b.Navigation("SensorType");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("MetroClimate.Data.Models.Station", b =>
                {
                    b.HasOne("MetroClimate.Data.Models.User", "User")
                        .WithMany("Stations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stations_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MetroClimate.Data.Models.StationReading", b =>
                {
                    b.HasOne("MetroClimate.Data.Models.Sensor", "Sensor")
                        .WithMany("Readings")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_station_readings_sensors_sensor_id");

                    b.HasOne("MetroClimate.Data.Models.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_station_readings_stations_station_id");

                    b.Navigation("Sensor");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("MetroClimate.Data.Models.Sensor", b =>
                {
                    b.Navigation("Readings");
                });

            modelBuilder.Entity("MetroClimate.Data.Models.Station", b =>
                {
                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("MetroClimate.Data.Models.User", b =>
                {
                    b.Navigation("Stations");
                });
#pragma warning restore 612, 618
        }
    }
}
