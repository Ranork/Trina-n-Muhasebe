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
    public partial class Cari_Panel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Cari_Panel()
        {
            InitializeComponent();
        }
        

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cari_Kayit cri = new Cari_Kayit();
            cri.MdiParent = this;
            cri.Show();
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {

            int a = this.MdiChildren.Count();

            if (a<1)
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

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Cari_Panel_FormClosing(object sender, FormClosingEventArgs e)
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

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cari_Hareket crihar = new Cari_Hareket();
            crihar.MdiParent = this;
            crihar.Show();
        }
    }
}