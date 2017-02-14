namespace WhibService.DataAccessors
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;
  using MySql.Data.MySqlClient;
  using WhibModel;

  /// <summary>
  /// This class provides access to the city table in the database.
  /// </summary>
  public static class CityDataAccessor
  {
    /// <summary>
    /// The list of fields that are read back from the database for a city.
    /// </summary>
    private const string FieldList = "SELECT Id, IsDeleted, RegionId, EnglishName, LocalName, Population FROM City";

    /// <summary>
    /// Returns the list of all cities from the database.
    /// </summary>
    /// <returns>A list of all the cities from the database.</returns>
    public static IEnumerable<City> GetCities()
    {
      string querySql = string.Format("{0};", FieldList);
      return ExecuteSelectCity(querySql);
    }

    /// <summary>
    /// Returns the list of all cities from the database for a given region id.
    /// </summary>
    /// <param name="regionId">The region id to get the list for.</param>
    /// <returns>A list of all the cities from the database.</returns>
    public static IEnumerable<City> GetCitiesByRegionId(int regionId)
    {
      string querySql = string.Format("{0} WHERE RegionId = {1};", FieldList, regionId);
      return ExecuteSelectCity(querySql);
    }

    /// <summary>
    /// Returns an individual city with the given id number.
    /// </summary>
    /// <param name="id">The id number of the city to be returned.</param>
    /// <returns>The city for the given id number, or null if not found.</returns>
    public static City GetCity(int id)
    {
      string querySql = string.Format("{0} WHERE Id = {1};", FieldList, id);
      IEnumerable<City> cityList = ExecuteSelectCity(querySql);
      if (cityList == null || cityList.FirstOrDefault() == null || cityList.Count() > 1)
      {
        return null;
      }
      else
      {
        return cityList.First();
      }
    }

    /// <summary>
    /// Calls the merge stored procedure to insert or update a city as required.
    /// </summary>
    /// <param name="city">The city to be merged into the database.</param>
    public static void MergeCity(City city)
    {
      MySqlConnection connection = null;

      try
      {
        if (city != null)
        {
          using (connection = new MySqlConnection(DataAccessorBase.ConnectionString))
          {
            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand("City_Merge", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            DataAccessorBase.AddBooleanParam(sqlCommand, "_IsDeleted", city.IsDeleted);
            DataAccessorBase.AddStringParam(sqlCommand, "_RegionName", 200, city.RegionName);
            DataAccessorBase.AddStringParam(sqlCommand, "_EnglishName", 200, city.EnglishName);
            DataAccessorBase.AddStringParam(sqlCommand, "_LocalName", 200, city.LocalName, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);
            DataAccessorBase.AddInt64Param(sqlCommand, "_Population", city.Population);

            sqlCommand.ExecuteNonQuery();
          }

          if (connection.State != ConnectionState.Closed)
          {
            connection.Close();
          }
        }
      }
      catch { }
      finally
      {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
          connection.Close();
        }
      }
    }

    /// <summary>
    /// Returns a list of all cities from the database.
    /// </summary>
    /// <param name="querySql">The sql to get the cities from the database.</param>
    /// <returns>A list of all the cities from the database for the given query.</returns>
    private static IEnumerable<City> ExecuteSelectCity(string querySql)
    {
      List<City> cityList = new List<City>();
      MySqlConnection connection = null;

      try
      {
        using (connection = new MySqlConnection(DataAccessorBase.ConnectionString))
        {
          connection.Open();
          MySqlCommand sqlCommand = new MySqlCommand(querySql, connection);
          MySqlDataReader reader = sqlCommand.ExecuteReader();

          while (reader.Read())
          {
            City city = PopulateCityFromReader(reader);
            if (city != null)
            {
              cityList.Add(city);
            }
          }
        }

        if (connection.State != ConnectionState.Closed)
        {
          connection.Close();
        }
      }
      catch { }
      finally
      {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
          connection.Close();
        }
      }

      return cityList;
    }

    /// <summary>
    /// Creates and populates a city object from a database reader.
    /// </summary>
    /// <param name="reader">The reader object to get the field information from.</param>
    /// <returns>A city object, or null if there is no object (if the Id field is null).</returns>
    private static City PopulateCityFromReader(MySqlDataReader reader)
    {
      if (reader.GetValue(0) == DBNull.Value)
      {
        return null;
      }
      else
      {
        City city = new City();
        city.Id = reader.GetInt32(0);
        city.IsDeleted = reader.GetBoolean(1);
        city.RegionId = reader.GetInt32(2);
        city.EnglishName = reader.GetString(3);
        city.LocalName = reader.GetValue(4) == DBNull.Value ? (string)null : reader.GetString(5);
        city.Population = reader.GetInt64(5);
        return city;
      }
    }
  }
}