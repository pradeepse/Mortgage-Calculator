using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MortgageCalculator.UnitTests
{
    public class TestingDelegatingHandler<T> : DelegatingHandler
    {
        private Func<HttpRequestMessage, HttpResponseMessage> _httpResponseMessageFunc;

        public TestingDelegatingHandler(T value): this(HttpStatusCode.OK, value)
        { }

        public TestingDelegatingHandler(HttpStatusCode statusCode): this(statusCode, default(T))
        { }

        public TestingDelegatingHandler(HttpStatusCode statusCode, T value)
        {
           _httpResponseMessageFunc = request => request.CreateResponse(statusCode, value);
        }

        public TestingDelegatingHandler(Func<HttpRequestMessage, HttpResponseMessage> httpResponseMessageFunc)
        {
           _httpResponseMessageFunc = httpResponseMessageFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           return Task.Factory.StartNew(() => _httpResponseMessageFunc(request));
        }

}
}
