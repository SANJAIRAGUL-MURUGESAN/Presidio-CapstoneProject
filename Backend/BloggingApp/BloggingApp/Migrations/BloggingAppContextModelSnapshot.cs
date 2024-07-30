﻿// <auto-generated />
using System;
using BloggingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloggingApp.Migrations
{
    [DbContext(typeof(BloggingAppContext))]
    partial class BloggingAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BloggingApp.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BloggingApp.Models.Follow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Follows");
                });

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

            modelBuilder.Entity("BloggingApp.Models.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReplyDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReplyId")
                        .HasColumnType("int");

                    b.Property<string>("ReplyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("BloggingApp.Models.Retweet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActualTweetId")
                        .HasColumnType("int");

                    b.Property<string>("IsCommentEnable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetweetContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RetweetDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActualTweetId");

                    b.HasIndex("UserId");

                    b.ToTable("Retweets");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RetweetId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RetweetId");

                    b.ToTable("RetweetComments");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int>("RetweetCommentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RetweetCommentId");

                    b.ToTable("RetweetCommentLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReplyDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReplyId")
                        .HasColumnType("int");

                    b.Property<string>("ReplyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RetweetCommentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RetweetCommentId");

                    b.ToTable("RetweetCommentReplies");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentReplyLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int>("ReplyCommentReplyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReplyCommentReplyId");

                    b.ToTable("RetweetCommentReplyLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetHashTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("HashTagTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RetweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RetweetHashTags");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int>("RetweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RetweetId");

                    b.ToTable("RetweetLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetMentions", b =>
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

                    b.Property<int>("RetweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RetweetMentions");
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

            modelBuilder.Entity("BloggingApp.Models.TweetCommentLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.ToTable("TweetCommentLikes");
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

            modelBuilder.Entity("BloggingApp.Models.TweetLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.ToTable("TweetLikes");
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

            modelBuilder.Entity("BloggingApp.Models.TweetReplyLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int>("ReplyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReplyId");

                    b.ToTable("TweetReplyLikes");
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

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserProfileImgLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BloggingApp.Models.UserNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ContentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsUserSeen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificatioContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationPost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("BloggingApp.Models.Comment", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany("TweetComments")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tweet");
                });

            modelBuilder.Entity("BloggingApp.Models.Follow", b =>
                {
                    b.HasOne("BloggingApp.Models.User", "User")
                        .WithMany("Followers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("BloggingApp.Models.Reply", b =>
                {
                    b.HasOne("BloggingApp.Models.Comment", "Comment")
                        .WithMany("CommentReplies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("BloggingApp.Models.Retweet", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany()
                        .HasForeignKey("ActualTweetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloggingApp.Models.User", "User")
                        .WithMany("UserRetweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tweet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetComment", b =>
                {
                    b.HasOne("BloggingApp.Models.Retweet", "Retweet")
                        .WithMany("RetweetComments")
                        .HasForeignKey("RetweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Retweet");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.RetweetComment", "RetweetComment")
                        .WithMany("RetweetCommentLikes")
                        .HasForeignKey("RetweetCommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RetweetComment");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentReply", b =>
                {
                    b.HasOne("BloggingApp.Models.RetweetComment", "RetweetComment")
                        .WithMany("RetweetCommentReplies")
                        .HasForeignKey("RetweetCommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RetweetComment");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentReplyLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.RetweetCommentReply", "RetweetCommentReply")
                        .WithMany("RetweetCommentReplyLikes")
                        .HasForeignKey("ReplyCommentReplyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RetweetCommentReply");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.Retweet", "Retweet")
                        .WithMany("RetweetLikes")
                        .HasForeignKey("RetweetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Retweet");
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

            modelBuilder.Entity("BloggingApp.Models.TweetCommentLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.Comment", "Comment")
                        .WithMany("CommentLikes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Comment");
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

            modelBuilder.Entity("BloggingApp.Models.TweetLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.Tweet", "Tweet")
                        .WithMany("TweetLikes")
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

            modelBuilder.Entity("BloggingApp.Models.TweetReplyLikes", b =>
                {
                    b.HasOne("BloggingApp.Models.Reply", "Reply")
                        .WithMany("ReplyLikes")
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reply");
                });

            modelBuilder.Entity("BloggingApp.Models.Comment", b =>
                {
                    b.Navigation("CommentLikes");

                    b.Navigation("CommentReplies");
                });

            modelBuilder.Entity("BloggingApp.Models.Reply", b =>
                {
                    b.Navigation("ReplyLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.Retweet", b =>
                {
                    b.Navigation("RetweetComments");

                    b.Navigation("RetweetLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetComment", b =>
                {
                    b.Navigation("RetweetCommentLikes");

                    b.Navigation("RetweetCommentReplies");
                });

            modelBuilder.Entity("BloggingApp.Models.RetweetCommentReply", b =>
                {
                    b.Navigation("RetweetCommentReplyLikes");
                });

            modelBuilder.Entity("BloggingApp.Models.Tweet", b =>
                {
                    b.Navigation("TweetComments");

                    b.Navigation("TweetFiles");

                    b.Navigation("TweetHashTags");

                    b.Navigation("TweetLikes");

                    b.Navigation("TweetMentions");
                });

            modelBuilder.Entity("BloggingApp.Models.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("UserRetweets");

                    b.Navigation("UserTweets");
                });
#pragma warning restore 612, 618
        }
    }
}
