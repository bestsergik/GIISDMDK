using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallLogGIISDMDK.ViewModels;
using CallLogGIISDMDK.WorkWithFiles;
namespace CallLogGIISDMDK
{
    class Data
    {
        FileReader fileReader = new FileReader();
        List<int> idAppeals = new List<int>();
        List<int> idPersonalAppeals = new List<int>();
        //string pathToZipAppeals = @"Appeals.zip";

        public List<List<string>> GetAppeals()
        {
            return fileReader.GetAppeals();
        }
        public int GetNumberIdAppeal()
        {
            int numberIdAppeal = 0;
            List<List<string>> Appeals = GetAppeals();
            foreach (var appeal in Appeals)
            {
                idAppeals.Add(Convert.ToInt32(appeal[0]));
            }
            numberIdAppeal = idAppeals.Max();
            if (StaticData.CurrentId != 0)
            {
                numberIdAppeal = StaticData.CurrentId;
                return numberIdAppeal;

            }
            return numberIdAppeal + 1;
        }
        internal int GetNumberPersonalIdAppeal()
        {
            int numberPersonalIdAppeal = 0;
            List<List<string>> Appeals = GetAppeals();
            foreach (var appeal in Appeals)
            {
                idPersonalAppeals.Add(Convert.ToInt32(appeal[20]));
            }
            numberPersonalIdAppeal = idPersonalAppeals.Max();
            return numberPersonalIdAppeal + 1;
            //int numberPersonalIdAppeal = 0;
            //List<List<string>> Appeals = GetAppeals();
            //foreach (var appeal in Appeals)
            //{
            //    idAppeals.Add(Convert.ToInt32(appeal[18]));
            //}
            //numberPersonalIdAppeal = idAppeals.Max();
            //return numberPersonalIdAppeal + 1;
        }
        public List<List<string>> ConvertToDateTime(List<List<string>> appeals)
        {
            List<List<string>> Appeals = new List<List<string>>();
            return Appeals;
        }
        internal List<List<string>> GetAppealsSortByID(string ID, List<List<string>> appeals)
        {
            List<List<string>> _appeals = new List<List<string>>();
            foreach (var appeal in appeals)
            {
                if (appeal[20] == ID)
                    _appeals.Add(appeal);
            }
            return _appeals;
        }

        internal List<List<string>> GetAppealByPersonalID(object personalID)
        {
            List<List<string>> dataAppealByPersonalID = new List<List<string>>();
            foreach (var appeal in fileReader.GetAppeals())
            {
                if (appeal[20] == personalID.ToString())
                {
                    dataAppealByPersonalID.Add(appeal);
                }

            }
            return dataAppealByPersonalID;
        }

        //public bool IsFileReady()
        //{
        //    bool isReady = false;
        //    while (!isReady)
        //    {
        //        try
        //        {
        //            using (Stream stream = new FileStream(pathToZipAppeals, FileMode.Open))
        //            {
        //                isReady = true;
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return isReady;
        //}
    }
}
