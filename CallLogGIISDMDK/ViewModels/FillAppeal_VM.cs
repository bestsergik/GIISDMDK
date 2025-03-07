﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    class FillAppeal_VM : INotifyPropertyChanged
    {
        Data data = new Data();
        FileWriter fileWriter = new FileWriter();
        FileReader fileReader = new FileReader();
        FileRemover fileRemover = new FileRemover();
        FillAppeal_model fillAppeal_Model = new FillAppeal_model();
        private RelayCommand _addAppealCommand;
        private RelayCommand _checkFirstStepFillAppealCommand;
        private RelayCommand _checkSecondStepFillAppealCommand;
        private RelayCommand _loadedPageCommand;
        private RelayCommand _loadedPage2Command;
        private RelayCommand _selectionChangedCommand;
        private RelayCommand _updateComnListCommand;
        private ICommand _fillEditableAppealCommand;
        private ICommand _addTypeAppealCommand;
        private ICommand _addDetailTypeAppealCommand;
        private RelayCommand _deleteAppealCommand;
        private List<string> _minuteAppeal;
        private List<string> _hourAppeal;
        private List<string> _participantRoles;
        private List<string> _statuses;
        private List<string> _communicationСhannels;
        private List<string> _daysCurrentMonthStringFormat;
        private List<string> _displayAppeals = new List<string>();
        private int _currentHour = DateTime.Now.Hour;
        private int _positionPromptName = 2;
        private int _positionPromptPhoneNumber = 2;
        private int _positionCheckPhone = 1;
        private int _positionPromptCompany = 2;
        private int _positionCheckCompany = 1;
        private int _positionCheckName = 1;
        private int _positionPhoneForm = 1;
        private string _participantRole;
        private string _currentDay = DateTime.Now.ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU"));
        private string _currentMinute;
        private string _currentTime;
        private string _inputPhone;
        private string _appeal;
        private string _answer;
        private string _additionalInfo;
        private string _fullName;
        private string _email;
        private string _company;
        private string _userName;
        private string _status;
        private string _ogrn;
        private string _route = "Входящее";
        private string _inn;
        private string _sity;
        private string _type = "";
        private string _detailType = "";
        private string _selectedSuitableAppeal;
        private string _communicationChannel = "";
        private string _ID;
        private string _personalID;
        private string _promptsFullName = "";
        private string _promptsPhoneNumber = "";
        private string _promptsEmail = "";
        private string _promptsCompany = "";
        private string _promptsRole = "";
        private string _promptsAppeal = "";
        private string _promptsAnswer = "";
        private string _promptsStatus = "";
        private string _promptsInn = "";
        private string _promptsOgrn = "";
        private string _promptsSity = "";
        private string _promptsType = "";
        private string _promptsDetailType = "";
        private string _promptsCommunicationСhannel = "";
        private string _promptsTime = "";
        private bool _isFirstStepFillAppeal;
        private bool _isSecondStepFillAppeal;
        private bool _isIrregular;
        private bool _isIP;
        private bool _isHaveName;
        private bool _isEnableNameForm = true;
        private bool _isEnableCompanyForm = true;
        private bool _isMale;
        private bool _isFemale;
        private bool _isEnableMaleForm = true;
        private bool _isEnableFemaleForm = true;
        private bool _isEnablePhoneForm = true;
        private bool _isEnableIrregularCheck = true;
        private bool _isEmail;
        private bool _isReadyUpdateAppeals = false;
        private Visibility _choiseSex = Visibility.Hidden;
        private Visibility _isVisibleNumber7;
        string[] _prompts = new string[13];
        public FillAppeal_VM()
        {
            _hourAppeal = fillAppeal_Model.GetHoursAppeal();
            _minuteAppeal = fillAppeal_Model.GetMinutesAppeal();
            _daysCurrentMonthStringFormat = fillAppeal_Model.FillDaysCurrentMonth();
            _participantRoles = fillAppeal_Model.GetParticipantRoles();
            _statuses = fillAppeal_Model.GetStatusesAppeal();
            _communicationСhannels = fillAppeal_Model.GetTypesAppeal();
            FillPrompts();
        }

        #region Commands

        public ICommand ClickCommand
        {
            get
            {
                return _fillEditableAppealCommand ?? (_fillEditableAppealCommand = new RelayCommand(FillEditableAppeal));
            }
        }
        public ICommand AddTypeAppealCommand
        {
            get
            {
                return _addTypeAppealCommand ?? (_addTypeAppealCommand = new RelayCommand(AddTypeAppeal));
            }
        }
        public ICommand AddDetailTypeAppealCommand
        {
            get
            {
                return _addDetailTypeAppealCommand ?? (_addDetailTypeAppealCommand = new RelayCommand(AddDetailTypeAppeal));
            }
        }
        public RelayCommand DeleteAppealCommand
        {
            get
            {
                if (_deleteAppealCommand == null)
                {
                    _deleteAppealCommand = new RelayCommand(
                        p => this.DeleteAppeal()); ;
                }
                return _deleteAppealCommand;
            }
        }
        public RelayCommand AddAppealCommand
        {
            get
            {
                if (_addAppealCommand == null)
                {
                    _addAppealCommand = new RelayCommand(
                        p => this.AddAppeal()); ;
                }
                return _addAppealCommand;
            }
        }
        public RelayCommand LoadedPage2Command
        {
            get
            {
                if (_loadedPage2Command == null)
                {
                    _loadedPage2Command = new RelayCommand(
                        p => this.NewAppeal2()); ;
                }
                return _loadedPage2Command;
            }
        }

        public RelayCommand UpdateComnListCommand
        {
            get
            {
                if (_updateComnListCommand == null)
                {
                    _updateComnListCommand = new RelayCommand(
                        p => this.UpdateComnList()); ;
                }
                return _updateComnListCommand;
            }
        }
        private void UpdateComnList()
        {
            DisplayAppeals = fillAppeal_Model.CheckSuitableAppeal(SelectedSuitableAppeal);
        }
        public RelayCommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                {
                    _selectionChangedCommand = new RelayCommand(
                        p => this.SelectionChanged()); ;
                }
                return _selectionChangedCommand;
            }
        }
        private void SelectionChanged()
        {
            FillAppeal();
        }
        public RelayCommand CheckFirstStepFillAppealCommand
        {
            get
            {
                if (_checkFirstStepFillAppealCommand == null)
                {
                    _checkFirstStepFillAppealCommand = new RelayCommand(
                        p => this.CheckFirstStepFillAppeal(_checkFirstStepFillAppealCommand)); ;
                }
                return _checkFirstStepFillAppealCommand;
            }
        }
        public RelayCommand CheckSecondStepFillAppealCommand
        {
            get
            {
                if (_checkSecondStepFillAppealCommand == null)
                {
                    _checkSecondStepFillAppealCommand = new RelayCommand(
                        p => this.CheckSecondStepFillAppeal()); ;
                }
                return _checkSecondStepFillAppealCommand;
            }
        }
        public RelayCommand LoadedPageCommand
        {
            get
            {
                if (_loadedPageCommand == null)
                {
                    _loadedPageCommand = new RelayCommand(
                        p => this.NewAppeal()); ;
                }
                return _loadedPageCommand;
            }
        }

        #endregion
        #region Methods
        private void EditAppeal()
        {
        }
        void FillDisplayAppeals()
        {
            foreach (var appeal in data.GetAppeals())
            {
                DisplayAppeals.Add($"{appeal[0]},{appeal[1]},{appeal[2]}");
            }
        }
        private void NewAppeal()
        {
            StaticData.CurrentId = 0;
            if (StaticData.IsNewAppeal)
            {
                InputPhone = "";
                AdditionalInfo = "";
                FullName = "";
                Email = "";
                Company = "";
               
                Type = "";
                DetailType = "";
                UserName = "";
                Ogrn = "";
                Inn = "";
                Sity = "";
                Status = "";
                CurrentMinute = "";
                IsIrregular = false;
                IsEnableCompanyForm = true;
                IsIP = false;
                IsEmail = false;
                IsHaveName = false;
                IsEnableFemaleForm = true;
                IsEnableMaleForm = true;
                IsMale = false;
                IsFemale = false;
                IsEnablePhoneForm = true;
                IsEnableNameForm = true;
                CommunicationChannel = "";
                ParticipantRole = "";
                StaticData.IsNewAppeal = false;
                _route = "Входящее";
            }
        }
        private void NewAppeal2()
        {
            if (StaticData.IsNewAppeal2)
            {
                Appeal = "";
                AdditionalInfo = "";
                Answer = "";
                StaticData.IsNewAppeal2 = false;
            }
        }
        private void FillPrompts()
        {
            _prompts[0] = _promptsFullName;
            _prompts[1] = _promptsPhoneNumber;
            _prompts[2] = _promptsEmail;
            _prompts[3] = _promptsCompany;
            _prompts[4] = _promptsRole;
            _prompts[5] = _promptsStatus;
            _prompts[6] = _promptsInn;
            _prompts[7] = _promptsSity;
            _prompts[8] = _promptsOgrn;
            _prompts[9] = _promptsCommunicationСhannel;
            _prompts[10] = _promptsType;
            _prompts[11] = _promptsTime;
            _prompts[12] = _promptsDetailType;
        }
        private void AddDetailTypeAppeal(object detailType)
        {
            string typeAppeal = detailType.ToString();
            _detailType = fillAppeal_Model.AddTypeAppeal(_detailType, typeAppeal);
            CheckFirstStepFillAppeal(null);
            PromptsDetailType = fillAppeal_Model.ClearPrompt(DetailType, PromptsDetailType);
        }
        private void AddTypeAppeal(object type)
        {
            string typeAppeal = type.ToString();
            _type = fillAppeal_Model.AddTypeAppeal(_type, typeAppeal);
            CheckFirstStepFillAppeal(null);
            PromptsType = fillAppeal_Model.ClearPrompt(Type, PromptsType);
        }
        public void FillEditableAppeal(object appeal)
        {
            StaticData.CurrentPersonalId = appeal.ToString();
        }
        private void DeleteAppeal()
        {
            fileRemover.onRemoveComplete += FileRemover_onRemoveComplete;
            fileRemover.RemoveAppeal();
            fileRemover.RemovingComplete();
            fileRemover.onRemoveComplete -= FileRemover_onRemoveComplete;
        }
        private void FileRemover_onRemoveComplete()
        {
        }
        void CheckFirstStepFillAppeal(object p)
        {
            _prompts = fillAppeal_Model.CheckLeghtFields(FullName, InputPhone, Email, Company, ParticipantRole, Status, IsIrregular, Inn, Sity, Ogrn, CommunicationChannel, Type, CurrentMinute, DetailType);
            IsFirstStepFillAppeal = fillAppeal_Model.isValidFirstStep;
            if (p != null)
                ShowPrompts();
        }
        private void FillAppeal()
        {
            //FullName = fillAppeal_Model.suitableAppeal[0];
            //Company = fillAppeal_Model.suitableAppeal[1];
            //InputPhone = fillAppeal_Model.suitableAppeal[2];
            //Inn = fillAppeal_Model.suitableAppeal[3];
            //Sity = fillAppeal_Model.suitableAppeal[4];
            //ParticipantRole = fillAppeal_Model.suitableAppeal[5];
            //Status = fillAppeal_Model.suitableAppeal[6];
            //Email = fillAppeal_Model.suitableAppeal[7];
            //Ogrn = fillAppeal_Model.suitableAppeal[8];
            //CurrentDay = fillAppeal_Model.suitableAppeal[9];
            //CurrentHour = Convert.ToInt32(fillAppeal_Model.suitableAppeal[10]);
            //CurrentMinute = fillAppeal_Model.suitableAppeal[11];
        }
        void CheckSecondStepFillAppeal()
        {
            //_prompts = fillAppeal_Model.CheckLeghtFields(FullName, InputPhone, Email, Company, ParticipantRole);
            if (Appeal == null || Appeal == "")
            {
                PromptsAppeal = "Обязательное поле";
                IsSecondStepFillAppeal = false;
            }
            else
            {
                PromptsAppeal = "";
                IsSecondStepFillAppeal = true;
            }
            ShowPrompts();
        }
        private void ShowPrompts()
        {
            PromptsFullName = _prompts[0];
            PromptsPhoneNumber = _prompts[1];
            PromptsEmail = _prompts[2];
            PromptsCompany = _prompts[3];
            PromptsRole = _prompts[4];
            PromptsStatus = _prompts[5];
            PromptsInn = _prompts[6];
            PromptsSity = _prompts[7];
            PromptsOgrn = _prompts[8];
            PromptsCommunicationСhannel = _prompts[9];
            PromptsType = _prompts[10];
            PromptsTime = _prompts[11];
            PromptsDetailType = _prompts[12];
        }
        private void AddAppeal()
        {
            Thread myThread = new Thread(new ThreadStart(Adding));
            myThread.Start();
        }
        private void Adding()
        {
            DateTime dateForConverted = DateTime.Parse(CurrentDay);
            string convertedDate = dateForConverted.ToString("dd/MM/yyyy");
            CurrentTime = (DateTime.Parse($"{CurrentHour.ToString()}:{CurrentMinute}")).ToString("HH:mm");
            fileWriter.WriteAppealToFile(FullName, Company, Sity, InputPhone, Inn, ParticipantRole, Type, Status, Email, Ogrn, convertedDate, Appeal, AdditionalInfo, CommunicationChannel, CurrentTime, Route, Answer, DetailType);
        }
        private void ShowChoiseSex()
        {
            if (IsHaveName)
            {
                ChoiseSex = Visibility.Visible;
                IsEnableNameForm = false;
                FullName = "";
            }
            else
            {
                ChoiseSex = Visibility.Hidden;
                IsEnableNameForm = true;
                FullName = "";
                IsMale = false;
                IsFemale = false;
            }
        }
        void CheckPositionControls()
        {
            if (PromptsFullName != "")
            {
                PositionPromptName = 1;
                PositionCheckName = 2;
            }
            else
            {
                PositionPromptName = 2;
                PositionCheckName = 1;
            }
            if (PromptsPhoneNumber != "")
            {
                PositionPromptPhoneNumber = 1;
                PositionCheckPhone = 2;
            }
            else
            {
                PositionPromptPhoneNumber = 2;
                PositionCheckPhone = 1;
            }
            if (PromptsCompany != "")
            {
                PositionPromptCompany = 1;
                PositionCheckCompany = 2;
            }
            else
            {
                PositionPromptCompany = 2;
                PositionCheckCompany = 1;
            }
        }
        #endregion
        #region Bolean properties
        public bool IsFirstStepFillAppeal
        {
            get { return _isFirstStepFillAppeal; }
            set
            {
                _isFirstStepFillAppeal = value;
                OnPropertyChanged("IsFirstStepFillAppeal");
            }
        }
        public bool IsEnableNameForm
        {
            get { return _isEnableNameForm; }
            set
            {
                _isEnableNameForm = value;
                OnPropertyChanged("IsEnableNameForm");
            }
        }
        public bool IsEnableCompanyForm
        {
            get { return _isEnableCompanyForm; }
            set
            {
                _isEnableCompanyForm = value;
                OnPropertyChanged("IsEnableCompanyForm");
            }
        }
        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                _isMale = value;
                OnPropertyChanged("IsMale");
                BlockSexForm();
            }
        }
        public bool IsFemale
        {
            get { return _isFemale; }
            set
            {
                _isFemale = value;
                OnPropertyChanged("IsFemale");
                BlockSexForm();
            }
        }
        public bool IsEnableFemaleForm
        {
            get { return _isEnableFemaleForm; }
            set
            {
                _isEnableFemaleForm = value;
                OnPropertyChanged("IsEnableFemaleForm");
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
        private void BlockCompanyForm()
        {
            if (IsIP)
            {
                IsEnableCompanyForm = false;
                Company = "ИП";
            }
            else
            {
                IsEnableCompanyForm = true;
                Company = "";
            }
        }
        private void BlockSexForm()
        {
            if (IsFemale)
            {
                IsEnableMaleForm = false;
                FullName = "Женщина";
            }
            else if (IsMale)
            {
                IsEnableFemaleForm = false;
                FullName = "Мужчина";
            }
            else if (!IsFemale && !IsMale)
            {
                IsEnableFemaleForm = true;
                IsEnableMaleForm = true;
                FullName = "";
            }
        }
        public bool IsEnableMaleForm
        {
            get { return _isEnableMaleForm; }
            set
            {
                _isEnableMaleForm = value;
                OnPropertyChanged("IsEnableMaleForm");
            }
        }
        public bool IsEmail
        {
            get { return _isEmail; }
            set
            {
                _isEmail = value;
                OnPropertyChanged("IsEmail");
                SettingPhoneForm();
            }
        }
        public bool IsReadyUpdateAppeals
        {
            get { return _isReadyUpdateAppeals; }
            set
            {
                _isReadyUpdateAppeals = value;
                OnPropertyChanged("IsReadyUpdateAppeals");
                SettingPhoneForm();
            }
        }
        private void SettingPhoneForm()
        {
            if (IsEmail)
            {
                IsVisibleNumber7 = Visibility.Hidden;
                PositionPhoneForm = 0;
                IsEnablePhoneForm = false;
                IsEnableIrregularCheck = false;
            }
            else
            {
                InputPhone = "";
                IsVisibleNumber7 = Visibility.Visible;
                PositionPhoneForm = 1;
                IsEnablePhoneForm = true;
                IsEnableIrregularCheck = true;
            }
        }
        private void SettingPhoneFormIrregular()
        {
            if (IsIrregular)
            {
                IsVisibleNumber7 = Visibility.Hidden;
                PositionPhoneForm = 0;
            }
            else
            {
                IsVisibleNumber7 = Visibility.Visible;
                PositionPhoneForm = 1;
            }
        }
        public Visibility ChoiseSex
        {
            get { return _choiseSex; }
            set
            {
                _choiseSex = value;
                OnPropertyChanged("ChoiseSex");
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
        public bool IsSecondStepFillAppeal
        {
            get { return _isSecondStepFillAppeal; }
            set
            {
                _isSecondStepFillAppeal = value;
                OnPropertyChanged("IsSecondStepFillAppeal");
            }
        }
        public bool IsHaveName
        {
            get { return _isHaveName; }
            set
            {
                _isHaveName = value;
                OnPropertyChanged("IsHaveName");
                ShowChoiseSex();
            }
        }
        public bool IsIrregular
        {
            get { return _isIrregular; }
            set
            {
                _isIrregular = value;
                OnPropertyChanged("IsIrregular");
                if (InputPhone != null) InputPhone = "";
                SettingPhoneFormIrregular();
                // CheckSecondStepFillAppeal();
            }
        }
        public bool IsIP
        {
            get { return _isIP; }
            set
            {
                _isIP = value;
                OnPropertyChanged("IsIP");
                BlockCompanyForm();
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
        #endregion
        #region String properties
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        public string CommunicationChannel
        {
            get { return _communicationChannel; }
            set
            {
                _communicationChannel = value;
                OnPropertyChanged("CommunicationChannel");
                CheckFirstStepFillAppeal(null);
                PromptsCommunicationСhannel = fillAppeal_Model.ClearPrompt(CommunicationChannel, PromptsCommunicationСhannel);
                if (CommunicationChannel == "Email") PromptsPhoneNumber = "";
                else if (CommunicationChannel == "Телефон") PromptsEmail = "";
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
        public string CurrentDay
        {
            get { return _currentDay; }
            set
            {
                _currentDay = value;
                OnPropertyChanged("CurrentDay");
            }
        }
        public string Appeal
        {
            get { return _appeal; }
            set
            {
                _appeal = value;
                OnPropertyChanged("Appeal");
                CheckSecondStepFillAppeal();
            }
        }
        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
                CheckSecondStepFillAppeal();
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
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
            }
        }
        public string CurrentMinute
        {
            get { return _currentMinute; }
            set
            {
                _currentMinute = value;
                OnPropertyChanged("CurrentMinute");
                CheckFirstStepFillAppeal(null);
                PromptsTime = fillAppeal_Model.ClearPrompt(CurrentMinute, PromptsTime);
            }
        }
        public string ParticipantRole
        {
            get { return _participantRole; }
            set
            {
                _participantRole = value;
                OnPropertyChanged("ParticipantRole");
                CheckFirstStepFillAppeal(null);
                PromptsRole = fillAppeal_Model.ClearPrompt(ParticipantRole, PromptsRole);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
                CheckFirstStepFillAppeal(null);
                PromptsEmail = fillAppeal_Model.ClearPrompt(Email, PromptsEmail);
            }
        }
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
                CheckFirstStepFillAppeal(null);
                PromptsFullName = fillAppeal_Model.ClearPrompt(FullName, PromptsFullName);
            }
        }
        public string InputPhone
        {
            get { return _inputPhone; }
            set
            {
                _inputPhone = value;
                OnPropertyChanged("InputPhone");
                if (_inputPhone != "" && !IsIrregular)
                    _inputPhone = fillAppeal_Model.CheckInputNumber(InputPhone);
                CheckFirstStepFillAppeal(null);
                //if (_inputPhone == "Обращение по почте") IsEmail = true;
                PromptsPhoneNumber = fillAppeal_Model.ClearPrompt(InputPhone, PromptsPhoneNumber);
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
                CheckFirstStepFillAppeal(null);
                PromptsStatus = fillAppeal_Model.ClearPrompt(Status, PromptsStatus);
            }
        }
        public string Inn
        {
            get { return _inn; }
            set
            {
                _inn = value;
                OnPropertyChanged("Inn");
                if (_inn != "")
                    _inn = fillAppeal_Model.CheckInputInn(Inn);
                CheckFirstStepFillAppeal(null);
                PromptsInn = fillAppeal_Model.ClearPrompt(Inn, PromptsInn);
            }
        }
        public string Ogrn
        {
            get { return _ogrn; }
            set
            {
                _ogrn = value;
                OnPropertyChanged("Ogrn");
                if (_ogrn != "")
                    _ogrn = fillAppeal_Model.CheckInputOgrn(Ogrn);
                CheckFirstStepFillAppeal(null);
                PromptsOgrn = fillAppeal_Model.ClearPrompt(Ogrn, PromptsOgrn);
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
        public string SelectedSuitableAppeal
        {
            get { return _selectedSuitableAppeal; }
            set
            {
                _selectedSuitableAppeal = value;
                OnPropertyChanged("SelectedSuitableAppeal");
            }
        }
        public string Sity
        {
            get { return _sity; }
            set
            {
                _sity = value;
                OnPropertyChanged("Sity");
                CheckFirstStepFillAppeal(null);
                PromptsSity = fillAppeal_Model.ClearPrompt(Ogrn, PromptsSity);
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
                CheckFirstStepFillAppeal(null);
                PromptsType = fillAppeal_Model.ClearPrompt(Type, PromptsType);
            }
        }
        public string DetailType
        {
            get { return _detailType; }
            set
            {
                _detailType = value;
                OnPropertyChanged("DetailType");
                CheckFirstStepFillAppeal(null);
                PromptsType = fillAppeal_Model.ClearPrompt(DetailType, PromptsType);
            }
        }
        public string PromptsFullName
        {
            get { return _promptsFullName; }
            set
            {
                _promptsFullName = value;
                OnPropertyChanged("PromptsFullName");
                CheckPositionControls();
            }
        }
        public string PromptsInn
        {
            get { return _promptsInn; }
            set
            {
                _promptsInn = value;
                OnPropertyChanged("PromptsInn");
                CheckPositionControls();
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
        public string PromptsDetailType
        {
            get { return _promptsDetailType; }
            set
            {
                _promptsDetailType = value;
                OnPropertyChanged("PromptsDetailType");
            }
        }

        public string PromptsCommunicationСhannel
        {
            get { return _promptsCommunicationСhannel; }
            set
            {
                _promptsCommunicationСhannel = value;
                OnPropertyChanged("PromptsCommunicationСhannel");
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
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
                CheckFirstStepFillAppeal(null);
                PromptsCompany = fillAppeal_Model.ClearPrompt(Company, PromptsCompany);
            }
        }
        public string PromptsPhoneNumber
        {
            get { return _promptsPhoneNumber; }
            set
            {
                _promptsPhoneNumber = value;
                OnPropertyChanged("PromptsPhoneNumber");
                CheckPositionControls();
            }
        }
        public string PromptsOgrn
        {
            get { return _promptsOgrn; }
            set
            {
                _promptsOgrn = value;
                OnPropertyChanged("PromptsOgrn");
                CheckPositionControls();
            }
        }
        public string PromptsSity
        {
            get { return _promptsSity; }
            set
            {
                _promptsSity = value;
                OnPropertyChanged("PromptsSity");
                CheckPositionControls();
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
        public string PromptsCompany
        {
            get { return _promptsCompany; }
            set
            {
                _promptsCompany = value;
                OnPropertyChanged("PromptsCompany");
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
        public string PromptsRole
        {
            get { return _promptsRole; }
            set
            {
                _promptsRole = value;
                OnPropertyChanged("PromptsRole");
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
        #endregion
        #region Int properties
        public int CurrentHour
        {
            get { return _currentHour; }
            set
            {
                _currentHour = value;
                OnPropertyChanged("CurrentHour");
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
        public int PositionCheckName
        {
            get { return _positionCheckName; }
            set
            {
                _positionCheckName = value;
                OnPropertyChanged("PositionCheckName");
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
        public int PositionCheckCompany
        {
            get { return _positionCheckCompany; }
            set
            {
                _positionCheckCompany = value;
                OnPropertyChanged("PositionCheckCompany");
            }
        }
        public int PositionPromptName
        {
            get { return _positionPromptName; }
            set
            {
                _positionPromptName = value;
                OnPropertyChanged("PositionPromptName");
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
        public int PositionPromptCompany
        {
            get { return _positionPromptCompany; }
            set
            {
                _positionPromptCompany = value;
                OnPropertyChanged("PositionPromptCompany");
            }
        }
        #endregion
        #region List Properties
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
        public List<string> DisplayAppeals
        {
            get { return _displayAppeals; }
            set
            {
                _displayAppeals = value;
                OnPropertyChanged("DisplayAppeals");
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
        public List<string> ParticipantRoles
        {
            get { return _participantRoles; }
            set
            {
                _participantRoles = value;
                OnPropertyChanged("ParticipantRoles");
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
        public List<string> CommunicationСhannels
        {
            get { return _communicationСhannels; }
            set
            {
                _communicationСhannels = value;
                OnPropertyChanged("CommunicationСhannels");
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
