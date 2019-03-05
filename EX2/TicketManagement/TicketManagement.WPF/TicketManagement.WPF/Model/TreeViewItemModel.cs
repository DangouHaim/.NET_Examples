using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.WPF.Model
{
    class TreeViewItemModel : INotifyPropertyChanged
    {
        public TreeViewItemModel(VenueModel venue)
        {
            Name = venue.Description;
            Venue = venue;
        }

        public TreeViewItemModel(VenueModel venue, IEnumerable<LayoutModel> layouts)
        {
            Name = venue.Description;
            Venue = venue;
            InnerNodes = new ObservableCollection<TreeViewItemModel>();
            foreach(var v in layouts)
            {
                InnerNodes.Add(new TreeViewItemModel(v));
            }
        }

        public TreeViewItemModel(LayoutModel layout)
        {
            Name = layout.Description;
            Layout = layout;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private bool _isVenue = true;
        public bool IsVenue
        {
            get => _isVenue;
            set
            {
                _isVenue = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        private VenueModel _venue;
        public VenueModel Venue
        {
            get => _venue;
            set
            {
                _venue = value;
                OnPropertyChanged();
            }
        }

        private LayoutModel _layout;
        public LayoutModel Layout
        {
            get => _layout;
            set
            {
                IsVenue = false;
                _layout = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty { get => _innerNodes == null; }

        private ObservableCollection<TreeViewItemModel> _innerNodes;
        public ObservableCollection<TreeViewItemModel> InnerNodes
        {
            get => _innerNodes;
            set
            {
                _innerNodes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
