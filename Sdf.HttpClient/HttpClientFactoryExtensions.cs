using Microsoft.Extensions.Logging;
using Sdf.Fundamentals.Serializer;

namespace Sdf.Http
{
    public static class HttpClientFactoryExtensions
    {
        public static HttpClientWrapper CreateHttpClientWrapper(this IHttpClientFactory httpClientFactory, string name)
        {
            using var resolver = Bootstrapper.Instance.IocManager.GetResolver();
            var serializer = resolver.Resolve<ISerializer>();
            var log = resolver.Resolve<ILogger<HttpClientWrapper>>();

            return new HttpClientWrapper(httpClientFactory.CreateClient(name), serializer, log);
        }
    }
}
