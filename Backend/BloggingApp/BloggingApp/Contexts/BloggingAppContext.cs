﻿using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Contexts
{
    public class BloggingAppContext : DbContext
    {
        public BloggingAppContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TweetFiles> TweetFiles { get; set; }
        public DbSet<TweetMentions> TweetMentions { get; set; }
        public DbSet<TweetHashTags> TweetHashTags { get; set; }
        public DbSet<HashTags> HashTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping Users and their Tweets
            modelBuilder.Entity<Tweet>()
              .HasOne(r => r.User)
              .WithMany(e => e.UserTweets)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Tweet and corresponding TweetFiles
            modelBuilder.Entity<TweetFiles>()
              .HasOne(r => r.Tweet)
              .WithMany(e => e.TweetFiles)
              .HasForeignKey(r => r.TweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Tweet and corresponding TweetMentions
            modelBuilder.Entity<TweetMentions>()
              .HasOne(r => r.Tweet)
              .WithMany(e => e.TweetMentions)
              .HasForeignKey(r => r.TweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Tweet and corresponding TweetHashTags
            modelBuilder.Entity<TweetHashTags>()
              .HasOne(r => r.Tweet)
              .WithMany(e => e.TweetHashTags)
              .HasForeignKey(r => r.TweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
