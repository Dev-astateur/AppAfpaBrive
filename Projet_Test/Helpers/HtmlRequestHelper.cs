using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.Helpers
{
    public class HtmlRequestHelper
    {

        public string _url;
        public HtmlRequestHelper(string Url)
        {
            _url = Url;
        }

        public HttpRequestMessage CreateRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(_url + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        //public HttpRequestMessage CreateRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        //{
        //    HttpRequestMessage request = CreateRequest(url, mthv, method);
        //    request.Content = new ObjectContent<T>(content, formatter);

        //    return request;
        //}

    }
}
