namespace WhibManager
{
  using System;
  using System.Windows.Forms;
  using WhibModel;

  public partial class RegionControl : UserControl
  {
    private Region originalRegion;
    private Region currentRegion;

    public RegionControl()
    {
      InitializeComponent();
    }

    public void SetRegion(Region region)
    {
      this.originalRegion = region;
      this.currentRegion = new Region()
      {
        EnglishName = region.EnglishName,
        LocalName = region.LocalName,
        IsoCode2 = region.IsoCode2,
        IsoCode3 = region.IsoCode3,
        AreaSqKm = region.AreaSqKm,
        Population = region.Population,
        Capital_CityId = region.Capital_CityId,
        Capital_CityName = region.Capital_CityName,
        Largest_CityId = region.Largest_CityId,
        Largest_CityName = region.Largest_CityName,
      };

      txtEnglishName.Text = this.currentRegion.EnglishName;
      txtLocalName.Text = this.currentRegion.LocalName;
      txtIsoCode2.Text = this.currentRegion.IsoCode2;
      txtIsoCode3.Text = this.currentRegion.IsoCode3;
      txtPopulation.Text = currentRegion.Population.ToString("N0");
      txtAreaSqKm.Text = currentRegion.AreaSqKm.ToString("N1");
      txtCapitalCity.Text = currentRegion.Capital_CityName;
      txtLargestCity.Text = currentRegion.Largest_CityName;

      ctlCities.SetCities(region.Cities);
    }
  }
}
