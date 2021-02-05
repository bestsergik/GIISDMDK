using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CallLogGIISDMDK.Models;



namespace CallLogGIISDMDK.ViewModels
{
    class FillAppeal_VM : INotifyPropertyChanged
    {
        FillAppeal_model fillAppeal_Model = new FillAppeal_model();
        private RelayCommand _addAppealCommand;
        private RelayCommand _checkFirstStepFillAppealCommand;
        private RelayCommand _checkSecondStepFillAppealCommand;

        private List<string> _minuteAppeal;
        private List<string> _hourAppeal;
        private List<string> _participantRoles;
        private List<string> _daysCurrentMonthStringFormat;

        private int _currentHour = DateTime.Now.Hour;
        private int _positionPromptName = 2;
        private int _positionPromptPhoneNumber = 2;
        private int _positionCheckPhone = 1;
        private int _positionCheckName = 1;

        private string _participantRole;
        private string _currentDay = DateTime.Now.ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("ru-RU"));
        private string _currentMinute;
        private string _inputPhone;
        private string _appeal;
        private string _additionalInfo;
        private string _fullName;
        private string _email;
        private string _company;
        private string _userName;

        private string _promptsFullName = "";
        private string _promptsPhoneNumber = "";
        private string _promptsEmail = "";
        private string _promptsCompany = "";
        private string _promptsRole = "";
        private string _promptsAppeal = "";

        private bool _isFirstStepFillAppeal;
        private bool _isSecondStepFillAppeal;
        private bool _isIrregular;
        private bool _isHaveName;
        private bool _isEnableNameForm = true;
        private bool _isMale;
        private bool _isFemale;
        private bool _isEnableMaleForm = true;
        private bool _isEnableFemaleForm = true;

        private Visibility _choiseSex = Visibility.Hidden;



        string[] _prompts = new string[5];

        public FillAppeal_VM()
        {
            _hourAppeal = fillAppeal_Model.GetHoursAppeal();
            _minuteAppeal = fillAppeal_Model.GetMinutesAppeal();
            _daysCurrentMonthStringFormat = fillAppeal_Model.FillDaysCurrentMonth();
            _currentMinute = fillAppeal_Model.NearMinute();
            _participantRoles = fillAppeal_Model.GetParticipantRoles();
            _prompts[0] = _promptsFullName; _prompts[1] = _promptsPhoneNumber; _prompts[2] = _promptsEmail; _prompts[3] = _promptsCompany; _prompts[4] = _promptsRole;
        }

        #region Commands


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

        #endregion

        #region Methods

        void CheckFirstStepFillAppeal(object p)
        {
           
            _prompts = fillAppeal_Model.CheckLeghtFields(FullName, InputPhone, Email, Company, ParticipantRole, IsIrregular);
            IsFirstStepFillAppeal = fillAppeal_Model.isValidFirstStep;
           if(p != null)
            ShowPrompts();
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
        }
        private void AddAppeal()
        {
            fillAppeal_Model.SaveAppeal(FullName, InputPhone, CurrentDay, CurrentHour, CurrentMinute, Email, Company, ParticipantRole, Appeal, AdditionalInfo);
        }

        private void ShowChoiseSex()
        {
            if(IsHaveName)
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

        public Visibility ChoiseSex
        {
            get { return _choiseSex; }
            set
            {
                _choiseSex = value;
                OnPropertyChanged("ChoiseSex");
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

        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set
            {
                _additionalInfo = value;
                OnPropertyChanged("AdditionalInfo");
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

        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("Company");

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
                PromptsPhoneNumber = fillAppeal_Model.ClearPrompt(InputPhone, PromptsPhoneNumber);

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

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //public interface ICommand
        //{
        //    void Execute(object param);
        //    bool CanExecute(object param);
        //    event EventHandler<object> CanExecuteChanged;
        //}
    }
}
