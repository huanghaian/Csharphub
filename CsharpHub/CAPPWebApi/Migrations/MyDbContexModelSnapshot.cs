﻿// <auto-generated />
using System;
using CAPPWebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CAPPWebApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("CAPPWebApi.Models.TodayWeather", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOverTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sky")
                        .HasColumnType("TEXT");

                    b.Property<string>("Temperature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Today")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Weather")
                        .HasColumnType("TEXT");

                    b.Property<string>("Wind")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TodayWeathers");
                });

            modelBuilder.Entity("CAPPWebApi.Models.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .HasColumnType("TEXT");

                    b.Property<string>("Temperature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Weath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Wind")
                        .HasColumnType("TEXT");

                    b.Property<string>("WindLevel")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
