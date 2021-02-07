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
using DevExpress.XtraGrid.Views.Grid;

namespace TrinaAccess_1._2
{
    public partial class Cari_Kayit : DevExpress.XtraBars.TabForm
    {
        public Cari_Kayit()
        {
            InitializeComponent();
        }


        OleDbConnection con = new OleDbConnection(Giris.constr);
        public static bool kaydedilsin;



        public static void Kaydet()
        {
            kaydedilsin = true;
        }

        private void Temizle()
        {
            CariKodTB.Text = "";
            CariAdTB.Text = "";
            AdresTB.Text = "";
            IlceTB.Text = "";
            IlTB.Text = "";
            UlkeTB.Text = "";
            TelTB.Text = "";
            FaksTB.Text = "";
            VergiDaireTB.Text = "";
            VergiNoTB.Text = "";
            TCNoTB.Text = "";
            PostaTB.Text = "";
            MailTB.Text = "";
            WebTB.Text = "";
            AliciRB.Checked = true;
            GrupKod.Text = "";
            Kod1.Text = "";
            Kod2.Text = "";
            Kod3.Text = "";
            Kod4.Text = "";
            Kod5.Text = "";
            NotTB.Text = "";


            CariKodTB.Focus();


        }


        private void TabloYenile()
        {

            dataGridView1.Rows.Clear();

            con.Open();


            OleDbCommand datacek = new OleDbCommand("Select * From T_CARISBT", con);
            OleDbDataReader dr = datacek.ExecuteReader();



            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[1].ToString(), dr[2].ToString(), dr[5].ToString(), dr[4].ToString(), dr[3].ToString());
            }

