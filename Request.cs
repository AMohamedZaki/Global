using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace NT.Global.Net
{
    public class Request
    {

        public static List<T> Send<T>(string url, object Obj)
        {
            string json = JsonConvert.SerializeObject(Obj);
            string str = SendRequest(url, json);
            List<T> obj = JsonConvert.DeserializeObject<List<T>>(str);
            return obj;
        }

        public static string SendRequest(string url, string json)
        {
          
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Timeout = 20000;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Proxy = null;

            if (!string.IsNullOrEmpty(json))
            {
                using (var streamWriter = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            WebResponse response = request.GetResponse();
            return ReadStreamFromResponse(response);
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (System.IO.Stream responseStream = response.GetResponseStream())
            using (System.IO.StreamReader sr = new System.IO.StreamReader(responseStream))
            {
                //Need to return this response
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }
    }
}
