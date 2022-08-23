using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitBuddy.Infrastructure.Data.Migrations
{
    public partial class Add_UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "420a9d87-d812-46da-b1d4-a87001e39df5", "5162c04f-40ec-4063-b2ad-96040439c843", "UserRole", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "7b2f1dee-e647-41b4-8d67-4324c9204fcb", "27f18bfb-96e3-4f68-b149-481b60e43e0c", "UserRole", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "420a9d87-d812-46da-b1d4-a87001e39df5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b2f1dee-e647-41b4-8d67-4324c9204fcb");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
