using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

namespace CourtApp.Infrastructure.DbMigration.App
{
    /// <inheritdoc />
    public partial class _1stAppMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ai");

            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "ld");

            migrationBuilder.EnsureSchema(
                name: "ad");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

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
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    AffectedColumns = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "billing_detail",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LawyerId = table.Column<string>(type: "text", nullable: true),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    AccountNo = table.Column<string>(type: "text", nullable: true),
                    Branch = table.Column<string>(type: "text", nullable: true),
                    IfscCode = table.Column<string>(type: "text", nullable: true),
                    PanNumber = table.Column<string>(type: "text", nullable: true),
                    GstNo = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billing_detail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    OfficeEmail = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    ReferalBy = table.Column<string>(type: "text", nullable: true),
                    RegNo = table.Column<string>(type: "text", nullable: true),
                    Properiter = table.Column<string>(type: "text", nullable: true),
                    ClientType = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_act_type",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_book_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_book_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_cadre",
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
                    table.PrimaryKey("PK_m_cadre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_case_stage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee_type",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtFeeType = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_court_level",
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
                    table.PrimaryKey("PK_m_court_level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_do_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_do_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_expense_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_expense_head", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_frm_types",
                columns: table => new
                {
                    FormName = table.Column<string>(type: "text", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FieldsDetails = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_frm_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_fs_title",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_fs_title", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_gazzet_type",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_gazzet_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_lang_dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyWord = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MultiLangs = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_lang_dict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_lawyer",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    EnrollNumber = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    RelPerson = table.Column<string>(type: "text", nullable: true),
                    Relegion = table.Column<string>(type: "text", nullable: true),
                    Caste = table.Column<string>(type: "text", nullable: true),
                    ProfileImgPath = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_lawyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_head", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_publisher",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicationName = table.Column<string>(type: "text", nullable: false),
                    PropriatorName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_specialization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_state",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_state_court_language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_state_court_language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_subject",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_temp_frm_mapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FieldsMapping = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_temp_frm_mapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_template_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateName = table.Column<string>(type: "text", nullable: true),
                    TemplatePath = table.Column<string>(type: "text", nullable: true),
                    TemplateBody = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Tags = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_template_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "court_fee",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeeTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_court_fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_court_fee_m_court_fee_type_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalSchema: "account",
                        principalTable: "m_court_fee_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CourtLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseStageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_type_m_case_stage_CaseStageEntityId",
                        column: x => x.CaseStageEntityId,
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_type_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_part",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    GazetteTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_part", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_part_m_gazzet_type_GazetteTypeId",
                        column: x => x.GazetteTypeId,
                        principalSchema: "ad",
                        principalTable: "m_gazzet_type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OppositCouncilEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LawyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OppositCouncilEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OppositCouncilEntity_m_lawyer_LawyerId",
                        column: x => x.LawyerId,
                        principalSchema: "common",
                        principalTable: "m_lawyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding_sub_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "m_book",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_book_m_book_type_BookTypeId",
                        column: x => x.BookTypeId,
                        principalSchema: "ld",
                        principalTable: "m_book_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_book_m_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "ld",
                        principalTable: "m_publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "court_fee_structure",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MinValue = table.Column<double>(type: "double precision", nullable: false),
                    MaxValue = table.Column<double>(type: "double precision", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    FixAmount = table.Column<double>(type: "double precision", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_court_fee_structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_court_fee_structure_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_district",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_district_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_district",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_district_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ParentLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_location_m_location_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "m_location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_location_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_case_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_case_category_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_case_kind",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseKind = table.Column<string>(type: "text", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_kind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_case_kind_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_work_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_work_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_work_type_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_act",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActCategory = table.Column<string>(type: "text", nullable: false),
                    ActNumber = table.Column<int>(type: "integer", nullable: false),
                    SubActNumber = table.Column<int>(type: "integer", nullable: false),
                    ActYear = table.Column<int>(type: "integer", nullable: false),
                    AssentBy = table.Column<string>(type: "text", nullable: true),
                    AssentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Nature = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GazetteTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    GazetteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PageNo = table.Column<int>(type: "integer", nullable: true),
                    PublishedGazetteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ComeInforce = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_m_act_type_ActTypeId",
                        column: x => x.ActTypeId,
                        principalSchema: "ad",
                        principalTable: "m_act_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_act_m_gazzet_type_GazetteTypeId",
                        column: x => x.GazetteTypeId,
                        principalSchema: "ad",
                        principalTable: "m_gazzet_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_act_m_part_PartId",
                        column: x => x.PartId,
                        principalSchema: "ad",
                        principalTable: "m_part",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_act_m_subject_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "common",
                        principalTable: "m_subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_block",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_block_m_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_city",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_city", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_city_m_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CourtLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_m_location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "m_location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_c_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_c_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_case_category_NatureId",
                        column: x => x.NatureId,
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_work",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_work_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_work_m_work_type_WorkId",
                        column: x => x.WorkId,
                        principalTable: "m_work_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ad.m_repealed_rule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RepealedActID = table.Column<int>(type: "integer", nullable: false),
                    ActID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ad.m_repealed_rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ad.m_repealed_rule_m_act_ActID",
                        column: x => x.ActID,
                        principalSchema: "ad",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_amended",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AmendedActID = table.Column<int>(type: "integer", nullable: false),
                    ActID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act_amended", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_amended_m_act_ActID",
                        column: x => x.ActID,
                        principalSchema: "ad",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_book",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BookYear = table.Column<int>(type: "integer", nullable: false),
                    BookPageNo = table.Column<string>(type: "text", nullable: true),
                    BookSrNo = table.Column<int>(type: "integer", nullable: true),
                    Volume = table.Column<string>(type: "text", nullable: true),
                    ActId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_book_m_act_ActId",
                        column: x => x.ActId,
                        principalSchema: "ad",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_ward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_ward_m_city_CityId",
                        column: x => x.CityId,
                        principalTable: "m_city",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_complex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_complex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "m_court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "m_location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_case_template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    LanguageCode = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormName = table.Column<string>(type: "text", nullable: true),
                    FormTemplate = table.Column<string>(type: "text", nullable: true),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_case_template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "m_case_category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtComplexId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_hall",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    JudgeName = table.Column<string>(type: "text", nullable: true),
                    RoomNumber = table.Column<string>(type: "text", nullable: true),
                    CourtComplexId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Languages = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_hall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_hall_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalTable: "m_court_complex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "r_court_bench",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtBench_En = table.Column<string>(type: "text", nullable: true),
                    CourtBench_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_court_bench", x => x.Id);
                    table.ForeignKey(
                        name: "FK_r_court_bench_m_court_CourtMasterId",
                        column: x => x.CourtMasterId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_judge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CourtHallId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_judge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_judge_m_court_hall_CourtHallId",
                        column: x => x.CourtHallId,
                        principalTable: "m_court_hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_detail",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtBenchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseNo = table.Column<string>(type: "text", nullable: true),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    FirstTitle = table.Column<string>(type: "text", nullable: true),
                    FTitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondTitle = table.Column<string>(type: "text", nullable: true),
                    STitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CisNumber = table.Column<string>(type: "text", nullable: true),
                    CisYear = table.Column<int>(type: "integer", nullable: false),
                    CnrNumber = table.Column<string>(type: "text", nullable: true),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CaseStageId = table.Column<Guid>(type: "uuid", nullable: true),
                    LinkedCaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppearenceID = table.Column<Guid>(type: "uuid", nullable: false),
                    LCaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    ComplexId = table.Column<Guid>(type: "uuid", nullable: true),
                    StrengthId = table.Column<int>(type: "integer", nullable: false),
                    DisposalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_detail_case_detail_LinkedCaseId",
                        column: x => x.LinkedCaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ld",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_case_stage_CaseStageId",
                        column: x => x.CaseStageId,
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_complex_ComplexId",
                        column: x => x.ComplexId,
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_AppearenceID",
                        column: x => x.AppearenceID,
                        principalSchema: "ld",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_FTitleId",
                        column: x => x.FTitleId,
                        principalSchema: "ld",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_STitleId",
                        column: x => x.STitleId,
                        principalSchema: "ld",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_r_court_bench_CourtBenchId",
                        column: x => x.CourtBenchId,
                        principalSchema: "ld",
                        principalTable: "r_court_bench",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_assigned",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    LawyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_assigned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_assigned_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_detail_against",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImpugedOrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CaseNo = table.Column<string>(type: "text", nullable: true),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    CisYear = table.Column<int>(type: "integer", nullable: false),
                    OfficerName = table.Column<string>(type: "text", nullable: true),
                    CadreId = table.Column<Guid>(type: "uuid", nullable: true),
                    CisNo = table.Column<string>(type: "text", nullable: true),
                    CnrNo = table.Column<string>(type: "text", nullable: true),
                    CourtBenchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    ComplexId = table.Column<Guid>(type: "uuid", nullable: true),
                    StrengthId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail_against", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_detail_against_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_cadre_CadreId",
                        column: x => x.CadreId,
                        principalTable: "m_cadre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_court_complex_ComplexId",
                        column: x => x.ComplexId,
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                        column: x => x.CourtBenchId,
                        principalSchema: "ld",
                        principalTable: "r_court_bench",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_petition_detail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FieldDetails = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_petition_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_petition_detail_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_petition_detail_m_frm_types_FormId",
                        column: x => x.FormId,
                        principalTable: "m_frm_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_petition_detail_m_template_info_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "m_template_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_titles",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CaseApplicants = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_titles_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "r_case_docs",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DOTypeId = table.Column<int>(type: "integer", nullable: false),
                    DOId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    DocDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_case_docs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_r_case_docs_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_r_case_docs_m_do_type_DOId",
                        column: x => x.DOId,
                        principalSchema: "ld",
                        principalTable: "m_do_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "r_case_proceeding",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubHeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProceedingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProcWork = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_case_proceeding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_r_case_proceeding_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_r_case_proceeding_m_case_stage_StageId",
                        column: x => x.StageId,
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_r_case_proceeding_m_proceeding_head_HeadId",
                        column: x => x.HeadId,
                        principalSchema: "ld",
                        principalTable: "m_proceeding_head",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_r_case_proceeding_m_proceeding_sub_head_SubHeadId",
                        column: x => x.SubHeadId,
                        principalSchema: "ld",
                        principalTable: "m_proceeding_sub_head",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "r_case_working",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AppliedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReceivedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_case_working", x => x.Id);
                    table.ForeignKey(
                        name: "FK_r_case_working_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_r_case_working_m_work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "m_work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_r_case_working_m_work_type_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "m_work_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ad.m_repealed_rule_ActID",
                table: "ad.m_repealed_rule",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_case_assigned_CaseId",
                schema: "ld",
                table: "case_assigned",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_AppearenceID",
                schema: "ld",
                table: "case_detail",
                column: "AppearenceID");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseCategoryId",
                schema: "ld",
                table: "case_detail",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseStageId",
                schema: "ld",
                table: "case_detail",
                column: "CaseStageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ClientId",
                schema: "ld",
                table: "case_detail",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ComplexId",
                schema: "ld",
                table: "case_detail",
                column: "ComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtBenchId",
                schema: "ld",
                table: "case_detail",
                column: "CourtBenchId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_FTitleId",
                schema: "ld",
                table: "case_detail",
                column: "FTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_LinkedCaseId",
                schema: "ld",
                table: "case_detail",
                column: "LinkedCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_StateId",
                schema: "ld",
                table: "case_detail",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_STitleId",
                schema: "ld",
                table: "case_detail",
                column: "STitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CadreId",
                schema: "ld",
                table: "case_detail_against",
                column: "CadreId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseCategoryId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_ComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "ComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtBenchId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_StateId",
                schema: "ld",
                table: "case_detail_against",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_CaseId",
                table: "case_petition_detail",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_FormId",
                table: "case_petition_detail",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_TemplateId",
                table: "case_petition_detail",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_titles_CaseId",
                schema: "ld",
                table: "case_titles",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_chunk_citation_ChunkId",
                schema: "ai",
                table: "chunk_citation",
                column: "ChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_court_fee_FeeTypeId",
                schema: "account",
                table: "court_fee",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_court_fee_structure_StateId",
                schema: "account",
                table: "court_fee_structure",
                column: "StateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_m_act_ActTypeId",
                schema: "ad",
                table: "m_act",
                column: "ActTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_GazetteTypeId",
                schema: "ad",
                table: "m_act",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_PartId",
                schema: "ad",
                table: "m_act",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_SubjectId",
                schema: "ad",
                table: "m_act",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_amended_ActID",
                schema: "ad",
                table: "m_act_amended",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_book_ActId",
                schema: "ad",
                table: "m_act_book",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_m_block_DistrictId",
                table: "m_block",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_BookTypeId",
                schema: "ld",
                table: "m_book",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_PublisherId",
                schema: "ld",
                table: "m_book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_NatureId",
                schema: "ld",
                table: "m_c_type",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_m_cadre_Code",
                table: "m_cadre",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_case_category_Code",
                table: "m_case_category",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_case_category_CourtTypeId",
                table: "m_case_category",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_case_kind_CourtTypeId",
                schema: "ld",
                table: "m_case_kind",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_case_stage_Code",
                table: "m_case_stage",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_city_DistrictId",
                table: "m_city",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtLevelId",
                table: "m_court",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtTypeId1",
                table: "m_court",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_LocationId",
                table: "m_court",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_Name_LocationId",
                table: "m_court",
                columns: new[] { "Name", "LocationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtComplexId",
                schema: "ld",
                table: "m_court",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtTypeId",
                schema: "ld",
                table: "m_court",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_StateId",
                schema: "ld",
                table: "m_court",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_case_template_CaseCategoryId",
                table: "m_court_case_template",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_case_template_CaseTypeId",
                table: "m_court_case_template",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_case_template_CourtTypeId",
                table: "m_court_case_template",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_case_template_StateId",
                table: "m_court_case_template",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CourtDistrictId",
                table: "m_court_complex",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CourtId",
                table: "m_court_complex",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_LocationId",
                table: "m_court_complex",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_Name_StateId",
                table: "m_court_complex",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_StateId",
                table: "m_court_complex",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_Code_StateId",
                table: "m_court_district",
                columns: new[] { "Code", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_Name_StateId",
                table: "m_court_district",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_StateId",
                table: "m_court_district",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_Code_CourtComplexId",
                table: "m_court_hall",
                columns: new[] { "Code", "CourtComplexId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_CourtComplexId",
                table: "m_court_hall",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_level_Code",
                table: "m_court_level",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_CaseStageEntityId",
                table: "m_court_type",
                column: "CaseStageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_Code",
                table: "m_court_type",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_CourtLevelId",
                table: "m_court_type",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_m_district_StateId",
                table: "m_district",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_judge_CourtHallId",
                table: "m_judge",
                column: "CourtHallId");

            migrationBuilder.CreateIndex(
                name: "IX_m_location_Name_StateId",
                table: "m_location",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_location_ParentLocationId",
                table: "m_location",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_location_StateId",
                table: "m_location",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_part_GazetteTypeId",
                schema: "ad",
                table: "m_part",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_m_state_Code",
                table: "m_state",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_ward_CityId",
                table: "m_ward",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_CourtTypeId",
                table: "m_work",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_WorkId",
                table: "m_work",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_type_CourtTypeId",
                table: "m_work_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OppositCouncilEntity_LawyerId",
                table: "OppositCouncilEntity",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_docs_CaseId",
                schema: "ld",
                table: "r_case_docs",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_docs_DOId",
                schema: "ld",
                table: "r_case_docs",
                column: "DOId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_HeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_StageId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "SubHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_CaseId",
                schema: "ld",
                table: "r_case_working",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_WorkId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_WorkTypeId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ad.m_repealed_rule");

            migrationBuilder.DropTable(
                name: "ai_conversion_history",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "billing_detail",
                schema: "account");

            migrationBuilder.DropTable(
                name: "case_assigned",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "case_detail_against",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "case_petition_detail");

            migrationBuilder.DropTable(
                name: "case_titles",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "chunk_citation",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "court_fee",
                schema: "account");

            migrationBuilder.DropTable(
                name: "court_fee_structure",
                schema: "account");

            migrationBuilder.DropTable(
                name: "document_embedding",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "m_act_amended",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_act_book",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_block");

            migrationBuilder.DropTable(
                name: "m_book",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_case_kind",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_case_template");

            migrationBuilder.DropTable(
                name: "m_expense_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_judge");

            migrationBuilder.DropTable(
                name: "m_lang_dict");

            migrationBuilder.DropTable(
                name: "m_specialization");

            migrationBuilder.DropTable(
                name: "m_state_court_language");

            migrationBuilder.DropTable(
                name: "m_temp_frm_mapping");

            migrationBuilder.DropTable(
                name: "m_ward");

            migrationBuilder.DropTable(
                name: "OppositCouncilEntity");

            migrationBuilder.DropTable(
                name: "r_case_proceeding",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "r_case_working",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_cadre");

            migrationBuilder.DropTable(
                name: "m_frm_types");

            migrationBuilder.DropTable(
                name: "m_template_info");

            migrationBuilder.DropTable(
                name: "m_court_fee_type",
                schema: "account");

            migrationBuilder.DropTable(
                name: "document_chunk",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "m_act",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_book_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_publisher",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_hall");

            migrationBuilder.DropTable(
                name: "m_city");

            migrationBuilder.DropTable(
                name: "m_lawyer",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_proceeding_sub_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_work");

            migrationBuilder.DropTable(
                name: "r_case_docs",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_act_type",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_part",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_subject",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_district");

            migrationBuilder.DropTable(
                name: "m_proceeding_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_work_type");

            migrationBuilder.DropTable(
                name: "case_detail",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_do_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_gazzet_type",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "client",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_c_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_fs_title",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "r_court_bench",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_case_category");

            migrationBuilder.DropTable(
                name: "m_court",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_complex");

            migrationBuilder.DropTable(
                name: "m_court");

            migrationBuilder.DropTable(
                name: "m_court_district");

            migrationBuilder.DropTable(
                name: "m_court_type");

            migrationBuilder.DropTable(
                name: "m_location");

            migrationBuilder.DropTable(
                name: "m_case_stage");

            migrationBuilder.DropTable(
                name: "m_court_level");

            migrationBuilder.DropTable(
                name: "m_state");
        }
    }
}
