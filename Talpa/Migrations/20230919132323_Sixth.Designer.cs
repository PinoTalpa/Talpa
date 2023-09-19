﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Talpa_DAL.Data;

#nullable disable

namespace Talpa.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230919132323_Sixth")]
    partial class Sixth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Talpa_DAL.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.ActivityDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("QuarterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("QuarterId");

                    b.ToTable("ActivityDates");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.ActivityLimitation", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("LimitationId")
                        .HasColumnType("int");

                    b.HasIndex("ActivityId");

                    b.HasIndex("LimitationId");

                    b.ToTable("ActivityLimitations");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Limitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Limitations");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Quarter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Quarters");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.UserActivityDate", b =>
                {
                    b.Property<int>("ActivityDateId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasIndex("ActivityDateId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActivityDates");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Activity", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.Suggestion", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.ActivityDate", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_DAL.Entities.Quarter", "Quarter")
                        .WithMany()
                        .HasForeignKey("QuarterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Quarter");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.ActivityLimitation", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_DAL.Entities.Limitation", "Limitation")
                        .WithMany()
                        .HasForeignKey("LimitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Limitation");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Suggestion", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.UserActivityDate", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.ActivityDate", "ActivityDate")
                        .WithMany()
                        .HasForeignKey("ActivityDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityDate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.UserRole", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Talpa_DAL.Entities.Vote", b =>
                {
                    b.HasOne("Talpa_DAL.Entities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
