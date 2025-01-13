using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeNotificationSys.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewUserAndChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChannel_Channels_ChannelId",
                table: "UserChannel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChannel_Users_UserId",
                table: "UserChannel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChannel",
                table: "UserChannel");

            migrationBuilder.RenameTable(
                name: "UserChannel",
                newName: "UserChannels");

            migrationBuilder.RenameIndex(
                name: "IX_UserChannel_ChannelId",
                table: "UserChannels",
                newName: "IX_UserChannels_ChannelId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChannels",
                table: "UserChannels",
                columns: new[] { "UserId", "ChannelId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannels_Channels_ChannelId",
                table: "UserChannels",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannels_Users_UserId",
                table: "UserChannels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChannels_Channels_ChannelId",
                table: "UserChannels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChannels_Users_UserId",
                table: "UserChannels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChannels",
                table: "UserChannels");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserChannels",
                newName: "UserChannel");

            migrationBuilder.RenameIndex(
                name: "IX_UserChannels_ChannelId",
                table: "UserChannel",
                newName: "IX_UserChannel_ChannelId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChannel",
                table: "UserChannel",
                columns: new[] { "UserId", "ChannelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannel_Channels_ChannelId",
                table: "UserChannel",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannel_Users_UserId",
                table: "UserChannel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
