using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CallLogGIISDMDK.WorkWithFiles
{

    public class DefinerCorrectPathToAppeals
    {
        DateTime currentDate = DateTime.Now;
        string correctPathToZip;
        string correctPathToXml;
        string appealsXml = @"Appeals.xml";
        string appealsZip = @"Appeals.zip";
        public DefinerCorrectPathToAppeals()
        {
            string currentYear = currentDate.Year.ToString();
            string currentMonth = currentDate.Month.ToString();
            if (!File.Exists($"{currentYear}/{currentMonth}"))
            {
                if (File.Exists(currentYear))
                {
                    Directory.CreateDirectory($"{currentYear}/{currentMonth}");
                }
                else
                {
                    Directory.CreateDirectory(currentYear);
                    Directory.CreateDirectory($"{currentYear}/{currentMonth}");
                }
            }
            correctPathToZip = currentYear + "/" + currentMonth + "/" + appealsZip;
            correctPathToXml = currentYear + "/" + currentMonth + "/" + appealsXml;
            if (File.Exists($"{currentYear}/{currentMonth}/{appealsXml}"))
                File.Delete($"{currentYear}/{currentMonth}/{appealsXml}");
            StaticData.CorrectFolder = $"{currentYear}/{currentMonth}/";
        }
        public string GetCorrectPathToAppealsXml()
        {
            return correctPathToXml;
        }
        public string GetCorrectPathToAppealsZip()
        {
            return correctPathToZip;
        }
    }
}

