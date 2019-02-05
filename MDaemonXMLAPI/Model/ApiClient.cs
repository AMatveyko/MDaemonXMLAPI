using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MDaemonXMLAPI.Model
{
    public static class ApiClient
    {

        public static string Request(string host, string userName, string password, string requestXml)
        {
            string destinationUrl = $"https://{host}/MdMgmtWS";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            request.Credentials = new NetworkCredential(userName, password);

            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            request.ServerCertificateValidationCallback = delegate { return true; };
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            string responseStr = String.Empty;
            try
            {
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    responseStr = new StreamReader(responseStream).ReadToEnd();
                }
            }
            catch (Exception e)
            {
                responseStr = e.Message;
                //viewModel.Logging($"ошибка {e.Message}");
            }

            //responseStr = $"{requestXml}\n\n\n{responseStr}";

            return responseStr;
        }
    }
}
