using BloggingApp.Models;
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
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<TweetFiles> TweetFiles { get; set; }
        public DbSet<TweetMentions> TweetMentions { get; set; }
        public DbSet<TweetHashTags> TweetHashTags { get; set; }
        public DbSet<TweetLikes> TweetLikes { get; set; }
        public DbSet<RetweetLikes> RetweetLikes { get; set; }
        public DbSet<HashTags> HashTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<RetweetComment> RetweetComments { get; set; }
        public DbSet<RetweetCommentReply> RetweetCommentReplies { get; set; }
        public DbSet<TweetCommentLikes> TweetCommentLikes { get; set; }
        public DbSet<TweetReplyLikes> TweetReplyLikes { get; set; }
        public DbSet<RetweetCommentLikes> RetweetCommentLikes { get; set; }
        public DbSet<RetweetCommentReplyLikes> RetweetCommentReplyLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping Users and their Tweets
            modelBuilder.Entity<Tweet>()
              .HasOne(r => r.User)
              .WithMany(e => e.UserTweets)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Users and their Retweets
            modelBuilder.Entity<Retweet>()
              .HasOne(r => r.User)
              .WithMany(e => e.UserRetweets)
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

            // Mapping Users and their Comments
            modelBuilder.Entity<Comment>()
              .HasOne(r => r.Tweet)
              .WithMany(e => e.TweetComments)
              .HasForeignKey(r => r.TweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Comment and their Replies
            modelBuilder.Entity<Reply>()
              .HasOne(r => r.Comment)
              .WithMany(e => e.CommentReplies)
              .HasForeignKey(r => r.CommentId)
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

            // Mapping Tweet and corresponding TweetLikes
            modelBuilder.Entity<TweetLikes>()
              .HasOne(r => r.Tweet)
              .WithMany(e => e.TweetLikes)
              .HasForeignKey(r => r.TweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Retweet and corresponding RetweetLikes
            modelBuilder.Entity<RetweetLikes>()
              .HasOne(r => r.Retweet)
              .WithMany(e => e.RetweetLikes)
              .HasForeignKey(r => r.RetweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Retweet and their Comments
            modelBuilder.Entity<RetweetComment>()
              .HasOne(r => r.Retweet)
              .WithMany(e => e.RetweetComments)
              .HasForeignKey(r => r.RetweetId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping RetweetComments and their Replies
            modelBuilder.Entity<RetweetCommentReply>()
              .HasOne(r => r.RetweetComment)
              .WithMany(e => e.RetweetCommentReplies)
              .HasForeignKey(r => r.RetweetCommentId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping TweetComments and their Likes
            modelBuilder.Entity<TweetCommentLikes>()
              .HasOne(r => r.Comment)
              .WithMany(e => e.CommentLikes)
              .HasForeignKey(r => r.CommentId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping CommentReplies and their Likes
            modelBuilder.Entity<TweetReplyLikes>()
              .HasOne(r => r.Reply)
              .WithMany(e => e.ReplyLikes)
              .HasForeignKey(r => r.ReplyId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping RetweetComments and their Likes
            modelBuilder.Entity<RetweetCommentLikes>()
              .HasOne(r => r.RetweetComment)
              .WithMany(e => e.RetweetCommentLikes)
              .HasForeignKey(r => r.RetweetCommentId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping RetweetComment and their Likes
            modelBuilder.Entity<RetweetCommentReplyLikes>()
              .HasOne(r => r.RetweetCommentReply)
              .WithMany(e => e.RetweetCommentReplyLikes)
              .HasForeignKey(r => r.ReplyCommentReplyId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
