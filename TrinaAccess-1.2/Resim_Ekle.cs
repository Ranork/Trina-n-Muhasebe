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
using System.IO;
using System.Data.OleDb;

namespace TrinaAccess_1._2
{
    public partial class Resim_Ekle : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Resim_Ekle()
        {
            InitializeComponent();
        }

        private void textEdit2_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureEdit1.Image = Image.FromFile(textEdit2.Text);
            }
            catch { }
            
        }

        OleDbConnection con = new OleDbConnection(Giris.constr);
        string resimPath;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textEdit2.Text = openFileDialog1.FileName.ToString();
                resimPath = openFileDialog1.FileName.ToString();
                textEdit2_Leave(sender, e);
                simpleButton2.Focus();
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] resim = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            MessageBox.Show(Convert.ToString(resim));

        }
    }
}