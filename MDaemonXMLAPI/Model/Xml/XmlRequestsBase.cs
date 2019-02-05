using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model
{
    public abstract partial class XmlRequestsBase
    {

        protected enum operations
        {
            GetUserInfo,
            GetDomainList,
            CreateUser,
            GetDomainInfo
        }

        protected enum _xmlElement
        {
            MDaemon,
            API,
            Request,
            Operation,
            Parameters
        }

        protected enum _xmlAttribute
        {
            verbose,
            echo,
            version,
            encoding
        }
        protected enum _xmlDomainElements
        {
            Get, Domain, Details, Pruning, Administrators, EAS, EASDevices, EASPolicy, EASPolicies, WCIM, SmartHost, Signatures, WorldClient, Users, Lists
        }
        protected enum _xmlUserElements
        {
            FullName,
            Frozen,
            Disabled,
            MustChangepassword,
            DontExpirePassword,
            Password,
            Descriptions,
            AdminNotes,
            Title,
            FirstName,
            MiddleName,
            LastName,
            Suffix,
            Domain,
            Mailbox,
            Details,
            Aliases,
            GroupMembership,
            ListMembership
        }

        protected XmlDocument XmlRequest { get; private set; }
        protected XmlNode Parameters { get; private set; }

        private string requestVerbose = "1";
        private string requestEcho = "0";
        private string requestVersion = "18.0.1";
        private string requestEncoding = "utf-8";

        protected XmlRequestsBase(operations operation)
        {
            XmlRequest = new XmlDocument();
            XmlDeclaration declaration = XmlRequest.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlRequest.AppendChild(declaration);
            XmlNode MDaemonEl = XmlRequest.CreateElement(_xmlElement.MDaemon.ToString());
            XmlNode ApiEl = XmlRequest.CreateElement(_xmlElement.API.ToString());
            XmlNode RequestEl = XmlRequest.CreateElement(_xmlElement.Request.ToString());
            XmlNode OperationEl = XmlRequest.CreateElement(_xmlElement.Operation.ToString());
            Parameters = XmlRequest.CreateElement(_xmlElement.Parameters.ToString());

            XmlRequest.AppendChild(MDaemonEl);
            MDaemonEl.AppendChild(ApiEl);
            ApiEl.AppendChild(RequestEl);
            RequestEl.AppendChild(OperationEl);
            RequestEl.AppendChild(Parameters);

            OperationEl.InnerText = operation.ToString();

            XmlAttribute requestVerboseAtt = XmlRequest.CreateAttribute(_xmlAttribute.verbose.ToString());
            XmlAttribute requestEchoAtt = XmlRequest.CreateAttribute(_xmlAttribute.echo.ToString());
            XmlAttribute requestVersionAtt = XmlRequest.CreateAttribute(_xmlAttribute.version.ToString());
            XmlAttribute requestEncodingAtt = XmlRequest.CreateAttribute(_xmlAttribute.encoding.ToString());

            requestVerboseAtt.InnerText = requestVerbose;
            requestEchoAtt.InnerText = requestEcho;
            requestVersionAtt.InnerText = requestVersion;
            requestEncodingAtt.InnerText = requestEncoding;

            RequestEl.Attributes.Append(requestVerboseAtt);
            RequestEl.Attributes.Append(requestEchoAtt);
            RequestEl.Attributes.Append(requestVersionAtt);
            RequestEl.Attributes.Append(requestEncodingAtt);
        }

        public override string ToString()
        {
            return XmlRequest.OuterXml;
        }
    }
}
