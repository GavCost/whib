namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Xml.Serialization;

  public abstract class SummableObject
  {
    public virtual int Id { get; set; }

    public virtual string Name { get; set; }

    [XmlIgnore]
    public virtual List<Statistic> Statistics { get; protected set; }

    [XmlIgnore]
    public virtual List<Region> SubRegionList { get; protected set; }

    [XmlIgnore]
    public List<char> CharList { get; protected set; }

    /// <summary>
    /// Represents the area (in square kilometres) of the region.
    /// </summary>
    [XmlIgnore]
    public decimal AreaSqKm
    {
      get { return GetStatisticValue(Statistic.StatisticType.AreaSqKm); }
      set { SetStatisticValue(Statistic.StatisticType.AreaSqKm, value); }
    }

    /// <summary>
    /// Represents the population of the region.
    /// </summary>
    [XmlIgnore]
    public decimal Population
    {
      get { return GetStatisticValue(Statistic.StatisticType.Population); }
      set { SetStatisticValue(Statistic.StatisticType.Population, value); }
    }

    public SummableObject()
    {
      this.SubRegionList = new List<Region>();
      this.Statistics = new List<Statistic>();
      this.CharList = new List<char>();
    }

    public void CalculateStatistics()
    {
      CalculateTotal(Statistic.StatisticType.AreaSqKm);
      CalculateTotal(Statistic.StatisticType.Population);

      foreach (Region region in SubRegionList)
      {
        if (!CharList.Contains(region.Name[0]))
        {
          CharList.Add(region.Name[0]);
        }
      }
    }

    public Statistic GetStatistic(Statistic.StatisticType statistic)
    {
      Statistic stat = this.Statistics.Find(x => x.Id == statistic);
      if (stat == null)
      {
        stat = new Statistic() { Id = statistic };
        this.Statistics.Add(stat);
      }

      return stat;
    }

    public decimal GetStatisticValue(Statistic.StatisticType statistic)
    {
      Statistic stat = this.Statistics.Find(x => x.Id == statistic);
      if (stat != null)
      {
        return stat.Value;
      }
      else
      {
        return 0;
      }
    }

    public void SetStatisticValue(Statistic.StatisticType statistic, decimal value)
    {
      Statistic stat = this.Statistics.Find(x => x.Id == statistic);
      if (stat != null)
      {
        stat.Value = value;
      }
      else
      {
        this.Statistics.Add(new Statistic() { Id = statistic, Value = value });
      }
    }

    private decimal CalculateTotal(Statistic.StatisticType statType)
    {
      Statistic stat = this.GetStatistic(statType);

      // Get the total for the subregions.
      decimal subRegionTotal = 0;

      if (this.SubRegionList.Count > 0)
      {
        foreach (Region region in this.SubRegionList)
        {
          subRegionTotal += region.CalculateTotal(statType);
        }

        stat.Value = subRegionTotal;
      }

      return stat.Value;
    }
  }
}
