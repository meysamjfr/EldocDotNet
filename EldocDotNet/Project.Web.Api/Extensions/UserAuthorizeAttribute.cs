using Microsoft.AspNetCore.Mvc.Filters;
using Project.Application.DTOs.User;

namespace Project.Web.Api.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserDTO)context.HttpContext.Items["User"];
            if (user == null)
            {
                throw new UnauthorizedAccessException("invalid token");
            }
        }
    }
}