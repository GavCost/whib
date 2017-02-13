namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using WhibModel;
  using WhibRegion = WhibModel.Region;

  class Program
  {
    static void Main(string[] args)
    {
      // Test the initial posting of regions.
      int regionsPosted = TestPostRegions();

      // Test the getting of the regions.
      TestGetRegions(regionsPosted);

      // Test the getting of individual regions.
      TestGetRegion(1, true);
      TestGetRegion(2, true);
      TestGetRegion(regionsPosted, true);
      TestGetRegion(0, false);
      TestGetRegion(-1, false);
      TestGetRegion(regionsPosted + 1, false);

      // Test the posting of cities.
      int citiesPosted = TestPostCities();

      // Test the getting of cities.
      TestGetCities(citiesPosted);

      // Test the getting of individual cities.
      TestGetCity(1, true);
      TestGetCity(2, true);
      TestGetCity(citiesPosted, true);
      TestGetCity(0, false);
      TestGetCity(-1, false);
      TestGetCity(citiesPosted + 1, false);

      // Now we have cities and regions in there, update the regions to have their capital cities.
      TestPutRegions();
    }

    private static int TestPostRegions()
    {
      // Load the regions from the Xml file and post them in.
      Region world = XmlModelAccessor.LoadRegions();

      Dictionary<int, string> cityList = new Dictionary<int, string>();
      Dictionary<int, string> regionLookup = new Dictionary<int, string>();
      List<WhibRegion> regionList = new List<WhibRegion>();

      TransformRegion.BuildRegionLookup(world, regionLookup);
      TransformRegion.TransformRegions(world, regionList, regionLookup);

      // Post them in with the parents first, then their children.
      IEnumerable<WhibRegion> sortedRegions = regionList.OrderBy(x => x.RegionType).ThenBy(x => x.ParentName).ThenBy(x => x.EnglishName);
      foreach (WhibRegion region in sortedRegions)
      {
        RegionApiCaller.CallPostRegion(region);
      }

      JsonModelAccessor.SaveRegions(sortedRegions.ToList(), 1);
      return sortedRegions.Count();
    }

    private static void TestGetRegions(int regionCount)
    {
      List<WhibRegion> returnedRegionList = RegionApiCaller.CallGetRegions();
      if (returnedRegionList.Count != regionCount)
      {
        throw new ApplicationException("Mismatch in region count.");
      }

      JsonModelAccessor.SaveRegions(returnedRegionList, 2);
    }

    private static void TestGetRegion(int id, bool expectResponse)
    {
      WhibRegion returnedRegion = RegionApiCaller.CallGetRegion(id);

      if (expectResponse && returnedRegion == null)
      {
        throw new ApplicationException(string.Format("Failed to find expected region for id {0}.", id));
      }
      else if (!expectResponse && returnedRegion != null)
      {
        throw new ApplicationException(string.Format("Found unexpected region for id {0}.", id));
      }
    }

    private static void TestPutRegions()
    {
      List<WhibRegion> returnedRegionList = RegionApiCaller.CallGetRegions();
      List<City> returnedCityList = CityApiCaller.CallGetCities();

      foreach (WhibRegion region in returnedRegionList)
      {
        City city = returnedCityList.FirstOrDefault(x => x.RegionId == region.Id);

        if (city != null)
        {
          region.Capital_CityId = city.Id;
          RegionApiCaller.CallPutRegion(region);
        }
      }

      returnedRegionList = RegionApiCaller.CallGetRegions();
      JsonModelAccessor.SaveRegions(returnedRegionList, 3);
    }

    private static int TestPostCities()
    {
      // Load the regions from the Xml file and the database.
      Region world = XmlModelAccessor.LoadRegions();
      List<WhibRegion> regionList = RegionApiCaller.CallGetRegions();

      List<City> cityList = new List<City>();
      TransformCity.TransformCities(world, cityList);

      foreach (City city in cityList)
      {
        CityApiCaller.CallPostCity(city);
      }

      JsonModelAccessor.SaveCities(cityList, 1);
      return cityList.Count();
    }

    private static void TestGetCities(int cityCount)
    {
      List<City> returnedCityList = CityApiCaller.CallGetCities();
      if (returnedCityList.Count != cityCount)
      {
        throw new ApplicationException("Mismatch in city count.");
      }

      JsonModelAccessor.SaveCities(returnedCityList, 2);
    }

    private static void TestGetCity(int id, bool expectResponse)
    {
      City returnedCity = CityApiCaller.CallGetCity(id);

      if (expectResponse && returnedCity == null)
      {
        throw new ApplicationException(string.Format("Failed to find expected city for id {0}.", id));
      }
      else if (!expectResponse && returnedCity != null)
      {
        throw new ApplicationException(string.Format("Found unexpected city for id {0}.", id));
      }
    }
  }
}
