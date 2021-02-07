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
    public partial class KullaniciGecmis : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public KullaniciGecmis()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(Giris.constr);

        private void KullaniciGecmis_Load(object sender, EventArgs e)
        {
            textEdit1.Text = Giris.userid;

            OleDbCommand bilgigetir = new OleDbCommand("Select * From T_KULGECMIS Where KUL_ID='" + Giris.userid + "' ORDER BY Kimlik DESC", con);

            con.Open();
            OleDbDataReader dr = bilgigetir.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[4].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            con.Close();

        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string silkimlik = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            OleDbCommand sil = new OleDbCommand("DELETE FROM T_KULGECMIS WHERE Kimlik=" + silkimlik + "", con);

            con.Open();
            sil.ExecuteNonQuery();
            con.Close();

            KullaniciGecmis_Load(sender, e);

        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciGecmis_Load(sender, e);
        }
    }
}