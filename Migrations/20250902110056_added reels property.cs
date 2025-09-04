using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class addedreelsproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reelIdId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_reelIdId",
                table: "Notification",
                column: "reelIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_CloudinaryDB_reelIdId",
                table: "Notification",
                column: "reelIdId",
                principalTable: "CloudinaryDB",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_CloudinaryDB_reelIdId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_reelIdId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "reelIdId",
                table: "Notification");
        }
    }
}
