namespace WhibServiceTest
{
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Xml.Serialization;

  /// <summary>
  /// This class represents statistic for a region.  These can be anything from the basic population and area to more complex statistics.
  /// </summary>
  [DebuggerDisplay("Id = {Id}, Value = {Value}, IsCalculated = {IsCalculated}")]
  public class Statistic
  {
    public enum StatisticType
    {
      Unknown,
      Count,
      Initials,
      Population,
      AreaSqKm,
    }

    /// <summary>
    /// The type of the statistic.
    /// </summary>
    [XmlAttribute()]
    public StatisticType Id { get; set; }

    /// <summary>
    /// The value of the statistic.
    /// </summary>
    [XmlAttribute()]
    [DefaultValue(0)]
    public decimal Value { get; set; }
  }
}
