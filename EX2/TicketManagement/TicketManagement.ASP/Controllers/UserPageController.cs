using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TicketManagement.ASP.Util;
using TicketManagement.ASP.WCF.UserService;

namespace TicketManagement.ASP.Controllers
{
    public class UserPageController : Controller
    {
        private WCF.UserService.IUserService UserService { get; }

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

        public UserPageController()
        {
            UserService = new UserServiceClient();
        }

        // GET: UserPage
        [Authorized]
        public ActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            var r = User as ClaimsPrincipal;
            ViewBag.IsUser = r.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "user");
            ViewBag.IsManager = r.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "manager");
            if (ViewBag.IsUser == null)
            {
                ViewBag.IsUser = false;
            }
            if (ViewBag.IsManager == null)
            {
                ViewBag.IsManager = false;
            }
            return View(UserService.Get(int.Parse(claim.Value)));
        }

        [ClaimsAuthorize("user")]
        public ActionResult AddAmount()
        {
            return View();
        }

        [ClaimsAuthorize("user")]
        public ActionResult GetCart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            var u = UserService.Get(int.Parse(claim.Value));

            return View(UserService.GetCart(u.Id));
        }

        [HttpPost]
        [ClaimsAuthorize("user")]
        public ActionResult AddAmount(int amount)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            var u = UserService.Get(int.Parse(claim.Value));


            if(u != null)
            {
                u.Balance += amount;
                UserService.Update(u);
            }

            return RedirectToAction("Index");
        }

        [Authorized]
        public ActionResult SetLocale()
        {
            return View();
        }

        [HttpPost]
        [Authorized]
        public ActionResult SetLocale(string locale)
        {
            HttpCookie languageCookie = System.Web.HttpContext.Current.Request.Cookies["lang"] ?? new HttpCookie("lang");

            languageCookie.Value = locale;
            languageCookie.Expires = DateTime.Now.AddYears(1);

            Response.SetCookie(languageCookie);
            if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            return RedirectToAction("Index");
        }
    }
}
