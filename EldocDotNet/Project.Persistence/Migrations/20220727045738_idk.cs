using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistence.Migrations
{
    public partial class idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnilateralContracts_Users_UserId",
                table: "UnilateralContracts");

            migrationBuilder.DropTable(
                name: "BilateralContracts");

            migrationBuilder.DropTable(
                name: "FinancialContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnilateralContracts",
                table: "UnilateralContracts");

            migrationBuilder.RenameTable(
                name: "UnilateralContracts",
                newName: "ContractBase");

            migrationBuilder.RenameIndex(
                name: "IX_UnilateralContracts_UserId",
                table: "ContractBase",
                newName: "IX_ContractBase_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ContractType",
                table: "ContractBase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ContractBase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FinancialContract_ContractType",
                table: "ContractBase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstPartyOfService",
                table: "ContractBase",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPartyOfService",
                table: "ContractBase",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondUserId",
                table: "ContractBase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectToGovernmentLawType",
                table: "ContractBase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnilateralContract_ContractType",
                table: "ContractBase",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractBase",
                table: "ContractBase",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContractPartyAttorney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFirstParty = table.Column<bool>(type: "bit", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regulatory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DangShare = table.Column<int>(type: "int", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPartyAttorney", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPartyAttorney_ContractBase_ContractId",
                        column: x => x.ContractId,
                        principalTable: "ContractBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractBase_SecondUserId",
                table: "ContractBase",
                column: "SecondUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPartyAttorney_ContractId",
                table: "ContractPartyAttorney",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractBase_Users_SecondUserId",
                table: "ContractBase",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractBase_Users_UserId",
                table: "ContractBase",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractBase_Users_SecondUserId",
                table: "ContractBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractBase_Users_UserId",
                table: "ContractBase");

            migrationBuilder.DropTable(
                name: "ContractPartyAttorney");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractBase",
                table: "ContractBase");

            migrationBuilder.DropIndex(
                name: "IX_ContractBase_SecondUserId",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "FinancialContract_ContractType",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "FirstPartyOfService",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "SecondPartyOfService",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "SecondUserId",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "SubjectToGovernmentLawType",
                table: "ContractBase");

            migrationBuilder.DropColumn(
                name: "UnilateralContract_ContractType",
                table: "ContractBase");

            migrationBuilder.RenameTable(
                name: "ContractBase",
                newName: "UnilateralContracts");

            migrationBuilder.RenameIndex(
                name: "IX_ContractBase_UserId",
                table: "UnilateralContracts",
                newName: "IX_UnilateralContracts_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ContractType",
                table: "UnilateralContracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnilateralContracts",
                table: "UnilateralContracts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BilateralContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecondUserId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AfterSaleServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractSubject = table.Column<int>(type: "int", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guarantee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToGovernmentLaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilateralContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BilateralContracts_Users_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BilateralContracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AfterSaleServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractSubject = table.Column<int>(type: "int", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guarantee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToGovernmentLaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialContracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BilateralContracts_SecondUserId",
                table: "BilateralContracts",
                column: "SecondUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BilateralContracts_UserId",
                table: "BilateralContracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialContracts_UserId",
                table: "FinancialContracts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnilateralContracts_Users_UserId",
                table: "UnilateralContracts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
