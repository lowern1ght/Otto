﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Otto.Storage.City;

#nullable disable

namespace Otto.Storage.City.Migrations
{
    [DbContext(typeof(CityDbContext))]
    [Migration("20231001200437_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Otto.Storage.City.Entity.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint")
                        .HasColumnName("country_id");

                    b.Property<float>("Latitude")
                        .HasColumnType("real")
                        .HasColumnName("latitude");

                    b.Property<float>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("longitude");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_cities");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_cities_country_id");

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("Otto.Storage.City.Entity.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_countries");

                    b.ToTable("countries", (string)null);
                });

            modelBuilder.Entity("Otto.Storage.City.Entity.City", b =>
                {
                    b.HasOne("Otto.Storage.City.Entity.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cities_countries_country_id");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Otto.Storage.City.Entity.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