            con.Close();
        }

        private void Cari_Kayit_Load(object sender, EventArgs e)
        {
            tabFormControl1.SelectedPage = tabFormControl1.Pages[0];
            CariKodTB.Focus();

            TabloYenile();
            

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            tabFormControl1.SelectedPage = tabFormControl1.Pages[0];
            Temizle();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void KayitTimer_Tick(object sender, EventArgs e)
        {
            if (kaydedilsin)
            {
                KaydetInternal();
                kaydedilsin = false;
            }
        }
        int sonsatir;
        public void KaydetInternal()
        {

            if (CariKodTB.Text == "") { return; }


            OleDbCommand kodvarmi = new OleDbCommand("Select Count(CariKod) from T_CARISBT Where CariKod = '" + CariKodTB.Text + "'",con);
            con.Open();
            if (kodvarmi.ExecuteScalar().ToString() != "0")
            {
                // Kod Mevcut
                string tip="";
                if (AliciRB.Checked) { tip = "Alıcı"; }
                if (SaticiRB.Checked) { tip = "Satıcı"; }
                if (ToptanciRB.Checked) { tip = "Toptancı"; }
                if (KefilRB.Checked) { tip = "Kefil"; }
                if (MustahsilRB.Checked) { tip = "Müstahsil"; }
                if (DigerRB.Checked) { tip = "Diğer"; }

                



                OleDbCommand update = new OleDbCommand("UPDATE T_CARISBT SET CariAd ='" + CariAdTB.Text.Replace(Environment.NewLine, "-|-|-") + "', Adres = '" + AdresTB.Text.Replace(Environment.NewLine, "-|-|-") + "', Ilce = '" + IlceTB.Text + "', Il = '" + IlTB.Text + "', UlkeKodu='" + UlkeTB.Text + "', Telefon='" + TelTB.Text + "', Faks='" + FaksTB.Text + "', VergiDairesi='" + VergiDaireTB.Text + "', VergiNo='" + VergiNoTB.Text + "', TCNo='" + TCNoTB.Text + "', PostaKodu='" + PostaTB.Text + "', Tip='" + tip + "', EMail='" + MailTB.Text + "', WebAdresi = '" + WebTB.Text + "', Grup_Kodu = '" + GrupKod.Text + "', Kod_1 = '" + Kod1.Text + "', Kod_2 = '" + Kod2.Text + "', Kod_3 = '" + Kod3.Text + "', Kod_4 = '" + Kod4.Text + "', Kod_5 = '" + Kod5.Text + "', Notlar = '" + NotTB.Text.Replace(Environment.NewLine, "-|-|-") + "' Where CariKod = '" + CariKodTB.Text+"'",con);


                DialogResult dr = MessageBox.Show(CariKodTB.Text + " Kodundaki cariyi değiştirmek istediğinize emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try { con.Open(); } catch { }
                    update.ExecuteNonQuery();
                }

                OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','CARI','" + CariKodTB.Text + " Kodunun Cari Kaydı güncellendi.','" + DateTime.Now + "')", con);
                gecmis.ExecuteNonQuery();

                con.Close();

                TabloYenile();



            }
            else
            {
                // Kod Yeni Acilacak
                string tip = "";
                if (AliciRB.Checked) { tip = "Alıcı"; }
                if (SaticiRB.Checked) { tip = "Satıcı"; }
                if (ToptanciRB.Checked) { tip = "Toptancı"; }
                if (KefilRB.Checked) { tip = "Kefil"; }
                if (MustahsilRB.Checked) { tip = "Müstahsil"; }
                if (DigerRB.Checked) { tip = "Diğer"; }

                OleDbCommand insert = new OleDbCommand("Insert Into T_CARISBT (CariKod,CariAd,Adres,Ilce,Il,UlkeKodu,Telefon,Faks,VergiDairesi,VergiNo,TCNo,PostaKodu,Tip,EMail,WebAdresi,Grup_Kodu,Kod_1,Kod_2,Kod_3,Kod_4,Kod_5,Notlar) " +
                    "Values ('" + CariKodTB.Text + "','" + CariAdTB.Text + "','" + AdresTB.Text + "','" + IlceTB.Text + "','" + IlTB.Text + "','" + UlkeTB.Text + "','" + TelTB.Text + "','" + FaksTB.Text + "','" + VergiDaireTB.Text + "','" + VergiNoTB.Text + "','" + TCNoTB.Text + "','" + PostaTB.Text + "','" + tip + "','" + MailTB.Text + "','" + WebTB.Text + "','" + GrupKod.Text + "','" + Kod1.Text + "','" + Kod2.Text + "','" + Kod3.Text + "','" + Kod4.Text + "','" + Kod5.Text + "','" + NotTB.Text.Replace(Environment.NewLine, "-|-|-") + "')", con);


                DialogResult dr = MessageBox.Show(CariKodTB.Text + " Kodundaki cariyi oluşturmak istediğinize emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try { con.Open(); } catch { }
                    insert.ExecuteNonQuery();
                }

                OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','CARI','" + CariKodTB.Text + " Kodunun Cari Kaydı oluşturuldu.','" + DateTime.Now + "')", con);
                gecmis.ExecuteNonQuery();

                con.Close();

                sonsatir = dataGridView1.CurrentRow.Index;

                TabloYenile();

                dataGridView1.Rows[0].Selected = false;
                dataGridView1.Rows[sonsatir + 1].Selected = true;


                CariKodTB.Text = dataGridView1.Rows[sonsatir + 1].Cells[0].Value.ToString();
                CariKodTB.Focus();
                CariAdTB.Focus();


            }


            try { con.Close(); } catch { }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            KaydetInternal();
        }

        private void CariKodTB_Leave(object sender, EventArgs e)
        {
            string cari = CariKodTB.Text;

            string tip = "";

            con.Open();

            OleDbCommand datacek = new OleDbCommand("Select * From T_CARISBT Where CariKod = '" + cari + "'", con);
            OleDbDataReader dr = datacek.ExecuteReader();

            while (dr.Read())
            {
                CariAdTB.Text = dr[2].ToString().Replace("-|-|-", Environment.NewLine);
                AdresTB.Text = dr[3].ToString().Replace("-|-|-", Environment.NewLine);
                IlceTB.Text = dr[4].ToString();
                IlTB.Text = dr[5].ToString();
                UlkeTB.Text = dr[6].ToString();
                TelTB.Text = dr[7].ToString();
                FaksTB.Text = dr[8].ToString();
                VergiDaireTB.Text = dr[9].ToString();
                VergiNoTB.Text = dr[10].ToString();
                TCNoTB.Text = dr[11].ToString();
                PostaTB.Text = dr[12].ToString();

                tip = dr[13].ToString();

                MailTB.Text = dr[14].ToString();
                WebTB.Text = dr[15].ToString();

                GrupKod.Text = dr[16].ToString();
                Kod1.Text = dr[17].ToString();
                Kod2.Text = dr[18].ToString();
                Kod3.Text = dr[19].ToString();
                Kod4.Text = dr[20].ToString();
                Kod5.Text = dr[21].ToString();

                NotTB.Text = dr[22].ToString().Replace("-|-|-",Environment.NewLine);

            }

            if (tip == "Alıcı") { AliciRB.Checked = true; }
            if (tip == "Satıcı") { SaticiRB.Checked = true; }
            if (tip == "Toptancı") { ToptanciRB.Checked = true; }
            if (tip == "Kefil") { KefilRB.Checked = true; }
            if (tip == "Müstahsil") { MustahsilRB.Checked = true; }
            if (tip == "Diğer") { DigerRB.Checked = true; }


            con.Close();
            
            
            
            
            
            
            
            
            
            
            
            

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            CariKodTB.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            CariKodTB_Leave(sender, e);
            CariAdTB.Focus();
        }
        

        private void tabFormControl1_SelectedPageChanged(object sender, TabFormSelectedPageChangedEventArgs e)
        {
            if (CariKodTB.Text == null)
            {
                tabFormControl1.SelectedPage = tabFormControl1.Pages[0];
            }
        }

        private void simpleButton19_Click(object sender, EventArgs e) // Önceki Data
        {
            OleDbCommand onceki = new OleDbCommand("select top 1 CariKod From T_CARISBT Where CariKod < '" + CariKodTB.Text + "' ORDER BY CariKod DESC", con);
            try
            {
                con.Open();
                CariKodTB.Text = onceki.ExecuteScalar().ToString();
                con.Close();
                CariKodTB_Leave(sender, e);
                CariAdTB.Focus();


            }
            catch { con.Close(); return; }
        }

        private void simpleButton18_Click(object sender, EventArgs e) // Sonraki Data
        {
            OleDbCommand sonraki = new OleDbCommand("select top 1 CariKod From T_CARISBT Where CariKod > '" + CariKodTB.Text + "' ORDER BY CariKod", con);
            try
            {
                con.Open();
                CariKodTB.Text = sonraki.ExecuteScalar().ToString();
                con.Close();
                CariKodTB_Leave(sender, e);
                CariAdTB.Focus();


            }
            catch { con.Close(); return; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Rehber_Il ilreh = new Rehber_Il();
            ilreh.ShowDialog();
        }
    }
}