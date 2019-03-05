using System.Web.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Routing;
using TicketManagement.ASP.Util;
using TicketManagement.ASP.WCF.PurchaseService;

namespace TicketManagement.ASP.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly WCF.PurchaseService.IPurchaseService _purchaseService;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Request.Cookies["lang"] != null)
            {
                var value = Request.Cookies["lang"].Value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
            }

            if (Request.Cookies["access_token"] != null)
            {
                var value = Request.Cookies["access_token"].Value;
                if (!string.IsNullOrEmpty(value))
                {
                    var jwtToken = new JwtSecurityToken(value);
                    var claims = jwtToken.Claims;
                    ClaimsIdentity claim = new ClaimsIdentity(claims);
                    var cp = new ClaimsPrincipal(claim);
                    var transformer = new ClaimsAuthenticationManager();
                    var newPrincipal = transformer.Authenticate(string.Empty, cp);
                    Thread.CurrentPrincipal = newPrincipal;
                    HttpContext.User = newPrincipal;
                }
            }
        }

        public PurchaseController ()
        {

            _purchaseService = new PurchaseServiceClient();
        }

        // GET: Purchase
        [ClaimsAuthorize("user")]
        public ActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");

            return View(_purchaseService.GetForUser(int.Parse(claim.Value)));
        }
    }
}
