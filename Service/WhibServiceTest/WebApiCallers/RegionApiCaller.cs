namespace WhibServiceTest
{
  using System.Collections.Generic;
  using System.IO;
  using System.Net;
  using System.Text;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using WhibRegion = WhibService.Models.Region;

  internal class RegionApiCaller
  {
    internal static List<WhibRegion> CallGetRegions()
    {
      List<WhibRegion> regionList = new List<WhibRegion>();

      try
      {
        WebRequest request = WebRequest.Create("http://localhost:59998/api/Region");
        request.Method = "GET";
        request.ContentType = "application/json";

        WebResponse response = request.GetResponse();

        string result = string.Empty;
        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        {
          result = sr.ReadToEnd();
          sr.Close();
        }

        JArray jsonList = (JArray)JsonConvert.DeserializeObject(result);
        foreach (var item in jsonList)
        {
          regionList.Add(item.ToObject<WhibRegion>());
        }
      }
      catch { }

      return regionList;
    }

    internal static void CallPostRegion(WhibRegion region)
    {
      try
      {
        WebRequest request = WebRequest.Create("http://localhost:59998/api/Region");
        request.Method = "POST";
        request.ContentType = "application/json";

        // Create the data we want to send
        string json = JsonConvert.SerializeObject(region);
        byte[] byteData = Encoding.UTF8.GetBytes(json);
        request.ContentLength = byteData.Length;

        // Write data to request
        using (Stream postStream = request.GetRequestStream())
        {
          postStream.Write(byteData, 0, byteData.Length);
        }

        WebResponse response = request.GetResponse();

        string result = "";
        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        {
          result = sr.ReadToEnd();
          sr.Close();
        }
      }
      catch { }
    }
  }
}
