using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CallLogGIISDMDK.WorkWithFiles
{
    class DefinerAvailabilityAppealsByDate
    {
        DefinerCorrectPathToAppeals definerCorrectPathToAppeals = new DefinerCorrectPathToAppeals();
        List<int> Years = new List<int>();
        DateTime currentDate = DateTime.Now;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;

        bool isHAvailabilityAppeals;
        public DefinerAvailabilityAppealsByDate()
        {
            fillYears();
            string currentYear = currentDate.Year.ToString();
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
                if (currentYear.ToString() == year)
                {
                    if (currentMonth == numberMonth)
                        Console.WriteLine();
                }
                else if (numberMonth < 12)
                    numberMonth++;
            }
            else
            {
                if (numberMonth > 1)
                    numberMonth--;
            }
            date[0] = numberMonth.ToString();
            date[1] = year;
            return date;
        }
    }
}
