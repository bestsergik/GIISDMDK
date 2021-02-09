using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace CallLogGIISDMDK.Models
{
    class FillAppeal_model
    {
        List<string>  minutesAppeal = new List<string>();
        List<string> hoursAppeal = new List<string>();

        private string _pathToAppeals = @"callLog.txt";
        private string _pathToZipAppeals = @"CallLog.zip";

        public bool isValidFirstStep;

        Compress compress = new Compress();
        FileWorker fileWorker = new FileWorker();




        internal List<string> GetMinutesAppeal()
        {
            int stepMinutes = 0;
            int[] minutes = new int[12];

            // minutesAppeal = new string[11];
            for (int i = 0; i < minutes.Length; i++)
            {
                minutes[i] += stepMinutes;
                if (stepMinutes < 10) minutesAppeal.Add("0" + minutes[i].ToString());
                else minutesAppeal.Add(minutes[i].ToString());
                stepMinutes += 5;
            }
            return minutesAppeal;
        }

        internal List<string> GetHoursAppeal()
        {
            int[] hours = new int[25];
            
            for (int i = 0; i < hours.Length; i++)
            {
                hours[i] += i;
                if(i< 10) hoursAppeal.Add("0" + hours[i].ToString());
                else hoursAppeal.Add(hours[i].ToString());

            }
            return hoursAppeal;
        }

        internal List<string> GetParticipantRoles()
        {
            List<string> roles = new List<string>();
            roles.Add("Участник рынка");
            roles.Add("Сотрудник ФПП");
            return roles;
        }

        internal List<string[]> FillCallLog()
        {
            List<string[]> appeals = new List<string[]>();
            compress.DecompressData(_pathToZipAppeals);
            appeals = fileWorker.GetAppeals();
            File.Delete(_pathToAppeals);
            return appeals;
                //using (StreamReader sr = new StreamReader(_pathToAppeals, Encoding.Unicode))
                //{
                //    int lenghtAppeal = 0;
                //    string temp;
                //    while (true)
                //    {
                //        temp = sr.ReadLine();

                //        if (temp == null || temp.Length < 1) break;
                //        lenghtAppeal++;
                //        while (true)
                //        {
                //            temp += sr.ReadLine();
                //            if (temp.Substring(temp.Length - 5, 5) == "%%#%%")
                //            {
                //                lenghtAppeal++;
                //                if (lenghtAppeal == 11)
                //                    break;
                //            }
                //            else temp += "\r";
                //        }
                //        string[] symbolsSplit = new string[] { "%%#%%" };
                //        string[] lines = temp.Split(symbolsSplit, StringSplitOptions.None);
                //        appeals.Add(lines);
                //        lenghtAppeal = 0;
                //    }
                //}
            
            //File.Delete(_pathToAppeals);
            //return appeals;
        }

        internal List<string> GetStatusesAppeal()
        {
            List<string> statuses = new List<string>();
            statuses.Add("Закрыто");
            statuses.Add("Открыто");
            statuses.Add("Срочно");
            return statuses;
        }

        internal string[] CheckLeghtFields(string fullName, string inputPhone, string email, string company, string participantRole, string status, bool isReggular)
        {
            string[] prompts = new string[6] { "", "", "", "", "", ""};
            if (fullName == null || fullName.Length < 1)
            {
                prompts[0]  = "Обязательное поле";
            }
       
            if (inputPhone == null || inputPhone.Length < 1)
            {
                prompts[1] = "Обязательное поле";
            }
            if (inputPhone != null && inputPhone.Length > 0 && inputPhone.Length < 15)
                prompts[1] = "Некорректный номер телефона";
            if (isReggular && inputPhone.Length > 0) prompts[1] = "";

            if (email != null && email.Length > 0)
            {
                bool isAt = false;
                bool isPoint = false;
                foreach (var item in email)
                {
                    if (item == Convert.ToChar(".")) 
                        isPoint = true;
                    if (item == Convert.ToChar("@"))
                        isAt = true;
                }
                if (isAt == false || isPoint == false)
                {
                    prompts[2] = "Некорректный email";
                }
            }
         
            if (participantRole == null || participantRole.Length < 1)
            {
                prompts[4] = participantRole = "Обязательное поле";
            }

            if (status == null || status.Length < 1)
            {
                prompts[5] = status = "Обязательное поле";
            }

            for (int i = 0; i < prompts.Length; i++)
            {
                if (prompts[i] != "" )
                {
                    isValidFirstStep = false;
                    break;
                }
                else isValidFirstStep = true;
            }
            return prompts;
        }

        

        internal string NearMinute()
        {
            string firstPart = "0";
            int currentMinute = DateTime.Now.Minute;
            if (currentMinute > 9)
            {
                firstPart = currentMinute.ToString().Substring(0, 1);
                currentMinute = Convert.ToInt32(currentMinute.ToString().Substring(1));
            }
                
            while (true)
            {            
                if (currentMinute == 0 || currentMinute == 5)
                {
                    break;
                }
                    
                else currentMinute--; 
            }
            return firstPart +currentMinute.ToString();
        }

        internal void SaveAppeal(string fullName, string inputPhone, string currentDay, int currentHour, string currentMinute, string email, string company, string participantRole, string appeal, string additionalInfo)
        {
            if(File.Exists(_pathToZipAppeals))
            {
                compress.DecompressData(_pathToZipAppeals);
            }
            else 
            {
                using (FileStream fs = File.Create(_pathToAppeals))
                {
                    fs.Close();
                }
            }

            using (StreamWriter str = new StreamWriter(_pathToAppeals, true, Encoding.Unicode))
            {
                str.WriteLine($"{fullName}%%#%%");
                str.WriteLine($"{inputPhone}%%#%%");
                str.WriteLine($"{currentDay}%%#%%");
                str.WriteLine($"{currentHour}%%#%%");
                str.WriteLine($"{currentMinute}%%#%%");
                str.WriteLine($"{email}%%#%%");
                str.WriteLine($"{company}%%#%%");
                str.WriteLine($"{participantRole}%%#%%");
                str.WriteLine($"{appeal}%%#%%");
                str.WriteLine($"{additionalInfo}%%#%%");
                str.WriteLine($"{StaticData.User}%%#%%");
            }
            
            fileWorker.GetUserStatus();
            File.Delete(_pathToZipAppeals);
            compress.CompressData(_pathToAppeals, _pathToZipAppeals);
            File.Delete(_pathToAppeals);
        }

        internal string CheckInputNumber(string userInput)
        {
            string lastChar = userInput.Substring(userInput.Length - 1, 1);
            if(userInput.Length < 16)
            {
                Console.WriteLine(userInput.Substring(0, 1));
                if (userInput.Length == 5 || userInput.Length == 6 && userInput.Substring(0, 1) == "(")
                {
                    return userInput.Substring(1, 2);
                }
                for (int i = 0; i < 10; i++)
                {
                    if (lastChar == i.ToString())
                    {
                        if(userInput.Length == 4 && userInput.Substring(0) == "(")
                        {
                            userInput.Trim('('); userInput.Trim(')');
                        }
                        if (userInput.Length == 3)
                            return "(" + userInput + ") ";
                        if (userInput.Length == 9)
                            return userInput + " ";
                        if (userInput.Length == 12)
                            return userInput + " ";
                        return userInput;
                    }
                }
            }
          
            return userInput.Trim(Convert.ToChar(lastChar));
        }

        internal List<DateTime> GetDates(int year, int month)
        {
            Enumerable.Range(1, DateTime.DaysInMonth(year, month));
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }

        internal List<string> FillDaysCurrentMonth()
        {
            List<DateTime> daysCurrentMonth = GetDates(DateTime.Now.Year, DateTime.Now.Month);
            List<string> daysCurrentMonthStringFormat = new List<string>(); 
            daysCurrentMonthStringFormat = GetDaysUntillToday(daysCurrentMonth);
            return daysCurrentMonthStringFormat;
        }

        List<string> GetDaysUntillToday(List<DateTime> daysCurrentMonth)
        {
            List<string> daysUntillToday = new List<string>();
            int currentDay = DateTime.Now.Day;
            foreach (var days in daysCurrentMonth)
            {
                if (days.Day <= currentDay)
                    daysUntillToday.Add(days.ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("RU-RU")));
                else break;
            }
            return daysUntillToday;
        }

        internal string ClearPrompt(string property, string prompt)
        {
            if (property != null && property.Length > 0)
                return prompt = "";
            else return prompt;
        }
    }
}
