﻿namespace WhibService.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Web.Http;
  using WhibService.DataAccessors;
  using WhibModel;

  public class RegionController : ApiController
  {
    // GET: api/Region
    public IEnumerable<Region> Get()
    {
      return RegionDataAccessor.GetRegions();
    }

    // GET: api/Region/5
    public Region Get(int id)
    {
      return RegionDataAccessor.GetRegion(id);
    }

    // POST: api/Region
    public void Post([FromBody]Region value)
    {
      RegionDataAccessor.MergeRegion(value);
    }

    // PUT: api/Region/5
    public void Put(int id, [FromBody]Region value)
    {
      Region returnedRegion = RegionDataAccessor.GetRegion(id);

      if (returnedRegion == null)
      {
        throw new ApplicationException("Item does not exist.");
      }

      RegionDataAccessor.MergeRegion(value);
    }

    ////// DELETE: api/Region/5
    ////public void Delete(string name)
    ////{
    ////}
  }
}
