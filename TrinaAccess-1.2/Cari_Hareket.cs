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
    public partial class Cari_Hareket : DevExpress.XtraBars.TabForm
    {
        public Cari_Hareket()
        {
            InitializeComponent();
        }

        private void Cari_Hareket_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            TarihDT.DateTime = DateTime.Today;
            VadeDT.DateTime = DateTime.Today;

            dataGridView1.Columns["Borc"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Alacak"].DefaultCellStyle.ForeColor = Color.Green;

            dataGridView1.Columns["Borc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Alacak"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["Bakiye"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Bakiye"].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);

            backgroundWorker1.WorkerReportsProgress = true;


        }
        
        OleDbConnection con = new OleDbConnection(Giris.constr);
        public static bool kaydedilsin;



        public static void Kaydet()
        {
            kaydedilsin = true;
        }
        
        private void KaydetInternal()
        {
            if (TutarTB.Text == "") { return; }



            string tip = (TipCB.SelectedIndex + 1).ToString();

            if (HareketNoTB.Text != null)
            {
                OleDbCommand insert = new OleDbCommand("", con);
                if (BorcRB.Checked)
                {
                    insert.CommandText = "INSERT INTO T_CARIHAR (CariKod, Tarih, Tip, FisNo, Aciklama, VadeTarihi, Borc, Miktar, C_Rap) Values ('" + CariKodTB.Text + "','" + TarihDT.DateTime + "','" + tip + "','" + FisNoTB.Text + "','" + AciklamaTB.Text + "','" + VadeDT.DateTime + "','" + TutarTB.Text + "','" + MiktarTB.Text + "','" + CRapTB.Text + "')";


                }
                else if (AlacakRB.Checked)
                {
                    insert.CommandText = "INSERT INTO T_CARIHAR (CariKod, Tarih, Tip, FisNo, Aciklama, VadeTarihi, Alacak, Miktar, C_Rap) Values ('" + CariKodTB.Text + "','" + TarihDT.DateTime + "','" + tip + "','" + FisNoTB.Text + "','" + AciklamaTB.Text + "','" + VadeDT.DateTime + "','" + TutarTB.Text + "','" + MiktarTB.Text + "','" + CRapTB.Text + "')";
                }

                //Hata raporu
                if (insert.CommandText == "") { MessageBox.Show("Borç/Alacak bağıntısında sorun yaşandı." + Environment.NewLine + Environment.NewLine + "HATA KODU: 00162","Hata Raporu",MessageBoxButtons.OK,MessageBoxIcon.Error); return; }

                try
                {
                    con.Open();
                    insert.ExecuteNonQuery();

                    OleDbCommand gecmis = new OleDbCommand("INSERT INTO T_KULGECMIS (KUL_ID,ALAN,ACIKLAMA,TARIH) Values ('" + Giris.userid + "','CARI','" + CariKodTB.Text + " Koduna Cari Hareket kaydı eklendi.','" + DateTime.Now + "')", con);
                    gecmis.ExecuteNonQuery();

                    con.Close();
                }
                catch (OleDbException dbex)
                {
                    MessageBox.Show(dbex.ToString());
                }


            }
        }




        private void TabloYenile()
        {
            dataGridView1.Rows.Clear();

            OleDbCommand vericek = new OleDbCommand("SELECT * FROM T_CARIHAR WHERE CariKod='"+CariKodTB.Text+"'", con);

            con.Open();
            OleDbDataReader dr = vericek.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),dr[6].ToString(),dr[7].ToString(),dr[8].ToString(),"",dr[9].ToString());
            }
            con.Close();

            backgroundWorker1.RunWorkerAsync();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            progressBarControl1.Visible = true;

            int satirsayisi = dataGridView1.Rows.Count;

            if (satirsayisi <= 0) { return; }
            float toplamborc = 0, toplamalacak = 0;


            if (dataGridView1.Rows[0].Cells[6].Value.ToString() != "")
            {
                dataGridView1.Rows[0].Cells[8].Value = "-" + dataGridView1.Rows[0].Cells[6].Value.ToString();
                toplamborc = toplamborc + Convert.ToSingle(dataGridView1.Rows[0].Cells[6].Value);
            }

            else
            {
                dataGridView1.Rows[0].Cells[8].Value = dataGridView1.Rows[0].Cells[7].Value.ToString();
                toplamalacak = toplamalacak + Convert.ToSingle(dataGridView1.Rows[0].Cells[7].Value);
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
                    simdikibakiye = Convert.ToSingle(dgr.Cells[6].Value) * -1;
                    toplamborc = toplamborc + Convert.ToSingle(dgr.Cells[6].Value);
                }
                else
                {
                    simdikibakiye = Convert.ToSingle(dgr.Cells[7].Value);
                    toplamalacak = toplamalacak + Convert.ToSingle(dgr.Cells[7].Value);
                }

                dgr.Cells[8].Value = Convert.ToString(oncekibakiye + simdikibakiye);

                backgroundWorker1.ReportProgress(i);

            }

            ToplamalacakTB.Text = toplamalacak.ToString()+ " ₺";
            ToplamborcTB.Text = toplamborc.ToString() + " ₺";

            if (toplamborc > toplamalacak)
            {
                BakiyeTB.Text = (toplamalacak - toplamborc).ToString() + " ₺";
                BakiyeTB.ForeColor = Color.Red;
                separatorControl3.LineColor = Color.Red;
            }
            else if (toplamborc < toplamalacak)
            {
                BakiyeTB.Text = (toplamalacak - toplamborc).ToString() + " ₺";
                BakiyeTB.ForeColor = Color.Green;
                separatorControl3.LineColor = Color.Green;
            }





        } //-------- BAKİYE HESAPLAYICI
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarControl1.Text = (Convert.ToInt32(progressBarControl1.Text) + 1).ToString();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarControl1.Text = "0";
            progressBarControl1.Visible = false;
        }





        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            KaydetInternal();
        }

        private void CariKodTB_Leave(object sender, EventArgs e)
        {
            if (CariKodTB.Text == "") { return; }

            OleDbCommand adgetir = new OleDbCommand("Select CariAd From T_CARISBT Where CariKod='" + CariKodTB.Text + "'", con);

            try
            {
                con.Open();
                textEdit2.Text = adgetir.ExecuteScalar().ToString();
                con.Close();
            }
            catch { textEdit2.Text = ""; return; }

            if (textEdit2.Text != null)
            {
                TabloYenile();
            }

            


        }

        private void simpleButton2_Click(object sender, EventArgs e) // --- Sonraki data
        {
            OleDbCommand sonraki = new OleDbCommand("select top 1 CariKod From T_CARISBT Where CariKod > '"+CariKodTB.Text+ "' ORDER BY CariKod", con);
            try
            {
                con.Open();
                CariKodTB.Text = sonraki.ExecuteScalar().ToString();
                con.Close();
                CariKodTB_Leave(sender, e);
                TarihDT.Focus();
                

            } catch { con.Close(); return; }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e) // --- Önceki Data
        {
            OleDbCommand onceki = new OleDbCommand("select top 1 CariKod From T_CARISBT Where CariKod < '" + CariKodTB.Text + "' ORDER BY CariKod DESC", con);
            try
            {
                con.Open();
                CariKodTB.Text = onceki.ExecuteScalar().ToString();
                con.Close();
                CariKodTB_Leave(sender, e);
                TarihDT.Focus();


            }
            catch { con.Close(); return; }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
                
        }
    }
}