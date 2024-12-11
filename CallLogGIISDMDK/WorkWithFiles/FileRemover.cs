using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CallLogGIISDMDK.WorkWithFiles
{
    class FileRemover
    {
        Compress fileArchiving = new Compress();
        string pathToZipAppeals = @"Appeals.zip";
        string pathToAppeals = @"appeals.xml";
        public delegate void MethodContainer();
        public event MethodContainer onRemoveComplete;
        public void RemoveAppeal()
        {
            if (File.Exists(pathToZipAppeals))
                fileArchiving.DecompressData(pathToZipAppeals);
            List<List<string>> appeals = new List<List<string>>();
           
            XDocument xdoc = XDocument.Load(pathToAppeals);
            if (File.Exists(pathToAppeals))
            {
                foreach (XElement currentAppeal in xdoc.Element("Appeals").Elements())
                {
                    //Console.WriteLine(currentAppeal.Element("personalID").Name);
                    if (currentAppeal.Element("personalID").Value == StaticData.CurrentPersonalId)
                    {
                        currentAppeal.Remove();
                        break;
                    }
                }
                xdoc.Save(pathToAppeals);
                fileArchiving.CompressData(pathToAppeals, pathToZipAppeals);
                File.Delete(pathToAppeals);
            }
        }
    
        internal void RemovingComplete()
        {
            onRemoveComplete();
        }
    }
}
