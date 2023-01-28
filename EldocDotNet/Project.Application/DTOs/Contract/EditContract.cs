namespace Project.Application.DTOs.Contract
{
    public class EditContract
    {
        public int BargainCode { get; set; }
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
    }
}
