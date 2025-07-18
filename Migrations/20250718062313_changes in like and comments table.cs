using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class changesinlikeandcommentstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "PostsPostId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PostsPostId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostsPostId",
                table: "Likes",
                column: "PostsPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostsPostId",
                table: "Comments",
                column: "PostsPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostsPostId",
                table: "Comments",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostsPostId",
                table: "Likes",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostsPostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostsPostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PostsPostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostsPostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostsPostId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "PostsPostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
