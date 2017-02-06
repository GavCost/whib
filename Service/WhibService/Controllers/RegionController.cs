using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhibService.DataAccessors;
using WhibService.Models;

namespace WhibService.Controllers
{
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
      return new Region()
      {
        EnglishName = "Test",
        LocalName = "Testski",
        AreaSqKm = 123.45M,
        Population = 67890,
      };
    }

    // POST: api/Region
    public void Post([FromBody]Region value)
    {
      RegionDataAccessor.MergeRegion(value);
    }

    // PUT: api/Region/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE: api/Region/5
    public void Delete(int id)
    {
    }
  }
}
