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
        // List of unsafe patterns or keywords, including single characters
        private static readonly List<string> unsafePatterns = new List<string>
    {
        "malware", "phishing", "hacker",
        "virus", "trojan", "exploit", "attack", "unauthorized",
        "#", "%", "<", ">", "{", "}", "-", "=", "`", "&", "*", "?", "!", "\"", ";", ":", "'", "+", "@"
    };
        // Method to check if a URL is unsafe
        public static bool IsUnsafeUrl(string url)
        {
            // Check if URL contains any unsafe patterns
            foreach (var pattern in unsafePatterns)
            {
                if (url.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // URL is unsafe
                }
            }

            return false; // URL is safe
        }
    }
}
