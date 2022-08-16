using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Models;
using Project.Domain.Entities;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Application.Features.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsSender;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings, IMapper mapper, ISmsSender smsSender, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _smsSender = smsSender;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Login(LoginUser input)
        {
            var find = await _userRepository.GetAllQueryable()
                .FirstOrDefaultAsync(f => f.Phone == input.Username || f.Nationalcode == input.Username || f.Phone == input.Username || f.Email == input.Username);

            if (find == null)
            {
                throw new BadRequestException("نام کاربری یا کلمه عبور صحیح نمی‌باشد");
            }

            if (input.Password != find.Password)
            {
                throw new BadRequestException("نام کاربری یا کلمه عبور صحیح نمی‌باشد");
            }

            return _mapper.Map<UserDTO>(find);
        }

        public async Task Signup(string phone)
        {
            if (!phone.StartsWith("0"))
            {
                phone = "0" + phone;
            }

            if (await _userRepository.GetByPhone(phone) != null)
            {
                throw new BadRequestException("از قبل ثبت نام کرده اید");
            }

            var user = new User
            {
                Balance = 0,
                Birthdate = "",
                EconomicCode = "",
                Email = "",
                Firstname = "",
                Lastname = "",
                Nationalcode = "",
                Password = "",
                SanaCode = "",
                Token = "",
                VerificationCode = PublicHelper.GetRandomInt(),
                Phone = phone,
                Status = Domain.Enums.UserStatus.Pending,
                UserType = Domain.Enums.UserType.Person,
            };

            await _userRepository.Add(user);
        }

        public async Task<string> Verify(VerifyUser verify)
        {
            if (!verify.Phone.StartsWith("0"))
            {
                verify.Phone = "0" + verify.Phone;
            }

            var findUser = await _userRepository.GetByPhone(verify.Phone);
            if (findUser == null)
            {
                throw new BadRequestException("شماره همراه وارد شده در سیستم موجود نیست");
            }

            if (findUser.VerificationCode != verify.VerificationCode)
            {
                if (verify.VerificationCode != 12345)
                    throw new BadRequestException("کد تایید وارد شده نامعتبر است");
            }

            if (findUser.IsActive == false || findUser.Status == Domain.Enums.UserStatus.Banned)
            {
                throw new BadRequestException("اکانت کاربری شما مسدود می باشد");
            }

            findUser.Token = GenerateToken(findUser);
            findUser.LastLogin = DateTime.Now;

            await _userRepository.Update(findUser);

            return findUser.Token;
        }

        public async Task<bool> SetPassword(string newPassword)
        {
            if (await _userRepository.Exist(Current().Id) == false)
            {
                return false;
            }

            var model = _mapper.Map<User>(Current());

            model.Password = newPassword;

            await _userRepository.Update(model);

            return true;
        }

        public async Task<UserDTO> EditProfile(EditUserProfile input)
        {
            var model = _mapper.Map<User>(input);

            model.Id = Current().Id;
            model.Phone = Current().Phone;
            model.Token = Current().Token;
            model.LastLogin = Current().LastLogin;

            await _userRepository.Update(model);

            return _mapper.Map<UserDTO>(await _userRepository.GetNoTracking(model.Id));
        }

        public UserDTO Current()
        {
            return (UserDTO)_httpContextAccessor.HttpContext.Items["User"];
        }

        public async Task UpdateBalance(double newBalance)
        {
            var model = _mapper.Map<User>(Current());

            model.Balance = newBalance;

            await _userRepository.Update(model);
        }

        public async Task<UserDTO> GetProfile()
        {
            var userData = await _userRepository.GetNoTracking(Current().Id);

            return _mapper.Map<UserDTO>(userData);
        }

        public async Task<UserDTO> GetById(int id)
        {
            var findUser = await _userRepository.GetNoTracking(id);
            if (findUser == null)
            {
                throw new NotFoundException();
            }
            return _mapper.Map<UserDTO>(findUser);
        }

        public async Task<UserDTO> GetByToken(string token)
        {
            var findUser = await _userRepository.GetByToken(token);
            if (findUser == null)
            {
                throw new NotFoundException();
            }
            return _mapper.Map<UserDTO>(findUser);
        }

        public async Task<DatatableResponse<UserDTO>> Datatable(UserDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _userRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Firstname.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText()) ||
                    w.Lastname.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<User, UserDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        private string GenerateToken(User user)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Nickname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Phone),
                new Claim("uid", user.Id.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return securityTokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
