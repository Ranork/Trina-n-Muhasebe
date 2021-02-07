using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace TrinaAccess_1._2
{
    public partial class Panel : DevExpress.XtraBars.TabForm
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void Panel_Load(object sender, EventArgs e)
        {
            Size a = new Size(809, 459);

            this.Size = new Size(0, 0);
            this.Enabled = false;
            this.ShowInTaskbar = false;

            Giris grs = new Giris();
            grs.ShowDialog();

            WindowState = FormWindowState.Normal;
            timer1.Enabled = true;

            this.Size = a;
            this.Enabled = true;
            this.ShowInTaskbar = true;



            barStaticItem1.Caption = "   " + DateTime.Today.ToString("dd.MM.yyyy");
            barStaticItem2.Caption = Giris.userid;
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            radialMenu1.ShowPopup(Cursor.Position);
            radialMenu1.Expand();
            
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {

            Application.ExitThread();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hakkinda hknd = new Hakkinda();
            radialMenu1.HidePopup();
            hknd.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;
            timer1.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Cari_Panel cri = new Cari_Panel();
            cri.Show();
        }

        private void Panel_FormClosing(object sender, FormClosingEventArgs e)
        {

            radialMenu1.Dispose();
            System.Threading.Thread.Sleep(50);

            DialogResult dr = MessageBox.Show("Çıkış yapmak İstediğinize emin misiniz ?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            Application.Exit();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Stok_Panel stk = new Stok_Panel();
            stk.Show();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            KullaniciPanel kpnl = new KullaniciPanel();
            kpnl.Show();
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {

            radialMenu1.HidePopup();
            simpleButton10_Click(sender, e);
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pictureBox4_DoubleClick(sender, e);
            }
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            radialMenu1.HidePopup();
            KullaniciGecmis gecmis = new KullaniciGecmis();
            gecmis.Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Fatura_Panel ftr = new Fatura_Panel();
            ftr.Show();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Kasa_Panel kasa = new Kasa_Panel();
            kasa.Show();
        }
    }
}