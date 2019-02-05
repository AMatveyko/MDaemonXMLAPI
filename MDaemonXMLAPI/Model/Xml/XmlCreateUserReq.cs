using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public class XmlCreateUserReq : XmlRequestsBase
    {

        //private List<string> _getList = new List<string>();
        private List<_xmlUserElements> _userDetails = new List<_xmlUserElements>()
        { _xmlUserElements.FullName, _xmlUserElements.Frozen, _xmlUserElements.Disabled, _xmlUserElements.MustChangepassword,
        _xmlUserElements.DontExpirePassword, _xmlUserElements.Password, _xmlUserElements.Descriptions, _xmlUserElements.AdminNotes,
        _xmlUserElements.Title, _xmlUserElements.FirstName, _xmlUserElements.MiddleName, _xmlUserElements.LastName, _xmlUserElements.Suffix};

        public XmlCreateUserReq(MailBox mailBox)
            : base(operations.CreateUser)
        {
            List<XmlNode> xmlNodesParameters = new List<XmlNode>();
            List<XmlNode> xmlNodesDetails = new List<XmlNode>();
            XmlNode tmpNode;

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.FullName.ToString());
            tmpNode.InnerText = mailBox.FullName;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Frozen.ToString());
            tmpNode.InnerText = mailBox.Frozen;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Disabled.ToString());
            tmpNode.InnerText = mailBox.Disabled;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.MustChangepassword.ToString());
            tmpNode.InnerText = mailBox.MustChangepassword;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.DontExpirePassword.ToString());
            tmpNode.InnerText = mailBox.DontExpirePassword;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Password.ToString());
            tmpNode.InnerText = mailBox.Password;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Descriptions.ToString());
            tmpNode.InnerText = mailBox.Descriptions;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.AdminNotes.ToString());
            tmpNode.InnerText = mailBox.AdminNotes;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Title.ToString());
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.FirstName.ToString());
            tmpNode.InnerText = mailBox.FirstName;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.MiddleName.ToString());
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.LastName.ToString());
            tmpNode.InnerText = mailBox.LastName;
            xmlNodesDetails.Add(tmpNode);

            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Suffix.ToString());
            xmlNodesDetails.Add(tmpNode);


            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Domain.ToString());
            tmpNode.InnerText = mailBox.Domain;
            xmlNodesParameters.Add(tmpNode);
            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Mailbox.ToString());
            tmpNode.InnerText = mailBox.Mailbox;
            xmlNodesParameters.Add(tmpNode);
            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Details.ToString());
            foreach (var node in xmlNodesDetails)
                tmpNode.AppendChild(node);
            xmlNodesParameters.Add(tmpNode);
            tmpNode = XmlRequest.CreateElement(_xmlUserElements.Aliases.ToString());
            xmlNodesParameters.Add(tmpNode);
            tmpNode = XmlRequest.CreateElement(_xmlUserElements.GroupMembership.ToString());
            xmlNodesParameters.Add(tmpNode);
            tmpNode = XmlRequest.CreateElement(_xmlUserElements.ListMembership.ToString());
            xmlNodesParameters.Add(tmpNode);

            foreach (var node in xmlNodesParameters)
                Parameters.AppendChild(node);

        }
    }
}
