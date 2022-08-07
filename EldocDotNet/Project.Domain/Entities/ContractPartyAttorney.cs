using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class ContractPartyAttorney : BaseEntity
    {
        public bool IsFirstParty { get; set; } = true;
        public int ContractId { get; set; }
        public ContractBase Contract { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Regulatory { get; set; }
        public int DangShare { get; set; }
        public string AttachmentUrl { get; set; }
    }
}