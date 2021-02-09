using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    

    class Authorization_VM : INotifyPropertyChanged
    {

        Compress compress = new Compress();
        FileReader fileReader = new FileReader();

        private RelayCommand loginCommand;
        private RelayCommand registrationCommand;
        private RelayCommand _logOutCommand;
        private RelayCommand _userProfileCommand;

        Authorization_model authorizationModel = new Authorization_model();

        private string PathToLogins = @"logins.txt";
        private string ZipPathToLogins = @"UserLogins.zip";

        private string loginEntry = "";
        private string loginRegistration = "";
        private string passwordEntry = "";
        private string passwordRegistration = "";
        private string repeatPasswordRegistration = "";

        private string promptsEntry;
        private string promptsEntryPassword;

        private string _expertName;
        private string _lvlExpert;
        private string _completeAppealsExpert;
        private string _statusExpert;

        private string promptsRegistration;
        private string promptsPasswordRegistration;
        private string promptsPasswordRegistrationRepeat;

        private bool isRegistrationPass;
        private bool isLogin;

        private Visibility _isVisibleTopButtons = Visibility.Hidden;

        private string _realPassword = "";

        Dictionary<string, string> loginPassword = new Dictionary<string, string>();

        public Authorization_VM()
        {
            fileReader.onReadingComplete += FileReader_onReadingComplete;
            CheckFileData();
        }

        private void FileReader_onReadingComplete()
        {
            ExpertName = StaticData.User;
            LvlExpert = StaticData.UserLvl.ToString();
            CompleteAppealsExpert = StaticData.UserTopLvl.ToString();
            StatusExpert = StaticData.UserStatus;
        }


        #region Commands
        public RelayCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(

                        p => this.CheckEntryUser());
                }
                return loginCommand;
            }
        }

        public RelayCommand UserProfileCommand
        {
            get
            {
                if (_userProfileCommand == null)
                {
                    _userProfileCommand = new RelayCommand(

                        p => this.FillUserProfile());
                }
                return _userProfileCommand;
            }
        }

       

        public RelayCommand LogOutCommand
        {
            get
            {
                if (_logOutCommand == null)
                {
                    _logOutCommand = new RelayCommand(

                        p => this.LogOut());
                }
                return _logOutCommand;
            }
        }

        private void LogOut()
        {
            IsVisibleTopButtons = Visibility.Hidden;
        }

        public RelayCommand RegistrationCommand
        {
            get
            {
                if (registrationCommand == null)
                {

                    registrationCommand = new RelayCommand(

                        p => this.CheckRegistrationUser());
                }
                return registrationCommand;
            }
        }

        #endregion

        #region Methods

        private void FillUserProfile()
        {
            Thread myThread = new Thread(new ThreadStart(FillProfile));
            myThread.Start();
        }

        private void FillProfile()
        {
             fileReader.GetAppeals();
             fileReader.ReadingComplete();
        }

        private void CheckRegistrationUser()
        {
            
            ClearPromptsRegistration();
            if (LoginRegistration == "")
            {
                PromptsRegistration = "Oбязательное поле";
            }
            if (PasswordRegistration == "")
            {
                PromptsRegistrationPassword = "Oбязательное поле";
            }
            else if (PasswordRegistration != RepeatPasswordRegistration)
            {
                PromptsRegistrationPassword = "Пароли не совпадают";
                PromptsRegistrationRepeatPassword = "Пароли не совпадают";
            }
            if (RepeatPasswordRegistration == "")
            {
                PromptsRegistrationRepeatPassword = "Oбязательное поле";
            }
            else if (PasswordRegistration != RepeatPasswordRegistration)
            {
                PromptsRegistrationPassword = "Пароли не совпадают";
                PromptsRegistrationRepeatPassword = "Пароли не совпадают";
            }
            else if (IsRegistrationPass == false)
            {
                PromptsRegistrationRepeatPassword = "Такой пользователь уже существует";
            }
            else authorizationModel.RegistrationUser(LoginRegistration, PasswordRegistration);
            if (IsRegistrationPass)
                IsVisibleTopButtons = Visibility.Visible;
        }

        private void CheckEntryUser()
        {
            ClearPromptsEntry();
            if (LoginEntry == "")
            {
                PromptsEntry = "Oбязательное поле";
            } 
            if (PasswordEntry == "")
            {
                PromptsEntryPassword = "Oбязательное поле";
            }
            if(PasswordEntry != "" && LoginEntry != "")
            {
                if(loginPassword.Count == 0)
                {
                    PromptsEntryPassword = "Зарегистрированных пользователей не найдено. Пройдите регистрацию";
                }
                else if(IsLogin == false)
                {
                    PromptsEntryPassword = "Неверный логин или пароль";
                }   
            }
            if(IsLogin)
                IsVisibleTopButtons = Visibility.Visible;

            //IsVisibleTopButtons = authorizationModel.CheckValid(IsLogin, LoginEntry);
        }

        private void CheckFileData()
        {      

            if(File.Exists(ZipPathToLogins))
            {
                compress.DecompressData(ZipPathToLogins);
            }

            if (File.Exists(PathToLogins))
            {
                using (StreamReader sr = new StreamReader(PathToLogins, Encoding.Unicode))
                {
                    while (true)
                    {
                        string temp = sr.ReadLine();
                        if (temp == null || temp.Length < 1) break;
                        string[] lines = temp.Split('-');
                        loginPassword.Add(lines[0], lines[1]);
                    }
                }
                File.Delete(PathToLogins);
            }
        }

        private void CompareLoginPassword()
        {
            if (loginPassword.Count != 0)
            {
                foreach (var key in loginPassword)
                {
                    if (LoginEntry == key.Key && _realPassword == key.Value)
                    {
                        IsLogin = true;
                        StaticData.User = LoginEntry;
                        break;
                    }
                    else
                    {
                        IsLogin = false;
                        StaticData.User = "";
                    }
                }
            }
        }

        private void ClearPromptsRegistration()
        {
            PromptsRegistration = "";
            PromptsRegistrationPassword = "";
            PromptsRegistrationRepeatPassword = "";
        }

        private void ClearPromptsEntry()
        {
            PromptsEntry = "";
            PromptsEntryPassword = "";
        }

        #endregion

        #region Properties

        public bool IsRegistrationPass
        {
            get { return isRegistrationPass; }
            set
            {
                isRegistrationPass = value;
                OnPropertyChanged("IsRegistrationPass");
            }
        }

        public bool IsLogin
        {
            get { return isLogin; }
            set
            {
                isLogin = value;
                OnPropertyChanged("IsLogin");
            }
        }

        public Visibility IsVisibleTopButtons
        {
            get { return _isVisibleTopButtons; }
            set
            {
                _isVisibleTopButtons = value;
                OnPropertyChanged("IsVisibleTopButtons");
            }
        }


        public string LoginEntry
        {
            get { return loginEntry; }
            set
            {
                
                loginEntry = value; 
                OnPropertyChanged("LoginEntry");
                CompareLoginPassword();
            }
        }

        public string PasswordEntry
        {
            get { return passwordEntry; }
            set
            {
                _realPassword = authorizationModel.HidePassword(value, _realPassword);
               
                passwordEntry = "";
                for (int i = 0; i < _realPassword.Length; i++)
                {
                    passwordEntry += "*";
                }
           
                OnPropertyChanged("PasswordEntry");
                CompareLoginPassword();
            }
        }

        public string StatusExpert
        {
            get { return _statusExpert; }
            set
            {
                _statusExpert = value;
                OnPropertyChanged("StatusExpert");
            }
        }

        public string LvlExpert
        {
            get { return _lvlExpert; }
            set
            {
                _lvlExpert = value;
                OnPropertyChanged("LvlExpert");
            }
        }

        public string CompleteAppealsExpert
        {
            get { return _completeAppealsExpert; }
            set
            {
                _completeAppealsExpert = value;
                OnPropertyChanged("CompleteAppealsExpert");
            }
        }

        public string ExpertName
        {
            get { return _expertName; }
            set
            {
                _expertName = value;
                OnPropertyChanged("ExpertName");
            }
        }


        public string PromptsEntry
        {
            get { return promptsEntry; }
            set
            {
                promptsEntry = value;
                OnPropertyChanged("PromptsEntry");
            }
        }

        public string PromptsEntryPassword
        {
            get { return promptsEntryPassword; }
            set
            {
                promptsEntryPassword = value;
                OnPropertyChanged("PromptsEntryPassword");
            }
        }

        public string PromptsRegistration
        {
            get { return promptsRegistration; }
            set
            {
                promptsRegistration = value;
                OnPropertyChanged("PromptsRegistration"); 
            }
        }

        public string PromptsRegistrationPassword
        {
            get { return promptsPasswordRegistration; }
            set
            {
                promptsPasswordRegistration = value;
                OnPropertyChanged("PromptsRegistrationPassword");
               
            }
        }

        public string PromptsRegistrationRepeatPassword
        {
            get { return promptsPasswordRegistrationRepeat; }
            set
            {
                promptsPasswordRegistrationRepeat = value;
                OnPropertyChanged("PromptsRegistrationRepeatPassword");
               
            }
        }

        public string LoginRegistration
        {
            get { return loginRegistration; }
            set
            {
                loginRegistration = value;
                OnPropertyChanged("LoginRegistration");
                loginRegistration = authorizationModel.CheckLenghtLoginRegistration(LoginRegistration);
                CheckValidRegistration();
                //CheckRegistrationUser();
            }
        }

        private void CheckValidRegistration()
        {
            if (LoginRegistration != "" && PasswordRegistration != "" && RepeatPasswordRegistration != "" && (PasswordRegistration == RepeatPasswordRegistration))
                IsRegistrationPass = authorizationModel.CheckLoginRegistration(LoginRegistration, loginPassword.Keys);
            if (IsRegistrationPass) StaticData.User = LoginRegistration;
        }

        public string PasswordRegistration
        {
            get { return passwordRegistration; }
            set
            {
                passwordRegistration = value; 
                OnPropertyChanged("PasswordRegistration");
                //CheckRegistrationUser();
                CheckValidRegistration();
            }
        }

        public string RepeatPasswordRegistration
        {
            get { return repeatPasswordRegistration; }
            set
            {
                repeatPasswordRegistration = value;
                OnPropertyChanged("RepeatPasswordRegistration");
                //CheckRegistrationUser();
                CheckValidRegistration();

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

        public interface ICommand
        {
            void Execute(object param);
            bool CanExecute(object param);
            event EventHandler<object> CanExecuteChanged;
        }

    }
}
