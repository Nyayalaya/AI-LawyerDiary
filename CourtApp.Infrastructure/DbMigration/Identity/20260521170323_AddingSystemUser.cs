using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.Identity
{
    /// <inheritdoc />
    public partial class AddingSystemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Subscription = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubscriptionStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_RegisteredDate",
                schema: "public",
                table: "SystemUsers",
                column: "RegisteredDate");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_Status",
                schema: "public",
                table: "SystemUsers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_Subscription",
                schema: "public",
                table: "SystemUsers",
                column: "Subscription");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_UserId",
                schema: "public",
                table: "SystemUsers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemUsers",
                schema: "public");
        }
    }
}
