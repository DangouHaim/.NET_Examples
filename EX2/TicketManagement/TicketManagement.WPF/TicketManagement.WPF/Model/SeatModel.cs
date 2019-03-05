using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.WPF.WCF.SeatService;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.View;
using TicketManagement.WPF.ViewModel;

namespace TicketManagement.WPF.Model
{
    class SeatModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AreaId { get; set; }

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

        private int _row;
        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                OnPropertyChanged();
            }
        }

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
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

        private UniversalCommand _editSeatCommand;
        public UniversalCommand EditSeatCommand
        {
            get
            {
                return _editSeatCommand ??
                       (_editSeatCommand = new UniversalCommand(x =>
                       {
                           var dialog = new EditSeatView();
                           dialog.DataContext = new EditSeatViewModel(this, _seatService, true);
                           dialog.ShowDialog();
                           OnPropertyChanged("RowNumber");
                       }));
            }
        }

        public string RowNumber => Row + " : " + Number;
        private SeatServiceClient _seatService;

        public SeatModel(SeatServiceClient service)
        {
            _seatService = service;
        }

        public static SeatModel FromEntity(Seat entity, SeatServiceClient service)
        {
            return new SeatModel(service)
            {
                AreaId = entity.AreaId,
                Id = entity.Id,
                Number = entity.Number,
                Row = entity.Row
            };
        }

        public static IEnumerable<SeatModel> FromEntityList(IEnumerable<SeatModel> entities, SeatServiceClient service)
        {
            List<SeatModel> res = new List<SeatModel>();
            foreach(var entity in entities)
            {
                res.Add(new SeatModel(service)
                {
                    AreaId = entity.AreaId,
                    Id = entity.Id,
                    Number = entity.Number,
                    Row = entity.Row
                });
            }
            return res;
        }

        public Seat ToEntity()
        {
            return new Seat()
            {
                AreaId = AreaId,
                Id = Id,
                Number = Number,
                Row = Row
            };
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
