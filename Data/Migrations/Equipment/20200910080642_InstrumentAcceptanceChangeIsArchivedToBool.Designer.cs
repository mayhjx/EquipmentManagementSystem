﻿// <auto-generated />
using System;
using EquipmentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    [DbContext(typeof(EquipmentContext))]
    [Migration("20200910080642_InstrumentAcceptanceChangeIsArchivedToBool")]
    partial class InstrumentAcceptanceChangeIsArchivedToBool
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EquipmentManagementSystem.Models.AccessoriesOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("PlaceTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<string>("SourceUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentId")
                        .IsUnique()
                        .HasFilter("[InstrumentId] IS NOT NULL");

                    b.ToTable("Assert");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Calibration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calibrator")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("Calibration");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Component", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Computer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID")
                        .IsUnique()
                        .HasFilter("[InstrumentID] IS NOT NULL");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Instrument", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CalibrationCycle")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MetrologicalCharacteristics")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NewSystemCode")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Principal")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Projects")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<DateTime>("StartUsingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.InstrumentAcceptance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AcceptanceDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BusinessLicenseFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessLicenseFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigurationListFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigurationListFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentAcceptanceReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentAcceptanceReportFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentCalibrationReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentCalibrationReportFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentCertificateFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentCertificateFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentFilesListFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentFilesListFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentResumeFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentResumeFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EstimatedArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EvaluationReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvaluationReportFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactoryAcceptanceCertificateFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactoryAcceptanceCertificateFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FactoryAcceptanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FactoryProductionLicenseFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactoryProductionLicenseFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeasibilityReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeasibilityReportFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InstallationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstallationNoteFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstallationNoteFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstallationRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InventoryCertificateFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InventoryCertificateFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InventoryRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAcceptance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDemo")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEngineerAssistance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFactoryAcceptance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInventoryComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSelfBuilt")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrainingUseAndMaintenance")
                        .HasColumnType("bit");

                    b.Property<string>("MedicalDeviceRegistrationCertificateFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicalDeviceRegistrationCertificateFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodConstructionRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PuchaseRequisitionFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PuchaseRequisitionFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceReportFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainingSignInFormFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainingSignInFormFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InstrumentAcceptance");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Investigation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Measures")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<string>("Operator")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("Investigation");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MaintenanceContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cycle")
                        .HasColumnType("int");

                    b.Property<string>("InstrumentPlatform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RemindTime")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Translation")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MaintenanceContent");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MaintenanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Operator")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("MaintenanceRecords");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("FoundedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Part")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phenomenon")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("MalfunctionInfo");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.MalfunctionWorkOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("InstrumentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("InstrumentID");

                    b.ToTable("MalfunctionWorkOrder");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Carrier")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("ColumnType")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Detector")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("IonSource")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("SimpleTestTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Repair", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsCritical")
                        .HasColumnType("bit");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<string>("Repairer")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("Repair");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.RepairRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BookingsTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Engineer")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Fixer")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MalfunctionWorkOrderID")
                        .IsUnique();

                    b.ToTable("RepairRequest");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.UsageRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTimeOfTest")
                        .HasColumnType("datetime2");

                    b.Property<string>("BlankSignal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColumnNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<float?>("ColumnPressure")
                        .HasColumnType("real");

                    b.Property<string>("ColumnTwoNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<float?>("ColumnTwoPressure")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PressureUnit")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(999)")
                        .HasMaxLength(999);

                    b.Property<int>("SampleNumber")
                        .HasColumnType("int");

                    b.Property<int>("TestNumber")
                        .HasColumnType("int");

                    b.Property<string>("TestSignal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VacuumDegree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VacuumDegreeUnit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("UsageRecord");
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Validation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttachmentFilePath")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("AttachmentName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FinishedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<int>("MalfunctionWorkOrderID")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceReportFileName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("PerformanceReportFilePath")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(999)")
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

            modelBuilder.Entity("EquipmentManagementSystem.Models.MaintenanceRecord", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId");

                    b.HasOne("EquipmentManagementSystem.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
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

            modelBuilder.Entity("EquipmentManagementSystem.Models.Project", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Group", "Group")
                        .WithMany("Projects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.Repair", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("Repair")
                        .HasForeignKey("EquipmentManagementSystem.Models.Repair", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.RepairRequest", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.MalfunctionWorkOrder", "MalfunctionWorkOrder")
                        .WithOne("RepairRequest")
                        .HasForeignKey("EquipmentManagementSystem.Models.RepairRequest", "MalfunctionWorkOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipmentManagementSystem.Models.UsageRecord", b =>
                {
                    b.HasOne("EquipmentManagementSystem.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId");

                    b.HasOne("EquipmentManagementSystem.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
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
