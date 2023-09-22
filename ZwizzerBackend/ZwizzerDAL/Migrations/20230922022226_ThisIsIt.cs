using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZwizzerDAL.Migrations
{
    /// <inheritdoc />
    public partial class ThisIsIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Zweets",
                columns: table => new
                {
                    ZweetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(280)", maxLength: 280, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zweets", x => x.ZweetId);
                    table.ForeignKey(
                        name: "FK_Zweets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZweetId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(280)", maxLength: 280, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Comments_Zweets_ZweetId",
                        column: x => x.ZweetId,
                        principalTable: "Zweets",
                        principalColumn: "ZweetId");
                });

            migrationBuilder.CreateTable(
                name: "Rezweets",
                columns: table => new
                {
                    RezweetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZweetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezweets", x => x.RezweetId);
                    table.ForeignKey(
                        name: "FK_Rezweets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Rezweets_Zweets_ZweetId",
                        column: x => x.ZweetId,
                        principalTable: "Zweets",
                        principalColumn: "ZweetId");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZweetId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Likes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Likes_Zweets_ZweetId",
                        column: x => x.ZweetId,
                        principalTable: "Zweets",
                        principalColumn: "ZweetId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BackgroundImagePath", "Bio", "Email", "HashedPassword", "ProfileImagePath", "UserName", "UserRole" },
                values: new object[,]
                {
                    { 1, "/BackgroundImages/default.jpg", "Welcome to my Zwizzer profile! I'm an Admin!", "Admin@gmail.com", "AQAAAAIAAYagAAAAEEu4Qe5cMaehcSGOiOsZJGetVww0qaL9vDvghghShobH12EpEQPFSprBZq7+Is+eJQ==", "/ProfileImages/default.jpg", "Admin", 2 },
                    { 2, "/BackgroundImages/default.jpg", "Welcome to my Zwizzer profile! I'm a Subscriber!", "Sharon@gmail.com", "AQAAAAIAAYagAAAAEFo7nxU1llvDH7ESD5m5EZk8gFdK6kpvxDLxs4Ck3Alzyio32m8tjWJWLR6wnf/DsA==", "/ProfileImages/default.jpg", "Sharon", 1 },
                    { 3, "/BackgroundImages/default.jpg", "Welcome to my Zwizzer profile! I'm a User!", "SickGamer420@gmail.com", "AQAAAAIAAYagAAAAECggEdD+4NE3v1/EYNF2zXcAxbP3ThiwQg+Gwe7Lh7rGkpYuy/wNrECu0SUpPTxjtg==", "/ProfileImages/default.jpg", "SickGamer420", 0 }
                });

            migrationBuilder.InsertData(
                table: "Zweets",
                columns: new[] { "ZweetId", "Content", "CreationTime", "UserId" },
                values: new object[,]
                {
                    { 1, "This is the first ever Zweet on Zwizzer!", new DateTime(2023, 9, 22, 3, 22, 26, 346, DateTimeKind.Local).AddTicks(4326), 1 },
                    { 2, "This is the second ever Zweet on Zwizzer! also, used as a test.", new DateTime(2023, 9, 22, 3, 52, 26, 346, DateTimeKind.Local).AddTicks(4370), 1 },
                    { 3, "I heard of this about this new platform from my friends, guess im one of the firsts to use it!", new DateTime(2023, 9, 22, 4, 16, 26, 346, DateTimeKind.Local).AddTicks(4374), 2 },
                    { 4, "So how's everybody day has been going?", new DateTime(2023, 9, 22, 4, 22, 26, 346, DateTimeKind.Local).AddTicks(4376), 2 },
                    { 5, "Are there any games on this platform? I want to beat all the records on them!", new DateTime(2023, 9, 22, 4, 52, 26, 346, DateTimeKind.Local).AddTicks(4378), 3 },
                    { 6, "Looking for group 4 World of Warcraft, any joiners?", new DateTime(2023, 9, 22, 4, 54, 14, 346, DateTimeKind.Local).AddTicks(4442), 3 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "CreationTime", "UserId", "ZweetId" },
                values: new object[,]
                {
                    { 1, "What a great era to be alive, now that we have Zwizzer!", new DateTime(2023, 9, 22, 4, 28, 26, 346, DateTimeKind.Local).AddTicks(4543), 3, 4 },
                    { 2, "Thank you for being the first testers of our lovely Social Media - Zwizzer!", new DateTime(2023, 9, 22, 4, 30, 50, 346, DateTimeKind.Local).AddTicks(4547), 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "LikeId", "CommentId", "UserId", "ZweetId" },
                values: new object[,]
                {
                    { 1, null, 1, 1 },
                    { 2, null, 2, 1 },
                    { 3, null, 3, 1 },
                    { 4, null, 1, 2 },
                    { 5, null, 2, 2 },
                    { 6, null, 1, 3 },
                    { 7, null, 2, 3 },
                    { 8, null, 3, 3 },
                    { 9, null, 1, 6 },
                    { 10, null, 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Rezweets",
                columns: new[] { "RezweetId", "UserId", "ZweetId" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 2, 1, 5 },
                    { 3, 3, 4 },
                    { 4, 2, 1 },
                    { 5, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "LikeId", "CommentId", "UserId", "ZweetId" },
                values: new object[,]
                {
                    { 11, 1, 1, null },
                    { 12, 1, 2, null },
                    { 13, 2, 2, null },
                    { 14, 2, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ZweetId",
                table: "Comments",
                column: "ZweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ZweetId",
                table: "Likes",
                column: "ZweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezweets_UserId",
                table: "Rezweets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezweets_ZweetId",
                table: "Rezweets",
                column: "ZweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zweets_UserId",
                table: "Zweets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Rezweets");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Zweets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
