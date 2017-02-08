namespace WhibService.DataAccessors
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using MySql.Data.MySqlClient;
  using WhibService.Models;

  public static class RegionDataAccessor
  {
    public static IEnumerable<Region> GetRegions()
    {
      List<Region> regionList = new List<Region>();
      MySqlConnection connection = null;

      try
      {
        using (connection = new MySqlConnection(DataAccessorBase.ConnectionString))
        {
          connection.Open();

          string querySql = "SELECT Id, IsDeleted, ParentId, RegionType, EnglishName, LocalName, IsoCode2, IsoCode3, AreaSqKm, Population, Capital_CityId, Largest_CityId FROM Region;";
          MySqlCommand sqlCommand = new MySqlCommand(querySql, connection);

          MySqlDataReader reader = sqlCommand.ExecuteReader();

          while (reader.Read())
          {
            Region region = new Region();
            region.Id = reader.GetInt32(0);
            region.IsDeleted = reader.GetBoolean(1);
            region.ParentId = reader.GetValue(2) == DBNull.Value ? (int?)null : reader.GetInt32(2);
            region.RegionType = (RegionType)reader.GetByte(3);
            region.EnglishName = reader.GetString(4);
            region.LocalName = reader.GetValue(5) == DBNull.Value ? (string)null : reader.GetString(5);
            region.IsoCode2 = reader.GetValue(6) == DBNull.Value ? (string)null : reader.GetString(6);
            region.IsoCode3 = reader.GetValue(7) == DBNull.Value ? (string)null : reader.GetString(7);
            region.AreaSqKm = reader.GetDecimal(8);
            region.Population = reader.GetInt64(9);
            region.Capital_CityId = reader.GetValue(10) == DBNull.Value ? (int?)null : reader.GetInt32(10);
            region.Largest_CityId = reader.GetValue(11) == DBNull.Value ? (int?)null : reader.GetInt32(11);
            regionList.Add(region);
          }
        }

        if (connection.State != ConnectionState.Closed)
        {
          connection.Close();
        }
      }
      catch (MySqlException ex)
      {
      }
      finally
      {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
          connection.Close();
        }
      }

      return regionList;
    }

    public static void MergeRegion(Region region)
    {
      MySqlConnection connection = null;

      try
      {
        if (region != null)
        {
          using (connection = new MySqlConnection(DataAccessorBase.ConnectionString))
          {
            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand("Region_Merge", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            DataAccessorBase.AddBooleanParam(sqlCommand, "_IsDeleted", region.IsDeleted);
            DataAccessorBase.AddInt32Param(sqlCommand, "_ParentId", region.ParentId, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);
            DataAccessorBase.AddByteParam(sqlCommand, "_RegionType", (byte)region.RegionType);
            DataAccessorBase.AddStringParam(sqlCommand, "_EnglishName", 200, region.EnglishName);
            DataAccessorBase.AddStringParam(sqlCommand, "_LocalName", 200, region.LocalName, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);
            DataAccessorBase.AddStringParam(sqlCommand, "_IsoCode2", 2, region.IsoCode2);
            DataAccessorBase.AddStringParam(sqlCommand, "_IsoCode3", 3, region.IsoCode3);
            DataAccessorBase.AddDecimalParam(sqlCommand, "_AreaSqKm", region.AreaSqKm);
            DataAccessorBase.AddInt64Param(sqlCommand, "_Population", region.Population);
            DataAccessorBase.AddInt32Param(sqlCommand, "_Capital_CityId", region.Capital_CityId, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);
            DataAccessorBase.AddInt32Param(sqlCommand, "_Largest_CityId", region.Largest_CityId, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);

            sqlCommand.ExecuteNonQuery();
          }

          if (connection.State != ConnectionState.Closed)
          {
            connection.Close();
          }
        }
      }
      catch (MySqlException ex)
      {
      }
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