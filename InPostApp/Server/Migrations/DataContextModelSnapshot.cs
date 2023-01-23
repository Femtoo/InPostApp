﻿// <auto-generated />
using System;
using InPostApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InPostApp.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InPostApp.Shared.Models.Locker", b =>
                {
                    b.Property<int>("LockerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LockerId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LockerId");

                    b.ToTable("Lockers");
                });

            modelBuilder.Entity("InPostApp.Shared.Models.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FromLockerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToLockerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromLockerId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("ToLockerId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("InPostApp.Shared.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InPostApp.Shared.Models.Shipment", b =>
                {
                    b.HasOne("InPostApp.Shared.Models.Locker", "FromLocker")
                        .WithMany("FromLockerDelivery")
                        .HasForeignKey("FromLockerId")
                        .IsRequired();

                    b.HasOne("InPostApp.Shared.Models.User", "Receiver")
                        .WithMany("ReceivedShipments")
                        .HasForeignKey("ReceiverId")
                        .IsRequired();

                    b.HasOne("InPostApp.Shared.Models.User", "Sender")
                        .WithMany("SentShipments")
                        .HasForeignKey("SenderId")
                        .IsRequired();

                    b.HasOne("InPostApp.Shared.Models.Locker", "ToLocker")
                        .WithMany("ToLockerDelivery")
                        .HasForeignKey("ToLockerId")
                        .IsRequired();

                    b.Navigation("FromLocker");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("ToLocker");
                });

            modelBuilder.Entity("InPostApp.Shared.Models.Locker", b =>
                {
                    b.Navigation("FromLockerDelivery");

                    b.Navigation("ToLockerDelivery");
                });

            modelBuilder.Entity("InPostApp.Shared.Models.User", b =>
                {
                    b.Navigation("ReceivedShipments");

                    b.Navigation("SentShipments");
                });
#pragma warning restore 612, 618
        }
    }
}