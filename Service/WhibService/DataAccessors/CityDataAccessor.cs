namespace WhibService.DataAccessors
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using MySql.Data.MySqlClient;
  using WhibService.Models;

  public static class CityDataAccessor
  {
    public static IEnumerable<City> GetCities()
    {
      List<City> cityList = new List<City>();
      MySqlConnection connection = null;

      try
      {
        using (connection = new MySqlConnection(DataAccessorBase.ConnectionString))
        {
          connection.Open();

          string querySql = "SELECT Id, IsDeleted, RegionId, EnglishName, LocalName, Population FROM City;";
          MySqlCommand sqlCommand = new MySqlCommand(querySql, connection);

          MySqlDataReader reader = sqlCommand.ExecuteReader();

          while (reader.Read())
          {
            City city = new City();
            city.Id = reader.GetInt32(0);
            city.IsDeleted = reader.GetBoolean(1);
            city.RegionId = reader.GetInt32(2);
            city.EnglishName = reader.GetString(4);
            city.LocalName = reader.GetValue(5) == DBNull.Value ? (string)null : reader.GetString(5);
            city.Population = reader.GetInt64(6);
            cityList.Add(city);
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
  }
}