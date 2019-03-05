using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.Model;
using TicketManagement.WPF.Util;
using TicketManagement.WPF.WCF.VenueService;

namespace TicketManagement.WPF.ViewModel
{
    class EditVenueViewModel : INotifyPropertyChanged
    {
        private readonly VenueServiceClient _venueService;
        private bool _edit = false;
        private VenueModel _origin;

        private LocalizedErrorManager _errorManager = LocalizedErrorManager.GetInstance();

        private VenueModel _model;
        public VenueModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        private string _currentError;
        public string CurrentError
        {
            get => _currentError;
            set
            {
                _currentError = value;
                OnPropertyChanged();
            }
        }

        public EditVenueViewModel(VenueModel model, VenueServiceClient service, bool edit = false)
        {
            _origin = new VenueModel()
            {
                Address = model.Address,
                Description = model.Description,
                Phone = model.Phone
            };
            Model = model;
            _venueService = service;
            _edit = edit;
        }

        private UniversalCommand _commitCommand;
        public UniversalCommand CommitCommand
        {
            get
            {
                return _commitCommand ??
                       (_commitCommand = new UniversalCommand(x =>
                       {
                           if(!_edit)
                           {
                               int id = _venueService.Save(Model.ToEntity());
                               if (id > 0)
                               {
                                   Model.Id = id;
                                   DialogResult = true;
                               }
                           }
                           else
                           {
                               if (_venueService.Update(Model.ToEntity()))
                               {
                                   DialogResult = true;
                               }
                               else
                               {
                                   ResetModel();
                               }
                           }
                           CurrentError = _errorManager.InsertError;
                       },
                           x => !string.IsNullOrEmpty(Model.Description)
                                && !string.IsNullOrEmpty(Model.Address)
                                && !string.IsNullOrEmpty(Model.Phone)));
            }
        }

        private UniversalCommand _cancelCommand;
        public UniversalCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = new UniversalCommand(x =>
                       {
                           ResetModel();
                           DialogResult = false;
                       }));
            }
        }

        private void ResetModel()
        {
            Model.Description = _origin.Description;
            Model.Phone = _origin.Phone;
            Model.Address = _origin.Address;
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
