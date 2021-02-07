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
using TrinaAccess_1._2;

namespace TrinaAccess_1._2
{
    public partial class Stok_Hareket : DevExpress.XtraBars.TabForm
    {
        public Stok_Hareket()
        {
            InitializeComponent();
        }



        OleDbConnection con = new OleDbConnection(Giris.constr);


        private void Stok_Hareket_Load(object sender, EventArgs e)
        {
            KullaniciTB.Text = Giris.userid;
            TarihDT.DateTime = DateTime.Now;

            backgroundWorker1.WorkerReportsProgress = true;
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void Kaydet()
        {
            if (StokKoduTB.Text == "") { return; }

            OleDbCommand stokkontrol = new OleDbCommand("SELECT StokAd FROM T_STOKSBT WHERE StokKodu='" + StokKoduTB.Text + "'", con);

            con.Open();
            if (stokkontrol.ExecuteScalar().ToString() == "") { MessageBox.Show("Stok Kodu Bulunamadı!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Error); return; }
            con.Close();

            if (DBKimlikTB.Text == "")
            {
                if (GirisRB.Checked)
                {
                    OleDbCommand insert = new OleDbCommand("Insert Into T_STOKHAR (StokKodu,Tarih,FisNo,Tip,Fiyat,GirisMiktar,Depo,Aciklama,KUL_ID) Values "+
                        "('"+StokKoduTB.Text+"','"+TarihDT.Text+"','"+FisNoTB.Text+"','"+TipTB.Text+"','"+FiyatTB.Text+"','"+MiktarTB.Text+"','"+DepoTB.Text+"','"+AciklamaTB.Text+"','"+KullaniciTB.Text+"')", con);
                    con.Open();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Koduna giriş hareketi eklendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    insert.ExecuteNonQuery();
                    con.Close();

                }
                else if (CikisRB.Checked)
                {
                    OleDbCommand insert = new OleDbCommand("Insert Into T_STOKHAR (StokKodu,Tarih,FisNo,Tip,Fiyat,CikisMiktar,Depo,Aciklama,KUL_ID) Values " +
                        "('" + StokKoduTB.Text + "','" + TarihDT.Text + "','" + FisNoTB.Text + "','" + TipTB.Text + "','" + FiyatTB.Text + "','" + MiktarTB.Text + "','" + DepoTB.Text + "','" + AciklamaTB.Text + "','" + KullaniciTB.Text + "')", con);
                    con.Open();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Koduna çıkış hareketi eklendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    insert.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if (DBKimlikTB.Text != "")
            {
                if (GirisRB.Checked)
                {
                    OleDbCommand update = new OleDbCommand("UPDATE T_STOKHAR SET Tarih='" + TarihDT.Text + "', FisNo='" + FisNoTB.Text + "', Tip='" + TipTB.Text + "', Fiyat='" + FiyatTB.Text + "', GirisMiktar='" + MiktarTB.Text + "', CikisMiktar='', Depo='" + DepoTB.Text + "', Aciklama='" + AciklamaTB.Text + "', KUL_ID='" + KullaniciTB.Text + "'", con);
                    con.Open();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Kodunun giriş hareketi düzenlendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    update.ExecuteNonQuery();
                    con.Close();
                }
                if (CikisRB.Checked)
                {
                    OleDbCommand update = new OleDbCommand("UPDATE T_STOKHAR SET Tarih='" + TarihDT.Text + "', FisNo='" + FisNoTB.Text + "', Tip='" + TipTB.Text + "', Fiyat='" + FiyatTB.Text + "', CikisMiktar='" + MiktarTB.Text + "', GirisMiktar='', Depo='" + DepoTB.Text + "', Aciklama='" + AciklamaTB.Text + "', KUL_ID='" + KullaniciTB.Text + "'", con);
                    con.Open();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Kodunun çıkış hareketi düzenlendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    update.ExecuteNonQuery();
                    con.Close();
                }
            }

            TabloYenile();

            FisNoTB.Text = "";
            TipTB.SelectedIndex = 0;
            FiyatTB.Text = "0,00";
            TarihDT.DateTime = DateTime.Now;
            GirisRB.Checked = true;
            DepoTB.Text = "";
            MiktarTB.Text = "0,00";
            AciklamaTB.Text = "";



        }

        private void StokKoduTB_Leave(object sender, EventArgs e)
        {
            TabloYenile();
        }

        private void TabloYenile()
        {

            dataGridView1.Rows.Clear();

            OleDbCommand stokadgetir = new OleDbCommand("Select StokAd From T_STOKSBT Where StokKodu='" + StokKoduTB.Text + "'", con);

            try
            {
                con.Open();
                StokAdTB.Text = stokadgetir.ExecuteScalar().ToString();
                con.Close();
            }
            catch
            {
                con.Close();
                StokAdTB.Text = "";
            }

            if (StokAdTB.Text == "")
            {
                StokKoduTB.Focus();
                return;
            }

            OleDbCommand tablogetir = new OleDbCommand("Select * From T_STOKHAR Where StokKodu = '" + StokKoduTB.Text + "'", con);
            con.Open();
            OleDbDataReader dr = tablogetir.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[9].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), "", dr[8].ToString());
            }
            con.Close();


            int satirsayisi = dataGridView1.Rows.Count;

            if (dataGridView1.Rows.Count < 1) { return; }

            float toplamgiris = 0, toplamcikis = 0;

            if (dataGridView1.Rows[0].Cells[6].Value.ToString() != "")
            {
                dataGridView1.Rows[0].Cells[8].Value = dataGridView1.Rows[0].Cells[6].Value.ToString();
                toplamgiris = toplamgiris + Convert.ToSingle(dataGridView1.Rows[0].Cells[6].Value);
            }
            else if (dataGridView1.Rows[0].Cells[7].Value.ToString() != "")
            {
                dataGridView1.Rows[0].Cells[8].Value = "-" + dataGridView1.Rows[0].Cells[7].Value.ToString();
                toplamcikis = toplamcikis + Convert.ToSingle(dataGridView1.Rows[0].Cells[7].Value);
            }

            progressBarControl1.Properties.Maximum = satirsayisi - 1;

            for (int i = 1; i <= satirsayisi - 1; i++)
            {
                DataGridViewRow dgr = dataGridView1.Rows[i];
                DataGridViewRow dgronce = dataGridView1.Rows[i - 1];

                float oncekibakiye = Convert.ToSingle(dgronce.Cells[8].Value);
                float simdikibakiye;

                if (dgr.Cells[6].Value != "")
                {
                    simdikibakiye = Convert.ToSingle(dgr.Cells[6].Value);
                    toplamgiris = toplamgiris + Convert.ToSingle(dgr.Cells[6].Value);
                }
                else
                {
                    simdikibakiye = Convert.ToSingle(dgr.Cells[7].Value) * -1;
                    toplamcikis = toplamcikis + Convert.ToSingle(dgr.Cells[7].Value);
                }
                dgr.Cells[8].Value = Convert.ToString(oncekibakiye + simdikibakiye);
                

            }

            ToplamCikisTB.Text = toplamcikis.ToString();
            ToplamGirisTB.Text = toplamgiris.ToString();

            if (toplamgiris > toplamcikis)
            {
                BakiyeTB.Text = (toplamgiris - toplamcikis).ToString();
                BakiyeTB.ForeColor = Color.Green;
                separatorControl3.LineColor = Color.Green;
            }
            else
            {
                BakiyeTB.Text = (toplamgiris - toplamcikis).ToString();
                BakiyeTB.ForeColor = Color.Red;
                separatorControl3.LineColor = Color.Red;
            }

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Kaydet();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void AciklamaTB_Leave(object sender, EventArgs e)
        {
            if (StokAdTB.Text != "" && MiktarTB.Text != "") { Kaydet(); }
        }

        private void simpleButton2_Click(object sender, EventArgs e) // Sonraki Data
        {
            OleDbCommand sonraki = new OleDbCommand("select top 1 StokKodu From T_STOKSBT Where StokKodu > '" + StokKoduTB.Text + "' ORDER BY StokKodu", con);
            try
            {
                con.Open();
                StokKoduTB.Text = sonraki.ExecuteScalar().ToString();
                con.Close();
                StokKoduTB_Leave(sender, e);
                TarihDT.Focus();


            }
            catch { con.Close(); return; }
        }

        private void simpleButton1_Click(object sender, EventArgs e) // önceki data
        {
            OleDbCommand onceki = new OleDbCommand("select top 1 StokKodu From T_STOKSBT Where StokKodu < '" + StokKoduTB.Text + "' ORDER BY StokKodu DESC", con);
            try
            {
                con.Open();
                StokKoduTB.Text = onceki.ExecuteScalar().ToString();
                con.Close();
                StokKoduTB_Leave(sender, e);
                TarihDT.Focus();


            }
            catch { con.Close(); return; }
        }
    }
}