using System.Collections.Generic;

namespace DataPresenter.Entity
{
    public class EventSeat
    {
        public int Id { get; set; }
        public int EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }

        public static EventSeat FromEntity(DAL.EventSeat data)
        {
            return new EventSeat()
            {
                EventAreaId = data.EventAreaId,
                Id = data.Id,
                Number = data.Number,
                Row = data.Row,
                State = data.State
            };
        }

        public static List<EventSeat> FromEntityList(IEnumerable<DAL.EventSeat> datas)
        {
            List<EventSeat> list = new List<EventSeat>();
            foreach(var data in datas)
            {
                list.Add(new EventSeat()
                {
                    EventAreaId = data.EventAreaId,
                    Id = data.Id,
                    Number = data.Number,
                    Row = data.Row,
                    State = data.State
                });
            }
            return list;
        }

        public DAL.EventSeat ToEntity()
        {
            return new DAL.EventSeat()
            {
                EventAreaId = EventAreaId,
                Id = Id,
                Number = Number,
                Row = Row,
                State = State
            };
        }
    }
}
