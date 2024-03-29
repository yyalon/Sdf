using Microsoft.Extensions.Logging;
using Sdf.Fundamentals.Serializer;
using System.Net.Http.Headers;
using System.Text;

namespace Sdf.Http
{
    public class HttpClientWrapper
    {
        public HttpClient InternalHttpClient { get; }

        private readonly ISerializer _serializer;
        private readonly ILogger<HttpClientWrapper> _log;
        public HttpClientWrapper(HttpClient HttpClient, ISerializer serializer, ILogger<HttpClientWrapper> log)
        {
            InternalHttpClient = HttpClient ?? throw new ArgumentNullException(nameof(HttpClient));
            _serializer = serializer;
            _log = log;
        }

        public async Task<HttpResult<TResult>> SendAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await InternalHttpClient.SendAsync(request, cancellationToken: cancellationToken);

            return await DeserializeToHttpResult<TResult>(response, cancellationToken);
        }

        public async Task<HttpResult<TResult>> PostJsonAsync<TResult>(string url, object data, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var json =await _serializer.SerializeAsync(data);
            var postContent = new StringContent(json, Encoding.UTF8);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequest.Content = postContent;
            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return await DeserializeToHttpResult<TResult>(response, cancellationToken);
        }

        public async Task<HttpResult<TResult>> PutJsonAsync<TResult>(string url, object data, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, url);

            var json = await _serializer.SerializeAsync(data);
            var postContent = new StringContent(json, Encoding.UTF8);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequest.Content = postContent;
            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return await DeserializeToHttpResult<TResult>(response, cancellationToken);
        }

        public async Task<HttpResult<TResult>> SendGetAsync<TResult>(string url, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return await DeserializeToHttpResult<TResult>(response, cancellationToken);
        }


        public async Task<HttpResult> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await InternalHttpClient.SendAsync(request, cancellationToken: cancellationToken);

            return DeserializeToHttpResult(response);
        }

        public async Task<HttpResult> PostJsonAsync(string url, object data, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var json = await _serializer.SerializeAsync(data);
            var postContent = new StringContent(json, Encoding.UTF8);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequest.Content = postContent;
            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return DeserializeToHttpResult(response);
        }

        public async Task<HttpResult> PutJsonAsync(string url, object data, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, url);

            var json = await _serializer.SerializeAsync(data);
            var postContent = new StringContent(json, Encoding.UTF8);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequest.Content = postContent;
            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return DeserializeToHttpResult(response);
        }

        public async Task<HttpResult> SendGetAsync(string url, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await InternalHttpClient.SendAsync(httpRequest, cancellationToken: cancellationToken);

            return DeserializeToHttpResult(response);
        }

        private async Task<HttpResult<T>> DeserializeToHttpResult<T>(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var httpResult = new HttpResult<T>(response);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            try
            {
                httpResult.Result = await _serializer.DeserializeAsync<T>(json);
            }
            catch
            {
                _log.LogError($"序列化json失败,type:{typeof(T)},json:{json}");
            }

            return httpResult;
        }

        private static HttpResult DeserializeToHttpResult(HttpResponseMessage response)=> new(response);
    }
}
