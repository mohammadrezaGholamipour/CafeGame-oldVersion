﻿// <auto-generated />
using System;
using CafeGameApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeGameApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("CafeGameApi.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FinalCost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HourRateId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("SystemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HourRateId");

                    b.HasIndex("SystemId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("CafeGameApi.Entities.BillFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("FoodId");

                    b.ToTable("BillFoods");
                });

            modelBuilder.Entity("CafeGameApi.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cost")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("CafeGameApi.Entities.HourRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rate")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("HourRates");
                });

            modelBuilder.Entity("CafeGameApi.Entities.PSSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PSSystems");
                });

            modelBuilder.Entity("CafeGameApi.Entities.Bill", b =>
                {
                    b.HasOne("CafeGameApi.Entities.HourRate", "HourRate")
                        .WithMany()
                        .HasForeignKey("HourRateId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("CafeGameApi.Entities.PSSystem", "System")
                        .WithMany("Bills")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("HourRate");

                    b.Navigation("System");
                });

            modelBuilder.Entity("CafeGameApi.Entities.BillFood", b =>
                {
                    b.HasOne("CafeGameApi.Entities.Bill", "Bill")
                        .WithMany("BillFoods")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("CafeGameApi.Entities.Food", "Food")
                        .WithMany("BillFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("CafeGameApi.Entities.Bill", b =>
                {
                    b.Navigation("BillFoods");
                });

            modelBuilder.Entity("CafeGameApi.Entities.Food", b =>
                {
                    b.Navigation("BillFoods");
                });

            modelBuilder.Entity("CafeGameApi.Entities.PSSystem", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
