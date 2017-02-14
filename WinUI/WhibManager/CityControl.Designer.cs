namespace WhibManager
{
  partial class CityControl
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
      this.txtPopulation = new System.Windows.Forms.TextBox();
      this.lblPopulation = new System.Windows.Forms.Label();
      this.txtLocalName = new System.Windows.Forms.TextBox();
      this.txtEnglishName = new System.Windows.Forms.TextBox();
      this.lblLocalName = new System.Windows.Forms.Label();
      this.lblEnglishName = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtPopulation
      // 
      this.txtPopulation.Location = new System.Drawing.Point(112, 55);
      this.txtPopulation.MaxLength = 30;
      this.txtPopulation.Name = "txtPopulation";
      this.txtPopulation.Size = new System.Drawing.Size(200, 20);
      this.txtPopulation.TabIndex = 39;
      this.txtPopulation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblPopulation
      // 
      this.lblPopulation.Location = new System.Drawing.Point(6, 58);
      this.lblPopulation.Name = "lblPopulation";
      this.lblPopulation.Size = new System.Drawing.Size(100, 13);
      this.lblPopulation.TabIndex = 38;
      this.lblPopulation.Text = "Population";
      // 
      // txtLocalName
      // 
      this.txtLocalName.Location = new System.Drawing.Point(112, 29);
      this.txtLocalName.MaxLength = 200;
      this.txtLocalName.Name = "txtLocalName";
      this.txtLocalName.Size = new System.Drawing.Size(200, 20);
      this.txtLocalName.TabIndex = 37;
      // 
      // txtEnglishName
      // 
      this.txtEnglishName.Location = new System.Drawing.Point(112, 3);
      this.txtEnglishName.MaxLength = 200;
      this.txtEnglishName.Name = "txtEnglishName";
      this.txtEnglishName.Size = new System.Drawing.Size(200, 20);
      this.txtEnglishName.TabIndex = 36;
      // 
      // lblLocalName
      // 
      this.lblLocalName.Location = new System.Drawing.Point(6, 32);
      this.lblLocalName.Name = "lblLocalName";
      this.lblLocalName.Size = new System.Drawing.Size(100, 13);
      this.lblLocalName.TabIndex = 35;
      this.lblLocalName.Text = "Local Name";
      // 
      // lblEnglishName
      // 
      this.lblEnglishName.Location = new System.Drawing.Point(6, 6);
      this.lblEnglishName.Name = "lblEnglishName";
      this.lblEnglishName.Size = new System.Drawing.Size(100, 13);
      this.lblEnglishName.TabIndex = 34;
      this.lblEnglishName.Text = "English Name";
      // 
      // CityControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtPopulation);
      this.Controls.Add(this.lblPopulation);
      this.Controls.Add(this.txtLocalName);
      this.Controls.Add(this.txtEnglishName);
      this.Controls.Add(this.lblLocalName);
      this.Controls.Add(this.lblEnglishName);
      this.Name = "CityControl";
      this.Size = new System.Drawing.Size(324, 86);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtPopulation;
    private System.Windows.Forms.Label lblPopulation;
    private System.Windows.Forms.TextBox txtLocalName;
    private System.Windows.Forms.TextBox txtEnglishName;
    private System.Windows.Forms.Label lblLocalName;
    private System.Windows.Forms.Label lblEnglishName;
  }
}
