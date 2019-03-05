using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace TicketManagement.ASP.Util
{
    public class ClaimsAuthorizeAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _claimValue;
        public ClaimsAuthorizeAttribute(string value)
        {
            this._claimValue = value;
        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user != null && user.HasClaim(ClaimsIdentity.DefaultRoleClaimType, _claimValue))
            {
                OnActionExecuting(filterContext);
            }
        }
    }
}
