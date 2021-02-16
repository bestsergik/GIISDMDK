using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallLogGIISDMDK.WorkWithFiles;

namespace CallLogGIISDMDK
{
    
    class Data
    {
        FileReader fileReader = new FileReader();
       public List<List<string>> GetAppeals()
        {
            return fileReader.GetAppeals();
        }
    }
}
