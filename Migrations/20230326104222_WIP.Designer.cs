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
    [Migration("20230326104222_WIP")]
    partial class WIP
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.Property<int>("ArticlesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagsName")
                        .HasColumnType("TEXT");

                    b.HasKey("ArticlesId", "TagsName");

                    b.HasIndex("TagsName");

                    b.ToTable("ArticleTags", (string)null);
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.Property<int>("FavoriteArticlesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FavoriteUsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FavoriteArticlesId", "FavoriteUsersId");

                    b.HasIndex("FavoriteUsersId");

                    b.ToTable("ArticleFavorites", (string)null);
                });

            modelBuilder.Entity("RealWorld.Domain.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RealWorld.Domain.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

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
                            Hash = new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 },
                            Image = "",
                            Salt = new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 },
                            UserName = "Victor"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "",
                            Email = "wan@gmail.com",
                            Hash = new byte[] { 131, 170, 69, 180, 0, 238, 23, 26, 141, 135, 47, 241, 246, 194, 129, 105, 21, 207, 106, 8, 189, 146, 233, 41, 140, 203, 118, 79, 61, 83, 168, 131, 82, 94, 124, 173, 185, 155, 155, 138, 19, 2, 129, 107, 247, 243, 114, 56, 61, 173, 1, 198, 176, 71, 231, 40, 53, 189, 102, 31, 81, 16, 180, 42 },
                            Image = "",
                            Salt = new byte[] { 78, 223, 120, 38, 96, 189, 215, 67, 166, 14, 29, 19, 36, 207, 119, 191 },
                            UserName = "SongGa"
                        });
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<int>("FollowersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FollowingsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FollowersId", "FollowingsId");

                    b.HasIndex("FollowingsId");

                    b.ToTable("UserFollowers", (string)null);
                });

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.HasOne("RealWorld.Domain.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWorld.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.HasOne("RealWorld.Domain.Article", null)
                        .WithMany()
                        .HasForeignKey("FavoriteArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWorld.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("FavoriteUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealWorld.Domain.Article", b =>
                {
                    b.HasOne("RealWorld.Domain.User", "Author")
                        .WithMany("CreatedArticles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("RealWorld.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("FollowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWorld.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealWorld.Domain.User", b =>
                {
                    b.Navigation("CreatedArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
