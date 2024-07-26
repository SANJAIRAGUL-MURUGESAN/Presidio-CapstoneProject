using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggingApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_HashTags_UserId",
                table: "HashTags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_ActualTweetId",
                table: "Retweets",
                column: "ActualTweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserId",
                table: "Retweets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetFiles_TweetId",
                table: "TweetFiles",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashTags_TweetId",
                table: "TweetHashTags",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetMentions_TweetId",
                table: "TweetMentions",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UserId",
                table: "Tweets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashTags");

            migrationBuilder.DropTable(
                name: "Retweets");

            migrationBuilder.DropTable(
                name: "TweetFiles");

            migrationBuilder.DropTable(
                name: "TweetHashTags");

            migrationBuilder.DropTable(
                name: "TweetMentions");

            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
