namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using WhibRegion = WhibService.Models.Region;
  using WhibRegionType = WhibService.Models.RegionType;

  internal static class TransformRegion
  {
    internal static void BuildRegionLookup(Region region, Dictionary<int, string> regionLookup)
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

    internal static void TransformRegions(Region region, List<WhibRegion> regionList, Dictionary<int, string> regionLookup)
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
 }
}
