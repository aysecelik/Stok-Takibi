using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;


namespace İşbirSünger
{
    public partial class DOSYADAN_MÜŞTERİ_YÜKLE : Form
    {
        public DOSYADAN_MÜŞTERİ_YÜKLE()
        {
            InitializeComponent();
        }
        baglanti baglan = new baglanti();
        SqlCommand komut;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = "TÜZEL_KİŞİ_EKLEME_DOSYA_FORMATI.xlsx";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        komut = new SqlCommand("Select ÖRNEKMESAJDOSYASI from ÖRNEKLER where ID = '7'", baglan.baglan());
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = "GERÇEK_KİŞİ_EKLEME_DOSYA_FORMATI.xlsx";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        komut = new SqlCommand("Select ÖRNEKMESAJDOSYASI from ÖRNEKLER where ID = '5'", baglan.baglan());
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
            if (string.IsNullOrEmpty(comboBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN ÖNCELİKLE MÜŞTERİ TÜRÜ SEÇİNİZ.");
            }
            else
            {
                if (comboBox1.Text == "GERÇEK KİŞİ")
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



                            dataGridView1.Rows.Clear();

                            //Veriler okunmaya başlıyor.
                            while (excelReader.Read())
                            {
                                counter++;
                                if (counter > 1)
                                {
                                    dataGridView1.Rows.Add(excelReader.GetString(0), excelReader.GetString(1), excelReader.GetDouble(2), excelReader.GetString(3), excelReader.GetString(4), excelReader.GetString(5), excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetDouble(9));
                                }

                            }

                            excelReader.Close();

                            dataGridView1.Visible = true;

                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("HATA"+ a.ToString());
                    }
                }
                else if (comboBox1.Text == "TÜZEL KİŞİ")
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
                            if (Path.GetExtension(dosya_adres)== ".xls")
                            {
                                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            }
                            else
                            {
                                //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            }


                            dataGridView2.Rows.Clear();


                            //Veriler okunmaya başlıyor.
                            while (excelReader.Read())
                            {
                                counter++;
                                if (counter > 1)
                                {
                                    dataGridView2.Rows.Add(excelReader.GetString(0), excelReader.GetString(1), excelReader.GetDouble(2), excelReader.GetString(3), excelReader.GetString(4), excelReader.GetString(5), excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10));
                                }
                                else
                                {

                                }
                            }

                            excelReader.Close();
                            dataGridView2.Visible = true;


                        }
                    }
                    catch (Exception A )
                    {
                        MessageBox.Show(A.ToString());
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true)
            {
                MessageBox.Show("DOSYA VE MÜŞTERİ TÜRÜ SEÇİLMELİDİR.");
            }
            else
            {

                try
                {
                    if (comboBox1.Text == "GERÇEK KİŞİ")
                    {
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            SqlCommand komutkaydet = new SqlCommand("insert into Müşteri (AdSoyad, Email, Telefon,Fax, IBAN,İl,İlçe,Mahalle,Adres,KişiTip,TcKimlik) values (@p1, @p2, @p3, @p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", baglan.baglan());
                            komutkaydet.Parameters.AddWithValue("@p1", dataGridView1.Rows[i].Cells[0].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p2", dataGridView1.Rows[i].Cells[1].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p3", dataGridView1.Rows[i].Cells[2].Value.ToString().Trim());
                            komutkaydet.Parameters.AddWithValue("@p4", dataGridView1.Rows[i].Cells[3].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p5", dataGridView1.Rows[i].Cells[4].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p6", dataGridView1.Rows[i].Cells[5].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p7", dataGridView1.Rows[i].Cells[6].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p8", dataGridView1.Rows[i].Cells[7].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p9", dataGridView1.Rows[i].Cells[8].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p10", "GERÇEK KİŞİ");
                            komutkaydet.Parameters.AddWithValue("@p11", dataGridView1.Rows[i].Cells[9].Value.ToString().Trim());


                            komutkaydet.ExecuteNonQuery();
                            baglan.baglan().Close();
                        }
                    }
                   else if (comboBox1.Text == "TÜZEL KİŞİ")
                    {
                        for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                        {
                            SqlCommand komutkaydet = new SqlCommand("insert into Müşteri (AdSoyad, Email, Telefon,Fax, IBAN,İl,İlçe,Mahalle,Adres,KişiTip,VergiDairesi,VergiNo) values (@p1, @p2, @p3, @p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)", baglan.baglan());
                            komutkaydet.Parameters.AddWithValue("@p1", dataGridView2.Rows[i].Cells[0].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p2", dataGridView2.Rows[i].Cells[1].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p3", dataGridView2.Rows[i].Cells[2].Value.ToString().Trim());
                            komutkaydet.Parameters.AddWithValue("@p4", dataGridView2.Rows[i].Cells[3].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p5", dataGridView2.Rows[i].Cells[4].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p6", dataGridView2.Rows[i].Cells[5].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p7", dataGridView2.Rows[i].Cells[6].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p8", dataGridView2.Rows[i].Cells[7].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p9", dataGridView2.Rows[i].Cells[8].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p10", "TÜZEL KİŞİ");
                            komutkaydet.Parameters.AddWithValue("@p11", dataGridView2.Rows[i].Cells[9].Value.ToString());
                            komutkaydet.Parameters.AddWithValue("@p12", dataGridView2.Rows[i].Cells[10].Value.ToString());



                            komutkaydet.ExecuteNonQuery();
                            baglan.baglan().Close();
                        }
                    }
                    MessageBox.Show("Kayıt Başarılı");
                    dataGridView1.Visible = false;
                    dataGridView2.Visible = false;
                    comboBox1.Text = "";
                    textBox4.Text = "";
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();



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

        private void DOSYADAN_MÜŞTERİ_YÜKLE_Load(object sender, EventArgs e)
        {

        }
    }
}

