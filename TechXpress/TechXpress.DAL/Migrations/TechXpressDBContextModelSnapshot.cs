﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechXpress.DAL.Data;

#nullable disable

namespace TechXpress.DAL.Migrations
{
    [DbContext(typeof(TechXpressDBContext))]
    partial class TechXpressDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductUser", b =>
                {
                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ProductsProductId", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ProductUser");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Category", b =>
                {
                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoryName");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Order", b =>
                {
                    b.Property<int?>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("OrderID"));

                    b.Property<string>("OrderDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Order_Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentID")
                        .HasColumnType("int");

                    b.Property<string>("Shipping_Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShoppingCart_ID")
                        .HasColumnType("int");

                    b.Property<int?>("TotalAmountToPay")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("ShoppingCart_ID")
                        .IsUnique()
                        .HasFilter("[ShoppingCart_ID] IS NOT NULL");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Payment", b =>
                {
                    b.Property<int?>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PaymentID"));

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentAmount")
                        .HasColumnType("int");

                    b.Property<string>("PaymentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.HasIndex("OrderID")
                        .IsUnique()
                        .HasFilter("[OrderID] IS NOT NULL");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Product", b =>
                {
                    b.Property<int?>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ProductId"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryName");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Review", b =>
                {
                    b.Property<int?>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ReviewId"));

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCart_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCart_ID"));

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberofItems")
                        .HasColumnType("int");

                    b.Property<int?>("Order_ID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ShoppingCart_ID");

                    b.HasIndex("UserID")
                        .IsUnique()
                        .HasFilter("[UserID] IS NOT NULL");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.User", b =>
                {
                    b.Property<int?>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("UserID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Password")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShoppingCart_ID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductUser", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechXpress.DAL.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Order", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.ShoppingCart", "ShoppingCart")
                        .WithOne("Order")
                        .HasForeignKey("TechXpress.DAL.Data.Models.Order", "ShoppingCart_ID");

                    b.HasOne("TechXpress.DAL.Data.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID");

                    b.Navigation("ShoppingCart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Payment", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("TechXpress.DAL.Data.Models.Payment", "OrderID");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Product", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryName");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Review", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductID");

                    b.HasOne("TechXpress.DAL.Data.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.ShoppingCart", b =>
                {
                    b.HasOne("TechXpress.DAL.Data.Models.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("TechXpress.DAL.Data.Models.ShoppingCart", "UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Order", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.Product", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.ShoppingCart", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("TechXpress.DAL.Data.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("ShoppingCart");
                });
#pragma warning restore 612, 618
        }
    }
}
