using Project.Domain.Enums;

namespace Project.Application.DTOs.User
{
    public class EditUserProfile
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationalcode { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Birthdate { get; set; }
        public string SanaCode { get; set; }
        public string EconomicCode { get; set; }
    }
}
