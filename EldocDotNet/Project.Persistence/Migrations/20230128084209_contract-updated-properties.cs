using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistence.Migrations
{
    public partial class contractupdatedproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellerPrepayment",
                table: "Contracts",
                newName: "SellerTrackingCode");

            migrationBuilder.RenameColumn(
                name: "SellerCheckNumber",
                table: "Contracts",
                newName: "SellerRemainingValidityLetter");

            migrationBuilder.RenameColumn(
                name: "SellerCheckDate",
                table: "Contracts",
                newName: "SellerPrepaymentMiddle");

            migrationBuilder.RenameColumn(
                name: "SellerCheckBankBranch",
                table: "Contracts",
                newName: "SellerPrepaymentLast");

            migrationBuilder.RenameColumn(
                name: "SellerCheckBank",
                table: "Contracts",
                newName: "SellerPrepaymentFirst");

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountLetterFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountLetterLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountLetterMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountNumberFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountNumberLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerAmountNumberMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerBargainBuilding",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerBargainStreet",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountBranchFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountBranchLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountBranchMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountNumberFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountNumberLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckAccountNumberMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBAccountBankFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBAccountBankLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBAccountBankMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankBranchFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankBranchLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankBranchMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckBankMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckDateFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckDateLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckDateMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckNumberFirst",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckNumberLast",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCheckNumberMiddle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDelayPaymentLetter",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDelayPaymentNumber",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDelayPossessionLetter",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDelayPossessionNumber",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDocumentArticle",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDocumentClause",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDocumentLink",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerIran",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerLawCourtResponsible",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerOtherConsiderPresentation",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerOtherItems",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPoliceLicensePlateLetter",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPrepareDocumentCity",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPrepareDocumentClock",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPrepareDocumentDate",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPrepareDocumentDay",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPrepareDocumentProvince",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerAmountLetterFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerAmountLetterLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerAmountLetterMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerAmountNumberFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerAmountNumberLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerAmountNumberMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerBargainBuilding",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerBargainStreet",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountBranchFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountBranchLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountBranchMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountNumberFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountNumberLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckAccountNumberMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBAccountBankFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBAccountBankLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBAccountBankMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankBranchFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankBranchLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankBranchMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckBankMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckDateFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckDateLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckDateMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckNumberFirst",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckNumberLast",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerCheckNumberMiddle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDelayPaymentLetter",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDelayPaymentNumber",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDelayPossessionLetter",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDelayPossessionNumber",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDocumentArticle",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDocumentClause",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerDocumentLink",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerIran",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerLawCourtResponsible",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerOtherConsiderPresentation",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerOtherItems",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPoliceLicensePlateLetter",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPrepareDocumentCity",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPrepareDocumentClock",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPrepareDocumentDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPrepareDocumentDay",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SellerPrepareDocumentProvince",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "SellerTrackingCode",
                table: "Contracts",
                newName: "SellerPrepayment");

            migrationBuilder.RenameColumn(
                name: "SellerRemainingValidityLetter",
                table: "Contracts",
                newName: "SellerCheckNumber");

            migrationBuilder.RenameColumn(
                name: "SellerPrepaymentMiddle",
                table: "Contracts",
                newName: "SellerCheckDate");

            migrationBuilder.RenameColumn(
                name: "SellerPrepaymentLast",
                table: "Contracts",
                newName: "SellerCheckBankBranch");

            migrationBuilder.RenameColumn(
                name: "SellerPrepaymentFirst",
                table: "Contracts",
                newName: "SellerCheckBank");
        }
    }
}
