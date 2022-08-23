using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitBuddy.Infrastructure.Data.Migrations
{
    public partial class Add_EventUserRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInterest_Events_EventId",
                table: "EventInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_EventInterest_Interests_InterestId",
                table: "EventInterest");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventInterest",
                table: "EventInterest");

            migrationBuilder.RenameTable(
                name: "EventInterest",
                newName: "EventInterests");

            migrationBuilder.RenameIndex(
                name: "IX_EventInterest_EventId",
                table: "EventInterests",
                newName: "IX_EventInterests_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventInterests",
                table: "EventInterests",
                columns: new[] { "InterestId", "EventId" });

            migrationBuilder.CreateTable(
                name: "EventUsersRegistration",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUsersRegistration", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventUsersRegistration_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUsersRegistration_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUsersRegistration_EventId",
                table: "EventUsersRegistration",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInterests_Events_EventId",
                table: "EventInterests",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInterests_Interests_InterestId",
                table: "EventInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInterests_Events_EventId",
                table: "EventInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_EventInterests_Interests_InterestId",
                table: "EventInterests");

            migrationBuilder.DropTable(
                name: "EventUsersRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventInterests",
                table: "EventInterests");

            migrationBuilder.RenameTable(
                name: "EventInterests",
                newName: "EventInterest");

            migrationBuilder.RenameIndex(
                name: "IX_EventInterests_EventId",
                table: "EventInterest",
                newName: "IX_EventInterest_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventInterest",
                table: "EventInterest",
                columns: new[] { "InterestId", "EventId" });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_EventId",
                table: "EventUser",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInterest_Events_EventId",
                table: "EventInterest",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInterest_Interests_InterestId",
                table: "EventInterest",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
