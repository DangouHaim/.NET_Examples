using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Rest;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.Model;
using TicketManagement.WPF.WebAPI;
using TicketManagement.WPF.WebAPI.Models;
using TicketManagement.WPF.WCF.VenueService;
using TicketManagement.WPF.WCF.LayoutService;
using TicketManagement.WPF.View;
using TicketManagement.WPF.WCF.AreaService;
using TicketManagement.WPF.WCF.SeatService;
using TicketManagement.WPF.Util;

//Swagger.json: https://localhost:44319/swagger/v1/swagger.json

namespace TicketManagement.WPF.ViewModel
{
    class ManagementViewModel : INotifyPropertyChanged, IDropTarget
    {
        private readonly WebAPIClient _userApi;
        private readonly VenueServiceClient _venueService;
        private readonly LayoutServiceClient _layoutService;
        private readonly AreaServiceClient _areaService;
        private readonly SeatServiceClient _seatService;

        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<TreeViewItemModel> VenueLayoutTreeSource { get; set; }
        public ObservableCollection<AreaModel> Areas { get; set; }

        private List<Area> _allAreas { get; set; }
        private List<Seat> _allSeats { get; set; }

        private LocalizedErrorManager _errorManager = LocalizedErrorManager.GetInstance();

        public ManagementViewModel()
        {
            _userApi = new WebAPIClient(new Uri("https://localhost:44319/"), new BasicAuthenticationCredentials());
            _venueService = new VenueServiceClient();
            _layoutService = new LayoutServiceClient();
            _areaService = new AreaServiceClient();
            _seatService = new SeatServiceClient();

            UsersInit();
            VenueLayoutTreeInit();
            AreaSeatListInit();
        }

        private void UsersInit()
        {
            Users = new ObservableCollection<UserModel>();
            foreach (var applicationUser in _userApi.ApiAccountAllGet())
            {
                if (applicationUser.UserName != LoginViewModel.CurrentPrincipal.Identity.Name)
                {
                    Users.Add(new UserModel()
                    {
                        Id = applicationUser.Id,
                        Email = applicationUser.Email,
                        Login = applicationUser.UserName,
                        Roles = new ObservableCollection<string>(_userApi.ApiAccountGetRolesGet(applicationUser.Id))
                    });
                }
            }
        }

        private void VenueLayoutTreeInit()
        {
            VenueLayoutTreeSource = new ObservableCollection<TreeViewItemModel>();

            var allVenues = _venueService.GetAll();
            if(allVenues.Any())
            {
                var allLayouts = _layoutService.GetAll();

                foreach (var v in allVenues)
                {
                    var r = from x in allLayouts where v.Id == x.VenueId select x;
                    if (r.Any())
                    {
                        VenueLayoutTreeSource.Add(new TreeViewItemModel(VenueModel.FromEntity(v),
                            LayoutModel.FromEntityList(r.ToList())));
                    }
                    else
                    {
                        VenueLayoutTreeSource.Add(new TreeViewItemModel(VenueModel.FromEntity(v)));
                    }
                }
            }
        }

        private void AreaSeatListInit()
        {
            Areas = new ObservableCollection<AreaModel>();

            _allAreas = _areaService.GetAll().ToList();

            if(_allAreas.Any())
            {
                _allSeats = _seatService.GetAll().ToList();
            }
        }

        private UniversalCommand _addRoleCommand;
        public UniversalCommand AddRoleCommand
        {
            get
            {
                return _addRoleCommand ??
                       (_addRoleCommand = new UniversalCommand(async x =>
                       {
                           if (x is ComboBoxItem i)
                           {
                               var role = i.Content.ToString();
                               var r = await _userApi.ApiAccountAddToRolePostAsync(CurrentUser.Id, role);
                               if (r.Value)
                               {
                                   CurrentUser.Roles.Add(role);
                               }
                               else
                               {
                                   CurrentError = _errorManager.RoleError;
                               }
                           }
                       },
                           x => CurrentUser != null));
            }
        }

