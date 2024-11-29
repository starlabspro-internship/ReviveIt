using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public static class UrlGuard
    {
        public class UnsafeUrlException : Exception
        {
            public UnsafeUrlException(string message) : base(message) { }
        }
        private static readonly List<string> unsafePatterns = new List<string>
        {
            "malware", "phishing", "hacker", "virus", "trojan", "exploit", "attack", "unauthorized",
            "#", "%", "<", ">", "{", "}", "-", "=", "`", "&", "*", "?", "!", ";", "'", "+", "@"
        };
        public static bool IsUnsafeUrl(string url)
        {
            foreach (var pattern in unsafePatterns)
            {
                if (url.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}
