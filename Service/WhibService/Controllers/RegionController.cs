﻿namespace WhibService.Controllers
{
  using System.Collections.Generic;
  using System.Web.Http;
  using WhibService.DataAccessors;
  using WhibService.Models;

  public class RegionController : ApiController
  {
    // GET: api/Region
    public IEnumerable<Region> Get()
    {
      return RegionDataAccessor.GetRegions();
    }

    ////// GET: api/Region/5
    ////public Region Get(string name)
    ////{
    ////  return new Region()
    ////  {
    ////    EnglishName = "Test",
    ////    LocalName = "Testski",
    ////    AreaSqKm = 123.45M,
    ////    Population = 67890,
    ////  };
    ////}

    // POST: api/Region
    public void Post([FromBody]Region value)
    {
      RegionDataAccessor.MergeRegion(value);
    }

    ////// PUT: api/Region/5
    ////public void Put(string name, [FromBody]string value)
    ////{
    ////}

    ////// DELETE: api/Region/5
    ////public void Delete(string name)
    ////{
    ////}
  }
}
