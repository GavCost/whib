namespace WhibService.Controllers
{
  using System;
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

    // PUT: api/City/5
    public void Put(int id, [FromBody]City value)
    {
      City returnedCity = CityDataAccessor.GetCity(id);

      if (returnedCity == null)
      {
        throw new ApplicationException("Item does not exist.");
      }

      CityDataAccessor.MergeCity(value);
    }

    ////// DELETE: api/City/5
    ////public void Delete(string name)
    ////{
    ////}
  }
}
