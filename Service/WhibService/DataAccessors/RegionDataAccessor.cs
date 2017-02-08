using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WhibService.Models;

namespace WhibService.DataAccessors
{
  public static class RegionDataAccessor
  {
    private const string connectionString = @"server=localhost;userid=whib;password=Abc123?!;database=whib";

    public static IEnumerable<Region> GetRegions()
    {
      List<Region> regionList = new List<Region>();
      MySqlConnection connection = null;

      try
      {
        using (connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          string querySql = "SELECT * FROM Region;";
          MySqlCommand sqlCommand = new MySqlCommand(querySql, connection);

          MySqlDataReader reader = sqlCommand.ExecuteReader();

          while (reader.Read())
          {
            Region region = new Region();
            region.Id = reader.GetInt32(0);
            region.EnglishName = reader.GetString(6);
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
          using (connection = new MySqlConnection(connectionString))
          {
            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand("Region_Merge", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            MySqlParameter isDeletedParam = new MySqlParameter("_IsDeleted", MySqlDbType.Bit);
            isDeletedParam.Value = region.IsDeleted;
            sqlCommand.Parameters.Add(isDeletedParam);

            MySqlParameter parentIdParam = new MySqlParameter("_ParentId", MySqlDbType.Int32);
            parentIdParam.Value = region.ParentId;
            sqlCommand.Parameters.Add(parentIdParam);

            MySqlParameter regionTypeParam = new MySqlParameter("_RegionType", MySqlDbType.Byte);
            regionTypeParam.Value = (byte)region.RegionType;
            sqlCommand.Parameters.Add(regionTypeParam);

            MySqlParameter englishNameParam = new MySqlParameter("_EnglishName", MySqlDbType.String, 200);
            englishNameParam.Value = region.EnglishName;
            sqlCommand.Parameters.Add(englishNameParam);

            MySqlParameter localNameParam = new MySqlParameter("_LocalName", MySqlDbType.String, 200);
            localNameParam.Value = region.LocalName;
            sqlCommand.Parameters.Add(localNameParam);

            MySqlParameter isoCode2Param = new MySqlParameter("_IsoCode2", MySqlDbType.String, 2);
            isoCode2Param.Value = region.IsoCode2;
            sqlCommand.Parameters.Add(isoCode2Param);

            MySqlParameter isoCode3Param = new MySqlParameter("_IsoCode3", MySqlDbType.String, 3);
            isoCode3Param.Value = region.IsoCode3;
            sqlCommand.Parameters.Add(isoCode3Param);

            MySqlParameter areaSqKmParam = new MySqlParameter("_AreaSqKm", MySqlDbType.Decimal);
            areaSqKmParam.Value = region.AreaSqKm;
            sqlCommand.Parameters.Add(areaSqKmParam);

            MySqlParameter populationParam = new MySqlParameter("_Population", MySqlDbType.Int64);
            populationParam.Value = region.Population;
            sqlCommand.Parameters.Add(populationParam);

            MySqlParameter capitalCityIdParam = new MySqlParameter("_Capital_CityId", MySqlDbType.Int32);
            capitalCityIdParam.Value = region.Capital_CityId;
            sqlCommand.Parameters.Add(capitalCityIdParam);

            MySqlParameter largestCityIdParam = new MySqlParameter("_Largest_CityId", MySqlDbType.Int32);
            largestCityIdParam.Value = region.Largest_CityId;
            sqlCommand.Parameters.Add(largestCityIdParam);

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