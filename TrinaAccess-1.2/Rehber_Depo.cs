using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using DevExpress.XtraBars;

namespace TrinaAccess_1._2
{
    public partial class Rehber_Depo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Rehber_Depo()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(Giris.constr);

        private void Rehber_Depo_Load(object sender, EventArgs e)
        {
            OleDbCommand listecek = new OleDbCommand("Select DepoKodu,DepoAdi From T_DEPOSBT", con);
            con.Open();
            try
            {
                OleDbDataReader dr = listecek.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());
                }
            }
            catch { }
            con.Close();
        }

        public static string secilidepokodu;

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            secilidepokodu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }
    }
}