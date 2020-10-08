using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace VvHelpers.Extensions
{
  public static class StringExtensions
  {
    public static byte[] ToBytes(this string data)
    {
      return Encoding.UTF8.GetBytes(data);
    }

    public static string GetValidUri(this string url)
    {

      if (url.Substring(0, 7) != "http://" && url.Substring(0, 8) != "https://")
      {
        url = "http://" + url;
      }

      return Uri.TryCreate(url, UriKind.Absolute, out Uri result) ? url : null;
    }

    public static T ToJsonObject<T>(this string s) where T : new()
    {
      try
      {
        JToken token = JToken.Parse(s);
        T search = token.ToObject<T>();
        return search;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return default(T);
      }
    }
  }
}
