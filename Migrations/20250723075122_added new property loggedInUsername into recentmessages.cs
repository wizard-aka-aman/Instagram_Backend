using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class addednewpropertyloggedInUsernameintorecentmessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoggedInUsername",
                table: "RecentMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedInUsername",
                table: "RecentMessages");
        }
    }
}
