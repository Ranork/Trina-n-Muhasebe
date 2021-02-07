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
    public partial class Rehber_Il : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Rehber_Il()
        {
            InitializeComponent();
        }

        private void Rehber_Il_Load(object sender, EventArgs e)
        {
            listeyenile();
        }
        OleDbConnection con = new OleDbConnection(Giris.constr);
        private void listeyenile()
        {
            OleDbCommand verigetir = new OleDbCommand("SELECT distinct Il_Kodu,Il_Adi FROM T_SEHIRSBT Order By Il_Kodu", con);

            con.Open();

            OleDbDataReader dr = verigetir.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());
            }

            con.Close();

        }

    }
}