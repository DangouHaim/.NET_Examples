using System.Collections.Generic;

namespace DataPresenter.Entity
{
    public class EventArea
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public decimal Price { get; set; }

        public static EventArea GetFromEntity(DAL.EventArea data)
        {
            return new EventArea()
            {
                CoordX = data.CoordX,
                CoordY = data.CoordY,
                Description = data.Description,
                EventId = data.EventId,
                Id = data.Id,
                LayoutId = data.LayoutId,
                Price = data.Price
            };
        }

        public static List<EventArea> GetFromEntityList(IEnumerable<DAL.EventArea> datas)
        {
            List<EventArea> list = new List<EventArea>();
            foreach (var data in datas)
            {
                list.Add(new EventArea()
                {
                    CoordX = data.CoordX,
                    CoordY = data.CoordY,
                    Description = data.Description,
                    EventId = data.EventId,
                    Id = data.Id,
                    LayoutId = data.LayoutId,
                    Price = data.Price
                });
            }
            return list;
        }

        public DAL.EventArea ToEntity()
        {
            return new DAL.EventArea()
            {
                CoordX = CoordX,
                CoordY = CoordY,
                Description = Description,
                EventId = EventId,
                Id = Id,
                LayoutId = LayoutId,
                Price = Price
            };
        }
    }
}
