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
    public partial class Stok_Panel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Stok_Panel()
        {
            InitializeComponent();
        }

        private void Stok_Panel_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Stok_Kayit stkkyt = new Stok_Kayit();
            stkkyt.MdiParent = this;
            stkkyt.Show();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Stok_Hareket stkhrkt = new Stok_Hareket();
            stkhrkt.MdiParent = this;
            stkhrkt.Show();
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            int a = this.MdiChildren.Count();

            if (a < 1)
            {
                return;
            }

            DialogResult dr = MessageBox.Show("Tüm sekmeleri kapatmak istediğinize emin misiniz ?", "Dikkat!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                foreach (Form frm in this.MdiChildren)
                {

                    frm.Close();
                }
            }
        }

        private void Stok_Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            int a = this.MdiChildren.Count();

            if (a > 0)
            {
                DialogResult dr = MessageBox.Show("Açık pencereler var çıkmak istediğinize emin misiniz ?", "Dikkat!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            Stok_DepoTanim depo = new Stok_DepoTanim();
            depo.ShowDialog();
        }
    }
}