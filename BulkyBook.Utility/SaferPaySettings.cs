using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class SaferPaySettings
    {
        public string SpecVersion { get; set; }
        public string CustomerId { get; set; }
        public string RequestId { get; set; }
        public int RetryIndicator { get; set; }
        public string TerminalId { get; set; }
        public string Success { get; set; }
        public string Fail { get; set; }
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
    }
}
