namespace WhibServiceTest
{
  using System.Collections.Generic;
  using WhibModel;

  internal static class TransformCity
  {
    internal static void TransformCities(Region region, List<City> cityList)
    {
      if (region != null)
      {
        if (!string.IsNullOrEmpty(region.Capital) && !region.Capital.Equals("N/A"))
        {
          if (region.Name.Equals("Georgia") || region.Name.Equals("La Rioja"))
          {
            // We have to skip these two because there are two regions with the same names and
            // the current Xml to Json model does not support that.  It will when a full Json
            // model is finished.
          }
          else
          {
            City city = new City()
            {
              RegionName = region.Name,
              EnglishName = region.Capital,
            };

            cityList.Add(city);
          }
        }

        foreach (Region subRegion in region.SubRegionList)
        {
          TransformCities(subRegion, cityList);
        }
      }
    }
  }
}
