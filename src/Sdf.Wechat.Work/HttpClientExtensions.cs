using Sdf.Fundamentals.Serializer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sdf.Wechat.Work
{
    public static class HttpClientExtensions
    {
        public static ISerializer Serializer()
        {
            using (var resolver = Bootstrapper.Instance.IocManager.GetResolver())
            {
                return resolver.Resolve<ISerializer>();
            }
        }

        public static HttpResponseModel<T> SendPost<T>(this HttpClient httpClient, string url, object data)
        {
            ISerializer serializer = Serializer();
            string json = serializer.Serialize(data);
            HttpContent postContent = new StringContent(json, Encoding.UTF8);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Content = postContent;
            HttpResponseMessage httpResponseMessage = httpClient.SendAsync(httpRequest).Result;
            HttpResponseModel<T> httpResponse = new HttpResponseModel<T>(httpResponseMessage.StatusCode);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var resReadAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                string resJson = resReadAsStringAsync.Result;
                httpResponse.Body = resJson;
                try
                {
                    var result = serializer.Deserialize<T>(resJson);
                    httpResponse.Result = result;
                }
                catch (Exception)
                {

                }
            }
            return httpResponse;
        }
        public static HttpResponseModel<T> SendGet<T>(this HttpClient httpClient, string url)
        {
            ISerializer serializer = Serializer();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponseMessage = httpClient.SendAsync(httpRequest).Result;
            HttpResponseModel<T> httpResponse = new HttpResponseModel<T>(httpResponseMessage.StatusCode);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var resReadAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                string resJson = resReadAsStringAsync.Result;
                httpResponse.Body = resJson;
                try
                {
                    var result = serializer.Deserialize<T>(resJson);
                    httpResponse.Result = result;
                }
                catch (Exception)
                {

                }

            }
            return httpResponse;
        }
    }
    public class HttpResponseModel<T>
    {
        public HttpResponseModel(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Result { get; set; }
        public string Body { get; set; }
    }
}
