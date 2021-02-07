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
    public partial class Giris : DevExpress.XtraEditors.XtraForm
    {
        public Giris()
        {
            InitializeComponent();
        }

        

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }


        public static string constr = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                    @"Data source= E:\Trina.mdb";

        OleDbConnection con = new OleDbConnection(constr);

        public static string userid = null;

        private void LoginButton_Click(object sender, EventArgs e)
        {
            OleDbCommand idvarmi = new OleDbCommand("SELECT COUNT(KUL_ID) FROM T_AUTH WHERE KUL_ID='" + textEdit1.Text + "'", con);
            con.Open();
            int count = 0;
            try { count = Convert.ToInt32(idvarmi.ExecuteScalar()); } catch { }

            if (count <= 0)
            {
                MessageBox.Show("Kullanıcı adı bulunamadı!" + Environment.NewLine, "Hata!                                        #01");
                con.Close();
                return;
            }

            con.Close();

            OleDbCommand passdogrumu = new OleDbCommand("SELECT COUNT(*) FROM T_AUTH WHERE KUL_ID='" + textEdit1.Text + "' AND KUL_PASS = '" + textEdit2.Text + "'", con);

            count = 0;

            con.Open();
            try { count = Convert.ToInt32(passdogrumu.ExecuteScalar()); } catch { }

            if (count <= 0)
            {
                MessageBox.Show("Şifre hatalı!" + Environment.NewLine, "Hata!                                             #02");
                con.Close();
                return;
            }

            con.Close();
            


            this.Hide();
            userid = FirstCharToUpper(textEdit1.Text);
            //Panel pnl = new Panel();
            //pnl.ShowDialog();

            OleDbCommand songirtar = new OleDbCommand("Update T_AUTH SET SON_GIRIS_TAR = '" + DateTime.Now + "' Where KUL_ID = '"+userid+"'", con);
            con.Open();
            songirtar.ExecuteNonQuery();
            con.Close();

            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void textEdit1_Enter(object sender, EventArgs e)
        {
            separatorControl2.Visible = true;
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            separatorControl2.Visible = false;
        }

        private void textEdit2_Enter(object sender, EventArgs e)
        {
            separatorControl3.Visible = true;
        }

        private void textEdit2_Leave(object sender, EventArgs e)
        {
            separatorControl3.Visible = false;
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userid == "" || userid == null)
            {
                Application.Exit();
            }
        }
    }
}