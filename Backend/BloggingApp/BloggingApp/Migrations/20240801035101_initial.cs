using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggingApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetweetHashTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashTagTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetHashTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetweetMentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentionerId = table.Column<int>(type: "int", nullable: false),
                    MentionedByUserId = table.Column<int>(type: "int", nullable: false),
                    MentionedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetMentions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificatioContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUserSeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TweetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPremiumHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    BioDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileImgLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FollowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HashTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashTagTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountInPosts = table.Column<int>(type: "int", nullable: false),
                    CountInComments = table.Column<int>(type: "int", nullable: false),
                    TweetLikes = table.Column<int>(type: "int", nullable: false),
                    HashTagCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HashTags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TweetContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TweetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCommentEnable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tweets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Retweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetweetContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetweetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCommentEnable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualTweetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retweets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retweets_Tweets_ActualTweetId",
                        column: x => x.ActualTweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retweets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetFiles_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetHashTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashTagTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetHashTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetHashTags_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetLikes_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetMentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentionerId = table.Column<int>(type: "int", nullable: false),
                    MentionedByUserId = table.Column<int>(type: "int", nullable: false),
                    MentionedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetMentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetMentions_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReplyDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetCommentLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetCommentLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetCommentLikes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetweetComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RetweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetComments_Retweets_RetweetId",
                        column: x => x.RetweetId,
                        principalTable: "Retweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    RetweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetLikes_Retweets_RetweetId",
                        column: x => x.RetweetId,
                        principalTable: "Retweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetReplyLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetReplyLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetReplyLikes_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetweetCommentLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    RetweetCommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetCommentLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetCommentLikes_RetweetComments_RetweetCommentId",
                        column: x => x.RetweetCommentId,
                        principalTable: "RetweetComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetweetCommentReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReplyDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetweetCommentId = table.Column<int>(type: "int", nullable: false),
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetCommentReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetCommentReplies_RetweetComments_RetweetCommentId",
                        column: x => x.RetweetCommentId,
                        principalTable: "RetweetComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetweetCommentReplyLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    ReplyCommentReplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetCommentReplyLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetCommentReplyLikes_RetweetCommentReplies_ReplyCommentReplyId",
                        column: x => x.ReplyCommentReplyId,
                        principalTable: "RetweetCommentReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TweetId",
                table: "Comments",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId",
                table: "Follows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HashTags_UserId",
                table: "HashTags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentId",
                table: "Replies",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetCommentLikes_RetweetCommentId",
                table: "RetweetCommentLikes",
                column: "RetweetCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetCommentReplies_RetweetCommentId",
                table: "RetweetCommentReplies",
                column: "RetweetCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetCommentReplyLikes_ReplyCommentReplyId",
                table: "RetweetCommentReplyLikes",
                column: "ReplyCommentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetComments_RetweetId",
                table: "RetweetComments",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetLikes_RetweetId",
                table: "RetweetLikes",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_ActualTweetId",
                table: "Retweets",
                column: "ActualTweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserId",
                table: "Retweets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetCommentLikes_CommentId",
                table: "TweetCommentLikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetFiles_TweetId",
                table: "TweetFiles",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashTags_TweetId",
                table: "TweetHashTags",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLikes_TweetId",
                table: "TweetLikes",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetMentions_TweetId",
                table: "TweetMentions",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetReplyLikes_ReplyId",
                table: "TweetReplyLikes",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UserId",
                table: "Tweets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "HashTags");

            migrationBuilder.DropTable(
                name: "RetweetCommentLikes");

            migrationBuilder.DropTable(
                name: "RetweetCommentReplyLikes");

            migrationBuilder.DropTable(
                name: "RetweetHashTags");

            migrationBuilder.DropTable(
                name: "RetweetLikes");

            migrationBuilder.DropTable(
                name: "RetweetMentions");

            migrationBuilder.DropTable(
                name: "TweetCommentLikes");

            migrationBuilder.DropTable(
                name: "TweetFiles");

            migrationBuilder.DropTable(
                name: "TweetHashTags");

            migrationBuilder.DropTable(
                name: "TweetLikes");

            migrationBuilder.DropTable(
                name: "TweetMentions");

            migrationBuilder.DropTable(
                name: "TweetReplyLikes");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "RetweetCommentReplies");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "RetweetComments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Retweets");

            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
