using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TrinaAccess_1._2
{
    public partial class Hakkinda : DevExpress.XtraEditors.XtraForm
    {
        public Hakkinda()
        {
            InitializeComponent();
        }

        private void Hakkinda_LocationChanged(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}