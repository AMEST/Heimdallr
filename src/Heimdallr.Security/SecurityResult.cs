using System;

namespace Heimdallr.Security
{
    public class SecurityResult
    {
        public string GeneratedPassword { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}