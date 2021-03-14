using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CallLogGIISDMDK.Models;
using CallLogGIISDMDK.WorkWithFiles;
namespace CallLogGIISDMDK.ViewModels
{
    class ViewAppeals_VM : INotifyPropertyChanged
    {
        DefinerAvailabilityAppealsByDate definerAvailabilityAppealsByDate = new DefinerAvailabilityAppealsByDate();
        public ObservableCollection<FillAppeal_VM> Appeals { get; set; }
        public ObservableCollection<FillAppeal_VM> DataAppealByID { get; set; }
        Data data = new Data();
        FileReader fileReader = new FileReader();
        FileWriter fileWriter = new FileWriter();
        private RelayCommand _updatecCallLogCommand;
        private RelayCommand _addDataToCurrentAppealCommmand;
        private RelayCommand _loadedPageCommand;
        private RelayCommand _loadedViewPageCommand;
        private ICommand _setTypeConnectCommand;
        private ICommand _setEmailPhoneCommand;
        private ICommand _addTypeAppealCommand;
        private ICommand _setStatusAppealCommand;
        private ICommand _setMonthCommand;
        private FillAppeal_VM _selectedAppeal;
        private FillAppeal_VM _currentAppeal;
        FillAppeal_model _fillAppeal_Model;
        List<List<string>> suitableAppeals;
        List<string> _singleAppeal = new List<string>();
        //List<List<string>> _allDataSingleAppeal = new List<List<string>>();
        List<List<string>> _appeals = new List<List<string>>();
        private List<string> _minuteAppeal;
        private List<string> _hourAppeal;
        private List<string> _statuses;
        private List<string> _types;
        private List<string> _communicationСhannels;
        private List<string> _routes;
        private List<string> _daysCurrentMonthStringFormat = new List<string>();
        private string _insertAppeal = String.Empty;
        private string _pathToZipAppeals = @"Appeals.zip";
        private string _insertIDAppeal = String.Empty;
        private int _currentHour = DateTime.Now.Hour;
        private string _currentDay = DateTime.Now.ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU"));
        private string _currentMinute;
        private string _currentTime;
        private string _phone;
        private string _appeal;
        private string _additionalInfo;
        private string _email;
        private string _status;
        private string _type = "";
        private string _route;
        private string _month;
        private string _year;
        private string _communicationСhannel;
        private string _ID;
        private string _personalID;
        private int _positionPromptPhoneNumber = 2;
        private int _positionCheckPhone = 1;
        private int _positionPhoneForm = 1;
        private string _promptsPhone = "";
        private string _promptsEmail = "";
        private string _promptsAppeal = "";
        private string _promptsStatus = "";
        private string _promptsType = "";
        private string _promptsTime = "";
        private string _promptsCommunicationChannel = "";
        private bool _isValidAddingDataToAppeal;
        private bool _isIrregular;
        private bool _isEnablePhoneForm = true;
        private bool _isEnableIrregularCheck = true;
        private bool _isEnableEmailCheck = true;
        // private bool _isEmail;
        private bool _isSelectedAppeal;
        private Visibility _isVisibleNumber7;
        string[] _prompts = new string[7];
        public ViewAppeals_VM()
        {
            Appeals = new ObservableCollection<FillAppeal_VM>();
            DataAppealByID = new ObservableCollection<FillAppeal_VM>();
            _fillAppeal_Model = new FillAppeal_model();
            fileReader.onReadingComplete += FileReader_onWrite;
            _fillAppeal_Model = new FillAppeal_model();
            SetDefaultFields();
            FillPrompts();
            _communicationСhannels = _fillAppeal_Model.GetTypesAppeal();
            _routes = _fillAppeal_Model.GetRoutes();

            _year = DateTime.Now.Year.ToString();
            _month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"));

        }
        public FillAppeal_VM SelectedAppeal
        {
            get { return _selectedAppeal; }
            set
            {
                _selectedAppeal = value;
                OnPropertyChanged("SelectedAppeal");
                _currentAppeal = _selectedAppeal;
                DataAppealByID.Clear();
                if (_selectedAppeal != null)
                {
                    IsSelectedAppeal = true;
                    StaticData.CurrentId = Convert.ToInt32(_selectedAppeal.ID);
                    ShowDataAppealByID(_selectedAppeal.ID);
                    ID = _selectedAppeal.ID;
                    // SetPhoneEmail();
                }
            }
        }
        public RelayCommand AddDataToCurrentAppealCommmand
        {
            get
            {
                if (_addDataToCurrentAppealCommmand == null)
                {
                    _addDataToCurrentAppealCommmand = new RelayCommand(
                        p => this.AddDataToCurrentAppeal()); ;
                }
                return _addDataToCurrentAppealCommmand;
            }
        }
        public RelayCommand LoadedPageCommand
        {
            get
            {
                if (_loadedPageCommand == null)
                {
                    _loadedPageCommand = new RelayCommand(
                        p => this.LoadedPage()); ;
                }
                return _loadedPageCommand;
            }
        }
        public RelayCommand LoadedViewPageCommand
        {
            get
            {
                if (_loadedViewPageCommand == null)
                {
                    _loadedViewPageCommand = new RelayCommand(
                        p => this.LoadedViewPage()); ;
                }
                return _loadedViewPageCommand;
            }
        }
        public ICommand AddTypeAppealCommand
        {
            get
            {
                return _addTypeAppealCommand ?? (_addTypeAppealCommand = new RelayCommand(AddTypeAppeal));
            }
        }
        public ICommand SetStatusAppealCommand
        {
            get
            {
                return _setStatusAppealCommand ?? (_setStatusAppealCommand = new RelayCommand(SetStatusAppeal));
            }
        }
        public ICommand SetMonthCommand
        {
            get
            {
                return _setMonthCommand ?? (_setMonthCommand = new RelayCommand(SetMonth));
            }
        }
        private void SetMonth(object route)
        {
            var num = DateTime.Parse($"{_month} 1, 2000");
            var numberMonth = num.Month;
            string[] date = new string[2];
            date = definerAvailabilityAppealsByDate.GetCorrectDate(route, _month, _year);
            ChangeRepresentationMonth(date[0]);
            Year = date[1];
            if (numberMonth.ToString() != date[0])
            {
                FillAppeals();
                numberMonth = Convert.ToInt32(date[0]);
            }
               
        }
        void ChangeRepresentationMonth(string month)
        {
            DateTime date = new DateTime(2000, Convert.ToInt32(month), 1);
            Month = date.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"));
        }
        private void SetStatusAppeal(object status)
        {
            string typeAppeal = status.ToString();
            if (status.ToString() == "inWork") Status = "Открыто";
            else Status = "Закрыто";
            ValidateAdding(null);
            PromptsStatus = _fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
        }
        private void AddTypeAppeal(object type)
        {
            string typeAppeal = type.ToString();
            _type = _fillAppeal_Model.AddTypeAppeal(_type, typeAppeal);
            ValidateAdding(null);
            PromptsType = _fillAppeal_Model.ClearPrompt(Type, PromptsType);
        }
        private void LoadedViewPage()
        {
            FillAppeals();
        }
        public RelayCommand UpdatecCallLogCommand
        {
            get
            {
                if (_updatecCallLogCommand == null)
                {
                    _updatecCallLogCommand = new RelayCommand(
                        p => this.FillAppeals());
                }
                return _updatecCallLogCommand;
            }
        }
        public ICommand SetTypeConnectCommand
        {
            get
            {
                return _setTypeConnectCommand ?? (_setTypeConnectCommand = new RelayCommand(SetTypeConnect));
            }
        }
        public ICommand SetEmailPhoneCommand
        {
            get
            {
                return _setEmailPhoneCommand ?? (_setEmailPhoneCommand = new RelayCommand(SetEmailPhone));
            }
        }
        private void SetEmailPhone(object model)
        {
            if (model != null)
            {
                if (((FillAppeal_VM)model).InputPhone != null)
                    Phone = ((FillAppeal_VM)model).InputPhone.ToString();
                if (((FillAppeal_VM)model).Email != null)
                    Email = ((FillAppeal_VM)model).Email.ToString();
            }
            StaticData.Phone = Phone;
            StaticData.Email = Email;
        }
        public void SetTypeConnect(object typeConnect)
        {
            string[] data = new string[2];
            // StaticData.DataAppealByPersonalID = data.GetAppealByPersonalID(personalID);
            //  string typeConnection = "";
            data = _fillAppeal_Model.GetTypeConnect(typeConnect);
            Route = data[0];
            CommunicationСhannel = data[1];
        }
        private void AddDataToCurrentAppeal()
        {
            _prompts = _fillAppeal_Model.CheckPrompts(Phone, Email, Status, CommunicationСhannel, Appeal, Type, CurrentMinute, IsIrregular);
            IsValidAddingDataToAppeal = _fillAppeal_Model.isValidationAddingDataToAppeal;
            if (!IsValidAddingDataToAppeal) ShowPrompts();
            if (IsValidAddingDataToAppeal)
            {
                Thread myThread = new Thread(new ThreadStart(Adding));
                myThread.Start();
            }
        }
        private void Adding()
        {
            DateTime dateForConverted = DateTime.Parse(CurrentDay);
            string convertedDate = dateForConverted.ToString("dd/MM/yyyy");
            CurrentTime = (DateTime.Parse($"{CurrentHour.ToString()}:{CurrentMinute}")).ToString("HH:mm");
           fileWriter.WriteAppealToFile(_currentAppeal.FullName, _currentAppeal.Company, _currentAppeal.Sity, Phone, _currentAppeal.Inn, _currentAppeal.ParticipantRole, Type, Status, Email, _currentAppeal.Ogrn, convertedDate, Appeal, AdditionalInfo, CommunicationСhannel, CurrentTime, Route);
      
        }
        private void LoadedPage()
        {
            SetDefaultFields();
        }
        void SetDefaultFields()
        {
            //Phone = "";

            Appeal = "";
            AdditionalInfo = "";
            CurrentMinute = "";
            Type = "";
            Route = "";
            CommunicationСhannel = "";

            _hourAppeal = new List<string>();
            _minuteAppeal = new List<string>();
            _daysCurrentMonthStringFormat = new List<string>();
            _statuses = new List<string>();
            _types = new List<string>();
            _routes = new List<string>();
            _communicationСhannels = new List<string>();

            _hourAppeal = _fillAppeal_Model.GetHoursAppeal();
            _minuteAppeal = _fillAppeal_Model.GetMinutesAppeal();
            _daysCurrentMonthStringFormat = _fillAppeal_Model.FillDaysCurrentMonth();
            _statuses = _fillAppeal_Model.GetStatusesAppeal();
            _types = _fillAppeal_Model.GetTypesAppeal();
            _communicationСhannels = _fillAppeal_Model.GetTypesAppeal();
            _routes = _fillAppeal_Model.GetRoutes();

            
        }
        private void ShowDataAppealByID(string appealID)
        {
            foreach (var appeal in _appeals)
            {
                if (appeal[0] == appealID)
                    App.Current.Dispatcher.BeginInvoke((Action)delegate // <--- HERE
                    {
                        DataAppealByID.Add(new FillAppeal_VM { ID = appeal[0], CurrentDay = Convert.ToDateTime(appeal[1]).ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU")), CurrentTime = appeal[2], CommunicationChannel = appeal[3], Type = appeal[4], Appeal = appeal[5], UserName = appeal[6], Sity = appeal[7], ParticipantRole = appeal[8], Route = appeal[9], AdditionalInfo = appeal[10], Company = appeal[11], FullName = appeal[12], InputPhone = appeal[13], Email = appeal[14], Inn = appeal[15], Ogrn = appeal[16], Status = appeal[17], PersonalID = appeal[18] });
                    });
            }
        }
        void ValidateAdding(object p)
        {
            _prompts = _fillAppeal_Model.CheckPrompts(Phone, Email, Status, CommunicationСhannel, Appeal, Type, CurrentMinute, IsIrregular);
            IsValidAddingDataToAppeal = _fillAppeal_Model.isValidationAddingDataToAppeal;
            if (p != null)
                ShowPrompts();
        }
        void ShowAppeal(List<List<string>> appeals)
        {
            _fillAppeal_Model.appealsId.Clear();
            foreach (var appeal in appeals)
            {
                if (!_fillAppeal_Model.CheckSameAppeals(appeal))
                {
                    App.Current.Dispatcher.BeginInvoke((Action)delegate // <--- HERE
                    {
                        Appeals.Add(new FillAppeal_VM { ID = appeal[0], CurrentDay = Convert.ToDateTime(appeal[1]).ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU")), CurrentTime = appeal[2], CommunicationChannel = appeal[3], Type = appeal[4], Appeal = appeal[5], UserName = appeal[6], Sity = appeal[7], ParticipantRole = appeal[8], Route = appeal[9], AdditionalInfo = appeal[10], Company = appeal[11], FullName = appeal[12], InputPhone = appeal[13], Email = appeal[14], Inn = appeal[15], Ogrn = appeal[16], Status = appeal[17], PersonalID = appeal[18] });
                    });
                }
            }
        }
        void CheckPositionControls()
        {
            if (PromptsPhone != "")
            {
                PositionPromptPhoneNumber = 1;
                PositionCheckPhone = 2;
            }
            else
            {
                PositionPromptPhoneNumber = 2;
                PositionCheckPhone = 1;
            }
        }
        private void FillPrompts()
        {
            _prompts[0] = _promptsPhone;
            _prompts[1] = _promptsEmail;
            _prompts[2] = _promptsStatus;
            _prompts[3] = _communicationСhannel;
            _prompts[4] = _promptsAppeal;
            _prompts[5] = _type;
            _prompts[6] = _currentMinute;
        }
        private void ShowPrompts()
        {
            PromptsPhone = _prompts[0];
            PromptsEmail = _prompts[1];
            PromptsStatus = _prompts[2];
            PromptsCommunicationChannel = _prompts[3];
            PromptsAppeal = _prompts[4];
            PromptsType = _prompts[5];
            PromptsTime = _prompts[6];
        }
        private void SearchInsertAppeal(string insertAppeal, int typeSearch)
        {
            suitableAppeals = new List<List<string>>();
            Appeals.Clear();
            suitableAppeals = _fillAppeal_Model.SearchInsertAppeal(insertAppeal, _appeals, typeSearch);
            ShowAppeal(suitableAppeals);
        }
        private void FillAppeal()
        {
            _appeals = fileReader.GetAppeals();
            fileReader.ReadingComplete();
        }
        private void FileReader_onWrite()
        {
            ShowAppeal(_appeals);
        }
        private void FillAppeals()
        {
            Appeals.Clear();
            Thread myThread = new Thread(new ThreadStart(FillAppeal));
            myThread.Start();
        }
        //private void SettingPhoneForm()
        //{
        //    if (IsEmail)
        //    {
        //        Phone = "Обращение по телефону";
        //        IsVisibleNumber7 = Visibility.Hidden;
        //        PositionPhoneForm = 0;
        //        IsEnablePhoneForm = false;
        //        IsEnableIrregularCheck = false;
        //    }
        //    else
        //    {
        //        Phone = "";
        //        IsVisibleNumber7 = Visibility.Visible;
        //        PositionPhoneForm = 1;
        //        IsEnablePhoneForm = true;
        //        IsEnableIrregularCheck = true;
        //    }
        //}
        private void SettingPhoneFormIrregular()
        {
            if (IsIrregular)
            {
                IsVisibleNumber7 = Visibility.Hidden;
                PositionPhoneForm = 0;
                IsEnableEmailCheck = false;
            }
            else
            {
                IsVisibleNumber7 = Visibility.Visible;
                PositionPhoneForm = 1;
                IsEnableEmailCheck = true;
            }
        }
        //public List<string> Appeal
        //{
        //    get { return _appeal; }
        //    set
        //    {
        //        _appeal = value;
        //        OnPropertyChanged("Appeal");
        //    }
        //}
        //private void FillHistoryAppeal(FillAppeal_VM selectedAppeal)
        //{
        //    _allDataSingleAppeal = data.GetAppealsSortByID(selectedAppeal.ID, _appeals);
        //}
        public List<string> SingleAppeal
        {
            get { return _singleAppeal; }
            set
            {
                _singleAppeal = value;
                OnPropertyChanged("SingleAppeal");
            }
        }
        public List<string> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                OnPropertyChanged("Statuses");
            }
        }
        public List<string> MinuteAppeal
        {
            get { return _minuteAppeal; }
            set
            {
                _minuteAppeal = value;
                OnPropertyChanged("MinuteAppeal");
            }
        }
        public List<string> HourAppeal
        {
            get { return _hourAppeal; }
            set
            {
                _hourAppeal = value;
                OnPropertyChanged("HourAppeal");
            }
        }
        public List<string> DaysCurrentMonth
        {
            get { return _daysCurrentMonthStringFormat; }
            set
            {
                _daysCurrentMonthStringFormat = value;
                OnPropertyChanged("DaysCurrentMonth");
            }
        }
        public List<string> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                OnPropertyChanged("Types");
            }
        }

