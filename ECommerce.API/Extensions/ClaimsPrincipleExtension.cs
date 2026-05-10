using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerce.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.Sub)
               ?? user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.Email)
               ?? user.FindFirstValue(ClaimTypes.Email);
    }
}