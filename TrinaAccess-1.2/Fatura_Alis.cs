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
using System.Globalization;

namespace TrinaAccess_1._2
{
    public partial class Fatura_Alis : DevExpress.XtraBars.TabForm
    {
        public Fatura_Alis()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(Giris.constr);
        private void Fatura_Alis_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            //barButtonItem2.Links[0].Visible = false;

            KullaniciTB.Text = Giris.userid;
            tabFormControl1.SelectedPage = tabFormControl1.Pages[0];

            TarihDT.DateTime = DateTime.Today;
            EntTarihDT.DateTime = DateTime.Today;
            FiiliTarihDT.DateTime = DateTime.Today;
            DovizTarihDT.DateTime = DateTime.Today;
            FiyatTarihDT.DateTime = DateTime.Today;

            OleDbCommand sonfatno = new OleDbCommand("Select Veri From T_TRINASBT Where Veri_Ad = 'SonAlisFaturaNo'", con);
            con.Open();
            FaturaNoTB.Text = "AF" + Convert.ToString(Convert.ToInt32(sonfatno.ExecuteScalar()) + 1);
        }

        void Connect()
        {
            try { con.Open(); }
            catch { con.Close(); con.Open(); }
        }

        void Disconnect()
        {
            try { con.Close(); }
            catch { }
        }

        private void tabFormControl1_SelectedPageChanging(object sender, TabFormSelectedPageChangingEventArgs e)
        {
            if (e.Page == tabFormControl1.Pages[0]) { return; }
            if (FaturaNoTB.Text == "")
            {
                e.Cancel = true;
                MessageBox.Show("Önce fatura numarası girmelisiniz!", "Dikkat!");
                return;
            }
            if (CariKodTB.Text == "")
            {
                e.Cancel = true;
                MessageBox.Show("Önce Cari Kod girmelisiniz!", "Dikkat!");
                return;

            }

            textEdit25.Text = FaturaNoTB.Text;
            textEdit3.Text = FaturaNoTB.Text;

            textEdit27.Text = TarihDT.Text;
            textEdit2.Text = TarihDT.Text;

            textEdit29.Text = Cariisim.Text;
            textEdit1.Text = Cariisim.Text;

            ListeYenile();

        }


