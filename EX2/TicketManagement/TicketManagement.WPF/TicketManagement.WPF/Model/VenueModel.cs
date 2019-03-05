using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.WPF.WCF.VenueService;

namespace TicketManagement.WPF.Model
{
    class VenueModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

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

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public static VenueModel FromEntity(Venue venue)
        {
            return new VenueModel()
            {
                Address = venue.Address,
                Description = venue.Description,
                Id = venue.Id,
                Phone = venue.Phone
            };
        }

        public Venue ToEntity()
        {
            return new Venue()
            {
                Phone = Phone,
                Id = Id,
                Description = Description,
                Address = Address
            };
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
