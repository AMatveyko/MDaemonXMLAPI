using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public static class XmlResponse
    {

        public async static void FillingUserInfoAsync(MailBox mailBox, ViewModel viewModel)
        {
            if (mailBox == null)
                return;
            string domainName = mailBox.Domain;
            string name = mailBox.Name;
            string hostName = viewModel.MailServer;
            string userName = viewModel.UserNameForMailServer;
            string password = viewModel.PasswordBox?.Password;
            if(viewModel.PasswordBox != null)
                await Task.Run(() =>
                {
                    XmlGetUserInfoReq xmlGetUserInfoReq = new XmlGetUserInfoReq(domainName, name);
                    string xmlResponse = ApiClient.Request(hostName, userName, password, xmlGetUserInfoReq.ToString());
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlResponse);
                    XmlNodeList xmlNodeList;
                    xmlNodeList = xmlDocument.GetElementsByTagName("Password");
                    if (xmlNodeList.Count > 0)
                        mailBox.Password = xmlNodeList[0].InnerText;
                    else
                    {
                        mailBox.NoOk();
                        return;
                    }

                    xmlNodeList = xmlDocument.GetElementsByTagName("FirstName");
                    if (xmlNodeList.Count > 0)
                        mailBox.FirstName = xmlNodeList[0].InnerText;

                    xmlNodeList = xmlDocument.GetElementsByTagName("LastName");
                    if (xmlNodeList.Count > 0)
                        mailBox.LastName = xmlNodeList[0].InnerText;

                    xmlNodeList = xmlDocument.GetElementsByTagName("Frozen");
                    if (xmlNodeList.Count > 0)
                        mailBox.Frozen = xmlNodeList[0].InnerText;

                    xmlNodeList = xmlDocument.GetElementsByTagName("Disabled");
                    if (xmlNodeList.Count > 0)
                        mailBox.Disabled = xmlNodeList[0].InnerText;

                    xmlNodeList = xmlDocument.GetElementsByTagName("MailDir");
                    if (xmlNodeList.Count > 0)
                        mailBox.MailDir = xmlNodeList[0].InnerText;
                });
        }

        public static List<MailBox> GetUserList(string xmlResponse, string domainName)
        {
            List<MailBox> userList = new List<MailBox>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("User");
            foreach (XmlNode user in xmlNodeList)
            {
                XmlAttributeCollection attributes = user.Attributes;
                foreach (XmlAttribute attribut in attributes)
                {
                    if (attribut.Name == "id")
                    {
                        userList.Add(new MailBox(attribut.Value, domainName));
                    }
                }
            }
            return userList;
        }
        public static string GetServerVersion(string xmlResponse)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("ProductVersion");
            if (xmlNodeList.Count > 0)
                return xmlNodeList[0].InnerText;
            return "err";
        }
        public static string GetServerUptime(string xmlResponse)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("Uptime");
            if (xmlNodeList.Count > 0)
                return xmlNodeList[0].InnerText;
            return "err";
        }
        public static string GetServerName(string xmlResponse)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("SERVER_NAME");
            if (xmlNodeList.Count > 0)
                return xmlNodeList[0].InnerText;
            return "err";
        }
        public static List<string> GetDomainList(string xmlResponse, IForLogging viewModel)
        {
            List<string> domainList = new List<string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);

            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("Domains");
            foreach (XmlNode node in xmlNodeList)
            {
                XmlNodeList domains = node.SelectNodes("Domain");
                foreach (XmlNode domain in domains)
                {
                    XmlAttributeCollection attributes = domain.Attributes;
                    foreach (XmlAttribute attribut in attributes)
                    {
                        if (attribut.Name == "id")
                        {
                            domainList.Add(attribut.Value);
                        }
                    }
                }
            }
            return domainList;
        }
        public static bool IsUserExist( string xmlResponse )
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("User");
            
            if (xmlNodeList.Count == 0)
                return false;
            else
                return true;
        }
    }
}
