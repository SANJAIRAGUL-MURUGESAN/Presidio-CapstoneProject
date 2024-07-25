﻿// <auto-generated />
using System;
using BloggingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloggingApp.Migrations
{
    [DbContext(typeof(BloggingAppContext))]
    [Migration("20240725155745_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BloggingApp.Models.HashTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountInComments")
                        .HasColumnType("int");

                    b.Property<int>("CountInPosts")
                        .HasColumnType("int");

                    b.Property<DateTime>("HashTagCreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("HashTagTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TweetLikes")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HashTags");
                });

            modelBuilder.Entity("BloggingApp.Models.Tweet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IsCommentEnable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepostTweetId")
                        .HasColumnType("int");

                    b.Property<string>("TweetContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TweetDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("File1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.ToTable("TweetFiles");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetHashTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("HashTagTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.ToTable("TweetHashTags");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetMentions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MentionedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MentionedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MentionerId")
                        .HasColumnType("int");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.ToTable("TweetMentions");
                });

            modelBuilder.Entity("BloggingApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BioDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsPremiumHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserMobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BloggingApp.Models.HashTags", b =>
                {
                    b.HasOne("BloggingApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloggingApp.Models.Tweet", b =>
                {
                    b.HasOne("BloggingApp.Models.User", "User")
                        .WithMany("UserTweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetFiles", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany("TweetFiles")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetHashTags", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany("TweetHashTags")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("BloggingApp.Models.TweetMentions", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany("TweetMentions")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("BloggingApp.Models.Tweet", b =>
                {
                    b.Navigation("TweetFiles");

                    b.Navigation("TweetHashTags");

                    b.Navigation("TweetMentions");
                });

            modelBuilder.Entity("BloggingApp.Models.User", b =>
                {
                    b.Navigation("UserTweets");
                });
#pragma warning restore 612, 618
        }
    }
}
