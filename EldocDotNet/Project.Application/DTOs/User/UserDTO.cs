using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.User
{
    public class UserDTO : BaseDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationalcode { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Birthdate { get; set; }
        public string SanaCode { get; set; }
        public string EconomicCode { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }

        public string GetNickName() => $"{Firstname} {Lastname}".Trim();
    }
}
