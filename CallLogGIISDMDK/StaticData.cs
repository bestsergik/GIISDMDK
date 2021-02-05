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
       static private string _userStatus;
       static private int _userLvl = 0;

        static public string User
        {
            get { return _user; }
            set { _user = value; }
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

        private static void DefineUserStatus()
        {
            if (UserLvl < 10)
                _userStatus = "малыш";
            else if (UserLvl > 9 && UserLvl < 20)
                _userStatus = "Новичёк";
            else if (UserLvl > 19 && UserLvl < 40)
                _userStatus = "Бывалый";
            else if (UserLvl > 39 && UserLvl < 100)
                _userStatus = "Опытный";
            else if (UserLvl > 99 && UserLvl < 300)
                _userStatus = "Эксперт";
            else if (UserLvl > 300 )
                _userStatus = "Советник Бога";
        }
    }
}
