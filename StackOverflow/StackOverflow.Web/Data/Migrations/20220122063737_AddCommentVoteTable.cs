using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Web.Data.Migrations
{
    public partial class AddCommentVoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVote",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpVote",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "CommentVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpVote = table.Column<bool>(type: "bit", nullable: false),
                    DownVote = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentVotes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentVotes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_ApplicationUserId",
                table: "CommentVotes",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVotes",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentVotes");

            migrationBuilder.AddColumn<int>(
                name: "DownVote",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVote",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
