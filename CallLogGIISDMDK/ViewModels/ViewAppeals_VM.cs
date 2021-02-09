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
    class ViewAppeals_VM
    {
        private string _pathToZipAppeals = @"Appeals.zip";
        private string _pathToAppeals = @"appeals.xml";
        private FillAppeal_VM _selectedAppeal;
        FillAppeal_model _fillAppeal_Model;
        private RelayCommand _updatecCallLogCommand;
        FileReader fileReader = new FileReader();
        List<List<string>> appeals = new List<List<string>>();

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
            foreach (var appeal in appeals)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate // <--- HERE
                {
                    Appeals.Add(new FillAppeal_VM { FullName = appeal[0], InputPhone = appeal[1], CurrentDay = appeal[2], CurrentHour = Convert.ToInt32(appeal[3]), CurrentMinute = appeal[4], Status = appeal[5], ParticipantRole = appeal[6], Email = appeal[7], Company = appeal[8], Appeal = appeal[9], AdditionalInfo = appeal[10], UserName = appeal[11] });
                });
            }
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

        private void FillAppeal()
        {
             appeals = fileReader.GetAppeals();
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