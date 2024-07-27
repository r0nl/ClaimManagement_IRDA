using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IRDA_DAL.Migrations
{
    /// <inheritdoc />
    public partial class IniMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "paymentOfClaims",
                columns: table => new
                {
                    reportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    payment = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentOfClaims", x => x.reportId);
                });

            migrationBuilder.CreateTable(
                name: "pendingStatusReports",
                columns: table => new
                {
                    reportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    stage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pendingStatusReports", x => new { x.reportId, x.stage });
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsuredFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuredLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfInsurance = table.Column<DateOnly>(type: "date", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyNo);
                });

            migrationBuilder.CreateTable(
                name: "Surveyors",
                columns: table => new
                {
                    SurveyorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateLimit = table.Column<int>(type: "int", nullable: false),
                    TimesAllocated = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveyors", x => x.SurveyorId);
                });

            migrationBuilder.CreateTable(
                name: "ClaimDetails",
                columns: table => new
                {
                    ClaimId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstimatedLoss = table.Column<int>(type: "int", nullable: false),
                    DateOfAccident = table.Column<DateOnly>(type: "date", nullable: false),
                    ClaimStatus = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Open"),
                    SurveyorID = table.Column<int>(type: "int", nullable: true),
                    AmtApprovedBySurveyor = table.Column<int>(type: "int", nullable: false),
                    InsuranceCompanyApproval = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WithdrawClaim = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SurveyorFees = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimDetails", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_ClaimDetails_Policies_PolicyNo",
                        column: x => x.PolicyNo,
                        principalTable: "Policies",
                        principalColumn: "PolicyNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimDetails_Surveyors_SurveyorID",
                        column: x => x.SurveyorID,
                        principalTable: "Surveyors",
                        principalColumn: "SurveyorId");
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "PolicyNo", "DateOfInsurance", "EmailId", "InsuredFirstName", "InsuredLastName", "VehicleNo", "status" },
                values: new object[,]
                {
                    { "CS12345", new DateOnly(2024, 1, 1), "p1@gmail.com", "Abcde", "Zyxwv", "1234", false },
                    { "CS23456", new DateOnly(2022, 10, 11), "p2@gmail.com", "Bcdef", "Yxwvu", "2345", true },
                    { "CS34567", new DateOnly(2023, 11, 12), "p3@gmail.com", "Cdefg", "Xwvut", "3456", false },
                    { "CS45678", new DateOnly(2024, 1, 21), "p4@gmail.com", "Defgh", "Wvuts", "4567", true },
                    { "CS56789", new DateOnly(2024, 1, 19), "p5@gmail.com", "Efghi", "Vutsr", "5678", true }
                });

            migrationBuilder.InsertData(
                table: "Surveyors",
                columns: new[] { "SurveyorId", "EstimateLimit", "FirstName", "LastName", "TimesAllocated" },
                values: new object[,]
                {
                    { 12345, 20000, "Abcde", "Zyxwv", 4 },
                    { 23456, 21000, "Bcdef", "Yxwvu", 5 },
                    { 34567, 22000, "Cdefg", "Xwvut", 6 },
                    { 45678, 23000, "Defgh", "Wvuts", 7 },
                    { 56789, 24000, "Efghi", "Vutsr", 8 }
                });

            migrationBuilder.InsertData(
                table: "ClaimDetails",
                columns: new[] { "ClaimId", "AmtApprovedBySurveyor", "ClaimStatus", "DateOfAccident", "EstimatedLoss", "InsuranceCompanyApproval", "PolicyNo", "SurveyorFees", "SurveyorID", "WithdrawClaim" },
                values: new object[,]
                {
                    { "1234567890", 9000, "Open", new DateOnly(2024, 2, 23), 10000, true, "CS12345", 1500, null, true },
                    { "2345678901", 12000, "Open", new DateOnly(2024, 1, 30), 13000, true, "CS45678", 1900, null, true }
                });

            migrationBuilder.InsertData(
                table: "ClaimDetails",
                columns: new[] { "ClaimId", "AmtApprovedBySurveyor", "ClaimStatus", "DateOfAccident", "EstimatedLoss", "PolicyNo", "SurveyorFees", "SurveyorID", "WithdrawClaim" },
                values: new object[] { "ERTYUIOPQW", 11000, "Open", new DateOnly(2023, 11, 30), 12000, "CS34567", 2000, 34567, true });

            migrationBuilder.InsertData(
                table: "ClaimDetails",
                columns: new[] { "ClaimId", "AmtApprovedBySurveyor", "ClaimStatus", "DateOfAccident", "EstimatedLoss", "InsuranceCompanyApproval", "PolicyNo", "SurveyorFees", "SurveyorID", "WithdrawClaim" },
                values: new object[,]
                {
                    { "QWERTYUIOP", 9000, "Closed", new DateOnly(2024, 2, 3), 10000, true, "CS12345", 1500, 12345, true },
                    { "RTYUIOPQWE", 12000, "Closed", new DateOnly(2024, 1, 31), 13000, true, "CS45678", 1900, 45678, true }
                });

            migrationBuilder.InsertData(
                table: "ClaimDetails",
                columns: new[] { "ClaimId", "AmtApprovedBySurveyor", "ClaimStatus", "DateOfAccident", "EstimatedLoss", "PolicyNo", "SurveyorFees", "SurveyorID" },
                values: new object[] { "TYUIOPQWER", 13000, "Closed", new DateOnly(2024, 2, 13), 14000, "CS56789", 1800, 56789 });

            migrationBuilder.InsertData(
                table: "ClaimDetails",
                columns: new[] { "ClaimId", "AmtApprovedBySurveyor", "ClaimStatus", "DateOfAccident", "EstimatedLoss", "InsuranceCompanyApproval", "PolicyNo", "SurveyorFees", "SurveyorID" },
                values: new object[] { "WERTYUIOPQ", 10000, "Open", new DateOnly(2023, 12, 13), 11000, true, "CS23456", 1700, 23456 });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_PolicyNo",
                table: "ClaimDetails",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_SurveyorID",
                table: "ClaimDetails",
                column: "SurveyorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimDetails");

            migrationBuilder.DropTable(
                name: "paymentOfClaims");

            migrationBuilder.DropTable(
                name: "pendingStatusReports");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Surveyors");
        }
    }
}
