﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaletteWebApp.Models;

#nullable disable

namespace PaletteWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240612060415_addNameToColor")]
    partial class addNameToColor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ColorPalette", b =>
                {
                    b.Property<int>("ColorsId")
                        .HasColumnType("int");

                    b.Property<int>("palettesId")
                        .HasColumnType("int");

                    b.HasKey("ColorsId", "palettesId");

                    b.HasIndex("palettesId");

                    b.ToTable("ColorPalette");
                });

            modelBuilder.Entity("PaletteWebApp.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("ColorCode")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("PaletteWebApp.Models.Palette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Palettes");
                });

            modelBuilder.Entity("ColorPalette", b =>
                {
                    b.HasOne("PaletteWebApp.Models.Color", null)
                        .WithMany()
                        .HasForeignKey("ColorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaletteWebApp.Models.Palette", null)
                        .WithMany()
                        .HasForeignKey("palettesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
