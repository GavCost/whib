namespace WhibServiceTest
{
  using System.IO;
  using System.Net;
  using System.Text;
  using Newtonsoft.Json;
  using WhibService.Models;

  internal class CityApiCaller
  {
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
