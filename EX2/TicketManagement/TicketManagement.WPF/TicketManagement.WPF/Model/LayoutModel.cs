using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.WPF.WCF.LayoutService;

namespace TicketManagement.WPF.Model
{
    class LayoutModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int VenueId { get; set; }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public static IEnumerable<LayoutModel> FromEntityList(IEnumerable<Layout> list)
        {
            List<LayoutModel> result = new List<LayoutModel>();

            foreach(var v in list)
            {
                result.Add(new LayoutModel()
                {
                    Description = v.Description,
                    Id = v.Id,
                    VenueId = v.VenueId
                });
            }

            return result;
        }

        public static LayoutModel FromEntity(Layout layout)
        {
            return new LayoutModel()
            {
                Description = layout.Description,
                Id = layout.Id,
                VenueId = layout.VenueId
            };
        }

        public Layout ToEntity()
        {
            return new Layout()
            {
                Id = Id,
                Description = Description,
                VenueId = VenueId
            };
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
