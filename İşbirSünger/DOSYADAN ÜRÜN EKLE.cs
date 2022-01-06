using ExcelDataReader;
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
    public partial class DOSYADAN_ÜRÜN_EKLE : Form
    {
        public DOSYADAN_ÜRÜN_EKLE()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Lütfen Dosya Seçiniz";
                openFileDialog1.Filter = " (*.xlsx)|*.xlsx";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string dosya_adres = openFileDialog1.FileName;
                    //Dosyanın okunacağı dizin
                    FileInfo fileinfo = new FileInfo(openFileDialog1.FileName);
                    textBox4.Text = fileinfo.Name;

                    //Dosyayı okuyacağımı ve gerekli izinlerin ayarlanması.
                    FileStream stream = File.Open(dosya_adres, FileMode.Open, FileAccess.Read);
                    //Encoding 1252 hatasını engellemek için;

                    ;

                    IExcelDataReader excelReader;
                    int counter = 0;

                    //Gönderdiğim dosya xls'mi xlsx formatında mı kontrol ediliyor.
                    if (Path.GetExtension(dosya_adres).ToUpper() == ".XLS")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }




                    //Veriler okunmaya başlıyor.
                    while (excelReader.Read())
                    {
                        counter++;
                        if (counter > 1)
                        {
                            dataGridView2.Rows.Add(excelReader.GetString(0), excelReader.GetString(1), excelReader.GetString(2), excelReader.GetString(3), excelReader.GetString(4), excelReader.GetDouble(5), excelReader.GetDouble(6), excelReader.GetString(7), excelReader.GetDouble(8));
                        }

                    }

                    excelReader.Close();
                    dataGridView2.Visible = true;

                }
            }
            catch (Exception a)
            {
                MessageBox.Show("HATA"+ a.ToString());
            }
        }
        baglanti baglan = new baglanti();
        SqlCommand komut;
        private SqlTransaction MyTransaction;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = "ÜRÜN_EKLEME_DOSYA_FORMATI.xlsx";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        komut = new SqlCommand("Select ÖRNEKMESAJDOSYASI from ÖRNEKLER where ID = '8'", baglan.baglan());
                        SqlDataReader oku = komut.ExecuteReader();
                        oku.Read();
                        byteData = (byte[])oku[0];
                        File.WriteAllBytes(savefile.FileName, byteData);
                        baglan.baglan().Close();
                    }
                }
            }
            catch (Exception A)
            {
                MessageBox.Show(A.ToString());
            }
        }
        SqlConnection trans = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                MessageBox.Show("DOSYA SEÇİLMELİDİR.");
            }
            else
            {

                try
                {
                    trans.Open();
                    MyTransaction = trans.BeginTransaction();

                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        SqlCommand komutkaydet = new SqlCommand("insert into Ürünler (ÜrünTürü, ÜrünKodu, ÜrünAdı,StokKodu, StokAdı,Tedarikçi,Miktar,Birim,GelişFiyatı,Tarih) values (@p1, @p2, @p3, @p4,@p5,@p6,@p7,@p8,@p9,@p10)", trans);
                        komutkaydet.Parameters.AddWithValue("@p1", dataGridView2.Rows[i].Cells[0].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p2", dataGridView2.Rows[i].Cells[1].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p3", dataGridView2.Rows[i].Cells[2].Value.ToString().Trim());
                        komutkaydet.Parameters.AddWithValue("@p4", dataGridView2.Rows[i].Cells[3].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p5", dataGridView2.Rows[i].Cells[4].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p6", dataGridView2.Rows[i].Cells[5].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p7", Convert.ToDecimal(dataGridView2.Rows[i].Cells[6].Value.ToString()));
                        komutkaydet.Parameters.AddWithValue("@p8", dataGridView2.Rows[i].Cells[7].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p9", Convert.ToDecimal(dataGridView2.Rows[i].Cells[7].Value.ToString()));
                        komutkaydet.Parameters.AddWithValue("@p10", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        komutkaydet.Transaction = MyTransaction;
                        komutkaydet.ExecuteNonQuery();
                        komutkaydet.CommandText = "SELECT MAX(ID) from Ürünler";
                        object ürünid = komutkaydet.ExecuteScalar();
                        int ürün = Convert.ToInt32(ürünid);
                        SqlCommand GünToplam = new SqlCommand("insert into GünToplam (Ürün,Gün, Toplam) " +
                                                "values (@a1, @a2, @a3)", trans);
                        GünToplam.Parameters.AddWithValue("@a1", ürün);
                        GünToplam.Parameters.AddWithValue("@a2", Convert.ToDateTime(dataGridView2.Rows[i].Cells[7].Value.ToString()).ToString("yyyyMMdd HH:mm:ss"));
                        GünToplam.Parameters.AddWithValue("@a3", Convert.ToDecimal(dataGridView2.Rows[i].Cells[6].Value.ToString()));
                        GünToplam.Transaction = MyTransaction;
                        GünToplam.ExecuteNonQuery();

                    }
                    MyTransaction.Commit();

                    MessageBox.Show("Kayıt Başarılı");
                    dataGridView2.Visible = false;
                    dataGridView2.Rows.Clear();
                    textBox4.Text = "";


                }
                catch (Exception a)
                {
                    MyTransaction.Rollback();
                    MessageBox.Show("HATA." + a.ToString());
                }
                finally
                {
                    if (trans.State == ConnectionState.Open)
                        trans.Close();
                }
            }
        }

        private void DOSYADAN_ÜRÜN_EKLE_Load(object sender, EventArgs e)
        {

        }
    }
}
