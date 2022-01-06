using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İşbirSünger
{
    public partial class örnek_format_yükle : Form
    {
        public örnek_format_yükle()
        {
            InitializeComponent();
        }
        byte[] bytes;
        private string path;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EXCEL Files | *.xlsx";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    path = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/xlsx";
            if (path != null)
            {
                if (contenttype != String.Empty)
                {
                    Stream fs = File.OpenRead(filePath);
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    textBox1.Text = fileinfo.Name;
                }
            }
        }
        baglanti baglan = new baglanti();

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komutkaydet = new SqlCommand("insert into ÖRNEKLER (ÖRNEKMESAJDOSYASI) values (@p1)", baglan.baglan());
                komutkaydet.Parameters.AddWithValue("@p1", SqlDbType.VarBinary).Value = bytes;

                komutkaydet.ExecuteNonQuery();
                baglan.baglan().Close();
                MessageBox.Show("Kayıt Başarılı");
            
            }
            catch (Exception a)
            {

                MessageBox.Show("HATA." + a.ToString());
            }
        }
    }
}
