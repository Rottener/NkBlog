using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NkBolg.Common.Net
{
    public partial class HttpHelper
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            //nginx方式
            string ip = HttpContextHelper.Current.Request.Headers["X-Real-IP"].ToString();
            if (!ip.Contains("::1") && !ip.Contains("127.0.0.1") && !string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
            ip = HttpContextHelper.Current.Request.Headers["X-Forwarded-For"].ToString();
            if (!ip.Contains("::1") && !ip.Contains("127.0.0.1") && !string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
            else
            {
                ip = HttpContextHelper.Current.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 获取服务端IP
        /// </summary>
        /// <returns></returns>
        public static string GetServerIp()
        {
            return HttpContextHelper.Current.Connection.LocalIpAddress.ToString();
        }

        /// <summary>
        /// 获取操作系统版本号
        /// </summary>
        /// <returns></returns>
        public static string GetOsVersion()
        {
            string userAgent = HttpContextHelper.Current.Request.Headers[HeaderNames.UserAgent];

            string osVersion = "未知";
            if (userAgent.Contains("NT 10.0"))
            {
                osVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                if (userAgent.Contains("64"))
                    osVersion = "Windows XP";
                else
                    osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string UserAgent()
        {
            return HttpContextHelper.Current.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        /// <summary>
        /// 根据提供的api获取物理地址
        /// </summary>
        /// <returns></returns>
        public static string GetAddressByApi()
        {
            try
            {
                string ip = GetClientIp();
                if (ip == "::1")
                {
                    return "本地地址";
                }
                var apiUrl = $"http://whois.pconline.com.cn/ip.jsp?ip={GetClientIp()}";
                return HttpGet(apiUrl).Replace("\n", string.Empty).Replace("\r", string.Empty);
            }
            catch (Exception ex)
            {
                //LogWriter.WriteLog(FolderName.Error, ex.Message);
                return "";
            }

        }





        private static readonly HttpClient httpClient;
        static HttpHelper()
        {
            httpClient = new HttpClient();
        }
        /// <summary>
        /// 发起POST同步请求
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="encoding">编码</param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <param name="headers">填充消息头</param>        
        /// <returns></returns>
        public static string HttpPost(string url, string postData = null, string encoding = "utf-8", string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            postData = postData ?? "";
            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            using (HttpContent httpContent = new StringContent(postData, Encoding.GetEncoding(encoding)))
            {
                if (contentType != null)
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }


        /// <summary>
        /// 发起POST异步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <param name="headers">填充消息头</param>        
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, string postData = null, string encoding = "utf-8", string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            postData = postData ?? "";
            httpClient.Timeout = new TimeSpan(0, 0, timeOut);
            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            using (HttpContent httpContent = new StringContent(postData, Encoding.GetEncoding(encoding)))
            {
                if (contentType != null)
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 发起GET同步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headers">请求头</param>
        /// <param name="contentType">请求内容格式</param>
        /// <returns></returns>
        public static string HttpGet(string url, string contentType = null, Dictionary<string, string> headers = null)
        {
            if (contentType != null)
                httpClient.DefaultRequestHeaders.Add("ContentType", contentType);
            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 发起GET异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, string contentType = null, Dictionary<string, string> headers = null)
        {

            if (contentType != null)
                httpClient.DefaultRequestHeaders.Add("ContentType", contentType);
            if (headers != null)
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            HttpResponseMessage response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
