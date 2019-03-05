using System.Collections.Generic;

namespace DataPresenter.Entity
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public System.DateTime EventDate { get; set; }
        public string AreaDescription { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
        public System.DateTime Date { get; set; }

        public static Purchase GetFromEntity(DAL.Purchase data)
        {
            return new Purchase()
            {
                AreaDescription = data.AreaDescription,
                Date = data.Date,
                EventDate = data.EventDate,
                EventDescription = data.EventDescription,
                EventName = data.EventName,
                Id = data.Id,
                SeatNumber = data.SeatNumber,
                SeatRow = data.SeatRow,
                UserId = data.UserId
            };
        }

        public static List<Purchase> GetFromEntityList(IEnumerable<DAL.Purchase> datas)
        {
            List<Purchase> list = new List<Purchase>();
            foreach (var data in datas)
            {
                list.Add(new Purchase()
                {
                    AreaDescription = data.AreaDescription,
                    Date = data.Date,
                    EventDate = data.EventDate,
                    EventDescription = data.EventDescription,
                    EventName = data.EventName,
                    Id = data.Id,
                    SeatNumber = data.SeatNumber,
                    SeatRow = data.SeatRow,
                    UserId = data.UserId
                });
            }
            return list;
        }

        public DAL.Purchase ConvertToEntity()
        {
            return new DAL.Purchase()
            {
                AreaDescription = AreaDescription,
                Date = Date,
                EventDate = EventDate,
                EventDescription = EventDescription,
                EventName = EventName,
                Id = Id,
                SeatNumber = SeatNumber,
                SeatRow = SeatRow,
                UserId = UserId
            };
        }
    }
}
