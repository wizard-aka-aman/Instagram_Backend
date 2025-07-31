using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class modifiyreellikeandcommment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReelId",
                table: "ReelLike");

            migrationBuilder.DropColumn(
                name: "ReelId",
                table: "ReelComment");

            migrationBuilder.AddColumn<string>(
                name: "publicId",
                table: "ReelLike",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "publicId",
                table: "ReelComment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "publicId",
                table: "ReelLike");

            migrationBuilder.DropColumn(
                name: "publicId",
                table: "ReelComment");

            migrationBuilder.AddColumn<int>(
                name: "ReelId",
                table: "ReelLike",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReelId",
                table: "ReelComment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
