using System.Collections.Generic;

namespace DataPresenter.Entity
{
    public class GetUserCart_Result3
    {
        public int EventSeatId { get; set; }
        public int Number { get; set; }
        public int Row { get; set; }
        public string AreaDescription { get; set; }
        public decimal Price { get; set; }
        public string LayoutDescription { get; set; }
        public string VenueDescription { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


        public static GetUserCart_Result3 FromEntity(DAL.DataEntity.GetUserCart_Result3 data)
        {
            return new GetUserCart_Result3()
            {
                Address = data.Address,
                AreaDescription = data.AreaDescription,
                EventSeatId = data.EventSeatId,
                LayoutDescription = data.LayoutDescription,
                Number = data.Number,
                Phone = data.Phone,
                Price = data.Price,
                Row = data.Row,
                VenueDescription = data.VenueDescription
            };
        }

        public static List<GetUserCart_Result3> GetFromEntityList(IEnumerable<DAL.DataEntity.GetUserCart_Result3> datas)
        {
            List<GetUserCart_Result3> list = new List<GetUserCart_Result3>();
            foreach (var data in datas)
            {
                list.Add(new GetUserCart_Result3()
                {
                    Address = data.Address,
                    AreaDescription = data.AreaDescription,
                    EventSeatId = data.EventSeatId,
                    LayoutDescription = data.LayoutDescription,
                    Number = data.Number,
                    Phone = data.Phone,
                    Price = data.Price,
                    Row = data.Row,
                    VenueDescription = data.VenueDescription
                });
            }
            return list;
        }

        public DAL.DataEntity.GetUserCart_Result3 ConvertToEntity()
        {
            return new DAL.DataEntity.GetUserCart_Result3()
            {
                Address = Address,
                AreaDescription = AreaDescription,
                EventSeatId = EventSeatId,
                LayoutDescription = LayoutDescription,
                Number = Number,
                Phone = Phone,
                Price = Price,
                Row = Row,
                VenueDescription = VenueDescription
            };
        }
    }
}
