using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CallLogGIISDMDK.Models;

namespace CallLogGIISDMDK.ViewModels
{
    class ViewAppeals_VM
    {
        private string _pathToZipAppeals = @"CallLog.zip";
        private FillAppeal_VM _selectedAppeal;
        FillAppeal_model _fillAppeal_Model;
        private RelayCommand _updatecCallLogCommand;

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
                List<string[]> appeals = _fillAppeal_Model.FillCallLog();

                foreach (var appeal in appeals)
                {
                        Appeals.Add(new FillAppeal_VM { FullName = appeal[0], InputPhone = appeal[1], CurrentDay = appeal[2], CurrentHour = Convert.ToInt32(appeal[3]), CurrentMinute = appeal[4], Email = appeal[5], Company = appeal[6], ParticipantRole = appeal[7], Appeal = appeal[8], AdditionalInfo = appeal[9], UserName= appeal[10]});
                }
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