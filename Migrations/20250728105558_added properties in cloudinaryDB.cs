using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class addedpropertiesincloudinaryDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "CloudinaryDB",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "CloudinaryDB",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "CloudinaryDB",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CloudinaryDB",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "CloudinaryDB");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "CloudinaryDB");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "CloudinaryDB");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CloudinaryDB");
        }
    }
}
