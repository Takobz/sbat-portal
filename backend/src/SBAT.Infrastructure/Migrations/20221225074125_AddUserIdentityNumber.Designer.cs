﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBAT.Infrastructure.Data;

#nullable disable

namespace SBAT.Infrastructure.Migrations
{
    [DbContext(typeof(SBATDbContext))]
    [Migration("20221225074125_AddUserIdentityNumber")]
    partial class AddUserIdentityNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("SBAT.Core.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PolicyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Relationship")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("UserId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("SBAT.Core.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CashBack")
                        .HasColumnType("REAL");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("REAL");

                    b.Property<string>("PlanInformation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlanType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WaitPeriod")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("SBAT.Core.Entities.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("SBAT.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstNames")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdentityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdentityType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SBAT.Core.Entities.Member", b =>
                {
                    b.HasOne("SBAT.Core.Entities.Policy", null)
                        .WithMany("Members")
                        .HasForeignKey("PolicyId");

                    b.HasOne("SBAT.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SBAT.Core.Entities.Policy", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
