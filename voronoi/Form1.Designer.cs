namespace voronoi
{
  partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BuStart = new System.Windows.Forms.ToolStripButton();
            this.BuEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.buMinus = new System.Windows.Forms.ToolStripLabel();
            this.buPlus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.buMinusMargin = new System.Windows.Forms.ToolStripLabel();
            this.buPlusMargin = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSave = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 523);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 719);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.BuStart_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BuStart,
            this.BuEnd,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.buMinus,
            this.buPlus,
            this.toolStripLabel3,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.buMinusMargin,
            this.buPlusMargin,
            this.toolStripLabel6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(805, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BuStart
            // 
            this.BuStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuStart.Image = ((System.Drawing.Image)(resources.GetObject("BuStart.Image")));
            this.BuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuStart.Name = "BuStart";
            this.BuStart.Size = new System.Drawing.Size(23, 22);
            this.BuStart.Text = "Start";
            this.BuStart.Click += new System.EventHandler(this.BuStart_Click);
            // 
            // BuEnd
            // 
            this.BuEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuEnd.Image = ((System.Drawing.Image)(resources.GetObject("BuEnd.Image")));
            this.BuEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuEnd.Name = "BuEnd";
            this.BuEnd.Size = new System.Drawing.Size(23, 22);
            this.BuEnd.Text = "toolStripButton2";
            this.BuEnd.Click += new System.EventHandler(this.BuEnd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel1.Text = "Random";
            // 
            // buMinus
            // 
            this.buMinus.Name = "buMinus";
            this.buMinus.Size = new System.Drawing.Size(12, 22);
            this.buMinus.Text = "-";
            this.buMinus.Click += new System.EventHandler(this.buMinus_Click);
            // 
            // buPlus
            // 
            this.buPlus.Name = "buPlus";
            this.buPlus.Size = new System.Drawing.Size(15, 22);
            this.buPlus.Text = "+";
            this.buPlus.Click += new System.EventHandler(this.buPlus_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(13, 22);
            this.toolStripLabel3.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel2.Text = "Error margin";
            // 
            // buMinusMargin
            // 
            this.buMinusMargin.Name = "buMinusMargin";
            this.buMinusMargin.Size = new System.Drawing.Size(12, 22);
            this.buMinusMargin.Text = "-";
            this.buMinusMargin.Click += new System.EventHandler(this.buMinusMargin_Click);
            // 
            // buPlusMargin
            // 
            this.buPlusMargin.Name = "buPlusMargin";
            this.buPlusMargin.Size = new System.Drawing.Size(15, 22);
            this.buPlusMargin.Text = "+";
            this.buPlusMargin.Click += new System.EventHandler(this.buPlusMargin_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(13, 22);
            this.toolStripLabel6.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(805, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniOpen,
            this.mniSave});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // mniOpen
            // 
            this.mniOpen.Name = "mniOpen";
            this.mniOpen.Size = new System.Drawing.Size(103, 22);
            this.mniOpen.Text = "Open";
            this.mniOpen.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // mniSave
            // 
            this.mniSave.Name = "mniSave";
            this.mniSave.Size = new System.Drawing.Size(103, 22);
            this.mniSave.Text = "Save";
            this.mniSave.Click += new System.EventHandler(this.mniSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 523);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Triangulate";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton BuStart;
    private System.Windows.Forms.ToolStripButton BuEnd;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mniOpen;
    private System.Windows.Forms.ToolStripMenuItem mniSave;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.ToolStripLabel buPlus;
    private System.Windows.Forms.ToolStripLabel buMinus;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripLabel buMinusMargin;
    private System.Windows.Forms.ToolStripLabel buPlusMargin;
    private System.Windows.Forms.ToolStripLabel toolStripLabel6;
  }
}

