using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.App
{
    /// <inheritdoc />
    public partial class _3ndMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_form_case_type");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_form_subtype",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_form",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "m_form_case_category_mapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormSubtypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_case_category_mapping", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_form_case_category_mapping_FormSubtypeId_CaseCategoryId",
                table: "m_form_case_category_mapping",
                columns: new[] { "FormSubtypeId", "CaseCategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_form_case_category_mapping");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_form_subtype");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_form");

            migrationBuilder.CreateTable(
                name: "m_form_case_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FormSubtypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_case_type", x => x.Id);
                });
        }
    }
}
