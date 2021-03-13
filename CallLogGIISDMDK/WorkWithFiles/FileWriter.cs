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
        DefinerCorrectPathToAppeals definerPath = new DefinerCorrectPathToAppeals();
        int ID = 1;
        int personalID = 1;
        FileReader fileReader = new FileReader();
        Compress fileArchiving = new Compress();
        //string pathToZipAppeals = @"Appeals.zip";
        //string pathToAppeals = @"appeals.xml";
        string pathToZipAppeals;
        string pathToAppeals;
        public FileWriter()
        {
            pathToZipAppeals = definerPath.GetCorrectPathToAppealsZip();
            pathToAppeals = definerPath.GetCorrectPathToAppealsXml();
        }
        Data data = new Data();
        public void WriteAppealToFile(string fullName, string company, string sity, string phoneNumber, string inn, string role, string type, string status, string email, string ogrn, string date, string appeal, string additionalInfo, string communicationChannel, string time, string route)
        {
            if (File.Exists(pathToZipAppeals))
            {
                ID = data.GetNumberIdAppeal();
                personalID = data.GetNumberPersonalIdAppeal();
            }
            if (File.Exists(pathToAppeals))
                File.Delete(pathToAppeals);
            if (!File.Exists(pathToZipAppeals))
            {
                XDocument appeals = new XDocument(
                   new XComment("Обращения в службу поддержки ГИИС ДМДК"),
                   new XElement("Appeals",
                     new XElement("Appeal",
              new XElement("ID", ID),
              new XElement("date", date),
              new XElement("time", time),
              new XElement("communicationСhannel", communicationChannel),
              new XElement("type", type),
              new XElement("appeal", appeal),
              new XElement("user", StaticData.User),
              new XElement("sity", sity),
              new XElement("role", role),
              new XElement("route", route),
              new XElement("additionalInfo", additionalInfo),
              new XElement("company", company),
              new XElement("fullName", fullName),
              new XElement("phoneNumber", phoneNumber),
              new XElement("email", email),
              new XElement("inn", inn),
              new XElement("ogrn", ogrn),
              new XElement("status", status),
              new XElement("personalID", personalID))
      
                   )
                 );
                appeals.Save(pathToAppeals);
            }
            else
            {
                fileArchiving.DecompressData(pathToZipAppeals);
                XDocument appeals = XDocument.Load(pathToAppeals);
                XElement root = new XElement("Appeal");
                root.Add(new XElement("ID", ID));
                root.Add(new XElement("date", date));
                root.Add(new XElement("time", time));
                root.Add(new XElement("communicationСhannel", communicationChannel));
                root.Add(new XElement("type", type));
                root.Add(new XElement("appeal", appeal));
                root.Add(new XElement("user", StaticData.User));
                root.Add(new XElement("sity", sity));
                root.Add(new XElement("role", role));
                root.Add(new XElement("route", route));
                root.Add(new XElement("additionalInfo", additionalInfo));
                root.Add(new XElement("company", company));
                root.Add(new XElement("fullName", fullName));
                root.Add(new XElement("phoneNumber", phoneNumber));
                root.Add(new XElement("email", email));
                root.Add(new XElement("inn", inn));
                root.Add(new XElement("ogrn", ogrn));
                root.Add(new XElement("status", status));
                root.Add(new XElement("personalID", personalID));

                appeals.Element("Appeals").Add(root);
                appeals.Save(pathToAppeals);
            }
            StaticData.IsNewAppeal = true;
            StaticData.IsNewAppeal2 = true;
            fileArchiving.CompressData(pathToAppeals, pathToZipAppeals);
            File.Delete(pathToAppeals);
        }
    }
}
