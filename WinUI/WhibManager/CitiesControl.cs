using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhibModel;

namespace WhibManager
{
  public partial class CitiesControl : UserControl
  {
    public CitiesControl()
    {
      InitializeComponent();
    }

    public void SetCities(List<City> cities)
    {
      lvwCities.BeginUpdate();
      lvwCities.Items.Clear();

      foreach (City city in cities)
      {
        ListViewItem cityItem = new ListViewItem(new string[] { city.EnglishName, city.Population.ToString("N0") });
        lvwCities.Items.Add(cityItem);
      }

      lvwCities.EndUpdate();
    }
  }
}
