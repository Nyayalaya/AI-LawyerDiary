using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class _1stMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ai");

            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.EnsureSchema(
                name: "cases");

            migrationBuilder.EnsureSchema(
                name: "clients");

            migrationBuilder.EnsureSchema(
                name: "masters");

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
                schema: "accounts",
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
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OfficeEmail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ReferalBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RegNo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Proprietor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ClientType = table.Column<string>(type: "text", nullable: false),
                    PAN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    GST = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
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
                name: "entity_properties",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyCode = table.Column<string>(type: "text", nullable: true),
                    PropertyName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    ControlType = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    IsSearchable = table.Column<bool>(type: "boolean", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false),
                    AllowMultiple = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    MaxLength = table.Column<int>(type: "integer", nullable: true),
                    MinValue = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    ValidationRegex = table.Column<string>(type: "text", nullable: true),
                    LookupSource = table.Column<string>(type: "text", nullable: true),
                    Placeholder = table.Column<string>(type: "text", nullable: true),
                    HelpText = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_act_type",
                schema: "masters",
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
                schema: "masters",
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
                schema: "masters",
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
                schema: "masters",
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
                schema: "accounts",
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
                schema: "masters",
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
                name: "m_document_type",
                schema: "masters",
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
                    table.PrimaryKey("PK_m_document_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_expense_head",
                schema: "accounts",
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
                name: "m_form_case_category_mapping",
                schema: "masters",
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

            migrationBuilder.CreateTable(
                name: "m_form_court",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormSubtypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_court", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_form_type",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_type", x => x.Id);
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
                schema: "cases",
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
                schema: "masters",
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
                schema: "common",
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
                name: "m_proceeding_type",
                schema: "masters",
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
                name: "m_publisher",
                schema: "masters",
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
                schema: "masters",
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
                schema: "common",
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
                schema: "common",
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
                name: "matter_types",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matter_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "court_fee",
                schema: "accounts",
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
                        principalSchema: "accounts",
                        principalTable: "m_court_fee_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_type",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_type_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalSchema: "masters",
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_form",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_form_m_form_type_FormTypeId",
                        column: x => x.FormTypeId,
                        principalSchema: "masters",
                        principalTable: "m_form_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_part",
                schema: "masters",
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
                        principalSchema: "masters",
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
                name: "m_proceeding",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_proceeding_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_book",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_book_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_book_m_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "masters",
                        principalTable: "m_publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "court_fee_structure",
                schema: "accounts",
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
                        principalSchema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_district",
                schema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_location",
                schema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_location_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matter_categories",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatterTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matter_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matter_categories_matter_types_MatterTypeId",
                        column: x => x.MatterTypeId,
                        principalSchema: "masters",
                        principalTable: "matter_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_case_category",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_case_kind",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_work_type",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_form_subtype",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_subtype", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_form_subtype_m_form_FormId",
                        column: x => x.FormId,
                        principalSchema: "masters",
                        principalTable: "m_form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_act",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_act_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_act_m_gazzet_type_GazetteTypeId",
                        column: x => x.GazetteTypeId,
                        principalSchema: "masters",
                        principalTable: "m_gazzet_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_act_m_part_PartId",
                        column: x => x.PartId,
                        principalSchema: "masters",
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
                schema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_city",
                schema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsVirtualCourt = table.Column<bool>(type: "boolean", nullable: false),
                    LocationEntityId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "FK_m_court_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_m_location_LocationEntityId",
                        column: x => x.LocationEntityId,
                        principalSchema: "common",
                        principalTable: "m_location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matter_subcategories",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatterCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matter_subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matter_subcategories_matter_categories_MatterCategoryId",
                        column: x => x.MatterCategoryId,
                        principalSchema: "masters",
                        principalTable: "matter_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_c_type",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_work",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_work_m_work_type_WorkId",
                        column: x => x.WorkId,
                        principalSchema: "masters",
                        principalTable: "m_work_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_form_template",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormSubtypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TemplateContent = table.Column<string>(type: "text", nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CaseTypeCode = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_form_template_m_form_subtype_FormSubtypeId",
                        column: x => x.FormSubtypeId,
                        principalSchema: "masters",
                        principalTable: "m_form_subtype",
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
                        principalSchema: "masters",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_amended",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_book",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_ward",
                schema: "common",
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
                        principalSchema: "common",
                        principalTable: "m_city",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_complex",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsVirtualComplex = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
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
                        principalSchema: "masters",
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
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
                        principalSchema: "masters",
                        principalTable: "m_c_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalSchema: "masters",
                        principalTable: "m_case_category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_case_template_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_form_template_version",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_form_template_version", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_form_template_version_m_form_template_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalSchema: "masters",
                        principalTable: "m_form_template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_1",
                schema: "masters",
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
                    table.PrimaryKey("PK_m_court_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_1_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalSchema: "masters",
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_1_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_1_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_1_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_hall",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    JudgeName = table.Column<string>(type: "text", nullable: true),
                    RoomNumber = table.Column<string>(type: "text", nullable: true),
                    CourtComplexId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SeatingCapacity = table.Column<int>(type: "integer", nullable: false),
                    HasVideoConference = table.Column<bool>(type: "boolean", nullable: false),
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
                        principalSchema: "masters",
                        principalTable: "m_court_complex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_hall_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "r_court_bench",
                schema: "masters",
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
                        name: "FK_r_court_bench_m_court_1_CourtMasterId",
                        column: x => x.CourtMasterId,
                        principalSchema: "masters",
                        principalTable: "m_court_1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_judge",
                schema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_detail",
                schema: "cases",
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
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "clients",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "masters",
                        principalTable: "m_c_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalSchema: "masters",
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_case_stage_CaseStageId",
                        column: x => x.CaseStageId,
                        principalSchema: "masters",
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_complex_ComplexId",
                        column: x => x.ComplexId,
                        principalSchema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_AppearenceID",
                        column: x => x.AppearenceID,
                        principalSchema: "cases",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_FTitleId",
                        column: x => x.FTitleId,
                        principalSchema: "cases",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_fs_title_STitleId",
                        column: x => x.STitleId,
                        principalSchema: "cases",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_r_court_bench_CourtBenchId",
                        column: x => x.CourtBenchId,
                        principalSchema: "masters",
                        principalTable: "r_court_bench",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_detail_data",
                schema: "cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryNumber = table.Column<string>(type: "text", nullable: true),
                    CaseNo = table.Column<string>(type: "text", nullable: true),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    InstitutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_case_detail_data_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalSchema: "cases",
                        principalTable: "case_detail_data",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "clients",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_client_OpponentClientId",
                        column: x => x.OpponentClientId,
                        principalSchema: "clients",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalSchema: "masters",
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_case_stage_CaseStageId",
                        column: x => x.CaseStageId,
                        principalSchema: "masters",
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "masters",
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalSchema: "masters",
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
                        principalSchema: "masters",
                        principalTable: "m_court_hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalSchema: "masters",
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_fs_title_FirstTitleId",
                        column: x => x.FirstTitleId,
                        principalSchema: "cases",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_data_m_fs_title_SecondTitleId",
                        column: x => x.SecondTitleId,
                        principalSchema: "cases",
                        principalTable: "m_fs_title",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_petition_detail",
                schema: "cases",
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
                        principalSchema: "cases",
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
                schema: "cases",
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
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_works",
                schema: "cases",
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
                    table.PrimaryKey("PK_case_works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_works_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_works_m_work_WorkId",
                        column: x => x.WorkId,
                        principalSchema: "masters",
                        principalTable: "m_work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_works_m_work_type_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalSchema: "masters",
                        principalTable: "m_work_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_against_data",
                schema: "cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImpugedOrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CourtLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtComplexId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtHallId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CaseNo = table.Column<string>(type: "text", nullable: true),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    CisNumber = table.Column<string>(type: "text", nullable: true),
                    CisYear = table.Column<int>(type: "integer", nullable: true),
                    CnrNumber = table.Column<string>(type: "text", nullable: true),
                    OfficerName = table.Column<string>(type: "text", nullable: true),
                    CadreId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaseDetailEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_against_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_against_data_case_detail_CaseDetailEntityId",
                        column: x => x.CaseDetailEntityId,
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_against_data_case_detail_data_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "cases",
                        principalTable: "case_detail_data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "masters",
                        principalTable: "m_c_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_cadre_CadreId",
                        column: x => x.CadreId,
                        principalSchema: "masters",
                        principalTable: "m_cadre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_against_data_m_case_category_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalSchema: "masters",
                        principalTable: "m_case_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "masters",
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_complex_CourtComplexId",
                        column: x => x.CourtComplexId,
                        principalSchema: "masters",
                        principalTable: "m_court_complex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_district_CourtDistrictId",
                        column: x => x.CourtDistrictId,
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_hall_CourtHallId",
                        column: x => x.CourtHallId,
                        principalSchema: "masters",
                        principalTable: "m_court_hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_level_CourtLevelId",
                        column: x => x.CourtLevelId,
                        principalSchema: "masters",
                        principalTable: "m_court_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "masters",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_against_data_m_state_StateId",
                        column: x => x.StateId,
                        principalSchema: "common",
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_assigned",
                schema: "cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    LawyerId = table.Column<string>(type: "text", nullable: true),
                    CaseEntityId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_assigned_case_detail_data_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalSchema: "cases",
                        principalTable: "case_detail_data",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "case_documents",
                schema: "cases",
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
                    CaseEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_documents_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_documents_case_detail_data_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalSchema: "cases",
                        principalTable: "case_detail_data",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_documents_m_document_type_DOId",
                        column: x => x.DOId,
                        principalSchema: "masters",
                        principalTable: "m_document_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_proceedings",
                schema: "cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubHeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProceedingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CaseEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProcWork = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_proceedings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_proceedings_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "cases",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_proceedings_case_detail_data_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalSchema: "cases",
                        principalTable: "case_detail_data",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_proceedings_m_case_stage_StageId",
                        column: x => x.StageId,
                        principalSchema: "masters",
                        principalTable: "m_case_stage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_proceedings_m_proceeding_SubHeadId",
                        column: x => x.SubHeadId,
                        principalSchema: "masters",
                        principalTable: "m_proceeding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_proceedings_m_proceeding_type_HeadId",
                        column: x => x.HeadId,
                        principalSchema: "masters",
                        principalTable: "m_proceeding_type",
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
                        name: "FK_document_chunk_case_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "cases",
                        principalTable: "case_documents",
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
                name: "IX_case_against_data_CadreId",
                schema: "cases",
                table: "case_against_data",
                column: "CadreId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CaseCategoryId",
                schema: "cases",
                table: "case_against_data",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CaseDetailEntityId",
                schema: "cases",
                table: "case_against_data",
                column: "CaseDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CaseId",
                schema: "cases",
                table: "case_against_data",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CaseTypeId",
                schema: "cases",
                table: "case_against_data",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtComplexId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtDistrictId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtHallId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtHallId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtLevelId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_CourtTypeId",
                schema: "cases",
                table: "case_against_data",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_against_data_StateId",
                schema: "cases",
                table: "case_against_data",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_assigned_CaseEntityId",
                schema: "cases",
                table: "case_assigned",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_assigned_CaseId",
                schema: "cases",
                table: "case_assigned",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_AppearenceID",
                schema: "cases",
                table: "case_detail",
                column: "AppearenceID");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseCategoryId",
                schema: "cases",
                table: "case_detail",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseStageId",
                schema: "cases",
                table: "case_detail",
                column: "CaseStageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseTypeId",
                schema: "cases",
                table: "case_detail",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ClientId",
                schema: "cases",
                table: "case_detail",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ComplexId",
                schema: "cases",
                table: "case_detail",
                column: "ComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtBenchId",
                schema: "cases",
                table: "case_detail",
                column: "CourtBenchId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtDistrictId",
                schema: "cases",
                table: "case_detail",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtTypeId",
                schema: "cases",
                table: "case_detail",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_FTitleId",
                schema: "cases",
                table: "case_detail",
                column: "FTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_LinkedCaseId",
                schema: "cases",
                table: "case_detail",
                column: "LinkedCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_StateId",
                schema: "cases",
                table: "case_detail",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_STitleId",
                schema: "cases",
                table: "case_detail",
                column: "STitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseCategoryId",
                schema: "cases",
                table: "case_detail_data",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseEntityId",
                schema: "cases",
                table: "case_detail_data",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CaseStageId",
                schema: "cases",
                table: "case_detail_data",
                column: "CaseStageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_ClientId",
                schema: "cases",
                table: "case_detail_data",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtComplexId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtDistrictId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtHallId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtHallId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtLevelId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_CourtTypeId",
                schema: "cases",
                table: "case_detail_data",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_FirstTitleId",
                schema: "cases",
                table: "case_detail_data",
                column: "FirstTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_OpponentClientId",
                schema: "cases",
                table: "case_detail_data",
                column: "OpponentClientId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_ParentCaseId",
                schema: "cases",
                table: "case_detail_data",
                column: "ParentCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_data_SecondTitleId",
                schema: "cases",
                table: "case_detail_data",
                column: "SecondTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_documents_CaseEntityId",
                schema: "cases",
                table: "case_documents",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_documents_CaseId",
                schema: "cases",
                table: "case_documents",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_documents_DOId",
                schema: "cases",
                table: "case_documents",
                column: "DOId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_CaseId",
                schema: "cases",
                table: "case_petition_detail",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_FormId",
                schema: "cases",
                table: "case_petition_detail",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_TemplateId",
                schema: "cases",
                table: "case_petition_detail",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_case_proceedings_CaseEntityId",
                schema: "cases",
                table: "case_proceedings",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_proceedings_CaseId",
                schema: "cases",
                table: "case_proceedings",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_proceedings_HeadId",
                schema: "cases",
                table: "case_proceedings",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_case_proceedings_StageId",
                schema: "cases",
                table: "case_proceedings",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_proceedings_SubHeadId",
                schema: "cases",
                table: "case_proceedings",
                column: "SubHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_case_titles_CaseId",
                schema: "cases",
                table: "case_titles",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_works_CaseId",
                schema: "cases",
                table: "case_works",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_works_WorkId",
                schema: "cases",
                table: "case_works",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_case_works_WorkTypeId",
                schema: "cases",
                table: "case_works",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_chunk_citation_ChunkId",
                schema: "ai",
                table: "chunk_citation",
                column: "ChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_client_Email",
                schema: "clients",
                table: "client",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_client_Mobile",
                schema: "clients",
                table: "client",
                column: "Mobile");

            migrationBuilder.CreateIndex(
                name: "IX_court_fee_FeeTypeId",
                schema: "accounts",
                table: "court_fee",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_court_fee_structure_StateId",
                schema: "accounts",
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
                name: "IX_entity_properties_EntityType_EntityId_PropertyCode",
                schema: "masters",
                table: "entity_properties",
                columns: new[] { "EntityType", "EntityId", "PropertyCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_act_ActTypeId",
                schema: "masters",
                table: "m_act",
                column: "ActTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_GazetteTypeId",
                schema: "masters",
                table: "m_act",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_PartId",
                schema: "masters",
                table: "m_act",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_SubjectId",
                schema: "masters",
                table: "m_act",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_amended_ActID",
                schema: "masters",
                table: "m_act_amended",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_book_ActId",
                schema: "masters",
                table: "m_act_book",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_m_block_DistrictId",
                schema: "common",
                table: "m_block",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_BookTypeId",
                schema: "masters",
                table: "m_book",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_PublisherId",
                schema: "masters",
                table: "m_book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_CourtTypeId",
                schema: "masters",
                table: "m_c_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_NatureId",
                schema: "masters",
                table: "m_c_type",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_m_cadre_Code",
                schema: "masters",
                table: "m_cadre",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_case_category_Code",
                schema: "masters",
                table: "m_case_category",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_case_category_CourtTypeId",
                schema: "masters",
                table: "m_case_category",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_case_kind_CourtTypeId",
                schema: "masters",
                table: "m_case_kind",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_case_stage_Code",
                schema: "masters",
                table: "m_case_stage",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_city_DistrictId",
                schema: "common",
                table: "m_city",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_Code",
                schema: "masters",
                table: "m_court",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "masters",
                table: "m_court",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtTypeId",
                schema: "masters",
                table: "m_court",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_LocationEntityId",
                schema: "masters",
                table: "m_court",
                column: "LocationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_StateId",
                schema: "masters",
                table: "m_court",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_1_CourtComplexId",
                schema: "masters",
                table: "m_court_1",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_1_CourtDistrictId",
                schema: "masters",
                table: "m_court_1",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_1_CourtTypeId",
                schema: "masters",
                table: "m_court_1",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_1_StateId",
                schema: "masters",
                table: "m_court_1",
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
                schema: "masters",
                table: "m_court_complex",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CourtId",
                schema: "masters",
                table: "m_court_complex",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_Name_StateId",
                schema: "masters",
                table: "m_court_complex",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_StateId",
                schema: "masters",
                table: "m_court_complex",
                column: "StateId");

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
                name: "IX_m_court_hall_CourtComplexId",
                schema: "masters",
                table: "m_court_hall",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_CourtTypeId",
                schema: "masters",
                table: "m_court_hall",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_hall_Name_CourtComplexId",
                schema: "masters",
                table: "m_court_hall",
                columns: new[] { "Name", "CourtComplexId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_level_Code",
                schema: "masters",
                table: "m_court_level",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_CaseStageEntityId",
                schema: "masters",
                table: "m_court_type",
                column: "CaseStageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_Code",
                schema: "masters",
                table: "m_court_type",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_type_CourtLevelId",
                schema: "masters",
                table: "m_court_type",
                column: "CourtLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_m_district_StateId",
                schema: "common",
                table: "m_district",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_form_FormTypeId",
                schema: "masters",
                table: "m_form",
                column: "FormTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_form_case_category_mapping_FormSubtypeId_CaseCategoryId",
                schema: "masters",
                table: "m_form_case_category_mapping",
                columns: new[] { "FormSubtypeId", "CaseCategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_form_subtype_FormId",
                schema: "masters",
                table: "m_form_subtype",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_m_form_template_FormSubtypeId",
                schema: "masters",
                table: "m_form_template",
                column: "FormSubtypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_form_template_version_FormTemplateId",
                schema: "masters",
                table: "m_form_template_version",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_form_type_Code",
                schema: "masters",
                table: "m_form_type",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_judge_CourtHallId",
                schema: "masters",
                table: "m_judge",
                column: "CourtHallId");

            migrationBuilder.CreateIndex(
                name: "IX_m_location_Name_StateId",
                schema: "common",
                table: "m_location",
                columns: new[] { "Name", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_location_ParentLocationId",
                schema: "common",
                table: "m_location",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_m_location_StateId",
                schema: "common",
                table: "m_location",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_part_GazetteTypeId",
                schema: "masters",
                table: "m_part",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_ProceedingTypeId",
                schema: "masters",
                table: "m_proceeding",
                column: "ProceedingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_state_Code",
                schema: "common",
                table: "m_state",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_ward_CityId",
                schema: "common",
                table: "m_ward",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_CourtTypeId",
                schema: "masters",
                table: "m_work",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_WorkId",
                schema: "masters",
                table: "m_work",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_type_CourtTypeId",
                schema: "masters",
                table: "m_work_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_matter_categories_MatterTypeId_Code",
                schema: "masters",
                table: "matter_categories",
                columns: new[] { "MatterTypeId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_matter_subcategories_MatterCategoryId_Code",
                schema: "masters",
                table: "matter_subcategories",
                columns: new[] { "MatterCategoryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_matter_types_Code",
                schema: "masters",
                table: "matter_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OppositCouncilEntity_LawyerId",
                table: "OppositCouncilEntity",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "masters",
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
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "case_against_data",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "case_assigned",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "case_petition_detail",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "case_proceedings",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "case_titles",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "case_works",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "chunk_citation",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "court_fee",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "court_fee_structure",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "document_embedding",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "entity_properties",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_act_amended",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_act_book",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_block",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_book",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_case_kind",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_case_template");

            migrationBuilder.DropTable(
                name: "m_expense_head",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "m_form_case_category_mapping",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_form_court",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_form_template_version",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_judge",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_lang_dict",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_specialization",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_state_court_language",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_temp_frm_mapping");

            migrationBuilder.DropTable(
                name: "m_ward",
                schema: "common");

            migrationBuilder.DropTable(
                name: "matter_subcategories",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "OppositCouncilEntity");

            migrationBuilder.DropTable(
                name: "m_cadre",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_frm_types");

            migrationBuilder.DropTable(
                name: "m_template_info");

            migrationBuilder.DropTable(
                name: "m_proceeding",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_work",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_fee_type",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "document_chunk",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "m_act",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_book_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_publisher",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_form_template",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_city",
                schema: "common");

            migrationBuilder.DropTable(
                name: "matter_categories",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_lawyer",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_proceeding_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_work_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "case_documents",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "m_act_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_part",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_subject",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_form_subtype",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_district",
                schema: "common");

            migrationBuilder.DropTable(
                name: "matter_types",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "case_detail_data",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "m_document_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_gazzet_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_form",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "case_detail",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "m_court_hall",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_form_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "client",
                schema: "clients");

            migrationBuilder.DropTable(
                name: "m_c_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_fs_title",
                schema: "cases");

            migrationBuilder.DropTable(
                name: "r_court_bench",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_case_category",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_1",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_complex",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_district");

            migrationBuilder.DropTable(
                name: "m_court_type",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_location",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_case_stage",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_court_level",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "m_state",
                schema: "common");
        }
    }
}
