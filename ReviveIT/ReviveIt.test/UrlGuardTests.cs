using Application.Common.Exceptions;
using Xunit;

namespace Application.Tests
{
    public class UrlGuardTests
    {
        [Theory]
        [InlineData("http://localhost/malware", true)]
        [InlineData("http://localhost/phishing", true)]
        [InlineData("http://localhost/hacker", true)]
        [InlineData("http://localhost/virus", true)]
        [InlineData("http://localhost/trojan", true)]
        [InlineData("http://localhost/exploit", true)]
        [InlineData("http://localhost/attack", true)]
        [InlineData("http://localhost/unauthorized", true)]
        [InlineData("http://localhost/#", true)]
        [InlineData("http://localhost/%", true)]
        [InlineData("http://localhost/<", true)]
        [InlineData("http://localhost/>", true)]
        [InlineData("http://localhost/{", true)]
        [InlineData("http://localhost/}", true)]
        [InlineData("http://localhost/-", true)]
        [InlineData("http://localhost/=", true)]
        [InlineData("http://localhost/`", true)]
        [InlineData("http://localhost/&", true)]
        [InlineData("http://localhost/*", true)]
        [InlineData("http://localhost/?", true)]
        [InlineData("http://localhost/!", true)]
        [InlineData("http://localhost/;", true)]
        [InlineData("http://localhost/'", true)]
        [InlineData("http://localhost/+", true)]
        [InlineData("http://localhost/@", true)]
        [InlineData("http://localhost/normal", false)]
        [InlineData("http://localhost/test", false)] 
        public void UrlGuard_IsUnsafeUrl_ShouldIdentifyUnsafeUrls(string url, bool expected)
        {
            var result = UrlGuard.IsUnsafeUrl(url);

            Assert.Equal(expected, result);
        }
    }
}
