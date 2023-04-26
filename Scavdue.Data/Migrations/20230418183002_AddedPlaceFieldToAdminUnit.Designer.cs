﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scavdue.Data;

#nullable disable

namespace Scavdue.Data.Migrations
{
    [DbContext(typeof(ScavdueApiDbContext))]
    [Migration("20230418183002_AddedPlaceFieldToAdminUnit")]
    partial class AddedPlaceFieldToAdminUnit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scavdue.Core.Models.AdministrativeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdministrativeLevel")
                        .HasColumnType("integer");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentAdministrativeUnitId")
                        .HasColumnType("integer");

                    b.Property<string>("Place")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("AdministrativeUnit_pkey");

                    b.HasIndex("CountryId");

                    b.HasIndex("ParentAdministrativeUnitId");

                    b.ToTable("AdministrativeUnits", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.AdministrativeUnitPolygon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdministrativeUnitId")
                        .HasColumnType("integer");

                    b.Property<float?>("CenterLat")
                        .HasColumnType("real");

                    b.Property<float?>("CenterLong")
                        .HasColumnType("real");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("AdministrativeUnitPolygon_pkey");

                    b.HasIndex("AdministrativeUnitId");

                    b.ToTable("AdministrativeUnitPolygons", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Iso3166")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Country_pkey");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.Population", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdministrativeUnitId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("Population_pkey");

                    b.HasIndex("AdministrativeUnitId");

                    b.ToTable("Populations", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdministrativeUnitId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UnitObjectTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("UnitObject_pkey");

                    b.HasIndex("AdministrativeUnitId");

                    b.HasIndex("UnitObjectTypeId");

                    b.ToTable("UnitObjects", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("UnitObjectClass_pkey");

                    b.ToTable("UnitObjectClasses", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectPolygon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float?>("CenterLat")
                        .HasColumnType("real");

                    b.Property<float?>("CenterLong")
                        .HasColumnType("real");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitObjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("UnitObjectPolygon_pkey");

                    b.HasIndex("UnitObjectId");

                    b.ToTable("UnitObjectPolygons", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitObjectClassId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("UnitObjectType_pkey");

                    b.HasIndex("UnitObjectClassId");

                    b.ToTable("UnitObjectTypes", (string)null);
                });

            modelBuilder.Entity("Scavdue.Core.Models.AdministrativeUnit", b =>
                {
                    b.HasOne("Scavdue.Core.Models.Country", "Country")
                        .WithMany("AdministrativeUnits")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scavdue.Core.Models.AdministrativeUnit", "ParentAdministrativeUnit")
                        .WithMany("ChildAdministrativeUnits")
                        .HasForeignKey("ParentAdministrativeUnitId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Country");

                    b.Navigation("ParentAdministrativeUnit");
                });

            modelBuilder.Entity("Scavdue.Core.Models.AdministrativeUnitPolygon", b =>
                {
                    b.HasOne("Scavdue.Core.Models.AdministrativeUnit", "AdministrativeUnit")
                        .WithMany("AdministrativeUnitPolygons")
                        .HasForeignKey("AdministrativeUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdministrativeUnit");
                });

            modelBuilder.Entity("Scavdue.Core.Models.Population", b =>
                {
                    b.HasOne("Scavdue.Core.Models.AdministrativeUnit", "AdministrativeUnit")
                        .WithMany("Populations")
                        .HasForeignKey("AdministrativeUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdministrativeUnit");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObject", b =>
                {
                    b.HasOne("Scavdue.Core.Models.AdministrativeUnit", "AdministrativeUnit")
                        .WithMany("UnitObjects")
                        .HasForeignKey("AdministrativeUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scavdue.Core.Models.UnitObjectType", "UnitObjectType")
                        .WithMany("UnitObjects")
                        .HasForeignKey("UnitObjectTypeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("AdministrativeUnit");

                    b.Navigation("UnitObjectType");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectPolygon", b =>
                {
                    b.HasOne("Scavdue.Core.Models.UnitObject", "UnitObject")
                        .WithMany("UnitObjectPolygons")
                        .HasForeignKey("UnitObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UnitObject");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectType", b =>
                {
                    b.HasOne("Scavdue.Core.Models.UnitObjectClass", "UnitObjectClass")
                        .WithMany("UnitObjectTypes")
                        .HasForeignKey("UnitObjectClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UnitObjectClass");
                });

            modelBuilder.Entity("Scavdue.Core.Models.AdministrativeUnit", b =>
                {
                    b.Navigation("AdministrativeUnitPolygons");

                    b.Navigation("ChildAdministrativeUnits");

                    b.Navigation("Populations");

                    b.Navigation("UnitObjects");
                });

            modelBuilder.Entity("Scavdue.Core.Models.Country", b =>
                {
                    b.Navigation("AdministrativeUnits");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObject", b =>
                {
                    b.Navigation("UnitObjectPolygons");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectClass", b =>
                {
                    b.Navigation("UnitObjectTypes");
                });

            modelBuilder.Entity("Scavdue.Core.Models.UnitObjectType", b =>
                {
                    b.Navigation("UnitObjects");
                });
#pragma warning restore 612, 618
        }
    }
}