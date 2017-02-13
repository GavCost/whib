namespace WhibServiceTest
{
  using System.Collections.Generic;
  using System.IO;
  using System.Net;
  using System.Text;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using WhibService.Models;

  internal class CityApiCaller
  {
    internal static List<City> CallGetCities()
    {
      List<City> cityList = new List<City>();

      try
      {
        WebRequest request = WebRequest.Create("http://localhost:59998/api/City");
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
          cityList.Add(item.ToObject<City>());
        }
      }
      catch { }

      return cityList;
    }

    internal static City CallGetCity(int id)
    {
      City city = null;

      try
      {
        WebRequest request = WebRequest.Create(string.Format("http://localhost:59998/api/City/{0}", id));
        request.Method = "GET";
        request.ContentType = "application/json";

        WebResponse response = request.GetResponse();

        string result = string.Empty;
        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        {
          result = sr.ReadToEnd();
          sr.Close();
        }

        city = JsonConvert.DeserializeObject<City>(result);
      }
      catch { }

      return city;
    }

    internal static void CallPostCity(City city)
    {
      try
      {
        WebRequest request = WebRequest.Create("http://localhost:59998/api/City");
        request.Method = "POST";
        request.ContentType = "application/json";

        // Create the data we want to send.
        string json = JsonConvert.SerializeObject(city);
        byte[] byteData = Encoding.UTF8.GetBytes(json);
        request.ContentLength = byteData.Length;

        // Write data to request.
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
