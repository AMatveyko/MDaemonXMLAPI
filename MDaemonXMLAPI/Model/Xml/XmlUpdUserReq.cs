using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public class XmlUpdUserReq : XmlRequestsBase
    {
        public XmlUpdUserReq(MailBox mailBox)
            : base(operations.UpdateUser)
        {
            //XmlNode DomainEl = XmlRequest.CreateElement("Domain");

            var DomainEl = XmlRequest.CreateElement(_xmlUserElements.Domain.ToString());
            var MailboxEl = XmlRequest.CreateElement(_xmlUserElements.Mailbox.ToString());
            var DetailsEl = XmlRequest.CreateElement(_xmlUserElements.Details.ToString());
            var PasswordEl = XmlRequest.CreateElement(_xmlUserElements.Password.ToString());
            var FrozenEl = XmlRequest.CreateElement(_xmlUserElements.Frozen.ToString());
            var DisabledEl = XmlRequest.CreateElement(_xmlUserElements.Disabled.ToString());
            Parameters.AppendChild(DomainEl);
            Parameters.AppendChild(MailboxEl);
            Parameters.AppendChild(DetailsEl);
            DetailsEl.AppendChild(PasswordEl);
            DetailsEl.AppendChild(FrozenEl);
            DetailsEl.AppendChild(DisabledEl);
            DomainEl.InnerText = mailBox.Domain;
            MailboxEl.InnerText = mailBox.Mailbox;
            PasswordEl.InnerText = mailBox.Password;
            FrozenEl.InnerText = mailBox.Frozen;
            DisabledEl.InnerText = mailBox.Disabled;
        }
    }
}
