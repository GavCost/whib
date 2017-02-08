namespace WhibService.DataAccessors
{
  using System;
  using MySql.Data.MySqlClient;

  internal static class DataAccessorBase
  {
    internal enum ParamConversionType
    {
      Default,
      ConvertDefaultValueToNull,
      ConvertNullToDefaultValue
    }

    internal const string ConnectionString = @"server=localhost;userid=whib;password=Abc123?!;database=whib";

    internal static void AddInt32Param(MySqlCommand sqlCommand, string parameterName, int? parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.Int32);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = (object)parameterValue.Value ?? DBNull.Value;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = parameterValue.HasValue ? (parameterValue == 0 ? DBNull.Value : (object)parameterValue) : DBNull.Value;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = (object)parameterValue.Value ?? 0;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }

    internal static void AddInt64Param(MySqlCommand sqlCommand, string parameterName, long? parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.Int64);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = (object)parameterValue.Value ?? DBNull.Value;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = parameterValue.HasValue ? (parameterValue == 0 ? DBNull.Value : (object)parameterValue) : DBNull.Value;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = (object)parameterValue.Value ?? 0;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }

    internal static void AddDecimalParam(MySqlCommand sqlCommand, string parameterName, decimal? parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.Decimal);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = (object)parameterValue.Value ?? DBNull.Value;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = parameterValue.HasValue ? (parameterValue == 0M ? DBNull.Value : (object)parameterValue) : DBNull.Value;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = (object)parameterValue.Value ?? 0M;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }

    internal static void AddStringParam(MySqlCommand sqlCommand, string parameterName, int size, string parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.String, size);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = string.IsNullOrWhiteSpace(parameterValue) ? DBNull.Value : (object)parameterValue;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = string.IsNullOrWhiteSpace(parameterValue) ? DBNull.Value : (object)parameterValue;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = parameterValue == null ? string.Empty : parameterValue;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }

    internal static void AddBooleanParam(MySqlCommand sqlCommand, string parameterName, bool? parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.Int32);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = (object)parameterValue.Value ?? DBNull.Value;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = parameterValue.HasValue ? (parameterValue == false ? DBNull.Value : (object)parameterValue) : DBNull.Value;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = (object)parameterValue.Value ?? false;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }

    internal static void AddByteParam(MySqlCommand sqlCommand, string parameterName, byte? parameterValue, ParamConversionType conversionType = ParamConversionType.Default)
    {
      MySqlParameter param = new MySqlParameter(parameterName, MySqlDbType.Byte);

      switch (conversionType)
      {
        case ParamConversionType.Default:
          param.Value = (object)parameterValue.Value ?? DBNull.Value;
          break;

        case ParamConversionType.ConvertDefaultValueToNull:
          param.Value = parameterValue.HasValue ? (parameterValue == 0 ? DBNull.Value : (object)parameterValue) : DBNull.Value;
          break;

        case ParamConversionType.ConvertNullToDefaultValue:
          param.Value = (object)parameterValue.Value ?? 0;
          break;
      }

      sqlCommand.Parameters.Add(param);
    }
  }
}