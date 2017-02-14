namespace WhibManager
{
  partial class WhibManagerForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tbpRegions = new System.Windows.Forms.TabPage();
      this.regionsControl1 = new RegionsControl();
      this.tbpOrganizations = new System.Windows.Forms.TabPage();
      this.tabControl1.SuspendLayout();
      this.tbpRegions.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tbpRegions);
      this.tabControl1.Controls.Add(this.tbpOrganizations);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(628, 475);
      this.tabControl1.TabIndex = 0;
      // 
      // tbpRegions
      // 
      this.tbpRegions.Controls.Add(this.regionsControl1);
      this.tbpRegions.Location = new System.Drawing.Point(4, 22);
      this.tbpRegions.Name = "tbpRegions";
      this.tbpRegions.Padding = new System.Windows.Forms.Padding(3);
      this.tbpRegions.Size = new System.Drawing.Size(620, 449);
      this.tbpRegions.TabIndex = 0;
      this.tbpRegions.Text = "Regions";
      // 
      // regionsControl1
      // 
      this.regionsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.regionsControl1.Location = new System.Drawing.Point(3, 3);
      this.regionsControl1.Name = "regionsControl1";
      this.regionsControl1.Size = new System.Drawing.Size(614, 443);
      this.regionsControl1.TabIndex = 0;
      // 
      // tbpOrganizations
      // 
      this.tbpOrganizations.Location = new System.Drawing.Point(4, 22);
      this.tbpOrganizations.Name = "tbpOrganizations";
      this.tbpOrganizations.Padding = new System.Windows.Forms.Padding(3);
      this.tbpOrganizations.Size = new System.Drawing.Size(620, 449);
      this.tbpOrganizations.TabIndex = 1;
      this.tbpOrganizations.Text = "Organizations";
      this.tbpOrganizations.UseVisualStyleBackColor = true;
      // 
      // WhibManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(628, 475);
      this.Controls.Add(this.tabControl1);
      this.Name = "WhibManager";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Whib Manager";
      this.Load += new System.EventHandler(this.WhibManager_Load);
      this.tabControl1.ResumeLayout(false);
      this.tbpRegions.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tbpRegions;
    private System.Windows.Forms.TabPage tbpOrganizations;
    private RegionsControl regionsControl1;
  }
}

