using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using BLL.ManagerServices;
using DAL;
using DAL.RepositoryBehaviours.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TicketManagement.WebAPI.CORE.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManagement.WebAPI.CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            TicketManagementContext ticketManagementContext)
        {
            var context = ticketManagementContext;
            _userService = new UserService(new EntityUserRepository(context), new EntityProcedureManager(context));
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("GetIdentity")]
        [HttpGet]
        private ClaimsIdentity GetIdentity(ApplicationUser usr, string uid, string[] roles)
        {
            if (usr != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, usr.UserName),
                    new Claim("UserId", uid)
                };
                foreach (var v in roles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, v));
                }
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [Route("Token")]
        [HttpGet]
        public async Task<string> Token(UserInfo userInfo)
        {
            var identity = GetIdentity(userInfo.User, userInfo.UserId, userInfo.Roles);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return "";
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        [Route("All")]
        [HttpGet]
        public List<ApplicationUser> All()
        {
            return _userManager.Users.ToList();
        }

        [Route("GetRoles")]
        [HttpGet]
        public async Task<List<string>> GetRoles(string id)
        {
            var r = new List<string>();
            var usr = await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                var list = await _userManager.GetRolesAsync(usr);
                r = list.ToList();
            }
            return r;
        }

        [Route("AddToRole")]
        [HttpPost]
        public async Task<bool> AddToRole(string id, string role)
        {
            bool r = false;
            var usr = await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                var ir = await _userManager.AddToRoleAsync(usr, role);
                r = ir.Succeeded;
            }

            return r;
        }

        [Route("RemoveFronRole")]
        [HttpDelete]
        public async Task<bool> RemoveFronRole(string id, string role)
        {
            bool r = false;
            var usr = await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                var ir = await _userManager.RemoveFromRoleAsync(usr, role);
                r = ir.Succeeded;
            }

            return r;
        }

        [Route("RemoveUser")]
        [HttpDelete]
        public async Task<bool> RemoveUser(string id)
        {
            bool r = false;
            var usr = await _userManager.FindByIdAsync(id);

            if (usr != null)
            {
                var ir = await _userManager.DeleteAsync(usr);
                r = ir.Succeeded;
            }

            return r;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<bool> Register(RegisterModel model)
        {
            bool r = false;
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.EMail, UserName = model.Name };

                var q = await _userManager.CreateAsync(user, model.Password);
                if (q.Succeeded)
                {
                    User u = new User()
                    {
                        Email = user.Email,
                        Name = user.UserName,
                        Password = user.PasswordHash
                    };

                    _userService.Save(u);
                    //no such role
                    //seed db to fix
                    var v = await _userManager.AddToRoleAsync(user, "user");
                    if (v.Succeeded)
                    {
                        r = true;
                    }
                }
            }
            return r;
        }

        [Route("RegisterForIdResult")]
        [HttpPost]
        public async Task<RegisterResult> RegisterForIdResult(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.EMail, UserName = model.Name };

                var q = await _userManager.CreateAsync(user, model.Password);
                if (q.Succeeded)
                {
                    User u = new User()
                    {
                        Email = user.Email,
                        Name = user.UserName,
                        Password = user.PasswordHash
                    };

                    _userService.Save(u);
                    //no such role
                    //seed db to fix
                    var v = await _userManager.AddToRoleAsync(user, "user");
                    if (v.Succeeded)
                    {
                        return new RegisterResult(v.Succeeded)
                        {
                            UserId = user.Id
                        };
                    }
                }
            }
            return new RegisterResult();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<string> Login(LoginModel login)
        {
            string token = "";
            int findedUserId = -1;
            ApplicationUser findedUser = null;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
                else
                {
                    User u;
                    var r = _userService.Find(user.Email);
                    if (r.Any())
                    {
                        u = r.First();

                        await _signInManager.SignOutAsync();

                        var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
                        if (signInResult.Succeeded)
                        {
                            if (u.Password == user.PasswordHash)
                            {
                                findedUserId = u.Id;
                                findedUser = user;
                                var roles = await _userManager.GetRolesAsync(user);
                                token = await Token(new UserInfo()
                                {
                                    User = findedUser,
                                    UserId = findedUserId.ToString(),
                                    Roles = roles.ToArray()
                                });

                            }
                        }
                    }
                }
            }

            return token;
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<bool> ChangePassword(string token, string password, string newPassword)
        {
            bool result = false;
            var jwtToken = new JwtSecurityToken(token);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtToken.Claims);
            var name = claimsIdentity.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var user = await _userManager.FindByNameAsync(name.Value);
            if (user != null)
            {
                var claim = claimsIdentity.FindFirst("UserId");
                var u = _userService.Get(int.Parse(claim.Value));

                if (u != null)
                {
                    var r = await _userManager.ChangePasswordAsync(user, password, newPassword);

                    if (r.Succeeded)
                    {
                        u.Password = user.PasswordHash;
                        _userService.Update(u);
                        result = true;
                    }
                }
            }
            return result;
        }

        [HttpPut]
        [Route("ChangeEmail")]
        public async Task<bool> ChangeEmail(string token, string email)
        {
            bool result = false;
            var jwtToken = new JwtSecurityToken(token);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtToken.Claims);
            var name = claimsIdentity.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var user = await _userManager.FindByNameAsync(name.Value);

            if (user != null)
            {
                var claim = claimsIdentity.FindFirst("UserId");
                var u = _userService.Get(int.Parse(claim.Value));

                if (u != null)
                {
                    user.Email = email;
                    var r = await _userManager.UpdateAsync(user);

                    if (r.Succeeded)
                    {
                        u.Email = email;
                        _userService.Update(u);
                        result = true;
                    }
                }
            }

            return result;
        }

        [HttpPut]
        [Route("ChangeUserEmail")]
        public async Task<bool> ChangeUserEmail(string id, string email)
        {
            bool result = false;
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var u = _userService.Find(user.UserName).First();

                if (u != null)
                {
                    user.Email = email;
                    var r = await _userManager.UpdateAsync(user);

                    if (r.Succeeded)
                    {
                        u.Email = email;
                        _userService.Update(u);
                        result = true;
                    }
                }
            }

            return result;
        }

        [HttpPut]
        [Route("ChangeName")]
        public async Task<bool> ChangeName(string token, string name)
        {
            bool result = false;
            var jwtToken = new JwtSecurityToken(token);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtToken.Claims);
            var nameClaim = claimsIdentity.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var user = await _userManager.FindByNameAsync(nameClaim.Value);

            if (user != null)
            {
                var claim = claimsIdentity.FindFirst("UserId");
                var u = _userService.Get(int.Parse(claim.Value));

                if (u != null)
                {
                    user.UserName = name;
                    var r = await _userManager.UpdateAsync(user);


                    if (r.Succeeded)
                    {
                        u.Name = name;
                        _userService.Update(u);
                        result = true;
                    }
                }
            }

            return result;
        }

        [HttpPut]
        [Route("ChangeUserName")]
        public async Task<bool> ChangeUserName(string id, string name)
        {
            bool result = false;
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var u = _userService.Find(user.UserName).First();

                if (u != null)
                {
                    user.UserName = name;
                    var r = await _userManager.UpdateAsync(user);


                    if (r.Succeeded)
                    {
                        u.Name = name;
                        _userService.Update(u);
                        result = true;
                    }
                }
            }

            return result;
        }

        [HttpGet]
        [Route("LogOff")]
        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
