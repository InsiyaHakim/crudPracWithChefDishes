﻿// <auto-generated />
using System;
using CRUDpractice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDpractice.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20190515164533_crud")]
    partial class crud
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CRUDpractice.Models.Chef", b =>
                {
                    b.Property<int>("chef_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("age")
                        .IsRequired();

                    b.Property<string>("f_name")
                        .IsRequired();

                    b.Property<string>("l_name")
                        .IsRequired();

                    b.HasKey("chef_id");

                    b.ToTable("Chef");
                });

            modelBuilder.Entity("CRUDpractice.Models.Dishes", b =>
                {
                    b.Property<int>("dish_id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("calories");

                    b.Property<int>("chef_id");

                    b.Property<string>("description")
                        .IsRequired();

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("dish_id");

                    b.HasIndex("chef_id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("CRUDpractice.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("f_name")
                        .IsRequired();

                    b.Property<string>("l_name")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<DateTime>("updated_at");

                    b.HasKey("user_id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("CRUDpractice.Models.Dishes", b =>
                {
                    b.HasOne("CRUDpractice.Models.Chef", "Chef")
                        .WithMany("Dishes")
                        .HasForeignKey("chef_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
