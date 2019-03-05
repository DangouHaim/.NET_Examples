using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TicketManagement.WPF.WCF.AreaService;
using TicketManagement.WPF.WCF.SeatService;

namespace TicketManagement.WPF.Model
{
    class AreaModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int LayoutId { get; set; }

        private readonly SeatServiceClient _service;

        public AreaModel()
        {
            _service = new SeatServiceClient();
            InnerItems = new ObservableCollection<SeatModel>();
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

        private int _coordX;
        public int CoordX
        {
            get => _coordX;
            set
            {
                _coordX = value;
                OnPropertyChanged();
            }
        }

        private int _coordY;
        public int CoordY
        {
            get => _coordY;
            set
            {
                _coordY = value;
                OnPropertyChanged();
            }
        }

        private bool _isWrong;
        public bool IsWrong
        {
            get => _isWrong;
            set
            {
                _isWrong = value;
                OnPropertyChanged();
                OnPropertyChanged("Color");
            }
        }

        private Brush _color = Brushes.Transparent;
        public Brush Color
        {
            get
            {
                if (_isWrong)
                {
                    return Brushes.Red;
                }
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SeatModel> _innerItems;
        public ObservableCollection<SeatModel> InnerItems {
            get => _innerItems;
            set
            {
                _innerItems = value;
                if(_innerItems != null)
                {
                    //Changing reference to this area id for each added seats, current area cen contain only seats
                    //with area id which equals id of current area
                    InnerItems.CollectionChanged += (s, e) => 
                    {
                        if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                        {
                            if (e.NewItems != null)
                            {
                                foreach (var item in e.NewItems)
                                {
                                    if(item is SeatModel v)
                                    {
                                        v.IsWrong = false;
                                        if (v.AreaId != Id)
                                        {
                                            int oldId = v.AreaId;
                                            v.AreaId = Id;
                                            if (!_service.Update(v.ToEntity()))
                                            {
                                                v.IsWrong = true;
                                                v.AreaId = oldId;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    };
                }
            }
        }

        public bool IsEmpty => InnerItems.Count == 0;

        public static AreaModel FromEntity(Area entity)
        {
            return new AreaModel()
            {
                CoordX = entity.CoordX,
                CoordY = entity.CoordY,
                Description = entity.Description,
                Id = entity.Id,
                LayoutId = entity.LayoutId
            };
        }

        public static IEnumerable<AreaModel> FromEntityList(IEnumerable<Area> entities)
        {
            var res = new List<AreaModel>();
            foreach(var entity in entities)
            {
                res.Add(new AreaModel()
                {
                    CoordX = entity.CoordX,
                    CoordY = entity.CoordY,
                    Description = entity.Description,
                    Id = entity.Id,
                    LayoutId = entity.LayoutId
                });
            }
            return res;
        }

        public Area ToEntity()
        {
            return new Area()
            {
                CoordX = CoordX,
                CoordY = CoordY,
                Description = Description,
                Id = Id,
                LayoutId = LayoutId
            };
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
