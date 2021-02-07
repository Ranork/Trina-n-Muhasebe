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
    public partial class KullaniciPanel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public KullaniciPanel()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(Giris.constr);
        private void KullaniciPanel_Load(object sender, EventArgs e)
        {
            textEdit1.Text = Giris.userid;

            OleDbCommand aciklama = new OleDbCommand("Select Aciklama From T_AUTH Where KUL_ID = '" + Giris.userid + "'", con);
            con.Open();
            textEdit2.Text = aciklama.ExecuteScalar().ToString();
            con.Close();

            OleDbCommand pass = new OleDbCommand("Select KUL_PASS From T_AUTH Where KUL_ID = '" + Giris.userid + "'", con);
            con.Open();
            textEdit3.Text = pass.ExecuteScalar().ToString();
            textEdit4.Text = textEdit3.Text;
            con.Close();

            OleDbCommand tamyetki = new OleDbCommand("Select Yonetici from T_AUTH Where KUL_ID='" + Giris.userid + "'", con);
            con.Open();
            checkEdit1.Checked = Convert.ToBoolean(tamyetki.ExecuteScalar());
            con.Close();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit3.Text != textEdit4.Text)
            {
                MessageBox.Show("Girdiğiniz iki şifre birbiri ile uyuşmamaktadır!");
                return;
            }


            DialogResult dr = MessageBox.Show("Kullanıcı bilgileniriz güncellenecek. Emin misiniz ?", "Dikkat!", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                //Kayıt
                OleDbCommand guncelle = new OleDbCommand("UPDATE T_AUTH SET Aciklama = '" + textEdit2.Text + "', KUL_PASS='" + textEdit3.Text + "' where KUL_ID='" + Giris.userid + "'", con);
                con.Open();
                guncelle.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt başarılı!");
                this.Close();
            }
            else if (dr == DialogResult.No)
            {
                this.Close();
            }
            else if (dr == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}