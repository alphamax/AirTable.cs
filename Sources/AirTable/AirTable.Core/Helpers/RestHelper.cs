using AirTable.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Helpers
{
    /// <summary>
    /// Easily manage Rest communication.
    /// </summary>
    public class RestHelper
    {
        /// <summary>
        /// Make a simple request with the given method, url & parameter list.
        /// No embeded content.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<string> MakeRequest(string httpMethod, string url, params RequestParameter[] parameters)
        {
            return await MakeRequest(httpMethod, url, null, parameters);
        }

        /// <summary>
        /// Make a simple request with the given method, url, JSON content & parameter list.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<string> MakeRequest(string httpMethod, string url, string content, params RequestParameter[] parameters)
        {
            var request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = httpMethod;
            foreach (var item in parameters)
            {
                request.Headers[item.Key] = item.Value;
            }

            if (!string.IsNullOrWhiteSpace(content))
            {
                // Set the content type of the data being posted.
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var response = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
                using (var streamWriter = new StreamWriter(response))
                {
                    streamWriter.Write((content));
                }
            }

            try
            {
                using (var response = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
                using (var streaReader = new StreamReader(response.GetResponseStream()))
                {

                    return streaReader.ReadToEnd();
                }
            }
            //Only catch API errors
            catch (WebException e)
            {
                using (StreamReader sr = new StreamReader(e.Response.GetResponseStream()))
                {
                    var error = sr.ReadToEnd();
                    throw new ApiException((int)((HttpWebResponse)e.Response).StatusCode, error);
                }
            }
        }
    }
}
