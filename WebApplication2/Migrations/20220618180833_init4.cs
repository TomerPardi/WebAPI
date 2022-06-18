using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_ContactId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ContactId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId1",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactUserId",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ContactId1_ContactUserId",
                table: "Message",
                columns: new[] { "ContactId1", "ContactUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_ContactId1_ContactUserId",
                table: "Message",
                columns: new[] { "ContactId1", "ContactUserId" },
                principalTable: "Contact",
                principalColumns: new[] { "Id", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_ContactId1_ContactUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ContactId1_ContactUserId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ContactUserId",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ContactId",
                table: "Message",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_ContactId",
                table: "Message",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id");
        }
    }
}