        private UniversalCommand _dropRoleCommand;
        public UniversalCommand DropRoleCommand
        {
            get
            {
                return _dropRoleCommand ??
                       (_dropRoleCommand = new UniversalCommand(async x =>
                       {
                           if (x is string i)
                           {
                               var role = i;
                               var r = await _userApi.ApiAccountRemoveFronRoleDeleteAsync(CurrentUser.Id, role);
                               if (r.Value)
                               {
                                   CurrentUser.Roles.Remove(role);
                               }
                               else
                               {
                                   CurrentError = _errorManager.RoleError;
                               }
                           }
                       },
                           x => x != null && CurrentUser != null));
            }
        }

        private UniversalCommand _dropUserCommand;
        public UniversalCommand DropUserCommand
        {
            get
            {
                return _dropUserCommand ??
                       (_dropUserCommand = new UniversalCommand(async x =>
                           {
                               var r = await _userApi.ApiAccountRemoveUserDeleteAsync(CurrentUser.Id);
                               if (r.Value)
                               {
                                   Users.Remove(CurrentUser);
                               }
                               else
                               {
                                   CurrentError = _errorManager.ChangeUserError;
                               }
                           },
                           x => CurrentUser != null && !AddUser));
            }
        }

        private UniversalCommand _addUserCommand;
        public UniversalCommand AddUserCommand
        {
            get
            {
                return _addUserCommand ??
                       (_addUserCommand = new UniversalCommand(x =>
                       {
                           var u = new UserModel();
                           Users.Insert(0, u);
                           CurrentUser = u;
                           AddUser = true;
                       },
                           x => !AddUser));
            }
        }

        private UniversalCommand _commitUserCommand;
        public UniversalCommand CommitUserCommand
        {
            get
            {
                return _commitUserCommand ??
                       (_commitUserCommand = new UniversalCommand(async x =>
                           {
                               if (x is PasswordBox s)
                               {
                                   var u = new RegisterModel(
                                       CurrentUser.Login,
                                       CurrentUser.Email,
                                       s.Password,
                                       s.Password
                                   );
                                   var r = await _userApi.ApiAccountRegisterForIdResultPostAsync(u);
                                   if (!string.IsNullOrEmpty(r.UserId))
                                   {
                                       CurrentUser.Id = r.UserId;
                                       CurrentUser.Roles = new ObservableCollection<string>(await _userApi.ApiAccountGetRolesGetAsync(r.UserId));
                                       AddUser = false;
                                   }
                                   else
                                   {
                                       CurrentError = _errorManager.ChangeUserError;
                                   }
                               }
                           },
                           x => AddUser 
                                && CurrentUser != null
                                && !string.IsNullOrEmpty(CurrentUser.Login)
                                && !string.IsNullOrEmpty(CurrentUser.Email)
                                && x is PasswordBox s
                                && !string.IsNullOrEmpty(s.Password)
                                ));
            }
        }

        private UniversalCommand _cancelCommitUserCommand;
        public UniversalCommand CancelCommitUserCommand
        {
            get
            {
                return _cancelCommitUserCommand ??
                       (_cancelCommitUserCommand = new UniversalCommand(x =>
                           {
                               AddUser = false;
                               Users.Remove(CurrentUser);
                               CurrentUser = null;
                           },
                           x => AddUser && CurrentUser != null));
            }
        }

        private UniversalCommand _changeNameCommand;
        public UniversalCommand ChangeNameCommand
        {
            get
            {
                return _changeNameCommand ??
                       (_changeNameCommand = new UniversalCommand(async x =>
                           {
                               CurrentUser.LoginError = false;
                               CurrentError = "";
                               var r = await _userApi.ApiAccountChangeUserNamePutAsync(CurrentUser.Id, CurrentUser.Login);
                               if (!r.Value)
                               {
                                   CurrentUser.LoginError = true;
                                   CurrentError = _errorManager.ChangeUserError;
                               }
                           },
                           x => CurrentUser != null && !AddUser));
            }
        }

        private UniversalCommand _changeEmailCommand;
        public UniversalCommand ChangeEmailCommand
        {
            get
            {
                return _changeEmailCommand ??
                       (_changeEmailCommand = new UniversalCommand(async x =>
                           {
                               CurrentUser.EmailError = false;
                               CurrentError = "";
                               var r = await _userApi.ApiAccountChangeUserEmailPutAsync(CurrentUser.Id, CurrentUser.Email);
                               if (!r.Value)
                               {
                                   CurrentUser.EmailError = true;
                                   CurrentError = _errorManager.ChangeUserError;
                               }
                           },
                           x => CurrentUser != null && !AddUser));
            }
        }

