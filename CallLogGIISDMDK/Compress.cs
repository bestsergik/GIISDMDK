using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogGIISDMDK
{
    class Compress
    {

        public void CompressData(string xmlFile, string zipFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = "werwolf"; //password for all files
                zip.AddFile(xmlFile);
                zip.Save(zipFile);
            }
        }


        public void DecompressData(string zipFile)
        {
            using (var zip = ZipFile.Read(zipFile))
            {
                zip.Password = "werwolf";
                zip.ExtractAll(Directory.GetCurrentDirectory(),
                ExtractExistingFileAction.DoNotOverwrite);
            }
        }


        //public void CompressUserLogins()
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.Password = "werwolf"; //password for all files
        //        zip.AddFile("logins.txt");
        //        zip.Save("UserLogins.zip");
        //    }
        //}

        //public void CompressCallLog()
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.Password = "werwolf"; //password for all files
        //        zip.AddFile("callLog.txt");
        //        zip.Save("CallLog.zip");
        //    }
        //}

        //public void DecompressUserLogins()
        //{
        //    using (var zip = ZipFile.Read("UserLogins.zip"))
        //    {
        //        zip.Password = "werwolf";
        //        zip.ExtractAll(Directory.GetCurrentDirectory(),
        //        ExtractExistingFileAction.DoNotOverwrite);
        //    }
        //}

        

        //public void DecompressCallLog()
        //{
        //    using (var zip = ZipFile.Read("CallLog.zip"))
        //    {
        //        zip.Password = "werwolf";
        //        zip.ExtractAll(Directory.GetCurrentDirectory(),
        //        ExtractExistingFileAction.DoNotOverwrite);
        //    }
        //}
    }
}
