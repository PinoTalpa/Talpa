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
    [Migration("20230923151008_Second")]
    partial class Second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ModelLayer.Models.ActivityDateDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.ToTable("ActivityDates");
                });

            modelBuilder.Entity("ModelLayer.Models.ActivityLimitationDto", b =>
                {
                    b.Property<int>("LimitationId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.HasIndex("LimitationId");

                    b.HasIndex("SuggestionId");

                    b.ToTable("ActivityLimitations");
                });

            modelBuilder.Entity("ModelLayer.Models.LimitationDto", b =>
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

            modelBuilder.Entity("ModelLayer.Models.RoleDto", b =>
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

            modelBuilder.Entity("ModelLayer.Models.SuggestionDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActivityState")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("ModelLayer.Models.UserActivityDateDto", b =>
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

            modelBuilder.Entity("ModelLayer.Models.UserDto", b =>
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

            modelBuilder.Entity("ModelLayer.Models.UserRoleDto", b =>
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

            modelBuilder.Entity("ModelLayer.Models.VoteDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("ModelLayer.Models.ActivityDateDto", b =>
                {
                    b.HasOne("ModelLayer.Models.SuggestionDto", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("ModelLayer.Models.ActivityLimitationDto", b =>
                {
                    b.HasOne("ModelLayer.Models.LimitationDto", "Limitation")
                        .WithMany()
                        .HasForeignKey("LimitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLayer.Models.SuggestionDto", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Limitation");

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("ModelLayer.Models.SuggestionDto", b =>
                {
                    b.HasOne("ModelLayer.Models.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelLayer.Models.UserActivityDateDto", b =>
                {
                    b.HasOne("ModelLayer.Models.ActivityDateDto", "ActivityDate")
                        .WithMany()
                        .HasForeignKey("ActivityDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLayer.Models.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityDate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelLayer.Models.UserRoleDto", b =>
                {
                    b.HasOne("ModelLayer.Models.RoleDto", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLayer.Models.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelLayer.Models.VoteDto", b =>
                {
                    b.HasOne("ModelLayer.Models.SuggestionDto", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLayer.Models.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
