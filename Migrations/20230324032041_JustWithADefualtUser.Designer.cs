﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealWorld.Infrastructure;

#nullable disable

namespace RealWorld.Migrations
{
    [DbContext(typeof(RealWorldContext))]
    [Migration("20230324032041_JustWithADefualtUser")]
    partial class JustWithADefualtUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("RealWorld.Domain.Article", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Favorited")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FavoritesCount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RealWorld.Domain.ArticleTag", b =>
                {
                    b.Property<string>("TagID")
                        .HasColumnType("TEXT");

                    b.Property<int>("ArticleID")
                        .HasColumnType("INTEGER");

                    b.HasKey("TagID", "ArticleID");

                    b.HasIndex("ArticleID");

                    b.ToTable("ArticleTag");
                });

            modelBuilder.Entity("RealWorld.Domain.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ArticleID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.HasIndex("ArticleID");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Name = "HelloWorld"
                        },
                        new
                        {
                            Name = "lsopaslkj"
                        },
                        new
                        {
                            Name = "HoangTheTrung"
                        },
                        new
                        {
                            Name = "hocChoGioi"
                        },
                        new
                        {
                            Name = "LonRoiConHayKhocNhe"
                        },
                        new
                        {
                            Name = "ChauLenBa"
                        });
                });

            modelBuilder.Entity("RealWorld.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "",
                            Email = "wanbicoi123@gmail.com",
                            Hash = new byte[] { 63, 181, 104, 190, 154, 165, 29, 167, 240, 5, 241, 150, 210, 185, 22, 215, 181, 186, 52, 145, 26, 70, 225, 251, 202, 215, 80, 195, 63, 57, 69, 81, 224, 28, 181, 110, 102, 17, 37, 40, 43, 229, 167, 137, 225, 148, 246, 173, 24, 189, 196, 129, 222, 18, 233, 217, 103, 53, 28, 145, 8, 247, 99, 194 },
                            Image = "",
                            Salt = new byte[] { 23, 55, 63, 104, 110, 198, 205, 78, 173, 107, 24, 206, 197, 119, 112, 116 },
                            UserName = "Victor"
                        });
                });

            modelBuilder.Entity("RealWorld.Domain.Article", b =>
                {
                    b.HasOne("RealWorld.Domain.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("RealWorld.Domain.ArticleTag", b =>
                {
                    b.HasOne("RealWorld.Domain.Article", "Article")
                        .WithMany("ArticleTags")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWorld.Domain.Tag", "Tag")
                        .WithMany("ArticleTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("RealWorld.Domain.Tag", b =>
                {
                    b.HasOne("RealWorld.Domain.Article", null)
                        .WithMany("TagList")
                        .HasForeignKey("ArticleID");
                });

            modelBuilder.Entity("RealWorld.Domain.Article", b =>
                {
                    b.Navigation("ArticleTags");

                    b.Navigation("TagList");
                });

            modelBuilder.Entity("RealWorld.Domain.Tag", b =>
                {
                    b.Navigation("ArticleTags");
                });
#pragma warning restore 612, 618
        }
    }
}
