using System.Security.Claims;

namespace UserModule.Persistence
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? principal)
        {
            if (principal?.Identity is not { IsAuthenticated: true })
                throw new UnauthorizedAccessException("User is not authenticated.");

            var userIdClaim = principal.FindFirst("UserId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("UserId claim is missing or invalid.");

            return userId;
        }

        public static List<string> GetRoles(this ClaimsPrincipal? principal)
        {
            if (principal?.Identity is not { IsAuthenticated: true })
                return new List<string>();

            return principal.FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
    }
}