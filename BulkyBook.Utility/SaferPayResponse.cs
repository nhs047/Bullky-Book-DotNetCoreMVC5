using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class SaferPayResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string RedirectUrl { get; set; }
        public SaferPayResponseHeader ResponseHeader = new SaferPayResponseHeader();
    }

    public class SaferPayResponseHeader
    {
        public string SpecVersion { get; set; }
        public string RequestId { get; set; }
    }
}
