using System.Security.Claims;

namespace Learnify.Web.Infrastructure.Util;

public static class UserUtil
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    }

    public static string GetUserMobile(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return (principal.FindFirst(ClaimTypes.MobilePhone)?.Value!);
    }
}

