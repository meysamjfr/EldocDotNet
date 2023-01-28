namespace Project.Application.DTOs.Contract
{
    public class ContractLevel5
    {
        public bool SellerIsCompany { get; set; }
        public int BargainCode { get; set; }
        public string SellerRegistryCompanyNumber { get; set; }
        public string SellerRegistryCompanyCity { get; set; }
        public string SellerRegistryCompanyDateNumber { get; set; }
        public string SellerRegistryCompanyDateLetter { get; set; }
        public string SellerDelayPaymentNumber { get; set; }
        public string SellerDelayPaymentLetter { get; set; }
        public string SellerDelayPossessionNumber { get; set; }
        public string SellerDelayPossessionLetter { get; set; }
    }
}