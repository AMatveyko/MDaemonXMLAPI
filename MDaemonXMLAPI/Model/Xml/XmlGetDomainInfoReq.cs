using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public class XmlGetDomainInfoReq : XmlRequestsBase
    {

        private List<_xmlDomainElements> _getList = new List<_xmlDomainElements>()
        { _xmlDomainElements.Details, _xmlDomainElements.Pruning, _xmlDomainElements.Administrators, _xmlDomainElements.Users };

        public XmlGetDomainInfoReq(string domainName)
            : base(operations.GetDomainInfo)
        {
            XmlNode xmlDomain = XmlRequest.CreateElement(_xmlDomainElements.Domain.ToString());
            xmlDomain.InnerText = domainName;
            Parameters.AppendChild(xmlDomain);
            XmlNode xmlGet = XmlRequest.CreateElement(_xmlDomainElements.Get.ToString());
            Parameters.AppendChild(xmlGet);
            foreach(var element in _getList)
            {
                xmlGet.AppendChild(XmlRequest.CreateElement(element.ToString()));
            }
        }
    }
}
