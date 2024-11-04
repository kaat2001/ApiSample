using ApiSample.Constants;
using System.Security.Claims;

namespace ApiSampleControllers.Extentions;

public static class UserExtensions
{
    public static bool HasCustomerClaim(this ClaimsPrincipal claimsPrincipal, int customerId)
    {
        return claimsPrincipal.HasClaim(ClaimTypeNames.Customer, customerId.ToString());
    }

    public static bool HasCustomerClaim(this ClaimsPrincipal claimsPrincipal, int[] customerIds)
    {
        if (customerIds == null || customerIds.Length == 0)
            return false;
        return customerIds.All(x => claimsPrincipal.HasClaim(ClaimTypeNames.Customer, x.ToString()));
    }

    public static bool HasRoleClaim(this ClaimsPrincipal claimsPrincipal, string roleName)
    {
        return claimsPrincipal.HasClaim(ClaimTypeNames.Role, roleName);
    }

    public static int GetId(this ClaimsPrincipal principal)
    {
        if (int.TryParse(GetClaim(principal, ClaimTypes.NameIdentifier), out var id))
            return id;

        return 0;
    }

    public static string? GetClaim(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.FirstOrDefault(p => p.Type == claimType)?.Value;
    }
}
