using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.DbServices;
using BLL.Exceptions;
using BLL.YoutubeLogic;
using BLL.VKLogic;
using DAL.RequestModel;
using DAL.ResponseModel;
using DAL.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Security.Claims;
using System.Globalization;
using System.Web.Routing;
using PandaWiki.Attributes;
using DAL.Entities;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System;
using PandaWiki.WcfServices;

namespace PandaWiki.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private readonly IUserPostService _userPostService;
        private readonly IYoutubeService _youtubeService;
        private readonly IVkService _vkService;
        private readonly ITelegramService _telegramService;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // Set language from cookies
            if (Request != null && Request.Cookies["lang"] != null)
            {
                var value = Request.Cookies["lang"].Value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
            }

            // Login from cookies
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

            _telegramService.SetSessionName(User.Identity.Name + "Session");
        }

        public SearchController(IYoutubeService youtubeService, IVkService vkService, IUserPostService userPostService)
        {
            _youtubeService = youtubeService;
            _userPostService = userPostService;
            _vkService = vkService;
            // Telegram api (TLSharp) does not work in ASP MVC, but it work through WCF Service
            _telegramService = new TelegramServiceClient();
        }

        public async Task<string> GetTelegramFile(string accessHash, string id, int version, int size, string type)
        {
            try
            {
                var buffer = await _telegramService.GetFileAsync(
                        long.Parse(accessHash), long.Parse(id), version, size);

                string base64String = Convert.ToBase64String(buffer, 0, buffer.Length);
                return "data:" + type + ";base64," + base64String;
            }
            catch { }
            return null;
        }

        public async Task<string> GetTelegramPhotoFileAsync(int localId, string secret, string volumeId, int size, string type)
        {
            try
            {
                var buffer = await _telegramService.GetPhotoFileAsync(localId, long.Parse(secret),
                    long.Parse(volumeId), size);

                string base64String = Convert.ToBase64String(buffer, 0, buffer.Length);
                return "data:" + type + ";base64," + base64String;
            }
            catch { }
            return null;
        }

        private async Task<IEnumerable<SearchModelResponse>> CheckPosts(IEnumerable<SearchModelResponse> list)
        {
            var data = list.ToList();
            foreach(var v in data)
            {
                if (v.Type == Messangers.YouTube.ToString())
                {
                    v.IsAdded = await _userPostService.Exists(new UserPost()
                    {
                        UserId = User.Identity.Name,
                        PostId = v.YoutubeModel.VideoId
                    });
                }

                if (v.Type == Messangers.Vk.ToString())
                {
                    v.IsAdded = await _userPostService.Exists(new UserPost()
                    {
                        UserId = User.Identity.Name,
                        PostId = v.VkModel.MixedId
                    });
                }
            }
            return data;
        }

        // GET: Search
        public async Task<ActionResult> Index([Bind(Include = "q")] QuerySearchModel query)
        {
            if(!string.IsNullOrEmpty(User.Identity.Name))
            {
                if (_telegramService.IsIdentityRequired())
                {
                    var user = await UserManager.FindByNameAsync(User.Identity.Name);
                    if (user != null)
                    {
                        try
                        {
                            TempData["AuthHash"] = await _telegramService.AuthAsync(user.PhoneNumber);
                            return RedirectToAction("CodeConfirm");
                        }
                        catch { }
                    }
                }
            }
            return View("Index", query);
        }

        public ActionResult CodeConfirm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CodeConfirm(string Code)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if(user != null)
            {
                try
                {
                    await _telegramService.AuthConfirmAsync(user.PhoneNumber, TempData["AuthHash"].ToString(), Code);
                }
                catch { }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Search(string q, int maxResults)
        {
            return await SearchPosts(q, TempData["Messangers"] as List<Messangers>);
        }

        [Authorised]
        public async Task<ActionResult> SavePost(string userId, string postId, string postType)
        {
            try
            {
                await _userPostService.Save(new DAL.Entities.UserPost()
                {
                    PostId = postId,
                    PostType = postType,
                    UserId = userId
                });
            }
            catch (InsertException ex)
            {
                ModelState.AddModelError("InsertException", ex.Message);
            }
            catch(Exception ex)
            {
            	ModelState.AddModelError("Exception", ex.Message);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorised]
        public async Task<ActionResult> RemovePost(string userId, string postId, string postType)
        {
            try
            {
                var result = (from x in await _userPostService.GetUserPosts(userId)
                               where x.PostId == postId && x.PostType == postType
                               select x)?.FirstOrDefault();
                if(result != null)
                {
                    await _userPostService.Delete(result.Id);
                }
            }
            catch (InsertException ex)
            {
                ModelState.AddModelError("InsertException", ex.Message);
            }
            catch(Exception ex)
            {
            	ModelState.AddModelError("Exception", ex.Message);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        private async Task<T> SetTaskTimeout<T>(Task<T> serviceCall, int duration = 15000)
        {
            var task = serviceCall;
            if (await Task.WhenAny(task, Task.Delay(duration)) == task)
            {
                return serviceCall.Result;
            }
            else
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Поиск видео
        /// </summary>
        /// <param name="search"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ActionResult> SearchVideo(string search, List<Messangers> result)
        {

            if (result == null || !result.Any())
                return PartialView("_Results", await CheckPosts(await _youtubeService.GetVideos(search)));

            foreach (var t in result)
            {
                switch (t)
                {
                    case Messangers.YouTube:
                        {
                            return PartialView("_Results", await CheckPosts(await _youtubeService.GetVideos(search)));
                        }
                    case Messangers.Vk:
                        {
                            return PartialView("_Results", await CheckPosts(await _vkService.GetVideos(search)));
                        }
                    case Messangers.Telegram:
                        {
                            break;
                        }
                }
            }



            return null;
        }

        /// <summary>
        /// Поиск постов
        /// </summary>
        /// <param name="search"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ActionResult> SearchPosts(string search, List<Messangers> result)
        {
            if (result == null || !result.Any())
                return PartialView("_Results", await CheckPosts(await _vkService.GetPosts(search)));

            foreach (var t in result)
            {
                switch (t)
                {
                    case Messangers.YouTube:
                        {
                            return PartialView("_Results", await CheckPosts(await _youtubeService.GetVideos(search)));
                        }
                    case Messangers.Vk:
                        {
                            return PartialView("_Results", await CheckPosts(await _vkService.GetPosts(search)));
                        }
                    case Messangers.Telegram:
                        {
                            try
                            {
                                var responce = await SetTaskTimeout<SearchModelResponse[]>(_telegramService.GetAudioAsync(search, 15));
                                return PartialView("_Results", await CheckPosts(responce));
                            }
                            catch (TimeoutException)
                            {
                                await _telegramService.RemoveSessionAsync();
                                return RedirectToAction("Index");
                            }
                            catch
                            {
                                return null;
                            }
                        }
                }
            }



            return null;
        }

        /// <summary>
        /// Поиск новостей
        /// </summary>
        /// <param name="search"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ActionResult> SearchNews(string search, List<Messangers> result)
        {

            if (result == null || !result.Any())
                return PartialView("_Results", await CheckPosts(await _vkService.GetNews(search)));

            foreach (var t in result)
            {
                switch (t)
                {
                    case Messangers.YouTube:
                        {
                            return PartialView("_Results", await CheckPosts(await _youtubeService.GetVideos(search)));
                        }
                    case Messangers.Vk:
                        {
                            return PartialView("_Results", await CheckPosts(await _vkService.GetNews(search)));
                        }
                    case Messangers.Telegram:
                        {
                            break;
                        }
                }
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            _telegramService.DisposeService();
            base.Dispose(disposing);
        }
    }
}