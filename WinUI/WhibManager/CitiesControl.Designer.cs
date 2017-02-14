namespace WhibManager
{
  partial class CitiesControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lvwCities = new System.Windows.Forms.ListView();
      this.clnCityName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.clhCityPopulation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.cityControl1 = new WhibManager.CityControl();
      this.SuspendLayout();
      // 
      // lvwCities
      // 
      this.lvwCities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnCityName,
            this.clhCityPopulation});
      this.lvwCities.Dock = System.Windows.Forms.DockStyle.Left;
      this.lvwCities.Location = new System.Drawing.Point(0, 0);
      this.lvwCities.Name = "lvwCities";
      this.lvwCities.Size = new System.Drawing.Size(231, 201);
      this.lvwCities.TabIndex = 0;
      this.lvwCities.UseCompatibleStateImageBehavior = false;
      this.lvwCities.View = System.Windows.Forms.View.Details;
      // 
      // clnCityName
      // 
      this.clnCityName.Text = "Name";
      this.clnCityName.Width = 140;
      // 
      // clhCityPopulation
      // 
      this.clhCityPopulation.Text = "Population";
      this.clhCityPopulation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.clhCityPopulation.Width = 80;
      // 
      // cityControl1
      // 
      this.cityControl1.Location = new System.Drawing.Point(237, 3);
      this.cityControl1.Name = "cityControl1";
      this.cityControl1.Size = new System.Drawing.Size(324, 86);
      this.cityControl1.TabIndex = 1;
      // 
      // CitiesControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.cityControl1);
      this.Controls.Add(this.lvwCities);
      this.Name = "CitiesControl";
      this.Size = new System.Drawing.Size(557, 201);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvwCities;
    private System.Windows.Forms.ColumnHeader clnCityName;
    private System.Windows.Forms.ColumnHeader clhCityPopulation;
    private CityControl cityControl1;
  }
}
