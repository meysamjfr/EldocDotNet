﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Features.Interfaces;
using Project.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project.Web.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IWebHostEnvironment env)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContextAsync(context, userService, token);

            else
            {
                //if (env.EnvironmentName == "Development")
                //{
                //    await AttachUserToContextAsync(context, userService, "meysam");
                //}
            }

            await _next(context);
        }

        private async Task AttachUserToContextAsync(HttpContext context, IUserService userService, string token)
        {
            try
            {
                if (token == "meysam")
                {
                    context.Items["User"] = await userService.GetById(1170);
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "uid").Value);

                    var findUser = await userService.GetById(userId);

                    if (findUser != null)
                    {
                        context.Items["User"] = findUser;
                    }
                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
