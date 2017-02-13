namespace WhibManager
{
  using System;
  using System.Windows.Forms;

  public partial class WhibManager : Form
  {
    public WhibManager()
    {
      InitializeComponent();
    }

    private void WhibManager_Load(object sender, EventArgs e)
    {
      regionControl1.LoadRegions();
    }
  }
}
