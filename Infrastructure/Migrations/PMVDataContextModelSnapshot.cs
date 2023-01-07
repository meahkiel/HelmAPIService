﻿// <auto-generated />
using System;
using Infrastructure.Context.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PMVDataContext))]
    partial class PMVDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("HLMPMV")
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.PMV.Alerts.ServiceAlert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Assigned")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiceAlerts", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Alerts.ServiceAlertDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KmAlert")
                        .HasColumnType("int");

                    b.Property<int>("KmInterval")
                        .HasColumnType("int");

                    b.Property<Guid>("ServiceAlertId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceAlertId");

                    b.ToTable("ServiceAlertDetails", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Assets.AssetRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentReading")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastInspectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LatestTransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetRecords", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Assets.ServiceLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KmAlert")
                        .HasColumnType("int");

                    b.Property<int>("KmInterval")
                        .HasColumnType("int");

                    b.Property<int>("LastReading")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiceLogs", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Fuels.FuelLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentNo")
                        .HasColumnType("int");

                    b.Property<int?>("EndShiftTankerKm")
                        .HasColumnType("int");

                    b.Property<string>("Fueler")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<float>("OpeningBalance")
                        .HasColumnType("real");

                    b.Property<float>("OpeningMeter")
                        .HasColumnType("real");

                    b.Property<int>("ReferenceNo")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShiftEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShiftStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StartShiftTankerKm")
                        .HasColumnType("int");

                    b.Property<string>("StationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelLog", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Fuels.FuelTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Driver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverQatarIdUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FuelDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FuelLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FuelStation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreviousReading")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("Reading")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FuelLogId");

                    b.ToTable("FuelTransactions", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.LogSheets.LogSheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EndShiftMeterReading")
                        .HasColumnType("int");

                    b.Property<int?>("EndShiftTankerKm")
                        .HasColumnType("int");

                    b.Property<string>("Fueler")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("ReferenceNo")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShiftEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ShiftStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StartShiftMeterReading")
                        .HasColumnType("int");

                    b.Property<int>("StartShiftTankerKm")
                        .HasColumnType("int");

                    b.Property<string>("StationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogSheets", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.LogSheets.LogSheetDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverQatarIdUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FuelTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LogSheetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OperatorDriver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreviousReading")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("Reading")
                        .HasColumnType("int");

                    b.Property<string>("RefillStation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LogSheetId");

                    b.ToTable("LogSheetDetails", "HLMPMV");
                });

            modelBuilder.Entity("Core.PMV.Alerts.ServiceAlertDetail", b =>
                {
                    b.HasOne("Core.PMV.Alerts.ServiceAlert", "ServiceAlert")
                        .WithMany("Details")
                        .HasForeignKey("ServiceAlertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceAlert");
                });

            modelBuilder.Entity("Core.PMV.Fuels.FuelLog", b =>
                {
                    b.OwnsOne("Core.Common.ValueObjects.PostingObject", "Post", b1 =>
                        {
                            b1.Property<Guid>("FuelLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsPosted")
                                .HasColumnType("bit");

                            b1.Property<DateTime?>("PostedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("FuelLogId");

                            b1.ToTable("FuelLog", "HLMPMV");

                            b1.WithOwner()
                                .HasForeignKey("FuelLogId");
                        });

                    b.Navigation("Post")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.PMV.Fuels.FuelTransaction", b =>
                {
                    b.HasOne("Core.PMV.Fuels.FuelLog", "FuelLog")
                        .WithMany("FuelTransactions")
                        .HasForeignKey("FuelLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelLog");
                });

            modelBuilder.Entity("Core.PMV.LogSheets.LogSheet", b =>
                {
                    b.OwnsOne("Core.Common.ValueObjects.PostingObject", "Post", b1 =>
                        {
                            b1.Property<Guid>("LogSheetId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsPosted")
                                .HasColumnType("bit");

                            b1.Property<DateTime?>("PostedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("LogSheetId");

                            b1.ToTable("LogSheets", "HLMPMV");

                            b1.WithOwner()
                                .HasForeignKey("LogSheetId");
                        });

                    b.Navigation("Post")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.PMV.LogSheets.LogSheetDetail", b =>
                {
                    b.HasOne("Core.PMV.LogSheets.LogSheet", "LogSheet")
                        .WithMany("Details")
                        .HasForeignKey("LogSheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogSheet");
                });

            modelBuilder.Entity("Core.PMV.Alerts.ServiceAlert", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("Core.PMV.Fuels.FuelLog", b =>
                {
                    b.Navigation("FuelTransactions");
                });

            modelBuilder.Entity("Core.PMV.LogSheets.LogSheet", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
