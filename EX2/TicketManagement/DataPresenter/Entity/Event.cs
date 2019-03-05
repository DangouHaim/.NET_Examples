using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataPresenter.Entity
{
    public class Event
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public int LayoutId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event date")]
        public System.DateTime EventDate { get; set; }

        public static Event GetFromEntity(DAL.Event data)
        {
            return new Event()
            {
                Description = data.Description,
                EventDate = data.EventDate,
                Id = data.Id,
                LayoutId = data.LayoutId,
                Name = data.Name
            };
        }

        public static List<Event> GetFromEntityList(IEnumerable<DAL.Event> datas)
        {
            List<Event> list = new List<Event>();
            foreach (var data in datas)
            {
                list.Add(new Event()
                {
                    Description = data.Description,
                    EventDate = data.EventDate,
                    Id = data.Id,
                    LayoutId = data.LayoutId,
                    Name = data.Name
                });
            }
            return list;
        }

        public DAL.Event ToEntity()
        {
            return new DAL.Event()
            {
                Description = Description,
                Name = Name,
                LayoutId = LayoutId,
                EventDate = EventDate,
                Id = Id
            };
        }
    }
}
