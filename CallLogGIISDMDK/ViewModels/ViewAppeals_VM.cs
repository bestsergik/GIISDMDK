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
        private ICommand _addDetailTypeAppealCommand;
        private ICommand _seekCommonTypeAppealCommand;
        private ICommand _seekDetailTypeAppealCommand;
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
        private List<string> _commonTypeAppeal;
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
        private string _answer;
        private string _additionalInfo;
        private string _email;
        private string _status;
        private string _type = "";
        private string _detailType = "";
        private string _route;
        private string _month;
        private string _year;
        private string _communicationСhannel;
        private string _ID;
        private string _personalID;
        private string _seekCommonTypeAppeal = "";
        private string _seekDetailTypeAppeal = "";
        private int _positionPromptPhoneNumber = 2;
        private int _positionCheckPhone = 1;
        private int _positionPhoneForm = 1;
        private string _promptsPhone = "";
        private string _promptsEmail = "";
        private string _promptsAppeal = "";
        private string _promptsAnswer = "";
        private string _promptsStatus = "";
        private string _promptsType = "";
        private string _promptsTime = "";
        private string _promptsDetailType = "";
        private string _promptsCommunicationChannel = "";
        private bool _isValidAddingDataToAppeal;
        private bool _isIrregular;
        private bool _isHaveAppeal;
        private bool _isEnableCheckAppeal = true;
        private bool _isEnableAnswer = true;
        private bool _isEnableAppeal = true;
        private bool _isEnableCheckAnswer = true;
        private bool _isHaveAnswer;
        private bool _isEnablePhoneForm = true;
        private bool _isEnableIrregularCheck = true;
        private bool _isEnableEmailCheck = true;
        private bool _isSelectedAppeal;
        private Visibility _isVisibleNumber7;
        string[] _prompts = new string[9];


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
            _commonTypeAppeal = _fillAppeal_Model.GetCommonTypeAppeal();
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
        #region Commands

        public ICommand AddDetailTypeAppealCommand
        {
            get
            {
                return _addDetailTypeAppealCommand ?? (_addDetailTypeAppealCommand = new RelayCommand(AddDetailTypeAppeal));
            }
        }
        public ICommand SeekCommonTypeAppealCommand
        {
            get
            {
                return _seekCommonTypeAppealCommand ?? (_seekCommonTypeAppealCommand = new RelayCommand(SearchCommonTypeAppeal));
            }
        }
        public ICommand SeekDetailTypeAppealCommand
        {
            get
            {
                return _seekDetailTypeAppealCommand ?? (_seekDetailTypeAppealCommand = new RelayCommand(SearhDetailTypeAppeal));
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
        #endregion
        #region Methods

        private void SetMonth(object route)
        {
            StaticData.CurrrentMonth = _month;
            StaticData.CurrrentYear = _year;
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
        private void SearchCommonTypeAppeal(object commonTypeAppeal)
        {
            string typeAppeal = commonTypeAppeal.ToString();
            _seekCommonTypeAppeal = _fillAppeal_Model.AddTypeAppeal(_seekCommonTypeAppeal, typeAppeal);
            SearchInsertAppeal(_seekCommonTypeAppeal, 2);
        }
        private void SearhDetailTypeAppeal(object detailTypeAppeal)
        {
            string typeAppeal = detailTypeAppeal.ToString();
            _seekDetailTypeAppeal = _fillAppeal_Model.AddTypeAppeal(_seekDetailTypeAppeal, typeAppeal);
            SearchInsertAppeal(_seekDetailTypeAppeal, 3);
        }

        // Set flags appeal and answer
        private void CustomChecks()
        {
            if (IsHaveAppeal)
            {
                Appeal = "";
                IsEnableAppeal = false;
                IsEnableCheckAnswer = false;
            }
            else
            {
                IsEnableAppeal = true;
                IsEnableCheckAnswer = true;
            }
            if (IsHaveAnswer)
            {
                Answer = "";
                IsEnableAnswer = false;
                IsEnableCheckAppeal = false;
            }
            else
            {
                IsEnableCheckAppeal = true;
                IsEnableAnswer = true;
            }
        }
        private void AddDetailTypeAppeal(object detailType)
        {
            string typeAppeal = detailType.ToString();
            _detailType = _fillAppeal_Model.AddTypeAppeal(_detailType, typeAppeal);
            ValidateAdding(null);
            PromptsDetailType = _fillAppeal_Model.ClearPrompt(DetailType, PromptsDetailType);
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
            _prompts = _fillAppeal_Model.CheckPrompts(Phone, Email, Status, CommunicationСhannel, Appeal, Answer, Type, DetailType, CurrentMinute, IsIrregular, IsHaveAppeal, IsHaveAnswer);
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
            fileWriter.WriteAppealToFile(_currentAppeal.FullName, _currentAppeal.Company, _currentAppeal.Sity, Phone, _currentAppeal.Inn, _currentAppeal.ParticipantRole, Type, Status, Email, _currentAppeal.Ogrn, convertedDate, Appeal, AdditionalInfo, CommunicationСhannel, CurrentTime, Route, Answer, DetailType);
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
            Answer = "";
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
                        DataAppealByID.Add(new FillAppeal_VM { ID = appeal[0], CurrentDay = Convert.ToDateTime(appeal[1]).ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU")), CurrentTime = appeal[2], CommunicationChannel = appeal[3], Type = appeal[4], DetailType = appeal[5], Appeal = appeal[6], Answer = appeal[7], UserName = appeal[8], Sity = appeal[9], ParticipantRole = appeal[10], Route = appeal[11], AdditionalInfo = appeal[12], Company = appeal[13], FullName = appeal[14], InputPhone = appeal[15], Email = appeal[16], Inn = appeal[17], Ogrn = appeal[18], Status = appeal[19], PersonalID = appeal[20] });
                    });
            }
        }
        void ValidateAdding(object p)
        {
            _prompts = _fillAppeal_Model.CheckPrompts(Phone, Email, Status, CommunicationСhannel, Appeal, Answer, Type, DetailType, CurrentMinute, IsIrregular, IsHaveAppeal, IsHaveAnswer);
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
                        Appeals.Add(new FillAppeal_VM { ID = appeal[0], CurrentDay = Convert.ToDateTime(appeal[1]).ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU")), CurrentTime = appeal[2], CommunicationChannel = appeal[3], Type = appeal[4], DetailType = appeal[5], Appeal = appeal[6], Answer = appeal[7], UserName = appeal[8], Sity = appeal[9], ParticipantRole = appeal[10], Route = appeal[11], AdditionalInfo = appeal[12], Company = appeal[13], FullName = appeal[14], InputPhone = appeal[15], Email = appeal[16], Inn = appeal[17], Ogrn = appeal[18], Status = appeal[19], PersonalID = appeal[20] });
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
            _prompts[4] = _promptsAnswer;
            _prompts[5] = _promptsType;
            _prompts[5] = _promptsDetailType;
            _prompts[6] = _promptsTime;
        }
        private void ShowPrompts()
        {
            PromptsPhone = _prompts[0];
            PromptsEmail = _prompts[1];
            PromptsStatus = _prompts[2];
            PromptsCommunicationChannel = _prompts[3];
            PromptsAppeal = _prompts[4];
            PromptsAnswer = _prompts[5];
            PromptsType = _prompts[6];
            PromptsDetailType = _prompts[7];
            PromptsTime = _prompts[8];
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
            StaticData.DataAppealByPersonalID = _appeals;
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
        #endregion
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
        public List<string> CommonTypeAppeal
        {
            get { return _commonTypeAppeal; }
            set
            {
                _commonTypeAppeal = value;
                OnPropertyChanged("CommonTypeAppeal");
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
        public string PromptsAnswer
        {
            get { return _promptsAnswer; }
            set
            {
                _promptsAnswer = value;
                OnPropertyChanged("PromptsAnswer");
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
        public string PromptsDetailType
        {
            get { return _promptsDetailType; }
            set
            {
                _promptsDetailType = value;
                OnPropertyChanged("PromptsDetailType");
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
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public string DetailType
        {
            get { return _detailType; }
            set
            {
                _detailType = value;
                OnPropertyChanged("DetailType");
            }
        }
        public string Route
        {
            get { return _route; }
            set
            {
                _route = value;
                OnPropertyChanged("Route");
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
        public string SeekCommonTypeAppeal
        {
            get { return _seekCommonTypeAppeal; }
            set
            {
                _seekCommonTypeAppeal = value;
                OnPropertyChanged("SeekCommonTypeAppeal");
            }
        }
        public string SeekDetailTypeAppeal
        {
            get { return _seekDetailTypeAppeal; }
            set
            {
                _seekDetailTypeAppeal = value;
                OnPropertyChanged("SeekDetailTypeAppeal");
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
        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
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
            }
        }
        public bool IsHaveAnswer
        {
            get { return _isHaveAnswer; }
            set
            {
                _isHaveAnswer = value;
                OnPropertyChanged("IsHaveAnswer");
                CustomChecks();
            }
        }
        public bool IsHaveAppeal
        {
            get { return _isHaveAppeal; }
            set
            {
                _isHaveAppeal = value;
                OnPropertyChanged("IsHaveAppeal");
                CustomChecks();
            }
        }

        public bool IsEnableCheckAnswer
        {
            get { return _isEnableCheckAnswer; }
            set
            {
                _isEnableCheckAnswer = value;
                OnPropertyChanged("IsEnableCheckAnswer");

            }
        }
        public bool IsEnableCheckAppeal
        {
            get { return _isEnableCheckAppeal; }
            set
            {
                _isEnableCheckAppeal = value;
                OnPropertyChanged("IsEnableCheckAppeal");
            }
        }
        public bool IsEnableAnswer
        {
            get { return _isEnableAnswer; }
            set
            {
                _isEnableAnswer = value;
                OnPropertyChanged("IsEnableAnswer");
            }
        }
        public bool IsEnableAppeal
        {
            get { return _isEnableAppeal; }
            set
            {
                _isEnableAppeal = value;
                OnPropertyChanged("IsEnableAppeal");
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

