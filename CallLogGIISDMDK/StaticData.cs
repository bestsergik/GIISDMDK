using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogGIISDMDK
{
   static class StaticData
    {
       static private string _user;
       static private string _userStatus = "малыш";
       static private int _userLvl = 0;
       static private int _userTopLvl = 0;
       static private bool _isLoggin = false;
   

        static public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        static public bool IsLoggin
        {
            get { return _isLoggin; }
            set { _isLoggin = value; }
        }

        static public string UserStatus
        {
            get { return _userStatus; }
            set { _userStatus = value; }
        }
        static public int UserLvl
        {
            get { return _userLvl; }
            set {
                _userLvl = value;
                DefineUserStatus();
            }
        }

        static public int UserTopLvl
        {
            get { return _userTopLvl; }
            set
            {
                _userTopLvl = value;
                DefineUserStatus();
            }
        }

        private static void DefineUserStatus()
        {
            if (UserLvl < 10)
                UserStatus = "малыш";
            else if (UserLvl > 9 && UserLvl < 20)
                UserStatus = "Новичёк";
            else if (UserLvl > 19 && UserLvl < 40)
                UserStatus = "Бывалый";
            else if (UserLvl > 39 && UserLvl < 100)
                UserStatus = "Опытный";
            else if (UserLvl > 99 && UserLvl < 300)
                UserStatus = "Эксперт";
            else if (UserLvl > 300)
                UserStatus = "Советник Бога";
        }


        static public void ClearData()
        {
            UserLvl = 0;
            UserTopLvl = 0;
        }
    }
}
