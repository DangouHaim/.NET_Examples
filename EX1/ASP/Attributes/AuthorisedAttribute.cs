using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PandaWiki.Attributes
{
    public class AuthorisedAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user != null && (user.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "user")
                                 || user.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "admin")
                                 || user.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "manager")))
            {
                OnActionExecuting(filterContext);
            }
        }
    }
}