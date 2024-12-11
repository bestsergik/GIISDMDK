using AmRoMessageDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CallLogGIISDMDK.WorkWithFiles
{
    class FileSaveDialog
    {
        Compress compress = new Compress();
        ConvertToExcel convertToExcel;
        public FileSaveDialog()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = $"Список обращений за {StaticData.CurrrentMonth} {StaticData.CurrrentYear}"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.xml)|*.xml"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                //string filename = dlg.FileName;
                string sourceFile = StaticData.CorrectPathToXml;
                string destinationFile = dlg.FileName;
                compress.DecompressData(StaticData.CorrectPathToZip);
                convertToExcel = new ConvertToExcel(destinationFile);
            }
        }

        void ReplacementChapters()
        {
            string replacedText = File.ReadAllText(StaticData.CorrectPathToXml);
            string[] chapters = new string[19] { "personalID", "date", "time", "communicationСhannel", "type", "appeal", "user", "sity", "role", "route", "additionalInfo", "company", "fullName", "phoneNumber", "email", "inn", "ogrn", "status", "ID" };
            string[] newChapters = new string[19] { "Идентификатор", "Дата", "Время", "Связь", "Тема", "Обращение", "Регистратор", "Город", "Роль", "Направленность", "Допольнительно", "Компания", "ФИО", "Телефон", "Email", "ИНН", "ОГРН", "Статус", "Номер" };
            //string[] newChapters = new string[19] { "номер", "date", "time", "communicationСhannel", "type", "appeal", "user", "sity", "role", "route", "additionalInfo", "company", "fullName", "phoneNumber", "email", "inn", "ogrn", "status", "ID" };
            for (int i = 0; i < chapters.Length; i++)
            {
                replacedText = replacedText.Replace(chapters[i], newChapters[i]);
            }
            File.WriteAllText(StaticData.CorrectPathToXml, replacedText);
        }
    }
}

