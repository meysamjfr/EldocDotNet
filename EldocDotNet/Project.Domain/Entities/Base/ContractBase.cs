using Project.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.Base
{
    public class ContractBase : BaseEntity
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        public User User { get; set; }

        public ContractSubject ContractSubject { get; set; }
        public string Guarantee { get; set; }
        public string AfterSaleServices { get; set; }
        public string SubjectToGovernmentLaw { get; set; }
        public SubjectToGovernmentLawType SubjectToGovernmentLawType { get; set; }
        public string FirstPartyOfService { get; set; }
        public string SecondPartyOfService { get; set; }
        public ICollection<ContractPartyAttorney> PartyAttorneys { get; set; }
    }
}

