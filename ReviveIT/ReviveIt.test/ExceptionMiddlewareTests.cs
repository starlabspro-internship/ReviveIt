using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using WebUI.MiddleWares;
using Xunit;

namespace Application.Tests
{
    public class ExceptionMiddlewareTests
    {
        private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock;
        private readonly RequestDelegate _next;

        public ExceptionMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            _next = (HttpContext context) => Task.CompletedTask;
        }

        [Theory]
        [InlineData("?url=http://localhost/malware", HttpStatusCode.BadRequest)]
        [InlineData("?url=http://localhost/--", HttpStatusCode.BadRequest)]
        [InlineData("?url=http://localhost/normal", HttpStatusCode.OK)]
        public async Task Invoke_ShouldReturnAccurateResponse(string queryString, HttpStatusCode expectedStatusCode)
        {
            var context = new DefaultHttpContext();
            context.Request.QueryString = new QueryString(queryString);

            var middleware = new ExceptionMiddleware(_next, _loggerMock.Object);

            await middleware.Invoke(context);

            Assert.Equal((int)expectedStatusCode, context.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_ShouldCheckUrlBeforeProceedingToNextMiddleware()
        {
            var context = new DefaultHttpContext();
            context.Request.QueryString = new QueryString("?url=http://localhost/malware");

            bool nextMiddlewareInvoked = false;

            var middleware = new ExceptionMiddleware(async (innerContext) =>
            {
                nextMiddlewareInvoked = true;
                innerContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await Task.CompletedTask;
            }, _loggerMock.Object);

            await middleware.Invoke(context);

            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
            Assert.False(nextMiddlewareInvoked);
        }
    }
}
