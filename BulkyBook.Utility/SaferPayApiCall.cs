using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class SaferPayApiCall: ISaferPayApiCall
    {
        static HttpClient client = new HttpClient();
        private readonly SaferPaySettings _SaferPaySettings;
        public SaferPayApiCall(IOptions<SaferPaySettings> options)
        {
            _SaferPaySettings = options.Value;
        }

        public SaferPayResponse CallSaferPayApi(string url, string amount, string currencyCode, string orderId, string description)
        {
            var requestBody = new RequestBody();
            requestBody.RequestHeader.SpecVersion = _SaferPaySettings.SpecVersion;
            requestBody.RequestHeader.CustomerId = _SaferPaySettings.CustomerId;
            requestBody.RequestHeader.RequestId = _SaferPaySettings.RequestId;
            requestBody.RequestHeader.RetryIndicator = _SaferPaySettings.RetryIndicator;
            requestBody.TerminalId = _SaferPaySettings.TerminalId;
            requestBody.Payment.OrderId = orderId;
            requestBody.Payment.Description = description;
            requestBody.Payment.Amount.Value = amount;
            requestBody.Payment.Amount.CurrencyCode = currencyCode;
            requestBody.ReturnUrls.Success = $"{_SaferPaySettings.Success}";
            requestBody.ReturnUrls.Fail = _SaferPaySettings.Fail;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("ContentType", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {_SaferPaySettings.Authorization}");
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = client.PostAsync($"{_SaferPaySettings.BaseUrl}{url}", stringContent).Result;
            SaferPayResponse resObj = JsonConvert.DeserializeObject<SaferPayResponse>(response.Content.ReadAsStringAsync().Result);
            return resObj;
        }
    }

    public class RequestHeader
    {
        public string SpecVersion { get; set; }
        public string CustomerId { get; set; }
        public string RequestId { get; set; }
        public int RetryIndicator { get; set; }
    }

    public class Amount
    {
        public string Value { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class Payment
    {
        public string OrderId { get; set; }
        public string Description { get; set; }
        public Amount Amount = new Amount();
    }

    public class ReturnUrls
    {
        public string Success { get; set; }
        public string Fail { get; set; }
    }

    public class RequestBody
    {
        public RequestHeader RequestHeader = new RequestHeader();
        public string TerminalId { get; set; }
        public Payment Payment = new Payment();
        public ReturnUrls ReturnUrls = new ReturnUrls();
    }
}
