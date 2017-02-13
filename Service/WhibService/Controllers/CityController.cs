namespace WhibService.Controllers
{
  using System.Collections.Generic;
  using System.Web.Http;
  using WhibService.DataAccessors;
  using WhibService.Models;

  public class CityController : ApiController
  {
    // GET: api/City
    public IEnumerable<City> Get()
    {
      return CityDataAccessor.GetCities();
    }

    // GET: api/City/5
    public City Get(int id)
    {
      return CityDataAccessor.GetCity(id);
    }

    // POST: api/City
    public void Post([FromBody]City value)
    {
      CityDataAccessor.MergeCity(value);
    }

    ////// PUT: api/City/5
    ////public void Put(string name, [FromBody]string value)
    ////{
    ////}

    ////// DELETE: api/City/5
    ////public void Delete(string name)
    ////{
    ////}
  }
}
