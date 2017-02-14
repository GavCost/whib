namespace WhibService.DataAccessors
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;
  using MySql.Data.MySqlClient;
  using WhibModel;

  /// <summary>
  /// This class provides access to the region table in the database.
  /// </summary>
  public static class RegionDataAccessor
  {
    /// <summary>
    /// The list of fields that are read back from the database for a region.
    /// </summary>
    private const string FieldList = "SELECT Id, IsDeleted, ParentId, Region_GetNameFromId(ParentId) AS ParentName, RegionType, EnglishName, LocalName, IsoCode2, IsoCode3, AreaSqKm, Population, Capital_CityId, City_GetNameFromId(Capital_CityId) AS Capital_CityName, Largest_CityId, City_GetNameFromId(Largest_CityId) AS Largest_CityName FROM Region";

    /// <summary>
    /// Returns the list of all regions from the database.
    /// </summary>
    /// <returns>A list of all the regions from the database.</returns>
    public static IEnumerable<Region> GetRegions()
    {
      string querySql = string.Format("{0};", FieldList);
      return ExecuteSelectRegion(querySql);
    }

    /// <summary>
    /// Returns the list of all regions from the database for a given region id.
    /// </summary>
    /// <param name="parentId">The region id to get the list for.</param>
    /// <returns>A list of all the cities from the database.</returns>
    public static IEnumerable<Region> GetRegionsByParentId(int parentId)
    {
      string querySql = string.Format("{0} WHERE ParentId = {1};", FieldList, parentId);
      return ExecuteSelectRegion(querySql);
    }

    /// <summary>
    /// Returns an individual region with the given id number.
    /// </summary>
    /// <param name="id">The id number of the region to be returned.</param>
    /// <returns>The region for the given id number, or null if not found.</returns>
    public static Region GetRegion(int id)
    {
      Region region = null;

      try
      {
        string querySql = string.Format("{0} WHERE Id = {1};", FieldList, id);
        IEnumerable<Region> regionList = ExecuteSelectRegion(querySql);
        if (regionList == null || regionList.FirstOrDefault() == null || regionList.Count() > 1)
        {
          return null;
        }
        else
        {
          region = regionList.First();

          // In these cases we get the list of sub-regions for a region and the list of cities.
          region.SubRegions = GetRegionsByParentId(region.Id).ToList();
          region.Cities = CityDataAccessor.GetCitiesByRegionId(region.Id).ToList();
          return region;
        }
      }
      catch { }

      return region;
    }

    /// <summary>
    /// Calls the merge stored procedure to insert or update a region as required.
    /// </summary>
    /// <param name="region">The region to be merged into the database.</param>
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
            DataAccessorBase.AddStringParam(sqlCommand, "_ParentName", 200, region.ParentName, DataAccessorBase.ParamConversionType.ConvertDefaultValueToNull);
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
    /// Returns a list of all regions from the database.
    /// </summary>
    /// <param name="querySql">The sql to get the regions from the database.</param>
    /// <returns>A list of all the regions from the database for the given query.</returns>
    private static IEnumerable<Region> ExecuteSelectRegion(string querySql)
    {
      List<Region> regionList = new List<Region>();
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
            Region region = PopulateRegionFromReader(reader);
            if (region != null)
            {
              regionList.Add(region);
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

      return regionList;
    }

    /// <summary>
    /// Creates and populates a region object from a database reader.
    /// </summary>
    /// <param name="reader">The reader object to get the field information from.</param>
    /// <returns>A region object, or null if there is no object (if the Id field is null).</returns>
    private static Region PopulateRegionFromReader(MySqlDataReader reader)
    {
      if (reader.GetValue(0) == DBNull.Value)
      {
        return null;
      }
      else
      {
        Region region = new Region();
        region.Id = reader.GetInt32(0);
        region.IsDeleted = reader.GetBoolean(1);
        region.ParentId = reader.GetValue(2) == DBNull.Value ? (int?)null : reader.GetInt32(2);
        region.ParentName = reader.GetValue(3) == DBNull.Value ? (string)null : reader.GetString(3);
        region.RegionType = (RegionType)reader.GetByte(4);
        region.EnglishName = reader.GetString(5);
        region.LocalName = reader.GetValue(6) == DBNull.Value ? (string)null : reader.GetString(6);
        region.IsoCode2 = reader.GetValue(7) == DBNull.Value ? (string)null : reader.GetString(7);
        region.IsoCode3 = reader.GetValue(8) == DBNull.Value ? (string)null : reader.GetString(8);
        region.AreaSqKm = reader.GetDecimal(9);
        region.Population = reader.GetInt64(10);
        region.Capital_CityId = reader.GetValue(11) == DBNull.Value ? (int?)null : reader.GetInt32(11);
        region.Capital_CityName = reader.GetValue(12) == DBNull.Value ? (string)null : reader.GetString(12);
        region.Largest_CityId = reader.GetValue(13) == DBNull.Value ? (int?)null : reader.GetInt32(13);
        region.Largest_CityName = reader.GetValue(14) == DBNull.Value ? (string)null : reader.GetString(14);
        return region;
      }
    }
  }
}