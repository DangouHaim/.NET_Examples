using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.Model;
using TicketManagement.WPF.Util;
using TicketManagement.WPF.WCF.AreaService;
using TicketManagement.WPF.WCF.LayoutService;
using TicketManagement.WPF.WCF.VenueService;

namespace TicketManagement.WPF.ViewModel
{
    class EditAreaViewModel : INotifyPropertyChanged
    {
        private readonly AreaServiceClient _service;
        private bool _edit = false;
        private AreaModel _origin;

        private LocalizedErrorManager _errorManager = LocalizedErrorManager.GetInstance();

        private AreaModel _model;
        public AreaModel Model
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

        public EditAreaViewModel(AreaModel model, AreaServiceClient service, bool edit = false)
        {
            _origin = new AreaModel()
            {
                CoordX = model.CoordX,
                CoordY = model.CoordY,
                Description = model.Description
            };
            Model = model;
            _service = service;
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
                           string err = "";
                           try
                           {
                               if (!_edit)
                               {
                                   int id = _service.Save(Model.ToEntity());
                                   if (id > 0)
                                   {
                                       Model.Id = id;
                                       DialogResult = true;
                                   }
                               }
                               else
                               {
                                   if (_service.Update(Model.ToEntity()))
                                   {
                                       DialogResult = true;
                                   }
                                   else
                                   {
                                       ResetModel();
                                   }
                               }
                           }
                           catch(FaultException<Exception> ex)
                           {
                               err = ex.Message;
                           }
                           CurrentError = _errorManager.InsertError + " " + err;
                       },
                           x => !string.IsNullOrEmpty(Model.Description)));
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
            Model.CoordX = _origin.CoordX;
            Model.CoordY = _origin.CoordY;
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
