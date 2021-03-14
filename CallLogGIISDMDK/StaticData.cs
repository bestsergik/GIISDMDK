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
        static private bool _isNewAppeal = false;
        static private bool _isNewAppeal2 = false;
        static private int _currentId = 0;
        static private string _correctPathToZip = "";
        static private string _correctPathToXml = "";
        static private string _currentPersonalId = "";
        static private List<List<string>> _dataAppealByPersonalID;

        static private string _phone;
        static private string _email;


        static public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        static public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        static public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        static public int CurrentId
        {
            get { return _currentId; }
            set { _currentId = value; }
        }
        static public string CurrentPersonalId
        {
            get { return _currentPersonalId; }
            set { _currentPersonalId = value; }
        }

        static public string CorrectPathToXml
        {
            get { return _correctPathToXml; }
            set { _correctPathToXml = value; }
        }
        static public string CorrectPathToZip
        {
            get { return _correctPathToZip; }
            set { _correctPathToZip = value; }
        }

        static public List<List<string>> DataAppealByPersonalID
        {
            get { return _dataAppealByPersonalID; }
            set { _dataAppealByPersonalID = value; }
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
            set
            {
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
        static public bool IsNewAppeal
        {
            get { return _isNewAppeal; }
            set
            {
                _isNewAppeal = value;
            }
        }
        static public bool IsNewAppeal2
        {
            get { return _isNewAppeal2; }
            set
            {
                _isNewAppeal2 = value;
            }
        }
        public static bool ContainsWithoutRegistr(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
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
