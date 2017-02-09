namespace WhibServiceTest
{
  using System.Collections.Generic;
  using WhibService.Models;

  internal static class TransformCity
  {
    internal static void TransformCities(Region region, List<City> cityList)
    {
      if (region != null)
      {
        if (!string.IsNullOrEmpty(region.Capital) && !region.Capital.Equals("N/A"))
        {
          City city = new City()
          {
            RegionName = region.Name,
            EnglishName = region.Capital,
          };

          cityList.Add(city);
        }

        foreach (Region subRegion in region.SubRegionList)
        {
          TransformCities(subRegion, cityList);
        }
      }
    }
  }
}
