namespace WhibModel
{
  using System.Diagnostics;

  /// <summary>
  /// This class represents a city.
  /// </summary>
  [DebuggerDisplay("Name = {EnglishName}, RegionName = {RegionName}, Id = {Id}")]
  public class City
  {
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int RegionId { get; set; }

    public string RegionName { get; set; }

    public string EnglishName { get; set; }

    public string LocalName { get; set; }

    public long Population { get; set; }
  }
}