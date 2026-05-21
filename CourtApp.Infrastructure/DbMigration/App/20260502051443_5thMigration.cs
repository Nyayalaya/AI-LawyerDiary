using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.App
{
    /// <inheritdoc />
    public partial class _5thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_head_HeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_sub_head_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropTable(
                name: "m_proceeding_sub_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_proceeding_head",
                schema: "ld");

            migrationBuilder.CreateTable(
                name: "m_proceeding_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProceedingTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_proceeding_m_proceeding_type_ProceedingTypeId",
                        column: x => x.ProceedingTypeId,
                        principalTable: "m_proceeding_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_ProceedingTypeId",
                table: "m_proceeding",
                column: "ProceedingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "SubHeadId",
                principalTable: "m_proceeding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_type_HeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "HeadId",
                principalTable: "m_proceeding_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_type_HeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropTable(
                name: "m_proceeding");

            migrationBuilder.DropTable(
                name: "m_proceeding_type");

            migrationBuilder.CreateTable(
                name: "m_proceeding_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_head", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding_sub_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_sub_head", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_proceeding_sub_head_m_proceeding_head_HeadId",
                        column: x => x.HeadId,
                        principalSchema: "ld",
                        principalTable: "m_proceeding_head",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "HeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_head_HeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "HeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_sub_head_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "SubHeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_sub_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
