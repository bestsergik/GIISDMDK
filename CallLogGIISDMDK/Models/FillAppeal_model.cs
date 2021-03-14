using CallLogGIISDMDK.ViewModels;
using CallLogGIISDMDK.WorkWithFiles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
namespace CallLogGIISDMDK.Models
{
    class FillAppeal_model
    {
        Data data = new Data();
        List<string> minutesAppeal = new List<string>();
        List<string> hoursAppeal = new List<string>();
        List<string> appeals = new List<string>();
        public List<string> suitableAppeal = new List<string>();
        private string _pathToAppeals = @"callLog.txt";
        private string _pathToZipAppeals = @"CallLog.zip";
        public bool isValidFirstStep;
        public bool isValidationAddingDataToAppeal;
        Compress compress = new Compress();
        FileWorker fileWorker = new FileWorker();
        FileWriter fileWriter = new FileWriter();
        List<string> suitableAppeals;
        public List<string> appealsId = new List<string>();
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
                if (i < 10) hoursAppeal.Add("0" + hours[i].ToString());
                else hoursAppeal.Add(hours[i].ToString());
            }
            return hoursAppeal;
        }
        internal List<string> GetParticipantRoles()
        {
            List<string> roles = new List<string>();
            roles.Add("Участник рынка");
            roles.Add("Сотрудник ФПП");
            roles.Add("Сотрудник Гохран");
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
        internal void AddDataToCurrentAppeal(FillAppeal_VM selectedAppeal)
        {
        }
        internal List<string> GetTypesAppeal()
        {
            List<string> types = new List<string>();
            types.Add("Телефон");
            types.Add("Email");
            return types;
        }
        public List<List<string>> GetAppeals()
        {
            return data.GetAppeals();
        }
        internal bool CheckSameAppeals(List<string> appeal)
        {
            bool isIdenticalId = false;
            if (appealsId.Count > 0)
            {
                foreach (var id in appealsId)
                {
                    if (appeal[0] == id)
                    {
                        isIdenticalId = true;
                        break;
                    }
                }
            }
            appealsId.Add(appeal[0]);
            return isIdenticalId;
        }

        internal List<string> GetRoutes()
        {
            List<string> statuses = new List<string>();
            statuses.Add("Входящий");
            statuses.Add("Исходящий");
            return statuses;
        }

        public List<string> CheckSuitableAppeal(string userInput)
        {
            suitableAppeals = new List<string>();
            foreach (var appeal in data.GetAppeals())
            {
                for (int fieldAppeal = 0; fieldAppeal < appeal.Count; fieldAppeal++)
                {
                    if (appeal[fieldAppeal].ContainsWithoutRegistr(userInput, StringComparison.OrdinalIgnoreCase))
                    {
                        suitableAppeal = appeal;
                        suitableAppeals.Add($"{appeal[1]}, {appeal[0]}, {appeal[5]}, {appeal[4]}");
                        break;
                    }
                }
            }
            return suitableAppeals;
        }
        internal List<List<string>> SearchInsertAppeal(string insertAppeal, List<List<string>> appeals, int typeSearch)
        {
            List<List<string>> suitableAppeals = new List<List<string>>();
            foreach (var appeal in appeals)
            {
                if (typeSearch == 1)
                {
                    if (appeal[16].Contains(insertAppeal))
                    {
                        suitableAppeals.Add(appeal);
                    }
                }
                else
                {
                    for (int fieldAppeal = 0; fieldAppeal < appeal.Count; fieldAppeal++)
                    {
                        if (appeal[fieldAppeal].ContainsWithoutRegistr(insertAppeal, StringComparison.OrdinalIgnoreCase))
                        {
                            suitableAppeals.Add(appeal);
                            break;
                        }
                    }
                }
            }
            return suitableAppeals;
        }
        internal string AddTypeAppeal(string Type, string type)
        {
            string typeAppeal = Type;
            if (!Type.Contains(type))
            {
                if (Type == "")
                    typeAppeal = type;
                else typeAppeal += "," + type;
            }
            else
            {
                if (typeAppeal.Substring(0, 1) == ",") typeAppeal = typeAppeal.Substring(1, typeAppeal.Length - 1);
                if (typeAppeal.Substring(typeAppeal.Length - 1, 1) == ",") typeAppeal = typeAppeal.Substring(0, typeAppeal.Length - 1);
                foreach (var item in Type)
                {
                    typeAppeal = typeAppeal.Replace(type, "");
                    if (typeAppeal.Contains(",,"))
                        typeAppeal = typeAppeal.Replace(",,", ",");
                }
                if (typeAppeal.Length > 0 && typeAppeal.Substring(typeAppeal.Length - 1, 1) == ",") typeAppeal = typeAppeal.Substring(0, typeAppeal.Length - 1);
                if (typeAppeal.Length > 0 && typeAppeal.Substring(0, 1) == ",") typeAppeal = typeAppeal.Substring(1, typeAppeal.Length - 1);
            }
            return typeAppeal;
        }
        internal List<string> GetStatusesAppeal()
        {
            List<string> statuses = new List<string>();
            statuses.Add("Закрыто");
            statuses.Add("Открыто");
            return statuses;
        }
        internal string[] CheckLeghtFields(string fullName, string inputPhone, string email, string company, string participantRole, string status, bool isReggular, string inn, string sity, string ogrn, string communicationСhannel, string type, string currentMinute)
        {
            string[] prompts = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
            if (fullName == null || fullName.Length < 1)
            {
                prompts[0] = "Обязательное поле";
            }
            if ((inputPhone == null && communicationСhannel != "Email") || (inputPhone != null && inputPhone.Length < 1 && communicationСhannel != "Email"))
            {
                prompts[1] = "Обязательное поле";
            }
            else if (inputPhone != null && inputPhone.Length > 0 && inputPhone.Length < 15)
                prompts[1] = "Некорректный номер телефона";
            else prompts[1] = "";
            //if (inputPhone == null || inputPhone.Length < 1)
            //{
            //    prompts[1] = "Обязательное поле";
            //}
            //if(inputPhone == null && communicationСhannel == "Email")
            //    prompts[1] = "";
            //else if(inputPhone != null && inputPhone.Length < 1 && communicationСhannel == "Email")
            //    prompts[1] = "";
            //if (inputPhone != null && inputPhone.Length > 0 && inputPhone.Length < 15)
            //    prompts[1] = "Некорректный номер телефона";
            if (isReggular && inputPhone != null && inputPhone.Length > 0) prompts[1] = "";
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
            if ((email == null || email.Length < 1) && communicationСhannel == "Email")
                prompts[2] = "Обязательное поле";
            if (company == null || company.Length < 1)
            {
                //prompts[3] = "Обязательное поле";
                prompts[3] = "";
            }
            if (participantRole == null || participantRole.Length < 1)
            {
                prompts[4] = participantRole = "Обязательное поле";
            }
            if (status == null || status.Length < 1)
            {
                prompts[5] = status = "Обязательное поле";
            }
            if (inn == null || inn.Length < 1)
            {
                prompts[6] = "";
                //prompts[6] = "Обязательное поле";
            }
            if (inn != null && inn.Length > 0 && inn.Length < 10)
                prompts[6] = "Некорректный ИНН";
            if (inn != null && inn.Length > 0 && inn.Length == 11)
                prompts[6] = "Некорректный ИНН";
            if (sity == null || sity.Length < 1)
            {
                prompts[7] = sity = "";
                //prompts[7] = sity = "Обязательное поле";
            }
            if (ogrn != null && ogrn.Length > 0 && ogrn.Length < 13)
                prompts[8] = "Некорректный ОГРН";
            if (ogrn != null && ogrn.Length > 0 && ogrn.Length == 14)
                prompts[8] = "Некорректный ОГРН";
            if (communicationСhannel == null || communicationСhannel.Length < 1)
            {
                prompts[9] = communicationСhannel = "Обязательное поле";
            }
            if (type == null || type.Length < 1)
            {
                prompts[10] = type = "Обязательное поле";
            }
            if (currentMinute == null || currentMinute.Length < 1)
            {
                prompts[11] = currentMinute = "Обязательное поле";
            }
            for (int i = 0; i < prompts.Length; i++)
            {
                if (prompts[i] != "")
                {
                    isValidFirstStep = false;
                    break;
                }
                else isValidFirstStep = true;
            }
            return prompts;
        }
        internal string[] GetTypeConnect(object typeConnect)
        {
            string[] data = new string[2];
            if (typeConnect.ToString() == "in")
            {
                data[0] = "Входящее";
                data[1] = "Телефон";
            }
            else if (typeConnect.ToString() == "out")
            {
                data[0] = "Исходящее";
                data[1] = "Телефон";
            }
            else if (typeConnect.ToString() == "inEmail")
            {
                data[0] = "Входящее";
                data[1] = "Email";
            }
            else
            {
                data[0] = "Исходящее";
                data[1] = "Email";
            }
            return data;
        }
        internal string[] CheckPrompts(string phone, string email, string status, string communicationChannel, string appeal, string type, string minute, bool isReggular)
        {
            string[] prompts = new string[7] { "", "", "", "", "", "", "" };
            if (phone == null || phone.Length < 1)
            {
                prompts[0] = "Обязательное поле";
            }
            if (phone != null && phone.Length > 0 && phone.Length < 15)
                prompts[0] = "Некорректный номер телефона";
            if ((isReggular && phone != null && phone.Length > 0) || communicationChannel == "Email") prompts[0] = "";

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
                    prompts[1] = "Некорректный email";
                }
            }
            else if (communicationChannel == "Email") prompts[1] = "Обязательное поле";


            if (status == null || status.Length < 1)
            {
                prompts[2] = status = "Обязательное поле";
            }
            if (communicationChannel == null || communicationChannel.Length < 1)
            {
                prompts[3] = communicationChannel = "Обязательное поле";
            }
            if (appeal == null || appeal.Length < 1)
            {
                prompts[4] = appeal = "Обязательное поле";
            }
            if (type == null || type.Length < 1)
            {
                prompts[5] = type = "Обязательное поле";
            }
            if (minute == null || minute.Length < 1)
            {
                prompts[6] = minute = "Обязательное поле";
            }
            for (int i = 0; i < prompts.Length; i++)
            {
                if (prompts[i] != "")
                {
                    isValidationAddingDataToAppeal = false;
                    break;
                }
                else isValidationAddingDataToAppeal = true;
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
            return firstPart + currentMinute.ToString();
        }
        internal void SaveAppeal(string fullName, string inputPhone, string currentDay, int currentHour, string currentMinute, string email, string company, string participantRole, string appeal, string additionalInfo)
        {
            if (File.Exists(_pathToZipAppeals))
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
            if (lastChar != "е")
            {
                if (userInput.Length < 16)
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
                            if (userInput.Length == 4 && userInput.Substring(0) == "(")
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
                return userInput.Substring(0, userInput.Length - 1);
            }
            else return "Обращение по почте";
        }
        internal string CheckPhone(string userInput)
        {
            string lastChar = userInput.Substring(userInput.Length - 1, 1);
            if (lastChar != "е")
            {
                if (userInput.Length < 16)
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
                            if (userInput.Length == 4 && userInput.Substring(0) == "(")
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
                return userInput.Substring(0, userInput.Length - 1);
            }
            else return "Обращение по почте";
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
        internal string CheckInputInn(string inn)
        {
            string lastChar = inn.Substring(inn.Length - 1, 1);
            if (inn.Length < 13)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (lastChar == i.ToString())
                    {
                        return inn;
                    }
                }
            }
            return inn.Substring(0, inn.Length - 1);
            //if (inn.Length < 13)
            //{
            //    return inn;
            //}
            //return inn.Substring(0, inn.Length - 1);
        }
        internal string CheckInputOgrn(string ogrn)
        {
            string lastChar = ogrn.Substring(ogrn.Length - 1, 1);
            if (ogrn.Length < 16)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (lastChar == i.ToString())
                    {
                        return ogrn;
                    }
                }
            }
            return ogrn.Substring(0, ogrn.Length - 1);
        }
    }
}

