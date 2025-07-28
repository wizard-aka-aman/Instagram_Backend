using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class addedpropertyprofilepictureinstorySeen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "StorySeen",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "StorySeen");
        }
    }
}
