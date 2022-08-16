using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.User;

namespace Project.Application.Features.Interfaces
{
    public interface IUserService
    {
        UserDTO Current();
        Task<DatatableResponse<UserDTO>> Datatable(UserDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<UserDTO> EditProfile(EditUserProfile input);
        Task<UserDTO> GetById(int id);
        Task<UserDTO> GetByToken(string token);
        Task<UserDTO> GetProfile();
        Task<UserDTO> Login(LoginUser input);
        Task<bool> SetPassword(string newPassword);
        Task Signup(string phone);
        Task UpdateBalance(double newBalance);
        Task<string> Verify(VerifyUser verify);
    }
}
