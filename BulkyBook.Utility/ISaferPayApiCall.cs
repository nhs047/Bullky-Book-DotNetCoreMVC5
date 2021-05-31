using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public interface ISaferPayApiCall
    {
        SaferPayResponse CallSaferPayApi(string url, string amount, string currencyCode, string orderId, string description);
    }
}
