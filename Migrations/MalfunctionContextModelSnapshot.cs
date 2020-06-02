﻿// <auto-generated />
using System;
using EquipmentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EquipmentManagementSystem.Migrations
{
    [DbContext(typeof(MalfunctionContext))]
    partial class MalfunctionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("EquipmentManagementSystem.Models.AccessoriesOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("PlaceTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("AccessoriesOrder");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Assert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<string>("SourceUnit")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentId")
                        .IsUnique();

                    b.ToTable("Assert");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Calibration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Calibrator")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("InstrumentID")
                        .HasColumnType("TEXT");

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("Calibration");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Component", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Computer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("IP")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID")
                        .IsUnique();

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Instrument", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<int>("CalibrationCycle")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("MetrologicalCharacteristics")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("NewSystemCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Principal")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("ProjectTeamName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Projects")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<DateTime>("StartUsingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Investigation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Measures")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<string>("Operator")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("Investigation");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Maintenance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Attachment")
                        .HasColumnType("BLOB");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<bool>("IsCritical")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<string>("Repairer")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Solution")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("Maintenance");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Attachment")
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("FoundedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Part")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Phenomenon")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("MalfunctionInfo");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionPart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("MalfunctionPart");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionPhenomenon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phenomenon")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("MalfunctionPhenomenon");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionReason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("MalfunctionReason");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionWorkOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("TEXT");

                    b.Property<int>("Progress")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("MalfunctionWorkOrder");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.RepairRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BookingsTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Engineer")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Fixer")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("RepairRequest");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Validation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Attachment")
                        .HasColumnType("BLOB");

                    b.Property<string>("AttachmentName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<byte[]>("EffectReportFile")
                        .HasColumnType("BLOB");

                    b.Property<string>("EffectReportFileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PerformanceReportFile")
                        .HasColumnType("BLOB");

                    b.Property<string>("PerformanceReportFileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Summary")
                        .HasColumnType("TEXT")
                        .HasMaxLength(999);

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("Validation");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.AccessoriesOrder", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("AccessoriesOrder")
                        .HasForeignKey("EquipmentManagementSystem.Models.AccessoriesOrder", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Assert", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithOne("Assert")
                        .HasForeignKey("EquipmentManagementSystem.Models.Assert", "InstrumentId");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Calibration", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithMany("Calibrations")
                        .HasForeignKey("InstrumentID");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Component", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithMany("Components")
                        .HasForeignKey("InstrumentID");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Computer", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithOne("Computer")
                        .HasForeignKey("EquipmentManagementSystem.Models.Computer", "InstrumentID");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Investigation", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("Investigation")
                        .HasForeignKey("EquipmentManagementSystem.Models.Investigation", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Maintenance", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("Maintenance")
                        .HasForeignKey("EquipmentManagementSystem.Models.Maintenance", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionInfo", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("MalfunctionInfo")
                        .HasForeignKey("EquipmentManagementSystem.Models.MalfunctionInfo", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionWorkOrder", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithMany("MalfunctionWorkOrder")
                        .HasForeignKey("InstrumentID");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.RepairRequest", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("RepairRequest")
                        .HasForeignKey("EquipmentManagementSystem.Models.RepairRequest", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Validation", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("Validation")
                        .HasForeignKey("EquipmentManagementSystem.Models.Validation", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
