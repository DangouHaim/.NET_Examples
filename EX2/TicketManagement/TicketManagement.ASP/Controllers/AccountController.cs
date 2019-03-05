using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Rest;
using TicketManagement.ASP.Util;
using TicketManagement.ASP.WCF.UserService;
using TicketManagement.ASP.WebAPI;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using LoginModel = TicketManagement.ASP.Models.LoginModel;
using RegisterModel = TicketManagement.ASP.Models.RegisterModel;

//Swagger.json: https://localhost:44319/swagger/v1/swagger.json

namespace TicketManagement.ASP.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private WCF.UserService.IUserService UserService { get; }

        private IWebAPIClient UserAPI { get; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Request.Cookies["lang"] == null)
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

        public AccountController()
        {
            UserService = new UserServiceClient();
            UserAPI = new WebAPIClient(new Uri("https://localhost:44319/"), new BasicAuthenticationCredentials());
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var result = await UserAPI.ApiAccountRegisterPostAsync(new WebAPI.Models.RegisterModel(model.Name, model.EMail,
                model.Password, model.PasswordConfirm));
            if (result != null && result.Value)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                   var result =
                    await UserAPI.ApiAccountLoginPostAsync(new WebAPI.Models.LoginModel(login.Email, login.Password));
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
                else
                {
                    var t = result.Split(new[] {'"', ':'})[4];

                    HttpCookie tokenCookie = System.Web.HttpContext.Current.Request.Cookies["access_token"] ?? new HttpCookie("access_token");

                    tokenCookie.Value = t;
                    tokenCookie.Expires = DateTime.Now.AddYears(1);

                    Response.SetCookie(tokenCookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid form");
            }
            return View(login);
        }

        [Authorized]
        public ActionResult ChangeName()
        {
            return View();
        }

        [HttpPost]
        [Authorized]
        public async Task<ActionResult> ChangeName(string name)
        {
            if (Request.Cookies["access_token"] != null)
            {
                var token = Request.Cookies["access_token"].Value;
                if (!string.IsNullOrEmpty(token))
                {
                    var r = await UserAPI.ApiAccountChangeNamePutAsync(token, name);
                    if (r.Value)
                    {
                        return RedirectToAction("Index", "UserPage");
                    }
                }
            }
            return View();
        }

        [Authorized]
        public ActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        [Authorized]
        public async Task<ActionResult> ChangeEmail(string email)
        {
            if (Request.Cookies["access_token"] != null)
            {
                var token = Request.Cookies["access_token"].Value;
                if (!string.IsNullOrEmpty(token))
                {
                    var r = await UserAPI.ApiAccountChangeEmailPutAsync(Request.Cookies["access_token"].Value, email);
                    if (r.Value)
                    {
                        return RedirectToAction("Index", "UserPage");
                    }
                }
            }
            return View();
        }

        [Authorized]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorized]
        public async Task<ActionResult> ChangePassword(string password, string newPassword)
        {
            if (Request.Cookies["access_token"] != null)
            {
                var token = Request.Cookies["access_token"].Value;
                if (!string.IsNullOrEmpty(token))
                {
                    var r = await UserAPI.ApiAccountChangePasswordPutAsync(Request.Cookies["access_token"].Value, password, newPassword);
                    if (r.Value)
                    {
                        return RedirectToAction("Index", "UserPage");
                    }
                }
            }
            return View();
        }

        [Authorized]
        public ActionResult LogOff()
        {
            UserAPI.ApiAccountLogOffGet();
            if (Request.Cookies["access_token"] != null)
            {
                Response.Cookies["access_token"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
