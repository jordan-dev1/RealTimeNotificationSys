using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeNotificationSys.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampToNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENG6ykArgUZEOGDYS33nVKCYsErnPT248WI4yssvcGlvPNADtpuextf2uQcpxkyD1g==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKvBvceGXWBBEMJXgw5ylEthubL7EZ04b1HhQuInYVTn7w/3HgS1IkOLVT/xv6MFeA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKUCE0ixT0B3HUln90mLx/Gb678uTByVI31W6eb2lGr5xKojXrXMr+tJuzEBF5RxJA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMiVFgElNB2ys7F5nWyjMDB6I8yzpWvamLowiIaAs9DzemDzI4ufZR6BzxPLp8xqpw==");
        }
    }
}
