using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigrations.App
{
    /// <inheritdoc />
    public partial class _8thMigration_UpdateCaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseEntityId",
                schema: "ld",
                table: "r_case_docs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LawyerId",
                schema: "ld",
                table: "case_assigned",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CaseEntityId",
                schema: "ld",
                table: "case_assigned",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "case_detail_data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryNumber = table.Column<string>(type: "text", nullable: true),
                    CaseNo = table.Column<string>(type: "text", nullable: true),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    FilingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DisposalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDisposed = table.Column<bool>(type: "boolean", nullable: false),
                    FirstTitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondTitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseFirstTitle = table.Column<string>(type: "text", nullable: true),
                    CaseSecondTitle = table.Column<string>(type: "text", nullable: true),
                    CourtLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtComplexId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtHallId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseStageId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaseStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Act = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<string>(type: "text", nullable: true),
                    PoliceStation = table.Column<string>(type: "text", nullable: true),
                    FIRNumber = table.Column<string>(type: "text", nullable: true),
                    FIRYear = table.Column<int>(type: "integer", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    OpponentClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsImportant = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsUrgent = table.Column<bool>(type: "boolean", nullable: false),
                    IsAssigned = table.Column<bool>(type: "boolean", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    InternalRemarks = table.Column<string>(type: "text", nullable: true),
                    ParentCaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaseEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_detail_data_case_detail_ParentCaseId",
                        column: x => x.ParentCaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_case_detail_data_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalTable: "case_detail_data",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ld",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_client_OpponentClientId",
                        column: x => x.OpponentClientId,
                        principalSchema: "ld",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_case_stage_CaseStageId",
                        column: x => x.CaseStageId,
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_hall_CourtHallId",
                        column: x => x.CourtHallId,
                        principalTable: "m_court_hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_fs_title_FirstTitleId",
                        column: x => x.FirstTitleId,
                        principalSchema: "ld",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_fs_title_SecondTitleId",
                        column: x => x.SecondTitleId,
                        principalSchema: "ld",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_docs_CaseEntityId",
                schema: "ld",
                table: "r_case_docs",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_assigned_CaseEntityId",
                schema: "ld",
                table: "case_assigned",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseCategoryId",
                table: "case_detail_data",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseEntityId",
                table: "case_detail_data",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseStageId",
                table: "case_detail_data",
                column: "CaseStageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_ClientId",
                table: "case_detail_data",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtComplexId",
                table: "case_detail_data",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtDistrictId",
                table: "case_detail_data",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtHallId",
                table: "case_detail_data",
                column: "CourtHallId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtId",
                table: "case_detail_data",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtLevelId",
                table: "case_detail_data",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtTypeId",
                table: "case_detail_data",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_FirstTitleId",
                table: "case_detail_data",
                column: "FirstTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_OpponentClientId",
                table: "case_detail_data",
                column: "OpponentClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_ParentCaseId",
                table: "case_detail_data",
                column: "ParentCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_SecondTitleId",
                table: "case_detail_data",
                column: "SecondTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_assigned_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "case_assigned",
                column: "CaseEntityId",
                principalTable: "case_detail_data",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_docs_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "r_case_docs",
                column: "CaseEntityId",
                principalTable: "case_detail_data",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseEntityId",
                principalTable: "case_detail_data",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_assigned_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "case_assigned");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_docs_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_case_detail_data_CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropTable(
                name: "case_detail_data");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_docs_CaseEntityId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropIndex(
                name: "IX_case_assigned_CaseEntityId",
                schema: "ld",
                table: "case_assigned");

            migrationBuilder.DropColumn(
                name: "CaseEntityId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "CaseEntityId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "CaseEntityId",
                schema: "ld",
                table: "case_assigned");

            migrationBuilder.AlterColumn<Guid>(
                name: "LawyerId",
                schema: "ld",
                table: "case_assigned",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
