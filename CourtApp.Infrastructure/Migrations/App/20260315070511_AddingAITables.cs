using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class AddingAITables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GazetteId",
                schema: "ad",
                table: "m_act");

            migrationBuilder.EnsureSchema(
                name: "ai");

            migrationBuilder.RenameColumn(
                name: "PublishedGazeteDate",
                schema: "ad",
                table: "m_act",
                newName: "PublishedGazetteDate");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "ld",
                table: "r_case_docs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                schema: "ld",
                table: "r_case_docs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                schema: "ld",
                table: "r_case_docs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Nature",
                schema: "ad",
                table: "m_act",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActName",
                schema: "ad",
                table: "m_act",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActCategory",
                schema: "ad",
                table: "m_act",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GazetteTypeId",
                schema: "ad",
                table: "m_act",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LangCode",
                table: "case_petition_detail",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ai_conversion_history",
                schema: "ai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_conversion_history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "document_chunk",
                schema: "ai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ChunkIndex = table.Column<int>(type: "integer", nullable: false),
                    PageNumber = table.Column<int>(type: "integer", nullable: false),
                    ActName = table.Column<string>(type: "text", nullable: true),
                    SectionName = table.Column<string>(type: "text", nullable: true),
                    CourtName = table.Column<string>(type: "text", nullable: true),
                    JudgeName = table.Column<string>(type: "text", nullable: true),
                    Citation = table.Column<string>(type: "text", nullable: true),
                    TokenCount = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: true),
                    SourceType = table.Column<string>(type: "text", nullable: true),
                    MetadataJson = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_chunk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_chunk_r_case_docs_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "ld",
                        principalTable: "r_case_docs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chunk_citation",
                schema: "ai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChunkId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActName = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<string>(type: "text", nullable: true),
                    CitationContent = table.Column<string>(type: "text", nullable: true),
                    CitationType = table.Column<string>(type: "text", nullable: true),
                    PageNumber = table.Column<int>(type: "integer", nullable: true),
                    NormalizedCitation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chunk_citation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chunk_citation_document_chunk_ChunkId",
                        column: x => x.ChunkId,
                        principalSchema: "ai",
                        principalTable: "document_chunk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document_embedding",
                schema: "ai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChunkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Embedding = table.Column<Vector>(type: "vector(1536)", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_embedding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_embedding_document_chunk_ChunkId",
                        column: x => x.ChunkId,
                        principalSchema: "ai",
                        principalTable: "document_chunk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_r_case_docs_CaseId",
                schema: "ld",
                table: "r_case_docs",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_GazetteTypeId",
                schema: "ad",
                table: "m_act",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_chunk_citation_ChunkId",
                schema: "ai",
                table: "chunk_citation",
                column: "ChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_document_chunk_DocumentId_PageNumber",
                schema: "ai",
                table: "document_chunk",
                columns: new[] { "DocumentId", "PageNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_document_embedding_ChunkId",
                schema: "ai",
                table: "document_embedding",
                column: "ChunkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_embedding_Embedding",
                schema: "ai",
                table: "document_embedding",
                column: "Embedding")
                .Annotation("Npgsql:IndexMethod", "hnsw")
                .Annotation("Npgsql:IndexOperators", new[] { "vector_cosine_ops" });

            migrationBuilder.AddForeignKey(
                name: "FK_m_act_m_gazzet_type_GazetteTypeId",
                schema: "ad",
                table: "m_act",
                column: "GazetteTypeId",
                principalSchema: "ad",
                principalTable: "m_gazzet_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_docs_case_detail_CaseId",
                schema: "ld",
                table: "r_case_docs",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_act_m_gazzet_type_GazetteTypeId",
                schema: "ad",
                table: "m_act");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_docs_case_detail_CaseId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropTable(
                name: "ai_conversion_history",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "chunk_citation",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "document_embedding",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "document_chunk",
                schema: "ai");

            migrationBuilder.DropIndex(
                name: "IX_r_case_docs_CaseId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropIndex(
                name: "IX_m_act_GazetteTypeId",
                schema: "ad",
                table: "m_act");

            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "FileSize",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "GazetteTypeId",
                schema: "ad",
                table: "m_act");

            migrationBuilder.DropColumn(
                name: "LangCode",
                table: "case_petition_detail");

            migrationBuilder.RenameColumn(
                name: "PublishedGazetteDate",
                schema: "ad",
                table: "m_act",
                newName: "PublishedGazeteDate");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Nature",
                schema: "ad",
                table: "m_act",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ActName",
                schema: "ad",
                table: "m_act",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ActCategory",
                schema: "ad",
                table: "m_act",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "GazetteId",
                schema: "ad",
                table: "m_act",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
