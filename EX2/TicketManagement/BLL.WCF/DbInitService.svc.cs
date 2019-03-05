using System;
using BLL.WCF.IServices;
using DAL;
using DAL.RepositoryBehaviours.Entity;
using Microsoft.EntityFrameworkCore;
using IAreaService = BLL.IServices.IAreaService;
using IEventAreaService = BLL.IServices.IEventAreaService;
using IEventSeatService = BLL.IServices.IEventSeatService;
using IEventService = BLL.IServices.IEventService;
using ILayoutService = BLL.IServices.ILayoutService;
using IPurchaseService = BLL.IServices.IPurchaseService;
using ISeatService = BLL.IServices.ISeatService;
using IVenueService = BLL.IServices.IVenueService;

namespace BLL.WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DbInitService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы DbInitService.svc или DbInitService.svc.cs в обозревателе решений и начните отладку.
    public class DbInitService : IDbInitService
    {
        public void Seed()
        {
            TicketManagementContext context = new TicketManagementContext();

            TicketManagementSeedData(context);
        }

        private void TicketManagementSeedData(TicketManagementContext context)
        {
            IVenueService vsvc = new ManagerServices.VenueService(new EntityVenueRepository(context));
            ILayoutService lsvc = new ManagerServices.LayoutService(new EntityLayoutRepository(context));
            IAreaService asvc = new ManagerServices.AreaService(new EntityAreaRepository(context));
            ISeatService ssvc = new ManagerServices.SeatService(new EntitySeatRepository(context));
            IEventService esvc = new ManagerServices.EventService(new EntityEventRepository(context));
            IEventSeatService essvc = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
            IEventAreaService easvc = new ManagerServices.EventAreaService(new EntityEventAreaRepository(context));
            IPurchaseService psvc = new ManagerServices.PurchaseService(new EntityPurchaseRepository(context));

            foreach (var x in essvc.GetAll())
            {
                context.Entry(x).State = EntityState.Deleted;
            }

            context.SaveChanges();

            foreach (var x in esvc.GetAll())
            {
                context.Entry(x).State = EntityState.Deleted;
            }

            context.SaveChanges();

            foreach (var x in ssvc.GetAll())
            {
                ssvc.Delete(x.Id, asvc, essvc, easvc);
            }

            foreach (var x in asvc.GetAll())
            {
                asvc.Delete(x.Id, essvc, easvc);
            }

            foreach (var x in lsvc.GetAll())
            {
                lsvc.Delete(x.Id, essvc, easvc);
            }

            foreach (var x in vsvc.GetAll())
            {
                vsvc.Delete(x.Id, essvc, easvc, lsvc);
            }

            foreach (var x in psvc.GetAll())
            {
                psvc.Delete(x.Id);
            }

            Venue v = new Venue()
            {
                Address = "Address000",
                Description = "Description000",
                Phone = "Phone000"
            };
            v.Id = vsvc.Save(v);

            Layout l = new Layout()
            {
                Description = "Description",
                VenueId = v.Id
            };
            l.Id = lsvc.Save(l, vsvc);

            Area a = new Area()
            {
                CoordX = 1,
                CoordY = 1,
                Description = "Description2",
                LayoutId = l.Id
            };
            a.Id = asvc.Save(a, lsvc, ssvc);

            Seat s = new Seat()
            {
                AreaId = a.Id,
                Number = 1,
                Row = 1
            };

            ssvc.Save(s, asvc);

            Event e = new Event()
            {
                Description = "Description3",
                EventDate = DateTime.Now,
                LayoutId = l.Id,
                Name = "Name"
            };



            Event e2 = new Event()
            {
                Description = "Description4",
                EventDate = DateTime.Now.AddDays(1),
                LayoutId = l.Id,
                Name = "Name2"
            };
            e.Id = context.AddEvent(e.Name, e.Description, e.LayoutId, e.EventDate);
            e2.Id = context.AddEvent(e2.Name, e2.Description, e2.LayoutId, e2.EventDate);

            foreach (var data in easvc.GetForEvent(e.Id))
            {
                data.Price = 100;
                easvc.Update(data);
            }

            foreach (var data in easvc.GetForEvent(e2.Id))
            {
                data.Price = 200;
                easvc.Update(data);
            }
        }
    }
}
