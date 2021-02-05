using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogGIISDMDK
{
    class FileWorker
    {
        private string _pathToAppeals = @"callLog.txt";
        
        public List<string[]> GetAppeals()
        {
            List<string[]> appeals = new List<string[]>();
            using (StreamReader sr = new StreamReader(_pathToAppeals, Encoding.Unicode))
            {
                int lenghtAppeal = 0;
                string temp;
                while (true)
                {
                    temp = sr.ReadLine();

                    if (temp == null || temp.Length < 1) break;
                    lenghtAppeal++;
                    while (true)
                    {
                        temp += sr.ReadLine();
                        if (temp.Substring(temp.Length - 5, 5) == "%%#%%")
                        {
                            lenghtAppeal++;
                            if (lenghtAppeal == 11)
                                break;
                        }
                        else temp += "\r";
                    }
                    string[] symbolsSplit = new string[] { "%%#%%" };
                    string[] lines = temp.Split(symbolsSplit, StringSplitOptions.None);
                    appeals.Add(lines);
                    lenghtAppeal = 0;
                }
            }
            return appeals;
        }

        internal void GetUserStatus()
        {
            using (StreamReader sr = new StreamReader(_pathToAppeals, Encoding.Unicode))
            {
                int lenghtAppeal = 0;
                string temp;
                while (true)
                {
                    temp = sr.ReadLine();

                    if (temp == null || temp.Length < 1) break;
                    lenghtAppeal++;
                    while (true)
                    {
                        temp += sr.ReadLine();
                        if (temp.Substring(temp.Length - 5, 5) == "%%#%%")
                        {
                            
                            lenghtAppeal++;
                            if (lenghtAppeal == 11)
                            {
                                UserLevelUp(temp);
                                
                                    break;
                            }
                                
                        }
                        else temp += "\r";
                    }
                    lenghtAppeal = 0;

                }
            }
        }

        private void UserLevelUp(string temp)
        {
            

            Console.WriteLine(temp.Substring(0, (temp.Length - 5)));
            if (temp.Substring(0, (temp.Length - 5)) == StaticData.User)
            {
                StaticData.UserLvl++;
            }
        }
    }
}