        public List<string> Routes
        {
            get { return _routes; }
            set
            {
                _routes = value;
                OnPropertyChanged("Routes");
            }
        }

        public List<string> CommunicationСhannels
        {
            get { return _communicationСhannels; }
            set
            {
                _communicationСhannels = value;
                OnPropertyChanged("CommunicationСhannels");
            }
        }

        public string PromptsAppeal
        {
            get { return _promptsAppeal; }
            set
            {
                _promptsAppeal = value;
                OnPropertyChanged("PromptsAppeal");
            }
        }
        public string PromptsEmail
        {
            get { return _promptsEmail; }
            set
            {
                _promptsEmail = value;
                OnPropertyChanged("PromptsEmail");
            }
        }
        public string PromptsPhone
        {
            get { return _promptsPhone; }
            set
            {
                _promptsAppeal = value;
                OnPropertyChanged("PromptsPhone");
                CheckPositionControls();
            }
        }
        public string PromptsStatus
        {
            get { return _promptsStatus; }
            set
            {
                _promptsStatus = value;
                OnPropertyChanged("PromptsStatus");
            }
        }
        public string PromptsType
        {
            get { return _promptsType; }
            set
            {
                _promptsType = value;
                OnPropertyChanged("PromptsType");
            }
        }
        public string PromptsTime
        {
            get { return _promptsTime; }
            set
            {
                _promptsTime = value;
                OnPropertyChanged("PromptsTime");
            }
        }
        public string PromptsCommunicationChannel
        {
            get { return _promptsCommunicationChannel; }
            set
            {
                _promptsCommunicationChannel = value;
                OnPropertyChanged("PromptsCommunicationChannel");
            }
        }
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
            }
        }
        public int CurrentHour
        {
            get { return _currentHour; }
            set
            {
                _currentHour = value;
                OnPropertyChanged("CurrentHour");
            }
        }
        public string CurrentDay
        {
            get { return _currentDay; }
            set
            {
                _currentDay = value;
                OnPropertyChanged("CurrentDay");
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
                //ValidateAdding(null);
                //PromptsStatus = _fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
                //CheckFirstStepFillAppeal(null);
                //PromptsType = fillAppeal_Model.ClearPrompt(Ogrn, PromptsType);
            }
        }
        public string Route
        {
            get { return _route; }
            set
            {
                _route = value;
                OnPropertyChanged("Route");
                //ValidateAdding(null);
                //PromptsStatus = _fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
            }
        }
        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged("Month");
                //ValidateAdding(null);
                //PromptsStatus = _fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
            }
        }
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
                //ValidateAdding(null);
                //PromptsStatus = _fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
            }
        }
        public string ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");
            }
        }
        public string PersonalID
        {
            get { return _personalID; }
            set
            {
                _personalID = value;
                OnPropertyChanged("PersonalID");
            }
        }
        public string Appeal
        {
            get { return _appeal; }
            set
            {
                _appeal = value;
                OnPropertyChanged("Appeal");
                ValidateAdding(null);
                PromptsAppeal = _fillAppeal_Model.ClearPrompt(Appeal, PromptsAppeal);
            }
        }
        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set
            {
                _additionalInfo = value;
                OnPropertyChanged("AdditionalInfo");
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
                if (_phone != null)
                {
                    if (_phone != "" && !IsIrregular)
                        _phone = _fillAppeal_Model.CheckPhone(Phone);
                    ValidateAdding(null);
                    PromptsPhone = _fillAppeal_Model.ClearPrompt(Phone, PromptsPhone);
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
                if (_email != null)
                {
                    ValidateAdding(null);
                    PromptsEmail = _fillAppeal_Model.ClearPrompt(Email, PromptsEmail);
                }
            }
        }
        public string CommunicationСhannel
        {
            get { return _communicationСhannel; }
            set
            {
                _communicationСhannel = value;
                OnPropertyChanged("CommunicationСhannel");
                ValidateAdding(null);
                PromptsType = _fillAppeal_Model.ClearPrompt(CommunicationСhannel, PromptsType);
            }
        }
        public string CurrentMinute
        {
            get { return _currentMinute; }
            set
            {
                _currentMinute = value;
                OnPropertyChanged("CurrentMinute");
            }
        }
        public string InsertAppeal
        {
            get { return _insertAppeal; }
            set
            {
                _insertAppeal = value;
                OnPropertyChanged("InsertAppeal");
                SearchInsertAppeal(InsertAppeal, 0);
            }
        }
        public string InsertIDAppeal
        {
            get { return _insertIDAppeal; }
            set
            {
                _insertIDAppeal = value;
                OnPropertyChanged("InsertAppeal");
                SearchInsertAppeal(InsertIDAppeal, 1);
            }
        }
        //public bool IsEmail
        //{
        //    get { return _isEmail; }
        //    set
        //    {
        //        _isEmail = value;
        //        OnPropertyChanged("IsEmail");
        //        SettingPhoneForm();
        //    }
        //}
        public bool IsSelectedAppeal
        {
            get { return _isSelectedAppeal; }
            set
            {
                _isSelectedAppeal = value;
                OnPropertyChanged("IsSelectedAppeal");
                // SettingPhoneForm();
            }
        }
        public int PositionPhoneForm
        {
            get { return _positionPhoneForm; }
            set
            {
                _positionPhoneForm = value;
                OnPropertyChanged("PositionPhoneForm");
            }
        }
        public int PositionCheckPhone
        {
            get { return _positionCheckPhone; }
            set
            {
                _positionCheckPhone = value;
                OnPropertyChanged("PositionCheckPhone");
            }
        }
        public int PositionPromptPhoneNumber
        {
            get { return _positionPromptPhoneNumber; }
            set
            {
                _positionPromptPhoneNumber = value;
                OnPropertyChanged("PositionPromptPhoneNumber");
            }
        }
        public Visibility IsVisibleNumber7
        {
            get { return _isVisibleNumber7; }
            set
            {
                _isVisibleNumber7 = value;
                OnPropertyChanged("IsVisibleNumber7");
            }
        }
        public bool IsIrregular
        {
            get { return _isIrregular; }
            set
            {
                _isIrregular = value;
                OnPropertyChanged("IsIrregular");
                if (Phone != null) Phone = "";
                SettingPhoneFormIrregular();
                // CheckSecondStepFillAppeal();
            }
        }
        public bool IsEnableIrregularCheck
        {
            get { return _isEnableIrregularCheck; }
            set
            {
                _isEnableIrregularCheck = value;
                OnPropertyChanged("IsEnableIrregularCheck");
            }
        }
        public bool IsEnableEmailCheck
        {
            get { return _isEnableEmailCheck; }
            set
            {
                _isEnableEmailCheck = value;
                OnPropertyChanged("IsEnableEmailCheck");
            }
        }
        public bool IsValidAddingDataToAppeal
        {
            get { return _isValidAddingDataToAppeal; }
            set
            {
                _isValidAddingDataToAppeal = value;
                OnPropertyChanged("IsValidAddingDataToAppeal");
            }
        }
        public bool IsEnablePhoneForm
        {
            get { return _isEnablePhoneForm; }
            set
            {
                _isEnablePhoneForm = value;
                OnPropertyChanged("IsEnablePhoneForm");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
