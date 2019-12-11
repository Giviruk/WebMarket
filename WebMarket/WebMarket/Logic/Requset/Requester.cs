using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebMarket.Logic
{
    public class WebRequestSender
    {
        public static async Task<string> PutAsync<T>(T item, string url)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/json";
            webRequest.Method = "PUT";
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(item));
                streamWriter.Flush();
                streamWriter.Close();
            }
            WebResponse response = null;
            try
            {
                response = await webRequest.GetResponseAsync();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    return "500";
                }
                else throw ex;
            }
            var responseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                var line = await sr.ReadToEndAsync();
                response.Close();
                return line;
            }
        }
        public static async Task<string> DeleteAsync<T>(T item, string url)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/json";
            webRequest.Method = "DELETE";
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(item));
                streamWriter.Flush();
                streamWriter.Close();
            }
            var response = await webRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                var line = await sr.ReadToEndAsync();
                response.Close();
                return line;
            }
        }

        public static async Task<string> PostAsync<T>(T item, string url)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(item));
                streamWriter.Flush();
                streamWriter.Close();
            }
            var response = await webRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                var line = await sr.ReadToEndAsync();
                response.Close();
                return line;
            }
        }

        public static async Task<string> PostTwoArgsAsync<T1, T2>(string url, T1 arg1, T2 arg2)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(arg1));
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(arg2));
                streamWriter.Flush();
                streamWriter.Close();
            }
            var response = await webRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                var line = await sr.ReadToEndAsync();
                response.Close();
                return line;
            }
        }
    }
}
