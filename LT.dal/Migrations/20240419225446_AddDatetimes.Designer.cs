﻿// <auto-generated />
using System;
using LT.dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LT.dal.Migrations
{
    [DbContext(typeof(LTDBContext))]
    [Migration("20240419225446_AddDatetimes")]
    partial class AddDatetimes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LT.model.EntityScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Acronym");

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Score")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("Score");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Scores", "dbo");
                });

            modelBuilder.Entity("LT.model.EntityTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });
#pragma warning restore 612, 618
        }
    }
}
