using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataPresenter.Entity;
using TicketManagement.ASP.Util;
using TicketManagement.ASP.WCF.EventAreaService;
using TicketManagement.ASP.WCF.EventSeatService;
using TicketManagement.ASP.WCF.EventService;
using TicketManagement.ASP.WCF.LayoutService;

namespace TicketManagement.ASP.Controllers
{
    public class EventController : Controller
    {
        private readonly WCF.LayoutService.ILayoutService _layoutService;
        private readonly WCF.EventService.IEventService _eventService;
        private readonly WCF.EventAreaService.IEventAreaService _eventAreaService;
        private readonly WCF.EventSeatService.IEventSeatService _eventSeatService;

        private string MapUrl(string path)
        {
            string appPath = Server.MapPath("/").ToLower();
            return $"/{path.ToLower().Replace(appPath, "").Replace(@"\", "/")}";
        }

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

        public EventController()
        {
            _eventSeatService = new EventSeatServiceClient();
            _layoutService = new LayoutServiceClient();
            _eventService = new EventServiceClient();
            _eventAreaService = new EventAreaServiceClient();
            _eventSeatService = new EventSeatServiceClient();
        }

        // GET: Event
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_eventService.VisibleEvents());
        }

        [AllowAnonymous]
        public ActionResult ToEvent(int id)
        {
            List<string> galery = new List<string>();
            VirtualPathUtility.ToAbsolute("~/");

            foreach (var v in _eventService.GetAttachedFilesForEvent(id))
            {
                galery.Add(MapUrl(v));
            }
            ViewBag.Galery = galery;

            var data = _eventService.Get(id);
            ViewBag.TicketCount = _eventService.TicketCount(id);

            //Getting area and linked seats to list of pairs
            List<Tuple<EventArea, IEnumerable<EventSeat>>> seatsData = new List<Tuple<EventArea, IEnumerable<EventSeat>>>();

            foreach (var v in _eventAreaService.GetForEvent(data.Id))
            {
                var eventSeats = _eventSeatService.GetForEventArea(v.Id);
                if (eventSeats.Any())
                {
                    seatsData.Add(new Tuple<EventArea, IEnumerable<EventSeat>>(
                        v, eventSeats));
                }
            }
            ViewBag.SeatsData = seatsData;
            var r = User as ClaimsPrincipal;
            ViewBag.IsUser = r.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "user");
            if (ViewBag.IsUser == null)
            {
                ViewBag.IsUser = false;
            }

            return View(data);
        }


        [HttpPost]
        [ClaimsAuthorize("user")]
        public ActionResult BuyTickets(int[] ids, int eventId, int from = 0)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            if (claim != null)
            {
                if (from > 0)
                {
                    if (ids != null)
                    {
                        foreach (var v in ids)
                        {
                            _eventService.Buy(int.Parse(claim.Value), v);
                        }
                    }
                }
                else
                {
                    if (ids != null)
                    {
                        foreach (var v in ids)
                        {
                            _eventService.ToCart(int.Parse(claim.Value), v);
                        }
                        //cart unlock
                        new Task(async () =>
                        {
                            var cl = claim.Value;

                            await Task.Delay(Properties.Settings.Default.CartDelay);

                            foreach (var v in ids)
                            {
                                _eventService.FromCart(int.Parse(cl), v);
                            }
                        }).Start();
                    }
                }
            }

            return RedirectToAction("ToEvent", new { Id = eventId });
        }

        [HttpPost]
        [ClaimsAuthorize("user")]
        public ActionResult BuyTicketsFromCart(int[] ids)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst("UserId");
            if (claim != null)
            {
                foreach (var v in ids)
                {
                    _eventService.Buy(int.Parse(claim.Value), v);
                }
            }

            return RedirectToAction("GetCart", "UserPage");
        }

        [AllowAnonymous]
        public ActionResult LoadSeats(int id)
        {
            return View("ToEvent");
        }

        [ClaimsAuthorize("manager")]
        public ActionResult ManageEvents(int layoutId = 0)
        {
            var layouts = _layoutService.GetAll().ToList();
            ViewBag.Layouts = layouts;
            List<Tuple<Event, Tuple<int, int>>> data = new List<Tuple<Event, Tuple<int, int>>>();

            List<Event> events;
            List<EventArea> eventAreas;

            if (layoutId > 0)
            {
                events = _eventService.GetForLayout(layoutId).ToList();
                eventAreas = _eventAreaService.GetForLayout(layoutId).ToList();
            }
            else
            {
                if (layouts.Any())
                {
                    events = _eventService.GetForLayout(layouts[0].Id).ToList();
                    eventAreas = _eventAreaService.GetForLayout(layouts[0].Id).ToList();
                }
                else
                {
                    events = _eventService.GetAll().ToList();
                    eventAreas = _eventAreaService.GetAll().ToList();
                }
            }

            ViewBag.EventAreas = eventAreas;

            foreach (var v in events)
            {
                data.Add(new Tuple<Event, Tuple<int, int>>(v, new Tuple<int, int>(_eventService.TicketCount(v.Id), _eventService.TicketCountTotal(v.Id))));
            }
            ViewBag.Events = data;
            //model for chnge prices for event areas
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("manager")]
        public ActionResult AddEvent(Event ev, HttpPostedFileBase file)
        {
            try
            {
                ev.Id = _eventService.Save(ev);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    _eventService.AttachFileToEvent(ev.Id, path);

                    if (ev.Id > -1)
                    {
                        ViewBag.Message = "Success";
                    }
                    else
                    {
                        ViewBag.Message = "Error: " + ev.Id;
                    }

                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return RedirectToAction("ManageEvents");
        }

        [ClaimsAuthorize("manager")]
        public ActionResult RemoveEvent(int[] ids)
        {
            if (ids != null)
            {
                foreach (var v in ids)
                {
                    _eventService.Delete(v);
                }
            }
            return RedirectToAction("ManageEvents");
        }

        [HttpPost]
        [ClaimsAuthorize("manager")]
        public ActionResult DeleteImages(int[] ids, int id)
        {
            foreach (var v in ids)
            {
                _eventService.DeleteFileFromEvent(v);
            }
            return RedirectToAction("EditEvent", "Event", new { Id = id });
        }

        [ClaimsAuthorize("manager")]
        public ActionResult EditEvent(int id, HttpPostedFileBase file)
        {

            List<Tuple<int, string>> galery = new List<Tuple<int, string>>();
            VirtualPathUtility.ToAbsolute("~/");

            foreach (var v in _eventService.GetAttachedFilesForEventToPair(id))
            {
                galery.Add(new Tuple<int, string>(v.Item1, MapUrl(v.Item2)));
            }
            ViewBag.Galery = galery;

            ViewBag.Layouts = _layoutService.GetAll();

            return View(_eventService.Get(id));
        }

        [ClaimsAuthorize("manager")]
        public ActionResult EditEventArea(int id)
        {
            return View(_eventAreaService.Get(id));
        }

        [HttpPost]
        [ClaimsAuthorize("manager")]
        public ActionResult EditEventArea(decimal price, int id)
        {
            var v = _eventAreaService.Get(id);
            if (v != null)
            {
                v.Price = price;
                _eventAreaService.Update(v);
            }
            return RedirectToAction("ManageEvents");
        }

        [HttpPost]
        [ClaimsAuthorize("manager")]
        public ActionResult EditEventConfirmed(Event v, HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                _eventService.AttachFileToEvent(v.Id, path);

                ViewBag.Message = "File uploaded successfully";
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            _eventService.Update(v);

            return RedirectToAction("EditEvent", "Event", new { Id = id });
        }
    }
}
