namespace WhibManager
{
  using System;
  using System.Windows.Forms;

  public partial class WhibManagerForm : Form
  {
    public WhibManagerForm()
    {
      InitializeComponent();
    }

    private void WhibManager_Load(object sender, EventArgs e)
    {
      regionsControl1.LoadRegions();
    }
  }
}
