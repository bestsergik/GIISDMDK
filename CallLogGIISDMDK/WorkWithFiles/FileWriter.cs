using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CallLogGIISDMDK.WorkWithFiles
{
    class FileWriter
    {
        FileReader fileReader = new FileReader();
        Compress fileArchiving = new Compress();
        string pathToZipAppeals = @"Appeals.zip";
        string pathToAppeals = @"appeals.xml";

        public void WriteAppealToFile(string fullName, string company, string phoneNumber, string inn, string sity, string role, string status, string email, string ogrn, string date, string currentHour, string currentMinute,  string appeal, string additionalInfo)
        {
            if (File.Exists(pathToAppeals))
                File.Delete(pathToAppeals);
            if (!File.Exists(pathToZipAppeals))
            {
                XDocument appeals = new XDocument(
                   new XComment("Обращения в службу поддержки ГИИС ДМДК"),
                   new XElement("Appeals",
                      new XElement("Appeal",
                      new XElement("fullName", fullName),
                      new XElement("company", company),
                      new XElement("phoneNumber", phoneNumber),
                      new XElement("inn", inn),
                      new XElement("sity", sity),
                      new XElement("role", role),
                      new XElement("status", status),
                      new XElement("email", email),
                      new XElement("ogrn", ogrn),
                      new XElement("date", date),
                      new XElement("currentHour", currentHour),
                      new XElement("currentMinute", currentMinute),
                      new XElement("appeal", appeal),
                      new XElement("additionalInfo", additionalInfo),
                      new XElement("user", StaticData.User))
                   )
                 );
                appeals.Save(pathToAppeals);
            }
            else
            {
                fileArchiving.DecompressData(pathToZipAppeals);
                XDocument appeals = XDocument.Load(pathToAppeals);
                
                XElement root = new XElement("Appeal");
                root.Add(new XElement("fullName", fullName));
                root.Add(new XElement("company", company));
                root.Add(new XElement("phoneNumber", phoneNumber));
                root.Add(new XElement("inn", inn));
                root.Add(new XElement("sity", sity));
                root.Add(new XElement("role", role));
                root.Add(new XElement("status", status));
                root.Add(new XElement("email", email));
                root.Add(new XElement("ogrn", ogrn));
                root.Add(new XElement("date", date));
                root.Add(new XElement("currentHour", currentHour));
                root.Add(new XElement("currentMinute", currentMinute));
                root.Add(new XElement("appeal", appeal));
                root.Add(new XElement("additionalInfo", additionalInfo));
                root.Add(new XElement("user", StaticData.User));
                appeals.Element("Appeals").Add(root);
                appeals.Save(pathToAppeals);
            }
            StaticData.IsNewAppeal = true;
            fileArchiving.CompressData(pathToAppeals, pathToZipAppeals);
            File.Delete(pathToAppeals);
        }
    }
}