        private UniversalCommand _selectionCommand;
        public UniversalCommand SelectionCommand
        {
            get
            {
                return _selectionCommand ??
                       (_selectionCommand = new UniversalCommand(x => { CurrentError = ""; }));
            }
        }

        private UniversalCommand _setDragableCommand;
        public UniversalCommand SetDragableCommand
        {
            get
            {
                return _setDragableCommand ??
                       (_setDragableCommand = new UniversalCommand(x =>
                       {
                           if (x is Button b)
                           {
                               CurrentDragable = b;
                           }
                       }));
            }
        }

        private UniversalCommand _treeViewEditCommand;
        public UniversalCommand TreeViewEditCommand
        {
            get
            {
                return _treeViewEditCommand ??
                       (_treeViewEditCommand = new UniversalCommand(x =>
                       {
                           if(CurrentTreeViewItem.IsVenue)
                           {
                               var dialog = new EditVenueView();
                               dialog.DataContext = new EditVenueViewModel(CurrentTreeViewItem.Venue, _venueService, true);
                               if(dialog.ShowDialog().Value)
                               {
                                   CurrentTreeViewItem.Name = CurrentTreeViewItem.Venue.Description;
                               }
                           }
                           else
                           {
                               var dialog = new EditLayoutView();
                               dialog.DataContext = new EditLayoutViewModel(CurrentTreeViewItem.Layout, _layoutService, true);
                               if(dialog.ShowDialog().Value)
                               {
                                   CurrentTreeViewItem.Name = CurrentTreeViewItem.Layout.Description;
                               }
                           }
                       }, x=> CurrentTreeViewItem != null));
            }
        }

        private UniversalCommand _treeViewRemoveCommand;
        public UniversalCommand TreeViewRemoveCommand
        {
            get
            {
                return _treeViewRemoveCommand ??
                       (_treeViewRemoveCommand = new UniversalCommand(x =>
                       {
                           for(int i = 0; i < VenueLayoutTreeSource.Count;) //remove all venues with each inner layouts
                           {
                               var v = VenueLayoutTreeSource[i];
                               if(v.IsChecked)
                               {
                                   if(v.IsVenue)
                                   {
                                       if(_venueService.Delete(v.Venue.Id))
                                       {
                                           VenueLayoutTreeSource.Remove(v);
                                       }
                                       else
                                       {
                                           i++;
                                       }
                                   }
                               }
                               else
                               {
                                   i++;
                               }
                           }

                           foreach(var node in VenueLayoutTreeSource) //remove left layouts
                           {
                               for (int i = 0; i < node.InnerNodes.Count;)
                               {
                                   var v = node.InnerNodes[i];
                                   if (v.IsChecked)
                                   {
                                       if (!v.IsVenue)
                                       {
                                           if(_layoutService.Delete(v.Layout.Id))
                                           {
                                               node.InnerNodes.Remove(v);
                                           }
                                           else
                                           {
                                               i++;
                                           }
                                       }
                                   }
                                   else
                                   {
                                       i++;
                                   }
                               }
                           }
                       }, x =>
                       {
                           bool res = false;
                           var r = from z in VenueLayoutTreeSource
                               where z.IsChecked
                               select z;
                           res = r.Any();
                           if(!res)
                           {
                               var z = from a in VenueLayoutTreeSource
                                       where !a.IsChecked
                                       select a;
                               if(z.Any())
                               {
                                   foreach(var v in z)
                                   {
                                       if(!v.IsEmpty)
                                       {
                                           foreach (var item in v.InnerNodes)
                                           {
                                               if (item.IsChecked)
                                               {
                                                   res = true;
                                                   break;
                                               }
                                           }
                                       }
                                       if(res)
                                       {
                                           break;
                                       }
                                   }
                               }
                           }
                           return res;
                       }));
            }
        }

