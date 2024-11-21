﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ST10257863_PROG6212_POE.Data;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241121194603_AddingFileStorageCapabilities")]
    partial class AddingFileStorageCapabilities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademicManager", b =>
                {
                    b.Property<int>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerID"));

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ManagerID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("AcademicManagers");

                    b.HasData(
                        new
                        {
                            ManagerID = 3001,
                            Campus = "Main Campus",
                            Department = "IT",
                            UserID = 1
                        },
                        new
                        {
                            ManagerID = 3002,
                            Campus = "East Campus",
                            Department = "Business",
                            UserID = 4
                        });
                });

            modelBuilder.Entity("Claim", b =>
                {
                    b.Property<int>("ClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimId"));

                    b.Property<string>("ApprovalComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CoordinatorId")
                        .HasColumnType("int");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HoursWorked")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("LecturerId")
                        .HasColumnType("int");

                    b.Property<string>("LecturerNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<decimal>("OvertimeHoursWorked")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SupportingDocuments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerificationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ClaimId");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("LecturerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("File", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<int>("ClaimId")
                        .HasColumnType("int");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FileId");

                    b.HasIndex("ClaimId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Lecturer", b =>
                {
                    b.Property<int>("LecturerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LecturerID"));

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("LecturerID");

                    b.HasIndex("UserID");

                    b.ToTable("Lecturers");

                    b.HasData(
                        new
                        {
                            LecturerID = 1001,
                            Campus = "Main Campus",
                            Department = "Computer Science",
                            HourlyRate = 500.00m,
                            UserID = 1
                        },
                        new
                        {
                            LecturerID = 1002,
                            Campus = "South Campus",
                            Department = "Mathematics",
                            HourlyRate = 450.00m,
                            UserID = 2
                        });
                });

            modelBuilder.Entity("ST10257863_PROG6212_POE.Models.Tables.Coordinator", b =>
                {
                    b.Property<int>("CoordinatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoordinatorID"));

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CoordinatorID");

                    b.HasIndex("UserID");

                    b.ToTable("Coordinators");

                    b.HasData(
                        new
                        {
                            CoordinatorID = 2001,
                            Campus = "North Campus",
                            Department = "Engineering",
                            UserID = 1
                        },
                        new
                        {
                            CoordinatorID = 2002,
                            Campus = "West Campus",
                            Department = "Science",
                            UserID = 3
                        });
                });

            modelBuilder.Entity("ST10257863_PROG6212_POE.Models.Tables.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("ContactInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            ContactInfo = "admin@example.com",
                            FirstName = "Admin",
                            LastName = "User",
                            Password = "adminPass123",
                            UserName = "admin"
                        },
                        new
                        {
                            UserID = 2,
                            ContactInfo = "lecturer@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "LecturerPass123",
                            UserName = "lecturerUser"
                        },
                        new
                        {
                            UserID = 3,
                            ContactInfo = "coordinator@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            Password = "CoordinatorPass123",
                            UserName = "coordinatorUser"
                        },
                        new
                        {
                            UserID = 4,
                            ContactInfo = "manager@example.com",
                            FirstName = "Mike",
                            LastName = "Johnson",
                            Password = "ManagerPass123",
                            UserName = "managerUser"
                        });
                });

            modelBuilder.Entity("AcademicManager", b =>
                {
                    b.HasOne("ST10257863_PROG6212_POE.Models.Tables.User", "User")
                        .WithOne()
                        .HasForeignKey("AcademicManager", "UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Claim", b =>
                {
                    b.HasOne("ST10257863_PROG6212_POE.Models.Tables.Coordinator", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.HasOne("Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicManager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.Navigation("Coordinator");

                    b.Navigation("Lecturer");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("File", b =>
                {
                    b.HasOne("Claim", "Claim")
                        .WithMany("ClaimFiles")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");
                });

            modelBuilder.Entity("Lecturer", b =>
                {
                    b.HasOne("ST10257863_PROG6212_POE.Models.Tables.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ST10257863_PROG6212_POE.Models.Tables.Coordinator", b =>
                {
                    b.HasOne("ST10257863_PROG6212_POE.Models.Tables.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Claim", b =>
                {
                    b.Navigation("ClaimFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
