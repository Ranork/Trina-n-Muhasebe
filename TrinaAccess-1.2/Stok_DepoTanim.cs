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
using System.Threading;

namespace TrinaAccess_1._2
{
    public partial class Stok_DepoTanim : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Stok_DepoTanim()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(Giris.constr);
        private void Stok_DepoTanim_Load(object sender, EventArgs e)
        {
            ListeyiYenile();
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            if (DepoKoduTB.Text != "")
            {
                OleDbCommand bilgigetir = new OleDbCommand("Select * From T_DEPOSBT WHERE DepoKodu='" + DepoKoduTB.Text + "'", con);
                con.Open();
                try
                {
                    OleDbDataReader dr = bilgigetir.ExecuteReader();
                    while(dr.Read())
                    {
                        DepoAdiTB.Text = dr[2].ToString();
                        KilitCB.Checked = Convert.ToBoolean(dr[3]);
                        CariKodTB.Text = dr[4].ToString();
                        EksiBakiyeCB.Checked = Convert.ToBoolean(dr[5]);
                    }
                }
                catch
                {
                    DepoKoduTB.Focus();
                }
                con.Close();

                con.Open();
                OleDbCommand carigetir = new OleDbCommand("Select CariAd From T_CARISBT Where CariKod='" + CariKodTB.Text + "'", con);
                try
                {
                    CariAdTB.Text = carigetir.ExecuteScalar().ToString();
                }
                catch { }
                con.Close();

            }
        }

        private void CariKodTB_Leave(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand carigetir = new OleDbCommand("Select CariAd From T_CARISBT Where CariKod='" + CariKodTB.Text + "'", con);
            try
            {
                CariAdTB.Text = carigetir.ExecuteScalar().ToString();
            }
            catch
            {
                CariAdTB.Focus();
            }
            con.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            DepoKoduTB.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            textEdit1_Leave(sender,e);
            DepoAdiTB.Focus();
        }

        private void EksiBakiyeCB_Leave(object sender, EventArgs e)
        {

            Kaydet();

        }


        private void Kaydet()
        {
            if (DepoKoduTB.Text == "") { return; }

            OleDbCommand kodctrl = new OleDbCommand("SELECT COUNT(DepoKodu) From T_DEPOSBT Where DepoKodu='" + DepoKoduTB.Text + "'", con);

            con.Open();
            if (Convert.ToInt32(kodctrl.ExecuteScalar()) == 0)
            {
                con.Close();
                //yeni kod
                OleDbCommand insert = new OleDbCommand("INSERT INTO T_DEPOSBT (DepoKodu,DepoAdi,Kilit,CariKodu,EksiBakiyeKontrol) VALUES ('" + DepoKoduTB.Text + "','" + DepoAdiTB.Text + "'," + KilitCB.Checked + ",'" + CariKodTB.Text + "'," + EksiBakiyeCB.Checked+")", con);
                con.Open();
                insert.ExecuteNonQuery();
                con.Close();
                Temizle();
                ListeyiYenile();
            }
            else
            {
                con.Close();
                //kod güncelle
                OleDbCommand update = new OleDbCommand("UPDATE T_DEPOSBT SET DepoAdi='" + DepoAdiTB.Text + "', Kilit=" + KilitCB.Checked + ", CariKodu='" + CariKodTB.Text + "', EksiBakiyeKontrol=" + EksiBakiyeCB.Checked + " Where DepoKodu='" + DepoKoduTB.Text + "'", con);
                con.Open();
                update.ExecuteNonQuery();
                con.Close();
                Temizle();
                ListeyiYenile();
            }

        }


        private void Temizle()
        {
            DepoKoduTB.Text = "";
            DepoAdiTB.Text = "";
            CariKodTB.Text = "";
            CariAdTB.Text = "";
            EksiBakiyeCB.Checked = false;
            KilitCB.Checked = false;
        }

        private void ListeyiYenile()
        {
            dataGridView1.Rows.Clear();

            OleDbCommand tablocek = new OleDbCommand("Select * From T_DEPOSBT", con);
            con.Open();
            OleDbDataReader dr = tablocek.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[1].ToString(), dr[2].ToString(), Convert.ToBoolean(dr[3]), dr[4].ToString());
            }
            con.Close();
        }


    }
}