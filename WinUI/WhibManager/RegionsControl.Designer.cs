namespace WhibManager
{
  partial class RegionsControl
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
      this.trvRegions = new System.Windows.Forms.TreeView();
      this.ctlRegion = new WhibManager.RegionControl();
      this.SuspendLayout();
      // 
      // trvRegions
      // 
      this.trvRegions.Dock = System.Windows.Forms.DockStyle.Left;
      this.trvRegions.Location = new System.Drawing.Point(0, 0);
      this.trvRegions.Name = "trvRegions";
      this.trvRegions.Size = new System.Drawing.Size(296, 446);
      this.trvRegions.TabIndex = 0;
      this.trvRegions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvRegions_AfterSelect);
      // 
      // ctlRegion
      // 
      this.ctlRegion.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ctlRegion.Location = new System.Drawing.Point(296, 0);
      this.ctlRegion.Name = "ctlRegion";
      this.ctlRegion.Size = new System.Drawing.Size(498, 446);
      this.ctlRegion.TabIndex = 1;
      // 
      // RegionsControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.ctlRegion);
      this.Controls.Add(this.trvRegions);
      this.Name = "RegionsControl";
      this.Size = new System.Drawing.Size(794, 446);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView trvRegions;
    private RegionControl ctlRegion;
  }
}
