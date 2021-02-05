using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CallLogGIISDMDK
{
    class XmlEditing
    {
        XmlDocument xDoc = new XmlDocument();

        List<string> dataXML = new List<string>();
        public void LoadXML(string path)
        {
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            GetData(xRoot);
        }

        private void GetData(XmlElement xRoot)
        {
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode fullName = xnode.Attributes.GetNamedItem("FullName");
                    XmlNode phoneNumber = xnode.Attributes.GetNamedItem("PhoneNumber");
                    XmlNode currentDay = xnode.Attributes.GetNamedItem("CurrentDay");
                    XmlNode currentHour = xnode.Attributes.GetNamedItem("CurrentHour");
                    XmlNode currentMinute = xnode.Attributes.GetNamedItem("CurrentMinute");
                    XmlNode email = xnode.Attributes.GetNamedItem("Email");
                    XmlNode company = xnode.Attributes.GetNamedItem("Company");
                    XmlNode participleRole = xnode.Attributes.GetNamedItem("ParticipleRole");
                    XmlNode appeal = xnode.Attributes.GetNamedItem("Appeal");
                    XmlNode additionalInfo = xnode.Attributes.GetNamedItem("AdditionalInfo");
                }

                foreach (XmlNode childnode in xnode.ChildNodes)
                {

                    switch(childnode.Name)
                    {
                        case "FullName":

                            break;
                        case "PhoneNumber":
                            break;
                        case "CurrentDay":
                            break;
                        case "CurrentHour":
                            break;
                        case "CurrentMinute":
                            break;
                        case "Email":
                            break;
                        case "Company":
                            break;
                        case "ParticipleRole":
                            break;
                        case "Appeal":
                            break;
                        case "AdditionalInfo":
                            break;
                    }
                  
                }
            }
        }
    }
}
