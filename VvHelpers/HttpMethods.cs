using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VvHelpers.Extensions;

namespace VvHelpers
{
  public class HttpMethods
  {
    public static ResponseHttp Get(string url, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();

      if (url == null) return null;

      HttpClient client = null;
      HttpResponseMessage response = null;

      try
      {

        client = new HttpClient();
        AddDefaultRequestHeaders(client, headers);
        response = client.GetAsync(url).Result;

        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);

        return http;

      }

      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        client.Dispose();
        response?.Dispose();
      }

      return null;
    }

    public static async Task<ResponseHttp> GetAsync(string url, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;
      HttpClient client = null;
      HttpResponseMessage response = null;
      try
      {
        client = new HttpClient();
        AddDefaultRequestHeaders(client, headers);
        response = await client.GetAsync(url);
        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);
        return http;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        client.Dispose();
        response?.Dispose();
      }

      return null;
    }

    public static ResponseHttp Head(string url, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpRequestMessage request = null;
      HttpResponseMessage response = null;
      HttpClient client = null;
      try
      {
        client = new HttpClient();
        request = new HttpRequestMessage(new HttpMethod("HEAD"), url);
        AddDefaultRequestHeaders(client, headers);
        response = client.SendAsync(request).Result;
        return new ResponseHttp(response);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        response?.Dispose();
        client.Dispose();
        request.Dispose();

      }
      return null;
    }

    public static async Task<ResponseHttp> HeadAsync(string url, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpRequestMessage request = null;
      HttpResponseMessage response = null;
      HttpClient client = null;
      try
      {
        client = new HttpClient();
        request = new HttpRequestMessage(new HttpMethod("HEAD"), url);
        AddDefaultRequestHeaders(client, headers);
        response = await client.SendAsync(request);
        return new ResponseHttp(response);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        response?.Dispose();
        client.Dispose();
        request.Dispose();
      }
      return null;
    }

    public static async Task<ResponseHttp> PostAsync(string url, object data, Dictionary<string, string> headers = null)
    {
      string str = JsonConvert.SerializeObject(data,
           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });
      StringContent content = new StringContent(str);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      ResponseHttp response = await PostAsync(url, content, headers);
      content.Dispose();
      return response;
    }

    public static async Task<ResponseHttp> PostAsync(string url, string data, Dictionary<string, string> headers = null)
    {
      StringContent content = new StringContent(data);
      ResponseHttp http = await PostAsync(url, content, headers);
      content.Dispose();
      return http;
    }

    public static async Task<ResponseHttp> PostAsync(string url, HttpContent data, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpResponseMessage response = null;
      HttpClient client = null;
      try
      {
        client = new HttpClient();
        AddHeaders(client, data, headers);

        response = await client.PostAsync(url, data);
        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);
        return http;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        client.Dispose();
        response?.Dispose();
      }

      return null;
    }

    public static async Task<ResponseHttp> PostAsync(string url, Dictionary<string, string> headers = null)
    {
      return await PostAsync(url, (HttpContent)null, headers);
    }

    public static ResponseHttp Put(string url, object data, Dictionary<string, string> headers = null)
    {
      string str = JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });
      StringContent content = new StringContent(str);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      ResponseHttp http = Put(url, content, headers);
      content.Dispose();
      return http;
    }

    public static ResponseHttp Put(string url, string data, Dictionary<string, string> headers = null)
    {
      StringContent content = new StringContent(data);
      ResponseHttp http = Put(url, data, headers);
      content.Dispose();
      return http;
    }

    public static ResponseHttp Put(string url, HttpContent content, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpResponseMessage response = null;
      HttpClient client = null;
      try
      {
        client = new HttpClient();
        AddHeaders(client, content, headers);
        response = client.PutAsync(url, content).Result;

        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);
        return http;

      }

      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        client.Dispose();
        response?.Dispose();
      }

      return null;
    }

    public static ResponseHttp Put(string url, Dictionary<string, string> headers = null)
    {
      return Put(url, (HttpContent)null, headers);
    }

    public static ResponseHttp Patch(string url, object data, Dictionary<string, string> headers = null)
    {
      string str = JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });
      StringContent content = new StringContent(str);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      ResponseHttp http = Patch(url, content, headers);
      content.Dispose();
      return http;
    }

    public static ResponseHttp Patch(string url, HttpContent content, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpResponseMessage response = null;
      HttpRequestMessage request = null;
      HttpClient client = null;

      try
      {
        client = new HttpClient();
        request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
        {
          Content = content
        };
        AddHeaders(client, content, headers);
        response = client.SendAsync(request).Result;
        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);
        return http;
      }

      catch (Exception)
      {
        return null;
      }
      finally
      {
        client.Dispose();
        response?.Dispose();
        request.Dispose();
      }

    }

    public static ResponseHttp Patch(string url, string data, Dictionary<string, string> headers = null)
    {
      StringContent content = new StringContent(data);
      ResponseHttp http = Patch(url, data, headers);
      content.Dispose();
      return http;
    }

    public static ResponseHttp Patch(string url, Dictionary<string, string> headers = null)
    {
      return Patch(url, (HttpContent)null, headers);
    }

    public static ResponseHttp Delete(string url, object data, Dictionary<string, string> headers = null)
    {
      string str = JsonConvert.SerializeObject(data,
          new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });

      StringContent content = new StringContent(str);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      ResponseHttp http = Delete(url, content, headers);
      content.Dispose();
      return http;
    }

    public static ResponseHttp Delete(string url, string data, Dictionary<string, string> headers = null)
    {
      StringContent content = new StringContent(data);
      ResponseHttp response = Delete(url, content, headers);
      content.Dispose();
      return response;
    }

    public static ResponseHttp Delete(string url, HttpContent content, Dictionary<string, string> headers = null)
    {
      url = url.GetValidUri();
      if (url == null) return null;

      HttpClient client = null;
      HttpRequestMessage request = null;
      HttpResponseMessage response = null;

      try
      {
        client = new HttpClient();
        AddHeaders(client, content, headers);

        request = new HttpRequestMessage(new HttpMethod("DELETE"), url)
        {
          Content = content
        };

        response = client.SendAsync(request).Result;
        ResponseHttp http = new ResponseHttp(response);
        http.SetBody(response);
        return http;
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        request.Dispose();
        client.Dispose();
        response?.Dispose();
      }

      return null;
    }

    public static ResponseHttp Delete(string url, Dictionary<string, string> headers = null)
    {
      return Delete(url, (HttpContent)null, headers);
    }

    public static void AddDefaultRequestHeaders(HttpClient client, Dictionary<string, string> headers)
    {
      if (headers == null) return;
      foreach (var item in headers)
      {
        client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
      }
    }

    public static void AddHeaders(HttpClient client, HttpContent content, Dictionary<string, string> headers)
    {
      if (headers == null) return;
      if (content == null)
      {
        AddDefaultRequestHeaders(client, headers);
        return;
      }
      foreach (var item in headers)
      {
        bool ok = !client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value)
            && content.Headers.TryAddWithoutValidation(item.Key, item.Value);
      }
    }

  }

  public static class HttpResponseHeadersExtensions
  {
    public static bool TryGetValue(this HttpResponseHeaders http, string name, out string value)
    {
      if (http.TryGetValues(name, out IEnumerable<string> values))
      {
        value = values.ElementAt(0);
        return true;
      }
      value = null;
      return false;
    }
  }

  public static class HttpContentHeadersExtensions
  {
    public static bool TryGetValue(this HttpContentHeaders http, string name, out string value)
    {
      if (http.TryGetValues(name, out IEnumerable<string> values))
      {
        value = values.ElementAt(0);
        return true;
      }
      value = null;
      return false;
    }
  }
}