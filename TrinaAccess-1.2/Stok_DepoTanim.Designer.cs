namespace TrinaAccess_1._2
{
    partial class Stok_DepoTanim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stok_DepoTanim));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.DepoKoduTB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.DepoAdiTB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.CariKodTB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.CariAdTB = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EksiBakiyeCB = new DevExpress.XtraEditors.CheckEdit();
            this.KilitCB = new DevExpress.XtraEditors.CheckEdit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Depokodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepoAd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilitli = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cari = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepoKoduTB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepoAdiTB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CariKodTB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CariAdTB.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EksiBakiyeCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KilitCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(451, 32);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Depo Kodu";
            // 
            // DepoKoduTB
            // 
            this.DepoKoduTB.Location = new System.Drawing.Point(73, 56);
            this.DepoKoduTB.MenuManager = this.ribbon;
            this.DepoKoduTB.Name = "DepoKoduTB";
            this.DepoKoduTB.Size = new System.Drawing.Size(100, 20);
            this.DepoKoduTB.TabIndex = 0;
            this.DepoKoduTB.Leave += new System.EventHandler(this.textEdit1_Leave);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 85);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Depo Adı";
            // 
            // DepoAdiTB
            // 
            this.DepoAdiTB.Location = new System.Drawing.Point(73, 82);
            this.DepoAdiTB.Name = "DepoAdiTB";
            this.DepoAdiTB.Size = new System.Drawing.Size(185, 20);
            this.DepoAdiTB.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(4, 111);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Cari Kodu";
            // 
            // CariKodTB
            // 
            this.CariKodTB.Location = new System.Drawing.Point(73, 108);
            this.CariKodTB.Name = "CariKodTB";
            this.CariKodTB.Size = new System.Drawing.Size(100, 20);
            this.CariKodTB.TabIndex = 2;
            this.CariKodTB.Leave += new System.EventHandler(this.CariKodTB_Leave);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(4, 137);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Cari Adı";
            // 
            // CariAdTB
            // 
            this.CariAdTB.Enabled = false;
            this.CariAdTB.Location = new System.Drawing.Point(73, 134);
            this.CariAdTB.Name = "CariAdTB";
            this.CariAdTB.Size = new System.Drawing.Size(185, 20);
            this.CariAdTB.TabIndex = 3;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(176, 108);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(26, 20);
            this.simpleButton3.TabIndex = 74;
            this.simpleButton3.TabStop = false;
            this.simpleButton3.ToolTip = "Rehber";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EksiBakiyeCB);
            this.groupBox1.Controls.Add(this.KilitCB);
            this.groupBox1.Location = new System.Drawing.Point(273, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 116);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ayarlar";
            // 
            // EksiBakiyeCB
            // 
            this.EksiBakiyeCB.Location = new System.Drawing.Point(6, 50);
            this.EksiBakiyeCB.Name = "EksiBakiyeCB";
            this.EksiBakiyeCB.Properties.Caption = "Eksi Bakiye Kontrol";
            this.EksiBakiyeCB.Size = new System.Drawing.Size(121, 19);
            this.EksiBakiyeCB.TabIndex = 5;
            this.EksiBakiyeCB.Leave += new System.EventHandler(this.EksiBakiyeCB_Leave);
            // 
            // KilitCB
            // 
            this.KilitCB.Location = new System.Drawing.Point(6, 25);
            this.KilitCB.MenuManager = this.ribbon;
            this.KilitCB.Name = "KilitCB";
            this.KilitCB.Properties.Caption = "Kilitli Depo";
            this.KilitCB.Size = new System.Drawing.Size(121, 19);
            this.KilitCB.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Depokodu,
            this.DepoAd,
            this.Kilitli,
            this.Cari});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 175);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 15;
            this.dataGridView1.RowTemplate.Height = 19;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(451, 334);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // Depokodu
            // 
            this.Depokodu.HeaderText = "Depo Kodu";
            this.Depokodu.Name = "Depokodu";
            this.Depokodu.ReadOnly = true;
            // 
            // DepoAd
            // 
            this.DepoAd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DepoAd.HeaderText = "Depo Adı";
            this.DepoAd.Name = "DepoAd";
            this.DepoAd.ReadOnly = true;
            // 
            // Kilitli
            // 
            this.Kilitli.HeaderText = "Kilitli?";
            this.Kilitli.Name = "Kilitli";
            this.Kilitli.ReadOnly = true;
            this.Kilitli.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kilitli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Cari
            // 
            this.Cari.HeaderText = "Cari Kodu";
            this.Cari.Name = "Cari";
            this.Cari.ReadOnly = true;
            // 
            // Stok_DepoTanim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 511);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.CariAdTB);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.DepoAdiTB);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.CariKodTB);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.DepoKoduTB);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(469, 520);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(465, 518);
            this.Name = "Stok_DepoTanim";
            this.Ribbon = this.ribbon;
            this.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Hidden;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Depo Tanımları";
            this.Load += new System.EventHandler(this.Stok_DepoTanim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepoKoduTB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepoAdiTB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CariKodTB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CariAdTB.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EksiBakiyeCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KilitCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit DepoKoduTB;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit DepoAdiTB;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit CariKodTB;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit CariAdTB;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit EksiBakiyeCB;
        private DevExpress.XtraEditors.CheckEdit KilitCB;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Depokodu;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepoAd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Kilitli;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cari;
    }
}