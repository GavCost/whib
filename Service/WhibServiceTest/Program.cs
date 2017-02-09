namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Newtonsoft.Json;
  using WhibRegion = WhibService.Models.Region;
  using WhibRegionType = WhibService.Models.RegionType;

  class Program
  {
    static void Main(string[] args)
    {
      // Load the test files and transform to Whib Regions.
      Region world = XmlModelAccessor.LoadRegions();
      Dictionary<int, string> cityList = new Dictionary<int, string>();
      Dictionary<int, string> regionLookup = new Dictionary<int, string>();
      List<WhibRegion> regionList = new List<WhibRegion>();
      ////BuildCityList(world, cityList);
      BuildRegionLookup(world, regionLookup);
      TransformRegions(world, regionList, regionLookup);
      IEnumerable<WhibRegion> sortedRegions = regionList.OrderBy(x => x.RegionType).ThenBy(x => x.ParentName).ThenBy(x => x.EnglishName);

      // Feed them into the WebService to test.
      ////foreach (var item in cityList)
      ////{
      ////  City city = new City()
      ////  {
      ////    RegionName = item.Key,
      ////    EnglishName = item.Value,
      ////  };
      ////  CallPostCity(city);
      ////}

      foreach (WhibRegion region in sortedRegions)
      {
        RegionApiCaller.CallPostRegion(region);
      }

      List<WhibRegion> returnedRegionList = RegionApiCaller.CallGetRegions();

      SaveRegions(sortedRegions.ToList());
    }

    ////private static void BuildCityList(Region region, Dictionary<int, string> cityList)
    ////{
    ////  if (region != null)
    ////  {
    ////    cityList.Add(region.Id, region.Capital);
    ////    foreach (Region subRegion in region.SubRegionList)
    ////    {
    ////      BuildCityList(subRegion, cityList);
    ////    }
    ////  }
    ////}

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
        if (codeList != null)
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
  }
}