        private UniversalCommand _areasRemoveCommand;
        public UniversalCommand AreasRemoveCommand
        {
            get
            {
                return _areasRemoveCommand ??
                       (_areasRemoveCommand = new UniversalCommand(x =>
                       {
                           var r = from z in Areas
                                   where z.IsChecked
                                   select z;
                           if(r.Any())
                           {
                               for(int i = 0; i < Areas.Count;)
                               {
                                   var v = Areas[i];
                                   if(v.IsChecked)
                                   {
                                       if(_areaService.Delete(v.Id))
                                       {
                                           Areas.Remove(v);
                                       }
                                       else
                                       {
                                           i++;
                                       }
                                   }
                                   else
                                   {
                                       i++;
                                   }
                               }
                           }
                           foreach(var a in Areas)
                           {
                               var res = from z in a.InnerItems
                                         where z.IsChecked
                                         select z;
                               if(res.Any())
                               {
                                   for (int i = 0; i < a.InnerItems.Count;)
                                   {
                                       var v = a.InnerItems[i];
                                       if (v.IsChecked)
                                       {
                                           if (_seatService.Delete(v.Id))
                                           {
                                               a.InnerItems.Remove(v);
                                           }
                                           else
                                           {
                                               i++;
                                           }
                                       }
                                       else
                                       {
                                           i++;
                                       }
                                   }
                               }
                           }
                       }, x =>
                       {
                           bool res = false;
                           var r = from z in Areas
                                   where z.IsChecked
                                   select z;
                           res = r.Any();
                           if (!res)
                           {
                               var z = from a in Areas
                                       where !a.IsChecked
                                       select a;
                               if (z.Any())
                               {
                                   foreach (var v in z)
                                   {
                                       if (!v.IsEmpty)
                                       {
                                           foreach (var item in v.InnerItems)
                                           {
                                               if (item.IsChecked)
                                               {
                                                   res = true;
                                                   break;
                                               }
                                           }
                                       }
                                       if (res)
                                       {
                                           break;
                                       }
                                   }
                               }
                           }
                           return res;
                       }));
            }
        }

        private UniversalCommand _editAreaCommand;
        public UniversalCommand EditAreaCommand
        {
            get
            {
                return _editAreaCommand ??
                       (_editAreaCommand = new UniversalCommand(x =>
                       {
                           var dialog = new EditAreaView();
                           dialog.DataContext = new EditAreaViewModel(CurrentArea, _areaService, true);
                           dialog.ShowDialog();
                       }, x => CurrentArea != null));
            }
        }

        private AreaModel _currentArea;
        public AreaModel CurrentArea
        {
            get => _currentArea;
            set
            {
                _currentArea = value;
                OnPropertyChanged();
            }
        }

