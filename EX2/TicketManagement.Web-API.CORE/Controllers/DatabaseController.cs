using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BLL.IServices;
using BLL.ManagerServices;
using DAL;
using DAL.RepositoryBehaviours.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TicketManagement.WebAPI.CORE.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManagement.WebAPI.CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;

        public DatabaseController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationContext appContext, 
            TicketManagementContext ticketManagementContext)
        {
            string s = "";
            var context = ticketManagementContext;
            _userService = new UserService(new EntityUserRepository(context), new EntityProcedureManager(context));
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = appContext;
        }


        [Route("Init")]
        [HttpGet]
        public async Task<List<ApplicationUser>> Init()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            //_context.Database.Migrate();


            //Roles
            var rAdmin = new IdentityRole()
            {
                Name = "admin"
            };
            var rUser = new IdentityRole()
            {
                Name = "user"
            };
            var rMgr = new IdentityRole()
            {
                Name = "manager"
            };

            await _roleManager.CreateAsync(rAdmin);
            await _roleManager.CreateAsync(rUser);
            await _roleManager.CreateAsync(rMgr);

            //Users
            var admin = new ApplicationUser()
            {
                Email = "admin@mail.ru",
                UserName = "admin",
            };
            string password = "AdminBoss11223344_";
            var r = await _userManager.CreateAsync(admin, password);

            if (r.Succeeded)
            {
                User u = new User()
                {
                    Email = admin.Email,
                    Name = admin.UserName,
                    Password = admin.PasswordHash,
                    Balance = 1000
                };

                var luser = _userService.Find(admin.Email);
                if (luser.Any())
                {
                    _userService.Delete(luser.First().Id);
                }

                _userService.Save(u);

                await _userManager.AddToRoleAsync(admin, rAdmin.Name);
                await _userManager.AddToRoleAsync(admin, rMgr.Name);
            }

            var mgr = new ApplicationUser()
            {
                Email = "mgr@mail.ru",
                UserName = "manager",
            };
            password = "ManagerBoss11223344_";
            r = await _userManager.CreateAsync(mgr, password);

            if (r.Succeeded)
            {
                User u = new User()
                {
                    Email = mgr.Email,
                    Name = mgr.UserName,
                    Password = mgr.PasswordHash,
                    Balance = 1000
                };

                var luser = _userService.Find(mgr.Email);
                if (luser.Any())
                {
                    _userService.Delete(luser.First().Id);
                }

                _userService.Save(u);

                await _userManager.AddToRoleAsync(mgr, rMgr.Name);
            }

            var user = new ApplicationUser()
            {
                Email = "user@mail.ru",
                UserName = "user",
            };
            password = "UserBoss11223344_";
            r = await _userManager.CreateAsync(user, password);

            if (r.Succeeded)
            {
                User u = new User()
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Password = user.PasswordHash,
                    Balance = 1000
                };

                var luser = _userService.Find(user.Email);
                if (luser.Any())
                {
                    _userService.Delete(luser.First().Id);
                }

                _userService.Save(u);

                await _userManager.AddToRoleAsync(user, rUser.Name);
            }




            var user2 = new ApplicationUser()
            {
                Email = "user2@mail.ru",
                UserName = "user2",
            };
            password = "UserBoss211223344_";
            r = await _userManager.CreateAsync(user2, password);

            if (r.Succeeded)
            {
                User u = new User()
                {
                    Email = user2.Email,
                    Name = user2.UserName,
                    Password = user2.PasswordHash,
                    Balance = 0
                };

                var luser = _userService.Find(user2.Email);
                if (luser.Any())
                {
                    _userService.Delete(luser.First().Id);
                }

                _userService.Save(u);

                await _userManager.AddToRoleAsync(user2, rUser.Name);
            }

            return _userManager.Users.ToList();
        }
    }
}
