using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class updaterecentmessagesaddedpropertylastmessagedateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessageDateTime",
                table: "RecentMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessageDateTime",
                table: "RecentMessages");
        }
    }
}
