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
    public partial class Stok_Kayit : DevExpress.XtraBars.TabForm
    {
        public Stok_Kayit()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(Giris.constr);


        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Stok_Kayit_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            ListeYenile();
            tabFormControl1.SelectedPage = tabFormControl1.Pages[0];
            StokKoduTB.Focus();
        }

        

        private void ListeYenile()
        {
            OleDbCommand listegetir = new OleDbCommand("Select * From T_STOKSBT", con);

            con.Open();
            OleDbDataReader dr = listegetir.ExecuteReader();


            dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[1].ToString(), dr[8].ToString(), dr[2].ToString(), dr[13].ToString());
            }
            con.Close();

        }


        private void Connect()
        {
            try { con.Open(); } catch { }
        }
        private void Disconnect()
        {
            try { con.Close(); } catch { }
        }

        int sonsatir;
        private void Kaydet()
        {
            OleDbCommand kodvarmi = new OleDbCommand("Select Count(StokKodu) From T_STOKSBT Where StokKodu='" + StokKoduTB.Text + "'", con);
            

            Connect();
            if (Convert.ToInt32(kodvarmi.ExecuteScalar()) >= 1)
            {
                // kod güncelle
                OleDbCommand update = new OleDbCommand("UPDATE T_STOKSBT SET Grup_Kodu='" + GrupKoduTB.Text + "', Kod_1='" + Kod1TB.Text + "', Kod_2='" + Kod2TB.Text + "', Kod_3='" + Kod3TB.Text + "', Kod_4='" + Kod4TB.Text + "', Kod_5='" + Kod5TB.Text + "', "+
                    "StokAd='" + StokAdTB.Text + "', SatisKDV='" + SatKDVTB.Text + "', AlisKDV='" + AlKDVTB.Text + "', MuhDetay='" + MuhDetayTB.Text + "', DepoKodu='" + DepoKoduTB.Text + "', Br1='" + Br1TB.Text + "', Br2='" + Br2TB.Text + "', Br2_Pay='" + Br2PayTB.Text + "', Br2_Payda='" + Br2PaydaTB.Text + "', Br3='" + Br3TB.Text + "', Br3_Pay='" + Br3PayTB.Text + "', Br3_Payda='" + Br3PaydaTB.Text + "', Satis_Br='" + SatisBrTB.Text + "', Alis_Br='" + AlisBrTB.Text + "' Where StokKodu = '"+StokKoduTB.Text+"'" , con);
                OleDbCommand update2 = new OleDbCommand("UPDATE T_STOKSBT2 SET BrAgirlik='" + BrAgirlikTB.Text + "', NakliyeTutar='" + NakliyeTutarTB.Text + "', OncekiKodu='" + OncekiKodTB.Text + "', En='" + EnTB.Text + "', Boy='" + BoyTB.Text + "', Yukseklik='" + GenislikTB.Text + "', " +
                    "Barkod1='" + Barkod1TB.Text + "', Barkod2='" + Barkod2TB.Text + "', Barkod3='" + Barkod3TB.Text + "', CariKodu='" + CariKoduTB.Text + "', UreticiKodu='" + UreticiKoduTB.Text + "', PazarlamaciKodu='" + PazarlamaciKoduTB.Text + "', SatisF1='" + SatisF1TB.Text + "', SatisF2='" + SatisF2TB.Text + "', SatisF3='" + SatisF3TB.Text + "', SatisF4='" + SatisF4TB.Text + "', AlisF1='" + AlisF1TB.Text + "', AlisF2='" + AlisF2TB.Text + "', AlisF3='" + AlisF3TB.Text + "', AlisF4='" + AlisF4TB.Text + "', AlisFiyati='"+AlisFiyatTB.Text+"', AlisTipi='"+AlisTipTB.Text+"', Maliyet='"+MaliyetTB.Text+"', SatisTipi='"+SatisTipTB.Text+"', SatisFiyati='"+SatisFiyatTB.Text+"'  Where StokKodu = '" + StokKoduTB.Text + "'", con);

                DialogResult dr = MessageBox.Show(StokKoduTB.Text + " Kodu mevcut. Güncellemek ister misiniz ?", "Dikkat!", MessageBoxButtons.YesNo);
                
                if (dr == DialogResult.No) { Disconnect(); return; }
                try
                {
                    Connect();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Kodunun Stok Kaydı güncellendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    update2.ExecuteNonQuery();
                    update.ExecuteNonQuery();
                    Disconnect();
                    
                }
                catch (OleDbException oex)
                {
                    MessageBox.Show(oex.ToString(),"Veritabanı hatası! 01204");
                    Disconnect();
                }


                

            }
            else
            {
                //kod aç
                OleDbCommand insert = new OleDbCommand("Insert Into T_STOKSBT (StokKodu,Grup_Kodu,Kod_1,Kod_2,Kod_3,Kod_4,Kod_5,StokAd,SatisKDV,AlisKDV,MuhDetay,DepoKodu,Br1,Br2,Br2_Pay,Br2_Payda,Br3,Br3_Pay,Br3_Payda,Satis_Br,Alis_Br)" +
                    " Values ('"+StokKoduTB.Text+"','" + GrupKoduTB.Text + "','" + Kod1TB.Text + "','" + Kod2TB.Text + "','" + Kod3TB.Text + "','" + Kod4TB.Text + "','" + Kod5TB.Text + "','" + StokAdTB.Text + "','" + SatKDVTB.Text + "','" + AlKDVTB.Text + "','" + MuhDetayTB.Text + "','" + DepoKoduTB.Text + "','" + Br1TB.Text + "','" + Br2TB.Text + "','" + Br2PayTB.Text + "','" + Br2PaydaTB.Text + "','" + Br3TB.Text + "','" + Br3PayTB.Text + "','" + Br3PaydaTB.Text + "','" + SatisBrTB.Text + "','" + AlisBrTB.Text + "')", con);
                OleDbCommand insert2 = new OleDbCommand("Insert Into T_STOKSBT2 (StokKodu,BrAgirlik,NakliyeTutar,OncekiKodu,En,Boy,Yukseklik,Barkod1,Barkod2,Barkod3,CariKodu,UreticiKodu,PazarlamaciKodu,SatisF1,SatisF2,SatisF3,SatisF4,AlisF1,AlisF2,AlisF3,AlisF4,AlisTipi,AlisFiyati,Maliyet,SatisTipi,SatisFiyati)" +
                    " Values ('" + StokKoduTB.Text + "','" + BrAgirlikTB.Text + "','" + NakliyeTutarTB.Text + "','" + OncekiKodTB.Text + "','" + EnTB.Text + "','" + BoyTB.Text + "','" + GenislikTB.Text + "','" + Barkod1TB.Text + "','" + Barkod2TB.Text + "','" + Barkod3TB.Text + "','" + CariKoduTB.Text + "','" + UreticiKoduTB.Text + "','" + PazarlamaciKoduTB.Text + "','" + SatisF1TB.Text + "','" + SatisF2TB.Text + "','" + SatisF3TB.Text + "','" + SatisF4TB.Text + "','" + AlisF1TB.Text + "','" + AlisF2TB.Text + "','" + AlisF3TB.Text + "','" + AlisF4TB.Text + "','" + AlisTipTB.Text + "','" + AlisFiyatTB.Text + "','" + MaliyetTB.Text + "','" + SatisTipTB.Text + "','" + SatisFiyatTB.Text + "')", con);

                try
                {
                    Connect();
                    insert.ExecuteNonQuery();
                    insert2.ExecuteNonQuery();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','STOK','" + StokKoduTB.Text + " Kodunun Stok Kaydı oluşturuldu.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();
                    Disconnect();

                }
                catch (OleDbException oex)
                {
                    MessageBox.Show(oex.ToString(), "Veritabanı hatası! 01205");
                    Disconnect();
                }

            }

            Disconnect();

            try { sonsatir = dataGridView1.CurrentRow.Index; }
            catch { sonsatir = -1; }

            ListeYenile();

            dataGridView1.Rows[0].Selected = false;
            try { dataGridView1.Rows[sonsatir + 1].Selected = true; } catch { }



            try { StokKoduTB.Text = dataGridView1.Rows[sonsatir + 1].Cells[0].Value.ToString(); } catch { StokKoduTB.Text = ""; }
            StokKoduTB.Focus();
            StokAdTB.Focus();




        }
        

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Kaydet();
        }

        private void StokKoduTB_Leave(object sender, EventArgs e)
        {
            if (StokKoduTB.Text != "")
            {
                OleDbCommand bilgigetir = new OleDbCommand("Select * From T_STOKSBT Where StokKodu = '"+StokKoduTB.Text+"'",con);

                Connect();

                OleDbDataReader dr = bilgigetir.ExecuteReader();

                while (dr.Read())
                {
                    GrupKoduTB.Text = dr[2].ToString();
                    Kod1TB.Text = dr[3].ToString();
                    Kod2TB.Text = dr[4].ToString();
                    Kod3TB.Text = dr[5].ToString();
                    Kod4TB.Text = dr[6].ToString();
                    Kod5TB.Text = dr[7].ToString();
                    StokAdTB.Text = dr[8].ToString();
                    SatKDVTB.Text = dr[9].ToString();
                    AlKDVTB.Text = dr[10].ToString();
                    MuhDetayTB.Text = dr[11].ToString();
                    DepoKoduTB.Text = dr[12].ToString();
                    Br1TB.Text = dr[13].ToString();
                    Br2TB.Text = dr[14].ToString();
                    Br2PayTB.Text = dr[15].ToString();
                    Br2PaydaTB.Text = dr[16].ToString();
                    Br3TB.Text = dr[17].ToString();
                    Br3PayTB.Text = dr[18].ToString();
                    Br3PaydaTB.Text = dr[19].ToString();
                    SatisBrTB.Text = dr[20].ToString();
                    AlisBrTB.Text = dr[21].ToString();
                }


                OleDbCommand bilgigetir2 = new OleDbCommand("Select * From T_STOKSBT2 Where StokKodu = '" + StokKoduTB.Text + "'", con);

                OleDbDataReader dr2 = bilgigetir2.ExecuteReader();

                while (dr2.Read())
                {
                    BrAgirlikTB.Text = dr2[2].ToString();
                    NakliyeTutarTB.Text = dr2[3].ToString();
                    OncekiKodTB.Text = dr2[4].ToString();
                    EnTB.Text = dr2[5].ToString();
                    BoyTB.Text = dr2[6].ToString();
                    GenislikTB.Text = dr2[7].ToString();
                    Barkod1TB.Text = dr2[8].ToString();
                    Barkod2TB.Text = dr2[9].ToString();
                    Barkod3TB.Text = dr2[10].ToString();
                    CariKoduTB.Text = dr2[11].ToString();
                    UreticiKoduTB.Text = dr2[12].ToString();
                    PazarlamaciKoduTB.Text = dr2[13].ToString();
                    SatisF1TB.Text = dr2[14].ToString();
                    SatisF2TB.Text = dr2[15].ToString();
                    SatisF3TB.Text = dr2[16].ToString();
                    SatisF4TB.Text = dr2[17].ToString();
                    AlisF1TB.Text = dr2[18].ToString();
                    AlisF2TB.Text = dr2[19].ToString();
                    AlisF3TB.Text = dr2[20].ToString();
                    AlisF4TB.Text = dr2[21].ToString();
                    AlisTipTB.Text = dr2[22].ToString();
                    AlisFiyatTB.Text = dr2[23].ToString();
                    MaliyetTB.Text = dr2[24].ToString();
                    SatisTipTB.Text = dr2[25].ToString();
                    SatisFiyatTB.Text = dr2[26].ToString();
                }

                Disconnect();

                
            }
            else
            {
                StokKoduTB.Text = "";
                GrupKoduTB.Text = "";
                Kod1TB.Text = "";
                Kod2TB.Text = "";
                Kod3TB.Text = "";
                Kod4TB.Text = "";
                Kod5TB.Text = "";
                StokAdTB.Text = "";
                SatKDVTB.Text = "0,18";
                AlKDVTB.Text = "0,18";
                MuhDetayTB.Text = "";
                DepoKoduTB.Text = "";
                Br1TB.Text = "";
                Br2TB.Text = "";
                Br2PayTB.Text = "";
                Br2PaydaTB.Text = "";
                Br3TB.Text = "";
                Br3PayTB.Text = "";
                Br3PaydaTB.Text = "";
                SatisBrTB.Text = "";
                AlisBrTB.Text = "";

                BrAgirlikTB.Text = "";
                NakliyeTutarTB.Text = "";
                OncekiKodTB.Text = "";
                EnTB.Text = "";
                BoyTB.Text = "";
                GenislikTB.Text = "";
                Barkod1TB.Text = "";
                Barkod2TB.Text = "";
                Barkod3TB.Text = "";
                CariKoduTB.Text = "";
                UreticiKoduTB.Text = "";
                PazarlamaciKoduTB.Text = "";
                SatisF1TB.Text = "";
                SatisF2TB.Text = "";
                SatisF3TB.Text = "";
                SatisF4TB.Text = "";
                AlisF1TB.Text = "";
                AlisF2TB.Text = "";
                AlisF3TB.Text = "";
                AlisF4TB.Text = "";
                AlisTipTB.Text = "";
                AlisFiyatTB.Text = "";
                MaliyetTB.Text = "";
                SatisTipTB.Text = "";
                SatisFiyatTB.Text = "";

                StokKoduTB.Focus();
            }

            DepoKoduTB_Leave(sender, e);

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            StokKoduTB.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            StokKoduTB_Leave(sender,e);
            StokAdTB.Focus();
        }

        private void simpleButton11_Click(object sender, EventArgs e) // Önceki Data
        {
            OleDbCommand onceki = new OleDbCommand("select top 1 StokKodu From T_STOKSBT Where StokKodu < '" + StokKoduTB.Text + "' ORDER BY StokKodu DESC", con);
            try
            {
                con.Open();
                StokKoduTB.Text = onceki.ExecuteScalar().ToString();
                con.Close();
                StokKoduTB_Leave(sender, e);
                StokAdTB.Focus();


            }
            catch { con.Close(); return; }
        }

        private void simpleButton10_Click(object sender, EventArgs e) // Sonraki Data
        {
            OleDbCommand sonraki = new OleDbCommand("select top 1 StokKodu From T_STOKSBT Where StokKodu > '" + StokKoduTB.Text + "' ORDER BY StokKodu", con);
            try
            {
                con.Open();
                StokKoduTB.Text = sonraki.ExecuteScalar().ToString();
                con.Close();
                StokKoduTB_Leave(sender, e);
                StokAdTB.Focus();


            }
            catch { con.Close(); return; }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            DialogResult dr = MessageBox.Show("Form temizlenecek. Emin misiniz ?", "Dikkat!", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No) { return; }

            StokKoduTB.Text = "";
            GrupKoduTB.Text = "";
            Kod1TB.Text = "";
            Kod2TB.Text = "";
            Kod3TB.Text = "";
            Kod4TB.Text = "";
            Kod5TB.Text = "";
            StokAdTB.Text = "";
            SatKDVTB.Text = "0,18";
            AlKDVTB.Text = "0,18";
            MuhDetayTB.Text = "";
            DepoKoduTB.Text = "";
            Br1TB.Text = "";
            Br2TB.Text = "";
            Br2PayTB.Text = "";
            Br2PaydaTB.Text = "";
            Br3TB.Text = "";
            Br3PayTB.Text = "";
            Br3PaydaTB.Text = "";
            SatisBrTB.Text = "";
            AlisBrTB.Text = "";

            BrAgirlikTB.Text = "";
            NakliyeTutarTB.Text = "";
            OncekiKodTB.Text = "";
            EnTB.Text = "";
            BoyTB.Text = "";
            GenislikTB.Text = "";
            Barkod1TB.Text = "";
            Barkod2TB.Text = "";
            Barkod3TB.Text = "";
            CariKoduTB.Text = "";
            UreticiKoduTB.Text = "";
            PazarlamaciKoduTB.Text = "";
            SatisF1TB.Text = "";
            SatisF2TB.Text = "";
            SatisF3TB.Text = "";
            SatisF4TB.Text = "";
            AlisF1TB.Text = "";
            AlisF2TB.Text = "";
            AlisF3TB.Text = "";
            AlisF4TB.Text = "";
            AlisTipTB.Text = "";
            AlisFiyatTB.Text = "";
            MaliyetTB.Text = "";
            SatisTipTB.Text = "";
            SatisFiyatTB.Text = "";

            StokKoduTB.Focus();


        }

        private void Stok_Kayit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Kaydet();
                e.SuppressKeyPress = true; 
            }

            if (e.KeyCode == Keys.F5)
            {
                Kaydet();
                e.SuppressKeyPress = true;
            }

        }

        private void DepoKoduTB_Leave(object sender, EventArgs e)
        {
            OleDbCommand depoadgetir = new OleDbCommand("Select DepoAdi From T_DEPOSBT Where DepoKodu='" + DepoKoduTB.Text + "'", con);
            con.Open();
            try
            {
                DepoAciklamaTB.Text = depoadgetir.ExecuteScalar().ToString();
            }
            catch { DepoAciklamaTB.Text = ""; }

            
            con.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Rehber_Stok rhbr = new Rehber_Stok();
            rhbr.ShowDialog();
            if (Rehber_Stok.secilistokkodu == "") { return; }

            StokKoduTB.Text = Rehber_Stok.secilistokkodu;
            Rehber_Stok.secilistokkodu = "";
            StokKoduTB_Leave(sender, e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Rehber_Depo rhbrdepo = new Rehber_Depo();
            rhbrdepo.ShowDialog();
            if (Rehber_Depo.secilidepokodu == "") { return; }

            DepoKoduTB.Text = Rehber_Depo.secilidepokodu;
            Rehber_Depo.secilidepokodu = "";
            DepoKoduTB_Leave(sender, e);
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Resim_Ekle rsm = new Resim_Ekle();
            rsm.ShowDialog();
        }
    }
}