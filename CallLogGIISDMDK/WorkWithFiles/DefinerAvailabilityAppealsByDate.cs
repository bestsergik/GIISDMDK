using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CallLogGIISDMDK.WorkWithFiles
{
    class DefinerAvailabilityAppealsByDate
    {
        DefinerCorrectPathToAppeals definerCorrectPathToAppeals = new DefinerCorrectPathToAppeals();
        bool firstEntry = false;
        string correctPathToZip = "";
        string correctPathToXml = "";
        List<int> Years = new List<int>();
        DateTime currentDate = DateTime.Now;
        //int currentMonth = DateTime.Now.Month;
        //int currentYear = DateTime.Now.Year;

        //bool isHAvailabilityAppeals;
        public DefinerAvailabilityAppealsByDate()
        {
            fillYears();
            string currentYear = currentDate.Year.ToString();
            
                correctPathToZip = definerCorrectPathToAppeals.GetCorrectPathToAppealsZip();
                correctPathToXml = definerCorrectPathToAppeals.GetCorrectPathToAppealsXml();
           
        }
        void fillYears()
        {
            int year = currentDate.Year;
            while (currentDate.Year != 2021)
            {
                Years.Add(year);
                fillMonths(year);
                year--;
            }
        }
        private void fillMonths(int year)
        {
            List<int> month = new List<int>();
            if (currentDate.Year != year)
            {
            }
            DateTime currentYear = new DateTime(year, 1, 1);
        }
        internal string[] GetCorrectDate(object route, string month, string year)
        {
            string[] date = new string[2];
            var num = DateTime.Parse($"{month} 1, 2000");
            var numberMonth = num.Month;
            if (route.ToString() == "Next")
            {
                if (numberMonth < 12)
                    numberMonth++;
                if(!CheckAvailabilityAppeals(year, numberMonth.ToString()))
                    numberMonth--;
            }
            else
            {
                if(numberMonth > 1)
                {
                    numberMonth--;
                    if (!CheckAvailabilityAppeals( year, numberMonth.ToString()))
                        numberMonth++;
                }
            }
            date[0] = numberMonth.ToString();
            date[1] = year;
            return date;
        }

        bool CheckAvailabilityAppeals(string year, string month)
        {
            if(File.Exists($"{year}/{month}/Appeals.zip"))
            {
                StaticData.CorrectPathToXml = $"{year}/{month}/Appeals.xml";
                StaticData.CorrectPathToZip = $"{year}/{month}/Appeals.zip";
                //correctPathToZip = $"{year}/{month}/Appeals.zip";
                //correctPathToXml = $"{year}/{month}/Appeals.xml";
                return true;
            }
            else return false;
        }

        public string[] GetCorrectPathToAppeal()
        {
            string[] correctPath = new string[2];
            if(StaticData.CorrectPathToXml == "")
            {
                correctPath[0] = correctPathToZip;
                correctPath[1] = correctPathToXml;
            }
            else
            {
                correctPath[0] = StaticData.CorrectPathToZip;
                correctPath[1] = StaticData.CorrectPathToXml;
            }
           
            return correctPath;
        }
    }
}
