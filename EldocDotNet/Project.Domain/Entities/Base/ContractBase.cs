using Project.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities.Base
{
    public class ContractBase : BaseEntity
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string WorkAddress { get; set; }
        public string Description { get; set; }







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

