using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AspNetCoreHero.Boilerplate.Web.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;
        }

        public string UserId { get; }
        public string Username { get; }
    }
}