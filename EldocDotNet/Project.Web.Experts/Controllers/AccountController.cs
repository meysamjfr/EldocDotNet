using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Application.DTOs.Expert;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Web.Experts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IExpertService _refferService;

        public AccountController(IExpertService refferService)
        {
            _refferService = refferService;
        }

        public IActionResult Index(string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            ViewBag.ReturnUrl = returnUrl;
            return View("index");
        }

        public IActionResult Login(string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            ViewBag.ReturnUrl = returnUrl;
            return View("index");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(ExpertLogin input)
        {
            var ReturnUrl = string.IsNullOrWhiteSpace(input.ReturnUrl) ? "/" : input.ReturnUrl;
            var user = await _refferService.Login(input);

            if (user is null)
            {
                return new Response<string>(ResponseStatus.BadRequest, message: "نام کاربری یا کلمه عبور صحیح نمی‌باشد").ToJsonResult();
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.Surname, user.Specialty),
                    new Claim(ClaimTypes.Role, "کارشناس"),
                    new Claim("DTO", JsonConvert.SerializeObject(user)),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = input.RememberMe,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = "/",
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            return new Response<string>(ResponseStatus.Succeed, data: ReturnUrl).ToJsonResult();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect(Url.Content("/account"));
        }
        public IActionResult AccessDenied()
        {
            ViewBag.ReturnUrl = "/";
            return View();
        }

    }
}