        private SeatModel _currentSeat;
        public SeatModel CurrentSeat
        {
            get => _currentSeat;
            set
            {
                _currentSeat = value;
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

        private bool _addUser;
        public bool AddUser
        {
            get => _addUser;
            set
            {
                _addUser = value;
                EnabledUserList = !value;
                OnPropertyChanged();
            }
        }

        private bool _enabledUserList = true;
        public bool EnabledUserList
        {
            get => _enabledUserList;
            set
            {
                _enabledUserList = value;
                OnPropertyChanged();
            }
        }

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        private Button _currentDragable;
        public Button CurrentDragable
        {
            get => _currentDragable;
            set
            {
                if (_currentDragable == value)
                    return;
                _currentDragable = value;
                OnPropertyChanged();
            }
        }

        private TreeViewItemModel _currentTreeViewItem;
        public TreeViewItemModel CurrentTreeViewItem
        {
            get => _currentTreeViewItem;
            set
            {
                Areas.Clear();

                if (_currentTreeViewItem == value)
                    return;
                _currentTreeViewItem = value;

                if (_currentTreeViewItem == null
                    || _currentTreeViewItem.IsVenue)
                    return;

                _allAreas = _areaService.GetAll().ToList();

                var areas = from x in _allAreas
                            where x.LayoutId == _currentTreeViewItem.Layout.Id
                            select x;
                if (areas.Any())
                {
                    _allSeats = _seatService.GetAll().ToList();
                    foreach (var v in areas)
                    {
                        var r = from x in _allSeats where x.AreaId == v.Id select x;
                        var item = AreaModel.FromEntity(v);
                        if (r.Any())
                        {
                            item.InnerItems = new ObservableCollection<SeatModel>();
                            foreach (var x in r)
                            {
                                item.InnerItems.Add(SeatModel.FromEntity(x, _seatService));
                            }
                        }
                        Areas.Add(item);
                    }
                }

                OnPropertyChanged();
            }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Copy;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (CurrentDragable != null)
            {
                //Venue to the tree
                if (CurrentDragable.Name == "Venue")
                {
                    var v = new VenueModel()
                    {
                        Description = "Venue"
                    };

                    var dialog = new EditVenueView();
                    dialog.DataContext = new EditVenueViewModel(v, _venueService);
                    if (dialog.ShowDialog().Value)
                    {
                        var current = new TreeViewItemModel(v);
                        VenueLayoutTreeSource.Add(current);
                        CurrentTreeViewItem = current;
                    }
                }

                //Layout to the venue in tree
                if (dropInfo.TargetItem is TreeViewItemModel m && m.IsVenue)
                {
                    if (CurrentDragable.Name == "Layout")
                    {
                        LayoutModel l = new LayoutModel()
                        {
                            Description = "Layout",
                            VenueId = m.Venue.Id
                        };

                        var dialog = new EditLayoutView();
                        dialog.DataContext = new EditLayoutViewModel(l, _layoutService);
                        if (dialog.ShowDialog().Value)
                        {
                            if (m.IsEmpty)
                            {
                                m.InnerNodes = new ObservableCollection<TreeViewItemModel>();
                            }
                            var current = new TreeViewItemModel(l);
                            m.InnerNodes.Add(current);
                            CurrentTreeViewItem = current;
                        }
                    }
                }

                //Area to the layout in tree
                if (dropInfo.TargetItem is TreeViewItemModel layout && !layout.IsVenue)
                {
                    if (CurrentDragable.Name == "Area")
                    {
                        AreaModel a = new AreaModel()
                        {
                            Description = "Area",
                            LayoutId = layout.Layout.Id
                        };

                        var dialog = new EditAreaView();
                        dialog.DataContext = new EditAreaViewModel(a, _areaService);
                        if (dialog.ShowDialog().Value)
                        {
                            Areas.Add(a);
                        }
                    }
                }

                //Seat to the area in area list
                if (dropInfo.TargetItem is AreaModel area)
                {
                    if (CurrentDragable.Name == "Seat")
                    {
                        SeatModel s = new SeatModel(_seatService)
                        {
                            AreaId = area.Id
                        };

                        var dialog = new EditSeatView();
                        dialog.DataContext = new EditSeatViewModel(s, _seatService);
                        if (dialog.ShowDialog().Value)
                        {
                            area.InnerItems.Add(s);
                        }
                    }
                }
                else
                {
                    //Area to the area list
                    if (!(dropInfo.TargetItem is SeatModel)
                        && dropInfo.VisualTarget is ListBox
                        && CurrentTreeViewItem != null //need to select layout for insert
                        && !CurrentTreeViewItem.IsVenue)
                    {
                        if (CurrentDragable.Name == "Area")
                        {
                            AreaModel a = new AreaModel()
                            {
                                Description = "Area",
                                LayoutId = CurrentTreeViewItem.Layout.Id
                            };

                            var dialog = new EditAreaView();
                            dialog.DataContext = new EditAreaViewModel(a, _areaService);
                            if (dialog.ShowDialog().Value)
                            {
                                Areas.Add(a);
                            }
                        }
                    }
                }
            }
            else
            {
                //Area from area list into the tree
                if (dropInfo.Data is AreaModel area
                    && dropInfo.TargetItem is TreeViewItemModel treeViewItem
                    && !treeViewItem.IsVenue)
                {
                    area.IsWrong = false;
                    int id = area.LayoutId;
                    if (treeViewItem.Layout.Id != area.LayoutId)
                    {
                        area.LayoutId = treeViewItem.Layout.Id;
                        if (_areaService.Update(area.ToEntity()))
                        {
                            var v = from x in _allAreas where x.Id == area.Id select x;
                            if(v.Any())
                            {
                                var x = v.First();
                                x.LayoutId = area.LayoutId;
                            }
                            Areas.Remove(area);
                        }
                        else
                        {
                            area.IsWrong = true;
                            area.LayoutId = id;
                        }
                    }
                }
            }

            CurrentDragable = null;
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
