using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ReviveIt.test.Providers;

public class MockHttpContextAccessor : IHttpContextAccessor
{
    public HttpContext HttpContext { get; set; } = new DefaultHttpContext();

    public MockHttpContextAccessor()
    {
    }
}
