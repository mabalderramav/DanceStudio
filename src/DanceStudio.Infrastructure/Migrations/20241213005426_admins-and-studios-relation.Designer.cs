﻿// <auto-generated />
using System;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DanceStudio.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241213005426_admins-and-studios-relation")]
    partial class adminsandstudiosrelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DanceStudio.Domain.Admins.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9b874476-d3ed-430d-8dfb-934582487dc1")
                        });
                });

            modelBuilder.Entity("DanceStudio.Domain.Studios.Studio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("roomIds")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RoomIds");

                    b.Property<string>("trainerIds")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TrainerIds");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("DanceStudio.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxStudios")
                        .HasColumnType("int")
                        .HasColumnName("MaxStudios");

                    b.Property<string>("StudioIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StudioIds");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("DanceStudio.Domain.Studios.Studio", b =>
                {
                    b.HasOne("DanceStudio.Domain.Subscriptions.Subscription", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
