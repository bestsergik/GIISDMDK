using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace CallLogGIISDMDK.WorkWithFiles
{
    class FileReader
    {
        Compress fileArchiving = new Compress();
        string pathToZipAppeals = @"Appeals.zip";
        string pathToAppeals = @"appeals.xml";
        public delegate void MethodContainer();
        public event MethodContainer onReadingComplete;

        public List<List<string>> GetAppeals()
        {
            StaticData.ClearData();
            fileArchiving.DecompressData(pathToZipAppeals);
            List<List<string>> appeals = new List<List<string>>();
            List<string> appeal;
            XDocument xdoc = XDocument.Load(pathToAppeals);
            foreach (XElement currentAppeal in xdoc.Element("Appeals").Elements("Appeal"))
            {
                appeal = new List<string>();
                XElement fullNameElement = currentAppeal.Element("fullName");
                XElement companyElement = currentAppeal.Element("company");
                XElement sityElement = currentAppeal.Element("sity");
                XElement phoneNumberElement = currentAppeal.Element("phoneNumber");
                XElement innElement = currentAppeal.Element("inn");
                XElement roleElement = currentAppeal.Element("role");
                XElement typeElement = currentAppeal.Element("type");
                XElement statusElement = currentAppeal.Element("status");
                XElement emailElement = currentAppeal.Element("email");
                XElement ogrnElement = currentAppeal.Element("ogrn");
                XElement dateElement = currentAppeal.Element("date");
                XElement currentHourElement = currentAppeal.Element("currentHour");
                XElement currentMinuteElement = currentAppeal.Element("currentMinute");
                XElement appealElement = currentAppeal.Element("appeal");
                XElement additionalInfoElement = currentAppeal.Element("additionalInfo");
                XElement userElement = currentAppeal.Element("user");

                appeal.Add(fullNameElement.Value);
                appeal.Add(companyElement.Value);
                appeal.Add(sityElement.Value);
                appeal.Add(phoneNumberElement.Value);
                appeal.Add(innElement.Value);
                appeal.Add(roleElement.Value);
                appeal.Add(typeElement.Value);
                appeal.Add(statusElement.Value);
                appeal.Add(emailElement.Value);
                appeal.Add(ogrnElement.Value);
                appeal.Add(dateElement.Value);
                appeal.Add(currentHourElement.Value);
                appeal.Add(currentMinuteElement.Value);
                appeal.Add(appealElement.Value);
                appeal.Add(additionalInfoElement.Value);
                appeal.Add(userElement.Value);
                appeals.Add(appeal);
                FillDataUser(statusElement.Value, userElement.Value);
            }

            File.Delete(pathToAppeals);
            appeals.Reverse();
            return appeals;
        }
        internal void ReadingComplete()
        {
            onReadingComplete();
        }
        private void FillDataUser(string status, string user)
        {
            if (user == StaticData.User)
            {
                StaticData.UserLvl++;
                if (status == "Закрыто")
                {
                    StaticData.UserTopLvl++;
                }
            }
        }
    }
}

