using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public class XmlGetUserInfoReq : XmlRequestsBase
    {

        private List<string> _getList = new List<string>() { "Details", "AdminRights", "Aliases", "GroupMembership", "ListMembership",
                                                             "Protocols", "AutoResponder", "Forwarding", "Restrictions", "Quotas", "Pruning",
                                                             "WorldClient", "WCIM", "RemoteAdmin", "Signatures", "Attachments", "IMAPFilters",
                                                             "MultiPOP", "SharedFolders", "BES", "BIS", "EAS", "EASDevices", "EASDeviceInfo",
                                                             "Notes", "Roles", "WhiteList", "Options", "Other" };

        public XmlGetUserInfoReq(string domain, string mailbox)
            : base(operations.GetUserInfo)
        {
            XmlNode DomainEl = XmlRequest.CreateElement("Domain");
            XmlNode MailBoxEl = XmlRequest.CreateElement("Mailbox");
            XmlNode GetEl = XmlRequest.CreateElement("Get");

            DomainEl.InnerText = domain;
            MailBoxEl.InnerText = mailbox;

            Parameters.AppendChild(DomainEl);
            Parameters.AppendChild(MailBoxEl);
            Parameters.AppendChild(GetEl);

            foreach (var child in _getList)
            {
                XmlNode ChildEl = XmlRequest.CreateElement(child);
                GetEl.AppendChild(ChildEl);
            }
        }
    }
}
