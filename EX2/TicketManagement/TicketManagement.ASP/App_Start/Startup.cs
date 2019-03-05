using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TicketManagement.ASP.Models;
using TicketManagement.ASP.WCF.DbInitService;

[assembly: OwinStartup(typeof(TicketManagement.ASP.Startup))]
namespace TicketManagement.ASP
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new DbInitServiceClient().Seed();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Accaunt/Login")
            });
        }
    }
}
