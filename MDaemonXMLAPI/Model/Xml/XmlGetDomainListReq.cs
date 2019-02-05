using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDaemonXMLAPI.Model.Xml
{
    public class XmlGetDomainListReq : XmlRequestsBase
    {

        private List<string> _getList = new List<string>();

        public XmlGetDomainListReq()
            : base(operations.GetDomainList)
        {
            XmlNode GetEl = XmlRequest.CreateElement(_xmlDomainElements.Get.ToString());
            Parameters.AppendChild(GetEl);
        }
    }
}