        private void ListeYenile()
        {
            OleDbCommand listecek = new OleDbCommand("Select * From T_ALFATLIST Where Fatura_No='" + FaturaNoTB.Text + "'", con);
            Connect();
            OleDbDataReader dr = listecek.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[2].ToString(), "", dr[4].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), "", dr[12].ToString());
            }
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                OleDbCommand sel = new OleDbCommand("Select StokAd From T_STOKSBT WHERE StokKodu='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", con);
                dataGridView1.Rows[i].Cells[2].Value = sel.ExecuteScalar().ToString();
            }
            con.Close();
        }

        private void ListeyeEkle()
        {
            OleDbCommand stokkodctrl = new OleDbCommand("SELECT COUNT(StokKodu) From T_STOKSBT WHERE StokKodu='" + StokKoduTB.Text + "'", con);
            Connect();
            if (Convert.ToInt32(stokkodctrl.ExecuteScalar()) < 1)
            {
                con.Close();
                return;
            }
            Disconnect();
            if (DataNoTB.Text == "0")
            {
                //insert

                OleDbCommand insert = new OleDbCommand("INSERT INTO T_ALFATLIST (Fatura_No,StokKodu,DepoKodu,Miktar,Fiyat_Tipi,DovizTip,Fiyat,KDV,Islem_Tarihi,Tutar) Values ('" + FaturaNoTB.Text + "','" + StokKoduTB.Text + "','" + DepoKoduTB.Text + "','" + MiktarTB.Text + "','" + FiyatTipiTB.Text + "','" + DovizTipTB.Text + "','" + FiyatTB.Text + "','" + KDVTB.Text + "','" + IslemTarihDT.Text + "','" + TutarTB.Text + "')", con);
                Connect();

                insert.ExecuteNonQuery();

                Disconnect();


                ListeYenile();
                DataNoTB.Text = "0";
            }
            else
            {
                //update
                OleDbCommand update = new OleDbCommand("UPDATE T_ALFATLIST SET StokKodu = '" + StokKoduTB.Text + "', DepoKodu = '" + DepoKoduTB.Text + "', Miktar = '" + MiktarTB.Text + "', Fiyat_Tipi = '" + FiyatTipiTB.Text + "', DovizTip = '" + DovizTipTB.Text + "', Fiyat = '" + FiyatTB.Text + "', KDV = '" + KDVTB.Text + "', Islem_Tarihi = '" + IslemTarihDT.Text + "', Tutar = '" + TutarTB.Text + "' Where Kimlik = " + Convert.ToInt32(DataNoTB.Text) + "", con);
                Connect();

                update.ExecuteNonQuery();

                Disconnect();

                ListeYenile();
                DataNoTB.Text = "0";
            }

        }



        private void Kaydet()
        {
            OleDbCommand kodvarmi = new OleDbCommand("Select Count(Fatura_No) From T_SATFATSBT Where Fatura_No = '" + FaturaNoTB.Text + "'", con);

            bool kdv = false;
            if (KDVCB.Checked) { kdv = true; }

            Connect();
            if (Convert.ToInt32(kodvarmi.ExecuteScalar()) <= 0)
            {
                // kod aç

                DialogResult dr = MessageBox.Show(FaturaNoTB.Text + " Numaralı fatura oluşturulacak. Emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) { return; }

                OleDbCommand insert = new OleDbCommand("Insert Into T_SATFATSBT (Fatura_No,Cari_Kod,Tarih,Ent_Tarih,Fiili_Tarih,Doviz_Tarih,Tip,Aciklama,Kullanici,Ref_Fatura,KDV_Dahil,Fiyat_Tarihi,Sevkiyat_Sekli,Sevkiyat_Adresi,Arac_Plakasi,Teslim_Alan,Teslim_Eden,Ek_Aciklama,Sip_No,Musteri_Kodu,Musteri_Adi,Sip_Aciklama) " +
                    "Values ('" + FaturaNoTB.Text + "','" + CariKodTB.Text + "','" + TarihDT.Text + "','" + EntTarihDT.Text + "','" + FiiliTarihDT.Text + "','" + DovizTarihDT.Text + "','" + TipTB.Text + "','" + AciklamaTB.Text + "','" + KullaniciTB.Text + "','" + RefFatTB.Text + "','" + kdv.ToString() + "','" + FiyatTarihDT.Text + "','" + SevkiyatSekliTB.Text + "','" + SevkiyatAdresTB.Text + "','" + AracPlakaTB.Text + "','" + TeslimAlanTB.Text + "','" + TeslimEdenTB.Text + "','" + EkAciklamaTB.Text + "','" + SiparişNoTB.Text + "','" + MusteriKodTB.Text + "','" + MusteriAdTB.Text + "','" + SiparisAciklamaTB.Text + "')", con);

                insert.ExecuteNonQuery();

                OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','FATURA','" + FaturaNoTB.Text + " Numaralı satış faturası oluşturuldu.','" + DateTime.Now + "')", con);
                gecmis.ExecuteNonQuery();

                OleDbCommand sonfatno = new OleDbCommand("Select Veri From T_TRINASBT Where Veri_Ad = 'SonSatisFaturaNo'", con);
                if (Convert.ToInt32(FaturaNoTB.Text.Substring(2)) == Convert.ToInt32(sonfatno.ExecuteScalar()) + 1)
                {
                    OleDbCommand faturanoart = new OleDbCommand("Update T_TRINASBT Set Veri='" + Convert.ToInt32(FaturaNoTB.Text.Substring(2)) + "' Where Veri_Ad='SonAlisFaturaNo'", con);
                    faturanoart.ExecuteNonQuery();
                }
                Disconnect();
            }


            else if (Convert.ToInt32(kodvarmi.ExecuteScalar()) == 1)
            {
                // kod güncelle

                DialogResult dr = MessageBox.Show(FaturaNoTB.Text + " Numaralı fatura güncellenecek. Emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) { return; }

                OleDbCommand update = new OleDbCommand("Update T_SATFATSBT Set Cari_Kod='" + CariKodTB.Text + "', Tarih='" + TarihDT.Text + "', Ent_Tarih='" + EntTarihDT.Text + "', Fiili_Tarih='" + FiiliTarihDT.Text + "', Doviz_Tarih='" + DovizTarihDT.Text + "', Tip='" + TipTB.Text + "', Aciklama='" + AciklamaTB.Text + "', Kullanici='" + Giris.userid + "', Ref_Fatura='" + RefFatTB.Text + "', KDV_Dahil='" + kdv + "', Fiyat_Tarihi='" + FiyatTarihDT.Text + "'," +
                    "Sevkiyat_Sekli='" + SevkiyatSekliTB.Text + "', Sevkiyat_Adresi='" + SevkiyatAdresTB.Text + "', Arac_Plakasi='" + AracPlakaTB.Text + "', Teslim_Alan='" + TeslimAlanTB.Text + "', Teslim_Eden='" + TeslimEdenTB.Text + "', Ek_Aciklama='" + EkAciklamaTB.Text + "', Sip_No='" + SiparişNoTB.Text + "', Musteri_Kodu='" + MusteriKodTB.Text + "', Musteri_Adi='" + MusteriAdTB.Text + "', Sip_Aciklama='" + SiparisAciklamaTB.Text + "' Where Fatura_No='" + FaturaNoTB.Text + "'", con);

                update.ExecuteNonQuery();

                OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','FATURA','" + FaturaNoTB.Text + " Numaralı satış faturası güncellendi.','" + DateTime.Now + "')", con);
                gecmis.ExecuteNonQuery();

                Disconnect();

            }


        }






        private void Temizle()
        {
            CariKodTB.Text = "";
            TarihDT.DateTime = DateTime.Now;
            EntTarihDT.DateTime = DateTime.Now;
            FiiliTarihDT.DateTime = DateTime.Now;
            DovizTarihDT.DateTime = DateTime.Now;
            TipTB.Text = "";
            AciklamaTB.Text = "";
            KullaniciTB.Text = Giris.userid;
            RefFatTB.Text = "";
            KDVCB.Checked = false;
            FiyatTarihDT.DateTime = DateTime.Now;

            Cariisim.Text = "";
            CariAciklama1.Text = "";
            CariAciklama2.Text = "";
            CariAciklama3.Text = "";

            SevkiyatSekliTB.Text = "";
            SevkiyatAdresTB.Text = "";
            AracPlakaTB.Text = "";
            TeslimAlanTB.Text = "";
            TeslimEdenTB.Text = "";
            EkAciklamaTB.Text = "";
            SiparişNoTB.Text = "";
            MusteriKodTB.Text = "";
            MusteriAdTB.Text = "";
            SiparisAciklamaTB.Text = "";

            dataGridView1.Rows.Clear();

            StokKoduTB.Text = "";
            StokAdiTB.Text = "";
            DepoKoduTB.Text = "";
            MiktarTB.Text = "";
            FiyatTipiTB.Text = "";

            DovizTipTB.Text = "0";
            DovizFiyatTB.Text = "0,00";
            DovizKuruTB.Text = "0";
            FiyatTB.Text = "0,0000";
            

            KDVTB.Text = "0,00";
            IslemTarihDT.DateTime = DateTime.Now;
            TutarTB.Text = "0,0000";
        }

        private void Fatura_Alis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                Kaydet();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }

        private void FaturaNoTB_Leave(object sender, EventArgs e)
        {
            OleDbCommand kontrol = new OleDbCommand("Select Count(Fatura_No) From T_SATFATSBT Where Fatura_No='" + FaturaNoTB.Text + "'", con);

            Connect();

            if (Convert.ToInt32(kontrol.ExecuteScalar()) < 1) { con.Close(); Temizle(); return; }


            OleDbCommand bilgigetir = new OleDbCommand("Select * From T_ALFATSBT Where Fatura_No = '" + FaturaNoTB.Text + "'", con);
            OleDbDataReader dr = bilgigetir.ExecuteReader();

            while (dr.Read())
            {
                CariKodTB.Text = dr[2].ToString();
                TarihDT.DateTime = Convert.ToDateTime(dr[3]);
                EntTarihDT.DateTime = Convert.ToDateTime(dr[4]);
                FiiliTarihDT.DateTime = Convert.ToDateTime(dr[5]);
                DovizTarihDT.DateTime = Convert.ToDateTime(dr[6]);
                TipTB.Text = dr[7].ToString();
                AciklamaTB.Text = dr[8].ToString();
                KullaniciTB.Text = dr[9].ToString();
                RefFatTB.Text = dr[10].ToString();
                KDVCB.Checked = Convert.ToBoolean(dr[11]);
                FiyatTarihDT.DateTime = Convert.ToDateTime(dr[12]);
                SevkiyatSekliTB.Text = dr[13].ToString();
                SevkiyatAdresTB.Text = dr[14].ToString();
                AracPlakaTB.Text = dr[15].ToString();
                TeslimAlanTB.Text = dr[16].ToString();
                TeslimEdenTB.Text = dr[17].ToString();
                EkAciklamaTB.Text = dr[18].ToString();
                SiparişNoTB.Text = dr[19].ToString();
                MusteriKodTB.Text = dr[20].ToString();
                MusteriAdTB.Text = dr[21].ToString();
                SiparisAciklamaTB.Text = dr[22].ToString();


            }

            OleDbCommand carigetir = new OleDbCommand("Select CariAd From T_CARISBT Where CariKod='" + CariKodTB.Text + "'", con);
            try { Cariisim.Text = carigetir.ExecuteScalar().ToString(); } catch { }

            Disconnect();

            OleDbCommand kapalimi = new OleDbCommand("Select Kapali From T_ALFATSBT Where Fatura_No = '" + FaturaNoTB.Text + "'", con);
            Connect();
            bool kapali = Convert.ToBoolean(kapalimi.ExecuteScalar());
            Disconnect();

            if (kapali)
            {
                barButtonItem2.Links[0].Visible = false;
                groupBox4.Enabled = false;
                groupControl1.Enabled = false;
                groupControl2.Enabled = false;
            }
            else
            {
                barButtonItem2.Links[0].Visible = true;
                groupBox4.Enabled = true;
                groupControl1.Enabled = true;
                groupControl2.Enabled = true;
            }
        }

        private void CariKodTB_Leave(object sender, EventArgs e)
        {
            Connect();
            OleDbCommand carigetir = new OleDbCommand("Select CariAd From T_CARISBT Where CariKod='" + CariKodTB.Text + "'", con);
            try { Cariisim.Text = carigetir.ExecuteScalar().ToString(); } catch { }
            Disconnect();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Kaydet();
        }

        private void IslemTarihDT_Leave(object sender, EventArgs e)
        {
            ListeyeEkle();
        }

        private void Duzenle()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            DataNoTB.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            StokAdiTB.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            OleDbCommand bilgigetir = new OleDbCommand("SELECT * FROM T_SATFATLIST WHERE Kimlik=" + DataNoTB.Text, con);



            Connect();
            MessageBox.Show(bilgigetir.ExecuteScalar().ToString());
            OleDbDataReader dr = bilgigetir.ExecuteReader();
            while (dr.Read())
            {
                StokKoduTB.Text = dr[2].ToString();
                DepoKoduTB.Text = dr[3].ToString();
                MiktarTB.Text = dr[4].ToString();
                FiyatTipiTB.Text = dr[5].ToString();
                try { DovizTipTB.Text = dr[6].ToString(); } catch { }
                FiyatTB.Text = dr[7].ToString();
                KDVTB.Text = dr[10].ToString();
                IslemTarihDT.Text = dr[11].ToString();
                TutarTB.Text = dr[12].ToString();
            }
            Disconnect();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Duzenle();
        }

        private void StokKoduTB_Leave(object sender, EventArgs e)
        {
            if (StokKoduTB.Text != "")
            {
                OleDbCommand stokadgetir = new OleDbCommand("SELECT StokAd From T_STOKSBT WHERE StokKodu='" + StokKoduTB.Text + "'", con);
                Connect();
                try { StokAdiTB.Text = stokadgetir.ExecuteScalar().ToString(); }
                catch { StokKoduTB.Focus(); }
                Disconnect();
            }
            else
            {
                StokAdiTB.Text = "";
            }
        }

        private void FiyatTipiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbCommand fiyatgetir = new OleDbCommand("Select AlisF" + FiyatTipiTB.SelectedIndex + " From T_STOKSBT2 Where StokKodu='" + StokKoduTB.Text + "'", con);
            try
            {
                Connect();
                //MessageBox.Show(fiyatgetir.CommandText+Environment.NewLine+ fiyatgetir.ExecuteScalar().ToString());
                FiyatTB.Text = fiyatgetir.ExecuteScalar().ToString();
            }
            catch { }
            Disconnect();
        }

        private void tabFormControl1_SelectedPageChanged(object sender, TabFormSelectedPageChangedEventArgs e)
        {
            float bruttoplam = 0;
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                bruttoplam = bruttoplam + float.Parse(Row.Cells[11].Value.ToString());
            }
            BrutToplamTB.Text = bruttoplam.ToString("0.00");
            GenelToplamTB.Text = bruttoplam.ToString("0.00");
        }

        private void KDVTB_Leave(object sender, EventArgs e)
        {

            if (float.Parse(KDVTB.Text) <= 0) { return; }

            float tutar = float.Parse(FiyatTB.Text)*float.Parse(MiktarTB.Text);
            tutar = tutar + (tutar * (float.Parse(KDVTB.Text)));
            TutarTB.Text = tutar.ToString("0.00");
        }

        private void fatAltHesap()
        {
            float tutar = 0;
            float brut = float.Parse(BrutToplamTB.Text);


            if (float.Parse(textEdit12.Text) >= 0)
            {
                tutar = brut - ((brut * float.Parse(textEdit12.Text) / 100));
            }
            if (float.Parse(textEdit15.Text) >= 0)
            {
                if (tutar == 0)
                {
                    tutar = brut - ((brut * float.Parse(textEdit15.Text) / 100));
                }
                else
                {
                    tutar = tutar - ((tutar * float.Parse(textEdit15.Text) / 100));
                }
            }
            if (float.Parse(textEdit18.Text) >= 0)
            {
                if (tutar == 0)
                {
                    tutar = brut - ((brut * float.Parse(textEdit18.Text) / 100));
                }
                else
                {
                    tutar = tutar - ((tutar * float.Parse(textEdit18.Text) / 100));
                }
            }

            if (float.Parse(textEdit41.Text) >= 0)
            {
                tutar = tutar + (tutar * (float.Parse(textEdit41.Text)));
            }

            progressBarControl1.Text = (float.Parse(textEdit41.Text) * 100).ToString();

            GenelToplamTB.Text = tutar.ToString("0.00");

        }

        private void textEdit12_Leave(object sender, EventArgs e)
        {
            textEdit12.Text = float.Parse(textEdit12.Text).ToString("0.00");
            fatAltHesap();
        }

        private void textEdit15_Leave(object sender, EventArgs e)
        {
            textEdit15.Text = float.Parse(textEdit15.Text).ToString("0.00");
            fatAltHesap();
        }

        private void textEdit18_Leave(object sender, EventArgs e)
        {
            textEdit18.Text = float.Parse(textEdit18.Text).ToString("0.00");
            fatAltHesap();
        }

        private void textEdit41_Leave(object sender, EventArgs e)
        {
            if (float.Parse(textEdit41.Text) >= 5)
            {
                textEdit41.Text = (float.Parse(textEdit41.Text) / 100).ToString();
            }

            textEdit41.Text = float.Parse(textEdit41.Text).ToString("0.00");
            fatAltHesap();
        }

        private void FaturaKapatBTN_Click(object sender, EventArgs e)
        {
            // KONTROLLER
            if (CariKodTB.Text == "") { MessageBox.Show("Faturanın carisi bulunamadı!"); }
            if (dataGridView1.RowCount <= 0) { MessageBox.Show("Liste bilgilerini lütfen doldurunuz!"); }
            if (float.Parse(GenelToplamTB.Text) <= 0) { MessageBox.Show("Fatura fiyatlandırmasında hata! Genel toplam geçersiz."); }

            //Fatura Sabit Kaydı
            Kaydet();

            //İşlem
            OleDbCommand insert = new OleDbCommand("INSERT INTO T_CARIHAR (CariKod, Tarih, Tip, FisNo, Aciklama, Borc, Miktar, C_Rap) Values ('" + CariKodTB.Text + "','" + TarihDT.DateTime + "','2','" + FaturaNoTB.Text + "','" + AciklamaTB.Text + "','" + float.Parse(GenelToplamTB.Text).ToString("0.00") + "','1','')", con);
            Connect();
            insert.ExecuteNonQuery();
            Disconnect();

            ListeYenile();

            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                string stokkodu = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string fiyat = dataGridView1.Rows[i].Cells[11].Value.ToString();
                string miktar = dataGridView1.Rows[i].Cells[3].Value.ToString();

                OleDbCommand depogetir = new OleDbCommand("Select DepoKodu From T_ALFATLIST Where Kimlik = " + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value).ToString(), con);
                Connect();
                string depo = depogetir.ExecuteScalar().ToString();
                Disconnect();

                OleDbCommand insertstok = new OleDbCommand("Insert Into T_STOKHAR (StokKodu,Tarih,FisNo,Tip,Fiyat,GirisMiktar,Depo,Aciklama,KUL_ID) Values " +
                        "('" + stokkodu + "','" + TarihDT.Text + "','" + FaturaNoTB.Text + "','D- Fatura','" + fiyat + "','" + miktar + "','" + depo + "','" + AciklamaTB.Text + "','" + KullaniciTB.Text + "')", con);
                Connect();
                insertstok.ExecuteNonQuery();
                Disconnect();

            }

            OleDbCommand fatKapat = new OleDbCommand("Update T_ALFATSBT set Kapali = true Where Fatura_No = '" + FaturaNoTB.Text + "'", con);
            Connect();
            fatKapat.ExecuteNonQuery();
            Disconnect();

            Fatura_Alis_Load(sender, e);
        }
    }
}