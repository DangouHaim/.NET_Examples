using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Rest;
using TicketManagement.ASP.WCF.SeatService;
using TicketManagement.ASP.WCF.VenueService;

namespace TicketManagement.ASP.Controllers
{
    public class HomeController : Controller
    {
        private WCF.VenueService.IVenueService _venueService;
        private WCF.SeatService.ISeatService _seatService;

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

        public HomeController()
        {
            _venueService = new VenueServiceClient();
            _seatService = new SeatServiceClient();
        }
        
        public ActionResult Index()
        {
            List<string> l = new List<string>();
            foreach (var venue in _venueService.GetAll())
            {
                l.Add(venue.Description);
            }

            ViewBag.data = l;
            
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
