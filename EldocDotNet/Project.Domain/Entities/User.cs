using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public int VerificationCode { get; set; } = 0;
        public UserStatus Status { get; set; }

        [NotMapped]
        public string Nickname
        {
            get
            {
                return $"{Firstname} {Lastname}".Trim();
            }
        }

        public string Password { get; set; }
        public string Nationalcode { get; set; }
        public UserType UserType { get; set; }
        public string Birthdate { get; set; }
        public string SanaCode { get; set; }
        public string EconomicCode { get; set; }
    }
}