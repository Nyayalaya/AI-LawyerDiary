using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class UpdateStateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_Hn",
                table: "m_state",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name_En",
                table: "m_state",
                newName: "Code");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "m_state",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_state_Code",
                table: "m_state",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_state_Code",
                table: "m_state");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "m_state");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "m_state",
                newName: "Name_Hn");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "m_state",
                newName: "Name_En");
        }
    }
}
