using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.Model;
using TicketManagement.WPF.Util;
using TicketManagement.WPF.WCF.LayoutService;
using TicketManagement.WPF.WCF.VenueService;

namespace TicketManagement.WPF.ViewModel
{
    class EditLayoutViewModel : INotifyPropertyChanged
    {
        private readonly LayoutServiceClient _service;
        private bool _edit = false;
        private LayoutModel _origin;

        private LocalizedErrorManager _errorManager = LocalizedErrorManager.GetInstance();

        private LayoutModel _model;
        public LayoutModel Model
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

        public EditLayoutViewModel(LayoutModel model, LayoutServiceClient service, bool edit = false)
        {
            _origin = new LayoutModel()
            {
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
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
