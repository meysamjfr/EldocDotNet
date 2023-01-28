using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistence.Migrations
{
    public partial class contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FactorId = table.Column<int>(type: "int", nullable: false),
                    BargainCode = table.Column<int>(type: "int", nullable: false),
                    BargainEnum = table.Column<int>(type: "int", nullable: false),
                    SellerIsCompany = table.Column<bool>(type: "bit", nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerFatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBirthDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBirthDayLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerTamami = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerAutomobileDevice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerSolarYearModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerEngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerVinNumer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPoliceLicensePlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerInsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerInsuranceCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRemainingValidity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerOtherAttachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerTotalAmountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerTotalAmountLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPrepayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCheckDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCheckBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCheckBankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainAlley = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainPlaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainFloor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBargainPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCancellationPaymentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCancellationPaymentLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRegistryCompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRegistryCompanyCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRegistryCompanyDateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRegistryCompanyDateLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerJurisdictionsProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerJurisdictionsCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
