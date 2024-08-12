using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MVC.Extensions;

public class CustomAuthorization
{
    public static bool UserClaimValidation(HttpContext context, string claimName, string claimValue)
    {
        if (context.User.Identity == null) throw new InvalidOperationException();

        return context.User.Identity.IsAuthenticated &&
            context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
    }
}

public class ClaimRequestFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimRequestFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        area = "Identity",
                        page = "Account/Login",
                        ReturnUrl = context.HttpContext.Request.Path.ToString()
                    }
                )
            );
        }

        if (!CustomAuthorization.UserClaimValidation(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}

public class ClaimAuthorizationAttribute : TypeFilterAttribute
{
    public ClaimAuthorizationAttribute(string claimName, string claimValue) : base(typeof(ClaimRequestFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}
