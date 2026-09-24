using System.Security.Claims;

namespace SmartSchool.Infrastructure.Persistence
{
    public static class UserExtentions
    {
        public static string? GetUserId(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
