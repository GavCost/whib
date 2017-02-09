namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using WhibService.Models;
  using WhibRegion = WhibService.Models.Region;

  class Program
  {
    static void Main(string[] args)
    {
      // Test the initial posting of regions.
      int regionsPosted = TestPostRegions();

      // Test the getting of the regions.
      TestGetRegions(regionsPosted);

      // Test the posting of cities.
      int citiesPosted = TestPostCities();
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
      //if (returnedRegionList.Count != regionCount)
      //{
      //  throw new ApplicationException("Mismatch in region count.");
      //}

      JsonModelAccessor.SaveRegions(returnedRegionList, 2);
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
  }
}
