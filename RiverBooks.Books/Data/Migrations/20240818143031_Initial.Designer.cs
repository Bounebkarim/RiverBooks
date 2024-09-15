﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiverBooks.Books.Data;

#nullable disable

namespace RiverBooks.Books.Migrations
{
  [DbContext(typeof(BookDbContext))]
    [Migration("20240818143031_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("books")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RiverBooks.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Books", "books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e3f0c672-4c8a-4d88-a379-9c55d7bb91a1"),
                            Author = "J.R.R. Tolkien",
                            Price = 9.99m,
                            Title = "The Fellowship of the Ring"
                        },
                        new
                        {
                            Id = new Guid("b1a82ab8-76e4-4bb4-8b4f-7a1b9ef3340f"),
                            Author = "J.R.R. Tolkien",
                            Price = 9.99m,
                            Title = "The Two Towers"
                        },
                        new
                        {
                            Id = new Guid("c2a29c12-5c6f-4f13-a0d2-f0930c5e63e4"),
                            Author = "J.R.R. Tolkien",
                            Price = 9.99m,
                            Title = "The Return of the King"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
