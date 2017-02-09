namespace WhibServiceTest
{
  using System.Collections.Generic;
  using System.IO;
  using Newtonsoft.Json;
  using WhibService.Models;
  using WhibRegion = WhibService.Models.Region;

  internal static class JsonModelAccessor
  {
    internal static void SaveRegions(List<WhibRegion> regionList, int saveNumber)
    {
      try
      {
        string jsonObject = JsonConvert.SerializeObject(regionList, Formatting.Indented);
        File.WriteAllText(string.Format(@"Data\Regions{0}.json", saveNumber), jsonObject);
      }
      catch
      { }
    }

    internal static void SaveCities(List<City> cityList, int saveNumber)
    {
      try
      {
        string jsonObject = JsonConvert.SerializeObject(cityList, Formatting.Indented);
        File.WriteAllText(string.Format(@"Data\Cities{0}.json", saveNumber), jsonObject);
      }
      catch
      { }
    }

  }
}
