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
    public partial class DOSYADAN_TEDARİKÇİ_YÜKLE : Form
    {
        public DOSYADAN_TEDARİKÇİ_YÜKLE()
        {
            InitializeComponent();
        }
        baglanti baglan = new baglanti();
        SqlCommand komut;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = "TEDARİKÇİ_EKLEME_DOSYA_FORMATI.xlsx";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        komut = new SqlCommand("Select ÖRNEKMESAJDOSYASI from ÖRNEKLER where ID = '6'", baglan.baglan());
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
                            dataGridView2.Rows.Add(excelReader.GetString(0), excelReader.GetString(1), excelReader.GetDouble(2), excelReader.GetString(3), excelReader.GetString(4), excelReader.GetString(5), excelReader.GetString(6), excelReader.GetString(7));
                        }

                    }

                    excelReader.Close();
                    dataGridView2.Visible = true;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
            }
        }

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


                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        SqlCommand komutkaydet = new SqlCommand("insert into Tedarikçiler (Tedarikçi, Email, Telefon,Fax, IBAN,Adres,VergiDairesi,VergiNo) values (@p1, @p2, @p3, @p4,@p5,@p6,@p7,@p8)", baglan.baglan());
                        komutkaydet.Parameters.AddWithValue("@p1", dataGridView2.Rows[i].Cells[0].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p2", dataGridView2.Rows[i].Cells[1].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p3", dataGridView2.Rows[i].Cells[2].Value.ToString().Trim());
                        komutkaydet.Parameters.AddWithValue("@p4", dataGridView2.Rows[i].Cells[3].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p5", dataGridView2.Rows[i].Cells[4].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p6", dataGridView2.Rows[i].Cells[5].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p7", dataGridView2.Rows[i].Cells[6].Value.ToString());
                        komutkaydet.Parameters.AddWithValue("@p8", dataGridView2.Rows[i].Cells[7].Value.ToString());
                        komutkaydet.ExecuteNonQuery();
                        baglan.baglan().Close();
                    }

                    MessageBox.Show("Kayıt Başarılı");
                    dataGridView2.Visible = false;
                    dataGridView2.Rows.Clear();
                    textBox4.Text = "";


                }
                catch (Exception a)
                {
                    MessageBox.Show("HATA." + a.ToString());
                }
                finally
                {
                    if (baglan.baglan().State == ConnectionState.Open)
                        baglan.baglan().Close();
                }
            }
        }

        private void DOSYADAN_TEDARİKÇİ_YÜKLE_Load(object sender, EventArgs e)
        {

        }
    }
}
