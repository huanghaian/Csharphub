﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWeb;

namespace TestWeb.Migrations
{
    [DbContext(typeof(MyDbContex))]
    [Migration("20200528102029_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("TestWeb.Models.WeatherModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .HasColumnType("TEXT");

                    b.Property<string>("Temperature")
                        .HasColumnType("TEXT");

                    b.Property<string>("Weath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Wind")
                        .HasColumnType("TEXT");

                    b.Property<string>("WindLevel")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WeatherModels");
                });
#pragma warning restore 612, 618
        }
    }
}
