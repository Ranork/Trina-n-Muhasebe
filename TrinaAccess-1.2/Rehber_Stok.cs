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
using System.Data.OleDb;

namespace TrinaAccess_1._2
{
    public partial class Rehber_Stok : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Rehber_Stok()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(Giris.constr);

        private void Rehber_Stok_Load(object sender, EventArgs e)
        {
            OleDbCommand listecek = new OleDbCommand("Select StokKodu,StokAd,Grup_Kodu,Br1 From T_STOKSBT", con);
            con.Open();
            try
            {
                OleDbDataReader dr = listecek.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                }
            }
            catch { }
            con.Close();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {

        }

        public static string secilistokkodu;

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            secilistokkodu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }
    }
}