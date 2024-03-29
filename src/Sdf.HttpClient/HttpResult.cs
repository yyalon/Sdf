using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sdf.Http
{
    public class HttpResult
    {
        public HttpResult(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }
        public HttpStatusCode StatusCode
        {
            get
            {
                return HttpResponseMessage.StatusCode;
            }
        }

        public HttpResponseMessage HttpResponseMessage { get; }
    }

    public class HttpResult<T> : HttpResult
    {
        public HttpResult(HttpResponseMessage httpResponseMessage) : base(httpResponseMessage)
        {
        }

        public T Result { get; set; }
    }
}
