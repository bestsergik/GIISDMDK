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
        DefinerAvailabilityAppealsByDate definerAvailabilityAppealsByDate = new DefinerAvailabilityAppealsByDate();
        DefinerCorrectPathToAppeals definerPath = new DefinerCorrectPathToAppeals();
        Compress fileArchiving = new Compress();
        //string pathToZipAppeals = @"Appeals.zip";
        //string pathToAppeals = @"appeals.xml";
        string pathToZipAppeals;
        string pathToAppeals;
        public delegate void MethodContainer();
        public event MethodContainer onReadingComplete;
        List<string> idAppeals;
        //string currentIdAppeal;
        public FileReader()
        {
            pathToZipAppeals = definerPath.GetCorrectPathToAppealsZip();
            pathToAppeals = definerPath.GetCorrectPathToAppealsXml();
        }
        public List<List<string>> GetAppeals()
        {
            DefinerCorrectPathToAppeals();
            List<List<string>> appeals = new List<List<string>>();
            if (File.Exists(pathToZipAppeals))
            {
                idAppeals = new List<string>();
                StaticData.ClearData();
                fileArchiving.DecompressData(pathToZipAppeals);
                List<string> appeal;
                XDocument xdoc = XDocument.Load(pathToAppeals);
                foreach (XElement currentAppeal in xdoc.Element("Appeals").Elements())
                {
                    appeal = new List<string>();
                    XElement IDElement = currentAppeal.Element("ID");
                    XElement dateElement = currentAppeal.Element("date");
                    XElement timeElement = currentAppeal.Element("time");
                    XElement communicationСhannelElement = currentAppeal.Element("communicationСhannel");
                    XElement typeElement = currentAppeal.Element("type");
                    XElement detailTypeElement = currentAppeal.Element("detailType");
                    XElement appealElement = currentAppeal.Element("appeal");
                    XElement answerElement = currentAppeal.Element("answer");
                    XElement userElement = currentAppeal.Element("user");
                    XElement sityElement = currentAppeal.Element("sity");
                    XElement roleElement = currentAppeal.Element("role");
                    XElement routeElement = currentAppeal.Element("route");
                    XElement additionalInfoElement = currentAppeal.Element("additionalInfo");
                    XElement companyElement = currentAppeal.Element("company");
                    XElement fullNameElement = currentAppeal.Element("fullName");
                    XElement phoneNumberElement = currentAppeal.Element("phoneNumber");
                    XElement emailElement = currentAppeal.Element("email");
                    XElement innElement = currentAppeal.Element("inn");
                    XElement ogrnElement = currentAppeal.Element("ogrn");
                    XElement statusElement = currentAppeal.Element("status");
                    XElement personalIDElement = currentAppeal.Element("personalID");
                    appeal.Add(IDElement.Value);
                    appeal.Add(dateElement.Value);
                    appeal.Add(timeElement.Value);
                    appeal.Add(communicationСhannelElement.Value);
                    appeal.Add(typeElement.Value);
                    appeal.Add(detailTypeElement.Value);
                    appeal.Add(appealElement.Value);
                    appeal.Add(answerElement.Value);
                    appeal.Add(userElement.Value);
                    appeal.Add(sityElement.Value);
                    appeal.Add(roleElement.Value);
                    appeal.Add(routeElement.Value);
                    appeal.Add(additionalInfoElement.Value);
                    appeal.Add(companyElement.Value);
                    appeal.Add(fullNameElement.Value);
                    appeal.Add(phoneNumberElement.Value);
                    appeal.Add(emailElement.Value);
                    appeal.Add(innElement.Value);
                    appeal.Add(ogrnElement.Value);
                    appeal.Add(statusElement.Value);
                    appeal.Add(personalIDElement.Value);
                    appeals.Add(appeal);
                    FillDataUser(statusElement.Value, userElement.Value);
                }
                File.Delete(pathToAppeals);
                appeals.Reverse();
                return appeals;
            }
            else return appeals;
        }
        private void DefinerCorrectPathToAppeals()
        {
            string[] correctPath = new string[2];
            correctPath = definerAvailabilityAppealsByDate.GetCorrectPathToAppeal();
            pathToZipAppeals = correctPath[0];
            pathToAppeals = correctPath[1];
        }
        bool CheckIdenticalId(List<string> idAppeals, string currentIdAppeal)
        {
            bool isIdenticalId = false;
            if (idAppeals.Count > 0)
            {
                foreach (var idAppeal in idAppeals)
                {
                    if (idAppeal == currentIdAppeal)
                    {
                        isIdenticalId = true;
                        break;
                    }
                }
            }
            return isIdenticalId;
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
