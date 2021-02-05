using CallLogGIISDMDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CallLogGIISDMDK.ViewModels
{
    class CommonInformation_VM : INotifyPropertyChanged
    {
        CommonInformation_model commonInformation = new CommonInformation_model();

        public ObservableCollection<CommonInformation_model> CommonInformation { get; set; }

        public CommonInformation_VM()
        {
            FillCommonInformation();
         
       
         
        }

        #region Commands

        #endregion

        #region Methods
        private void FillCommonInformation()
        {
            List<string> generalInformation;
            List<string> reductions;
            reductions = commonInformation.GetReductions();
            generalInformation = commonInformation.GetGeneralInformation();
            CommonInformation = new ObservableCollection<CommonInformation_model>();
            int maxLenght = (reductions.Count > generalInformation.Count) ? reductions.Count : generalInformation.Count;

            for (int i = 0; i < maxLenght; i+=2)
            {
                if (reductions.Count > generalInformation.Count)
                {
                    if(generalInformation.Count > i)
                    CommonInformation.Add(new CommonInformation_model
                    {

                        TitleInformation = generalInformation[i],
                        GeneralInformation = generalInformation[i + 1],
                        Reduction = reductions[i],
                        Decryption = reductions[i + 1]
                    });
                    else
                    {
                        CommonInformation.Add(new CommonInformation_model
                        {
                            Reduction = reductions[i],
                            Decryption = reductions[i + 1]
                        });
                    }
                }   
                else
                {
                    if (reductions.Count > i)
                        CommonInformation.Add(new CommonInformation_model
                        {

                            TitleInformation = generalInformation[i],
                            GeneralInformation = generalInformation[i + 1],
                            Reduction = reductions[i],
                            Decryption = reductions[i + 1]
                        });
                    else
                    {
                        CommonInformation.Add(new CommonInformation_model
                        {
                            TitleInformation = generalInformation[i],
                            GeneralInformation = generalInformation[i + 1],
                        });
                    }
                }
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
