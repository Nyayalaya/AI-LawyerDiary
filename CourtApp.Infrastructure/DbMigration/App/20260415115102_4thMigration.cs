using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.App
{
    /// <inheritdoc />
    public partial class _4thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_court_hall_Code_CourtComplexId",
                table: "m_court_hall");

            migrationBuilder.DropIndex(
                name: "IX_m_court_district_Code_StateId",
                table: "m_court_district");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_court_hall");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "m_court_district");

            migrationBuilder.DropColumn(
                name: "Properiter",
                schema: "ld",
                table: "client");

            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                schema: "ld",
                table: "client",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferalBy",
                schema: "ld",
                table: "client",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "ld",
                table: "client",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfficeEmail",
                schema: "ld",
                table: "client",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ld",
                table: "client",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "ld",
                table: "client",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ld",
                table: "client",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientType",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "ld",
                table: "client",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GST",
                schema: "ld",
                table: "client",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAN",
                schema: "ld",
                table: "client",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proprietor",
                schema: "ld",
                table: "client",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_Name_CourtComplexId",
                table: "m_court_hall",
                columns: new[] { "Name", "CourtComplexId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_Email",
                schema: "ld",
                table: "client",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_client_Mobile",
                schema: "ld",
                table: "client",
                column: "Mobile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_court_hall_Name_CourtComplexId",
                table: "m_court_hall");

            migrationBuilder.DropIndex(
                name: "IX_client_Email",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_Mobile",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "GST",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "PAN",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "Proprietor",
                schema: "ld",
                table: "client");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_court_hall",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "m_court_district",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferalBy",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfficeEmail",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientType",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Properiter",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_Code_CourtComplexId",
                table: "m_court_hall",
                columns: new[] { "Code", "CourtComplexId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_Code_StateId",
                table: "m_court_district",
                columns: new[] { "Code", "StateId" },
                unique: true);
        }
    }
}
