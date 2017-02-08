using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WhibRegion = WhibService.Models.Region;
using WhibRegionType = WhibService.Models.RegionType;

namespace WhibServiceTest
{
  class Program
  {
    static void Main(string[] args)
    {
      // Load the test files and transform to Whib Regions.
      Region world = LoadRegions();
      Dictionary<int, string> regionLookup = new Dictionary<int, string>();
      List<WhibRegion> regionList = new List<WhibRegion>();
      BuildRegionLookup(world, regionLookup);
      TransformRegions(world, regionList, regionLookup);
      IEnumerable<WhibRegion> sortedRegions = regionList.OrderBy(x => x.RegionType).ThenBy(x => x.ParentName).ThenBy(x => x.EnglishName);

      // Feed them into the WebService to test.
      foreach (WhibRegion region in sortedRegions)
      {
        CallWebService(region);
      }

      SaveRegions(sortedRegions.ToList());
    }

    private static void BuildRegionLookup(Region region, Dictionary<int, string> regionLookup)
    {
      if (region != null)
      {
        regionLookup.Add(region.Id, region.Name);
        foreach (Region subRegion in region.SubRegionList)
        {
          BuildRegionLookup(subRegion, regionLookup);
        }
      }
    }

    private static void TransformRegions(Region region, List<WhibRegion> regionList, Dictionary<int, string> regionLookup)
    {
      if (region != null)
      {
        WhibRegion whibRegion = new WhibRegion()
        {
          RegionType = ConvertRegionType(region.RegionType),
          ParentName = region.Parent_RegionId == 0 ? string.Empty : regionLookup[region.Parent_RegionId],
          EnglishName = region.Name,
          IsoCode2 = GetCode("Alpha2", region.Codes),
          IsoCode3 = GetCode("Alpha3", region.Codes),
          AreaSqKm = region.GetStatisticValue(Statistic.StatisticType.AreaSqKm),
          Population = Convert.ToInt64(region.GetStatisticValue(Statistic.StatisticType.Population)),
        };

        regionList.Add(whibRegion);

        foreach (Region subRegion in region.SubRegionList)
        {
          TransformRegions(subRegion, regionList, regionLookup);
        }
      }
    }

    private static string GetCode(string codeName, string codeList)
    {
      try
      {
        codeList = codeList.Replace("),(", "~");
        codeList = codeList.Replace("(", string.Empty);
        codeList = codeList.Replace(")", string.Empty);
        string[] codes = codeList.Split('~');

        foreach (string code in codes)
        {
          if (code.StartsWith(codeName))
          {
            return code.Split(',')[1];
          }
        }
      }
      catch { }

      return string.Empty;
    }

    private static WhibRegionType ConvertRegionType(Region.RegionLevel regionLevel)
    {
      switch (regionLevel)
      {
        case Region.RegionLevel.World:
          return WhibRegionType.World;
        case Region.RegionLevel.Continent:
          return WhibRegionType.Continent;
        case Region.RegionLevel.Country:
          return WhibRegionType.Country;
        case Region.RegionLevel.SubNational:
          return WhibRegionType.SubNational;
      }

      return WhibRegionType.Unknown;
    }

    private static Region LoadRegions()
    {
      Region world = null;

      try
      {
        Type[] subTypes = new Type[] { typeof(Statistic) };
        XmlSerializer serializer = new XmlSerializer(typeof(Region), subTypes);
        using (TextReader reader = new StreamReader(@"Data\Regions.xml"))
        {
          world = (Region)serializer.Deserialize(reader);
        }
      }
      catch
      { }

      return world;
    }

    private static void SaveRegions(List<WhibRegion> regionList)
    {
      try
      {
        string jsonObject = JsonConvert.SerializeObject(regionList, Formatting.Indented);
        File.WriteAllText(@"Data\Regions.json", jsonObject);
      }
      catch
      { }
    }

    private static void CallWebService(WhibRegion region)
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
