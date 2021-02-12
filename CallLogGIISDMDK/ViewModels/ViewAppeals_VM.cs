using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CallLogGIISDMDK.Models;
using CallLogGIISDMDK.WorkWithFiles;

namespace CallLogGIISDMDK.ViewModels
{
    class ViewAppeals_VM : INotifyPropertyChanged
    {

        
        

        private string _pathToZipAppeals = @"Appeals.zip";
        private FillAppeal_VM _selectedAppeal;
        FillAppeal_model _fillAppeal_Model;
        private RelayCommand _updatecCallLogCommand;
        FileReader fileReader = new FileReader();
        List<List<string>> suitableAppeals;
        List<List<string>> _appeals = new List<List<string>>();
        List<string> _appeal = new List<string>();
        string _insertAppeal = String.Empty;

        public ObservableCollection<FillAppeal_VM> Appeals { get; set; }
        public FillAppeal_VM SelectedAppeal
        {
            get { return _selectedAppeal; }
            set
            {
                _selectedAppeal = value;
                OnPropertyChanged("SelectedAppeal");
            }
        }

        public ViewAppeals_VM()
        {
            Appeals = new ObservableCollection<FillAppeal_VM>();
            _fillAppeal_Model = new FillAppeal_model();
            fileReader.onReadingComplete += FileReader_onWrite;
        }

        private void FileReader_onWrite()
        {
            ShowAppeal(_appeals);
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

        private void FillAppeals()
        {
            if (File.Exists(_pathToZipAppeals))
            {
                Appeals.Clear();
                Thread myThread = new Thread(new ThreadStart(FillAppeal));
                myThread.Start();
               
            }
        }


        public List<string> Appeal
        {
            get { return _appeal; }
            set
            {
                _appeal = value;
                OnPropertyChanged("Appeal");
            }
        }

        public string InsertAppeal
        {
            get { return _insertAppeal; }
            set
            {
                _insertAppeal = value;
                OnPropertyChanged("InsertAppeal");
                SearchInsertAppeal(InsertAppeal);
            }
        }

        void ShowAppeal(List<List<string>> appeals)
        {
            foreach (var appeal in appeals)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate // <--- HERE
                {
                    Appeals.Add(new FillAppeal_VM { FullName = appeal[0], Company = appeal[1], InputPhone = appeal[2], Inn = appeal[3], Sity = appeal[4], ParticipantRole = appeal[5], Status = appeal[6], Email = appeal[7],  Ogrn = appeal[8], CurrentDay = appeal[9],  CurrentHour = Convert.ToInt32(appeal[10]), CurrentMinute = appeal[11], Appeal = appeal[12] , AdditionalInfo = appeal[13], UserName = appeal[14] });
                });
            }
        }

        private void SearchInsertAppeal(string insertAppeal)
        {
            suitableAppeals = new List<List<string>>();
            Appeals.Clear();
            suitableAppeals =  _fillAppeal_Model.SearchInsertAppeal(insertAppeal, _appeals);
            ShowAppeal(suitableAppeals);
        }

        private void FillAppeal()
        {
            _appeals = fileReader.GetAppeals();
            fileReader.ReadingComplete();
        }

     

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}