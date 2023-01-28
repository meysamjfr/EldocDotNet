using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ContractStatus Status { get; set; }
        public int FactorId { get; set; } = 0;

        public int BargainCode { get; set; }
        public ContractType BargainEnum { get; set; }


        //level 1
        public bool SellerIsCompany { get; set; }
        public string SellerName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerFatherName { get; set; }
        public string SellerNationalCode { get; set; }
        public string SellerBirthDay { get; set; }
        public string SellerBirthDayLocation { get; set; }
        public string SellerAddress { get; set; }
        public string SellerMobileNumber { get; set; }
        public string SellerEmail { get; set; }


        //level 2
        public string SellerTamami { get; set; }
        public string SellerDong { get; set; }
        public string SellerAutomobileDevice { get; set; }
        public string SellerTip { get; set; }
        public string SellerSystem { get; set; }
        public string SellerSolarYearModel { get; set; }
        public string SellerChassisNumber { get; set; }
        public string SellerEngineNumber { get; set; }
        public string SellerVinNumer { get; set; }
        public string SellerPoliceLicensePlateNumber { get; set; }
        public string SellerPoliceLicensePlateLetter { get; set; }
        public string SellerIran { get; set; }
        public string SellerInsuranceNumber { get; set; }
        public string SellerInsuranceCompany { get; set; }
        public string SellerRemainingValidity { get; set; }
        public string SellerRemainingValidityLetter { get; set; }
        public string SellerOtherAttachments { get; set; }


        //level 3
        public string SellerTotalAmountNumber { get; set; }
        public string SellerTotalAmountLetter { get; set; }
        public string SellerAmountNumberFirst { get; set; }
        public string SellerAmountLetterFirst { get; set; }
        public string SellerPrepaymentFirst { get; set; }
        public string SellerCheckNumberFirst { get; set; }
        public string SellerCheckDateFirst { get; set; }
        public string SellerCheckBankFirst { get; set; }
        public string SellerCheckBankBranchFirst { get; set; }
        public string SellerCheckAccountNumberFirst { get; set; }
        public string SellerCheckBAccountBankFirst { get; set; }
        public string SellerCheckAccountBranchFirst { get; set; }
        public string SellerAmountNumberMiddle { get; set; }
        public string SellerAmountLetterMiddle { get; set; }
        public string SellerPrepaymentMiddle { get; set; }
        public string SellerCheckNumberMiddle { get; set; }
        public string SellerCheckDateMiddle { get; set; }
        public string SellerCheckBankMiddle { get; set; }
        public string SellerCheckBankBranchMiddle { get; set; }
        public string SellerCheckAccountNumberMiddle { get; set; }
        public string SellerCheckBAccountBankMiddle { get; set; }
        public string SellerCheckAccountBranchMiddle { get; set; }
        public string SellerAmountNumberLast { get; set; }
        public string SellerAmountLetterLast { get; set; }
        public string SellerPrepaymentLast { get; set; }
        public string SellerCheckNumberLast { get; set; }
        public string SellerCheckDateLast { get; set; }
        public string SellerCheckBankLast { get; set; }
        public string SellerCheckBankBranchLast { get; set; }
        public string SellerCheckAccountNumberLast { get; set; }
        public string SellerCheckBAccountBankLast { get; set; }
        public string SellerCheckAccountBranchLast { get; set; }

        //level 4
        public string SellerBargainDate { get; set; }
        public string SellerBargainProvince { get; set; }
        public string SellerBargainCity { get; set; }
        public string SellerBargainStreet { get; set; }
        public string SellerBargainAlley { get; set; }
        public string SellerBargainPlaque { get; set; }
        public string SellerBargainBuilding { get; set; }
        public string SellerBargainFloor { get; set; }
        public string SellerBargainUnit { get; set; }
        public string SellerBargainPostalCode { get; set; }
        public string SellerCancellationPaymentNumber { get; set; }
        public string SellerCancellationPaymentLetter { get; set; }


        //level 5
        public string SellerRegistryCompanyNumber { get; set; }
        public string SellerRegistryCompanyCity { get; set; }
        public string SellerRegistryCompanyDateNumber { get; set; }
        public string SellerRegistryCompanyDateLetter { get; set; }
        public string SellerDelayPaymentNumber { get; set; }
        public string SellerDelayPaymentLetter { get; set; }
        public string SellerDelayPossessionNumber { get; set; }
        public string SellerDelayPossessionLetter { get; set; }

        //level 6

        //level 7
        public string SellerPrepareDocumentDay { get; set; }
        public string SellerPrepareDocumentDate { get; set; }
        public string SellerPrepareDocumentClock { get; set; }
        public string SellerPrepareDocumentProvince { get; set; }
        public string SellerPrepareDocumentCity { get; set; }

        //level 8

        //level 9
        public string SellerJurisdictionsProvince { get; set; }
        public string SellerJurisdictionsCity { get; set; }

        //level 10

        //level 11
        public string SellerOtherConsiderPresentation { get; set; }
        public string SellerLawCourtResponsible { get; set; }
        public string SellerOtherItems { get; set; }
        public string SellerDocumentLink { get; set; }
        public string SellerDocumentArticle { get; set; }
        public string SellerDocumentClause { get; set; }
        public string SellerTrackingCode { get; set; }
    }
}