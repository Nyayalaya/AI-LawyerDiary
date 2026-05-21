using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.App
{
    /// <inheritdoc />
    public partial class _6thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_level_CourtLevelId",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_location_LocationId",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_court_CourtId",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_location_LocationId",
                table: "m_court_complex");

            migrationBuilder.DropIndex(
                name: "IX_m_court_complex_LocationId",
                table: "m_court_complex");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtLevelId",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_LocationId",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_Name_LocationId",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "m_court_complex");

            migrationBuilder.DropColumn(
                name: "CourtLevelId",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "m_court");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_court_hall",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourtTypeId",
                table: "m_court_hall",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasVideoConference",
                table: "m_court_hall",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "m_court_hall",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SeatingCapacity",
                table: "m_court_hall",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "m_court_complex",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                table: "m_court_complex",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "m_court_complex",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_court_complex",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtualComplex",
                table: "m_court_complex",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_court",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourtDistrictId",
                table: "m_court",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtualCourt",
                table: "m_court",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationEntityId",
                table: "m_court",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_CourtTypeId",
                table: "m_court_hall",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_Code",
                table: "m_court",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtDistrictId1",
                table: "m_court",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_LocationEntityId",
                table: "m_court",
                column: "LocationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_StateId1",
                table: "m_court",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                table: "m_court",
                column: "CourtDistrictId",
                principalTable: "m_court_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_location_LocationEntityId",
                table: "m_court",
                column: "LocationEntityId",
                principalTable: "m_location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_state_StateId",
                table: "m_court",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_CourtId",
                table: "m_court_complex",
                column: "CourtId",
                principalTable: "m_court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                table: "m_court_complex",
                column: "CourtDistrictId",
                principalTable: "m_court_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_hall_m_court_type_CourtTypeId",
                table: "m_court_hall",
                column: "CourtTypeId",
                principalTable: "m_court_type",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_location_LocationEntityId",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_state_StateId",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_court_CourtId",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_hall_m_court_type_CourtTypeId",
                table: "m_court_hall");

            migrationBuilder.DropIndex(
                name: "IX_m_court_hall_CourtTypeId",
                table: "m_court_hall");

            migrationBuilder.DropIndex(
                name: "IX_m_court_Code",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtDistrictId1",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_LocationEntityId",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_StateId1",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "CourtTypeId",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "HasVideoConference",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "SeatingCapacity",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "m_court_complex");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_court_complex");

            migrationBuilder.DropColumn(
                name: "IsVirtualComplex",
                table: "m_court_complex");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "CourtDistrictId",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "IsVirtualCourt",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "LocationEntityId",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "m_court");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "m_court_complex",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                table: "m_court_complex",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "m_court_complex",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourtLevelId",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_LocationId",
                table: "m_court_complex",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtLevelId",
                table: "m_court",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_LocationId",
                table: "m_court",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_Name_LocationId",
                table: "m_court",
                columns: new[] { "Name", "LocationId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_level_CourtLevelId",
                table: "m_court",
                column: "CourtLevelId",
                principalTable: "m_court_level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_location_LocationId",
                table: "m_court",
                column: "LocationId",
                principalTable: "m_location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_CourtId",
                table: "m_court_complex",
                column: "CourtId",
                principalTable: "m_court",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                table: "m_court_complex",
                column: "CourtDistrictId",
                principalTable: "m_court_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_location_LocationId",
                table: "m_court_complex",
                column: "LocationId",
                principalTable: "m_location",
                principalColumn: "Id");
        }
    }
}
