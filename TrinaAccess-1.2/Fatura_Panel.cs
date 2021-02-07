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
    public partial class Fatura_Panel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Fatura_Panel()
        {
            InitializeComponent();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Fatura_Satis satfat = new Fatura_Satis();
            satfat.MdiParent = this;
            satfat.Show();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Fatura_Alis alfat = new Fatura_Alis();
            alfat.MdiParent = this;
            alfat.Show();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}