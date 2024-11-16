using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiveOrganisations",
                columns: table => new
                {
                    OrganisationCode = table.Column<string>(type: "text", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "text", nullable: false),
                    OrganisationDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiveOrganisations", x => x.OrganisationCode);
                });

            migrationBuilder.CreateTable(
                name: "Divers",
                columns: table => new
                {
                    DiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiverName = table.Column<string>(type: "text", nullable: false),
                    DiverDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divers", x => x.DiverId);
                });

            migrationBuilder.CreateTable(
                name: "DiveSiteTypes",
                columns: table => new
                {
                    DiveSiteCode = table.Column<string>(type: "text", nullable: false),
                    DiveSiteDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiveSiteTypes", x => x.DiveSiteCode);
                });

            migrationBuilder.CreateTable(
                name: "LevelsOfCertification",
                columns: table => new
                {
                    CertificationCode = table.Column<string>(type: "text", nullable: false),
                    OrganisationCode = table.Column<string>(type: "text", nullable: false),
                    CertificationName = table.Column<string>(type: "text", nullable: false),
                    OtherDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsOfCertification", x => x.CertificationCode);
                    table.ForeignKey(
                        name: "FK_LevelsOfCertification_DiveOrganisations_OrganisationCode",
                        column: x => x.OrganisationCode,
                        principalTable: "DiveOrganisations",
                        principalColumn: "OrganisationCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiveSites",
                columns: table => new
                {
                    DiveSiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiveSiteCode = table.Column<string>(type: "text", nullable: false),
                    DiveSiteName = table.Column<string>(type: "text", nullable: false),
                    DiveSiteDescription = table.Column<string>(type: "text", nullable: false),
                    OtherDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiveSites", x => x.DiveSiteId);
                    table.ForeignKey(
                        name: "FK_DiveSites_DiveSiteTypes_DiveSiteCode",
                        column: x => x.DiveSiteCode,
                        principalTable: "DiveSiteTypes",
                        principalColumn: "DiveSiteCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiverCertifications",
                columns: table => new
                {
                    DiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificationCode = table.Column<string>(type: "text", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InstructorName = table.Column<string>(type: "text", nullable: false),
                    InstructionLocation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiverCertifications", x => new { x.DiverId, x.CertificationCode });
                    table.ForeignKey(
                        name: "FK_DiverCertifications_Divers_DiverId",
                        column: x => x.DiverId,
                        principalTable: "Divers",
                        principalColumn: "DiverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiverCertifications_LevelsOfCertification_CertificationCode",
                        column: x => x.CertificationCode,
                        principalTable: "LevelsOfCertification",
                        principalColumn: "CertificationCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dives",
                columns: table => new
                {
                    DiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiveSiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NightDiveYn = table.Column<bool>(type: "boolean", nullable: false),
                    OtherDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dives", x => x.DiveId);
                    table.ForeignKey(
                        name: "FK_Dives_DiveSites_DiveSiteId",
                        column: x => x.DiveSiteId,
                        principalTable: "DiveSites",
                        principalColumn: "DiveSiteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dives_Divers_DiverId",
                        column: x => x.DiverId,
                        principalTable: "Divers",
                        principalColumn: "DiverId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wrecks",
                columns: table => new
                {
                    DiveSiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    WreckDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WreckDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wrecks", x => x.DiveSiteId);
                    table.ForeignKey(
                        name: "FK_Wrecks_DiveSites_DiveSiteId",
                        column: x => x.DiveSiteId,
                        principalTable: "DiveSites",
                        principalColumn: "DiveSiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiverCertifications_CertificationCode",
                table: "DiverCertifications",
                column: "CertificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dives_DiverId",
                table: "Dives",
                column: "DiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Dives_DiveSiteId",
                table: "Dives",
                column: "DiveSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_DiveSites_DiveSiteCode",
                table: "DiveSites",
                column: "DiveSiteCode");

            migrationBuilder.CreateIndex(
                name: "IX_LevelsOfCertification_OrganisationCode",
                table: "LevelsOfCertification",
                column: "OrganisationCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiverCertifications");

            migrationBuilder.DropTable(
                name: "Dives");

            migrationBuilder.DropTable(
                name: "Wrecks");

            migrationBuilder.DropTable(
                name: "LevelsOfCertification");

            migrationBuilder.DropTable(
                name: "Divers");

            migrationBuilder.DropTable(
                name: "DiveSites");

            migrationBuilder.DropTable(
                name: "DiveOrganisations");

            migrationBuilder.DropTable(
                name: "DiveSiteTypes");
        }
    }
}
