using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VvHelpers
{
  public class ResponseHttp
  {
    private string _body;

    public HttpStatusCode StatusCode { get; }
    public HttpResponseHeaders Headers { get; set; }
    public string Body => _body;
    public long ContentLength { get; }

    public ResponseHttp(HttpResponseMessage response)
    {
      StatusCode = response.StatusCode;
      if (response.Content.Headers.TryGetValue("Content-Length", out string value))
      {
        long.TryParse(value, out long length);
        ContentLength = length;
      }

      Headers = response.Headers;
    }

    internal void SetBody(HttpResponseMessage response)
    {
      _body = response.Content.ReadAsStringAsync().Result;
    }

    internal async void SetBodyAsync(HttpResponseMessage response)
    {
      _body = await response.Content.ReadAsStringAsync();
    }
  }
}
