using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CallLogGIISDMDK.Models
{
    class Authorization_model
    {
        Compress compress = new Compress();

        private string PathToLogins = @"logins.txt";
        private string _zipPathToLogins = @"UserLogins.zip";
        //private string _pathToAppeals = @"callLog.txt";
        //private string _pathToZipAppeals = @"CallLog.zip";
        FileWorker fileWorker = new FileWorker();


        //public Authorization_model()
        //{
        //    if (File.Exists(_pathToZipAppeals))
        //    {
        //        compress.DecompressCallLog();
        //        fileWorker.GetUserStatus();
        //        File.Delete(_pathToAppeals);
        //    }
        //    else StaticData.UserStatus = "малыш";
        
        //}

        internal string CheckLenghtLoginRegistration(string loginRegistration)
        {
         if (loginRegistration.Length > 23) return loginRegistration.Substring(0, loginRegistration.Length - 1);
         else return loginRegistration;
        }

        internal bool CheckLoginRegistration(string loginRegistration, Dictionary<string, string>.KeyCollection loginPassword)
        {
            bool isPass = true;
            foreach (var login in loginPassword)
            {
                if (login == loginRegistration)
                {
                    isPass = false;
                    break;
                }
                else isPass = true;
            }
            return isPass;
            
           
        }

        internal string HidePassword(string value, string realPassword)
        {
            if (value.Length > realPassword.Length)
            {
                realPassword += value.Substring(value.Length - 1);
            }
            else if (value.Length > 0)
                realPassword = realPassword.Substring(0, realPassword.Length - 1);
            else realPassword = "";
            return realPassword;
        }

        internal void RegistrationUser(string loginRegistration, string passwordRegistration)
        {
            if (!File.Exists(_zipPathToLogins))
            {
                using (FileStream fs = File.Create(PathToLogins))
                {
                    fs.Close();
                }
            }
            else
            {
                compress.DecompressData(_zipPathToLogins);
            }

            using (StreamWriter str = new StreamWriter(PathToLogins, true, Encoding.Unicode))
            {
                str.WriteLine($"{loginRegistration}-{passwordRegistration}");
                StaticData.User = loginRegistration;
            }
            compress.CompressData(PathToLogins, _zipPathToLogins);
            File.Delete(PathToLogins);
        }
    }
}
