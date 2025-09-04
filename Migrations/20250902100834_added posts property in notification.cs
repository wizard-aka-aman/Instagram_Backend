using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class addedpostspropertyinnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_PostId1",
                table: "Notification",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Posts_PostId1",
                table: "Notification",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Posts_PostId1",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_PostId1",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Notification");
        }
    }
}
