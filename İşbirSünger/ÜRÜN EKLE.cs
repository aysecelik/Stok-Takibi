using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İşbirSünger
{
    public partial class ÜRÜN_EKLE : Form
    {
        public ÜRÜN_EKLE()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;

            label5.Text = "ÜRÜN TÜRÜ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
            label5.Text = "MİKTAR BİRİMİ";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            panel1.Visible = false;
        }
        baglanti baglan = new baglanti();
        SqlCommand komut;
        SqlConnection trans = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");

        private void button5_Click(object sender, EventArgs e)
        {
            if (label5.Text == "ÜRÜN TÜRÜ")
            {
                try
                {
                    SqlCommand komutkaydet = new SqlCommand("insert into ÜrünTürü (ÜrünTürü) values (@p1)", baglan.baglan());
                    komutkaydet.Parameters.AddWithValue("@p1", textBox1.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.baglan().Close();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox1.Text = "";
                    panel1.Visible = false;
                    comboBox9.Items.Clear();
                    komut = new SqlCommand("Select ÜrünTürü from ÜrünTürü order by ÜrünTürü", baglan.baglan());
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        comboBox9.Items.Add(oku[0].ToString());
                    }
                    baglan.baglan().Close();
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

            else if (label5.Text == "MİKTAR BİRİMİ")
            {
                try
                {
                    SqlCommand komutkaydet = new SqlCommand("insert into MiktarBirimi (MiktarBirimi) values (@p1)", baglan.baglan());
                    komutkaydet.Parameters.AddWithValue("@p1", textBox1.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.baglan().Close();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox1.Text = "";
                    panel1.Visible = false;
                    comboBox1.Items.Clear();
                    komut = new SqlCommand("Select MiktarBirimi from MiktarBirimi order by MiktarBirimi", baglan.baglan());
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        comboBox1.Items.Add(oku[0].ToString());
                    }
                    baglan.baglan().Close();
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
        List<int> id = new List<int>();
        SqlTransaction myTransaction;

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "insert into  Tedarikçiler(";
                string values = "Values (";


                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Tedarikçi ";
                    values += "'" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Email ";
                    values += "'" + textBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Telefon ";
                    values += "'" + maskedTextBox1.Text.Replace(" ", "") + "'";
                    degisken = true;
                }
                if (maskedTextBox2.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IBAN ";
                    values += "'" + maskedTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " FAX ";
                    values += "'" + textBox13.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Adres ";
                    values += "'" + richTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " VergiNo ";
                    values += "'" + textBox8.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " VergiDairesi ";
                    values += "'" + textBox7.Text + "'";
                    degisken = true;
                }
                filtre += ")";
                values += ")";
                filtre += values;
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan.baglan());
                komutkaydet.ExecuteNonQuery();
                baglan.baglan().Close();
                MessageBox.Show("Kayıt Başarılı");
                id.Clear();
                textBox15.Text = "";
                textBox13.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox10.Text = "";
                richTextBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                comboBox8.Items.Clear();
                komut = new SqlCommand("Select Tedarikçi,ID from Tedarikçiler order by Tedarikçi", baglan.baglan());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox8.Items.Add(oku[0].ToString());
                    id.Add((int)oku[1]);

                }
                baglan.baglan().Close();
            }
            catch (Exception A)
            {
                MessageBox.Show(A.ToString());
            }
            finally
            {
                if (baglan.baglan().State == ConnectionState.Open)
                    baglan.baglan().Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            textBox15.Text = "";
            textBox13.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            richTextBox2.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = true;
        }
        List<int> ürün = new List<int>();
        List<decimal> miktar = new List<decimal>();
        List<decimal> kullanılan = new List<decimal>();
        private void ÜRÜN_EKLE_Load(object sender, EventArgs e)
        {
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            try
            {

                id.Clear();
                comboBox8.Items.Clear();
                komut = new SqlCommand("Select Tedarikçi,ID from Tedarikçiler order by Tedarikçi", baglan.baglan());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox8.Items.Add(oku[0].ToString());
                    id.Add((int)oku[1]);

                }
                baglan.baglan().Close();
                comboBox1.Items.Clear();
                komut = new SqlCommand("Select MiktarBirimi from MiktarBirimi order by MiktarBirimi", baglan.baglan());
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    comboBox1.Items.Add(oku2[0].ToString());
                }
                baglan.baglan().Close();
                comboBox9.Items.Clear();
                komut = new SqlCommand("Select ÜrünTürü from ÜrünTürü order by ÜrünTürü", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox9.Items.Add(oku3[0].ToString());
                }
                baglan.baglan().Close();
                Boolean bayrak = true;
                komut = new SqlCommand("Select Ürün from GünToplam where Gün='" + DateTime.Now.ToString("yyyyMMdd") + "'", baglan.baglan());
                SqlDataReader oku4 = komut.ExecuteReader();
                while (oku4.Read())
                {
                    bayrak = false;
                }
                baglan.baglan().Close();
                if (bayrak == true)
                {
                    ürün.Clear();
                    miktar.Clear();
                    komut = new SqlCommand("Select ID,Miktar,Harcanan from ÜRÜNLER", baglan.baglan());
                    SqlDataReader oku5 = komut.ExecuteReader();
                    while (oku5.Read())
                    {
                        ürün.Add((int)oku5[0]);
                        miktar.Add((decimal)oku5[1]);
                        kullanılan.Add((decimal)oku5[2]);


                    }
                    baglan.baglan().Close();
                    for (int i = 0; i < ürün.Count; i++)
                    {
                        SqlCommand GünToplam = new SqlCommand("insert into GünToplam (Ürün,Gün, Toplam) " +
                                  "values (@a1, @a2, @a3,@a4)", baglan.baglan());
                        GünToplam.Parameters.AddWithValue("@a1", ürün[i]);
                        GünToplam.Parameters.AddWithValue("@a2", DateTime.Now.ToString("yyyyMMdd"));
                        GünToplam.Parameters.AddWithValue("@a3", miktar[i]);
                        GünToplam.Parameters.AddWithValue("@a4", kullanılan[i]);

                        GünToplam.ExecuteNonQuery();
                        baglan.baglan().Close();
                        SqlCommand hareket = new SqlCommand("insert into ÜrünHareketleri (Ürün,EklenenMiktar,Tarih,KullanımMiktarı) " +
                                 "values (@a1, 0, @a3,0)", baglan.baglan());
                        hareket.Parameters.AddWithValue("@a1", ürün[i]);
                        hareket.Parameters.AddWithValue("@a3", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));

                        hareket.ExecuteNonQuery();
                        baglan.baglan().Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("BEKLENMEDİK BİR HATA İLE KARŞILAŞILDI. LÜTFEN UYGULAMAYI TEKRAR BAŞLATIN.");
            }
            finally
            {

                if (baglan.baglan().State == ConnectionState.Open)
                    baglan.baglan().Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true)
            {
                MessageBox.Show("MİKTAR GİRİLMELİDİR.");
            }
            else
                try
                {
                    trans.Open();
                    myTransaction = trans.BeginTransaction();
                    bool degisken = false;
                    string filtre = "insert into  Ürünler (";
                    string values = "Values (";


                    if (string.IsNullOrEmpty(comboBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Tedarikçi ";
                        values += "'" + id[comboBox8.SelectedIndex] + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox17.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " ÜrünKodu ";
                        values += "'" + textBox17.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " ÜrünAdı ";
                        values += "'" + textBox2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox4.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " StokKodu ";
                        values += "'" + textBox4.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox5.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " StokAdı ";
                        values += "'" + textBox5.Text + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(textBox14.Text) == false && string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " GelişFiyatı ";
                        values += "'" + textBox14.Text + "." + textBox6.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox9.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " ÜrünTürü ";
                        values += "'" + comboBox9.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox9.Text) == false && string.IsNullOrEmpty(textBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Miktar ";
                        values += "'" + textBox9.Text + "." + textBox3.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Birim ";
                        values += "'" + comboBox1.Text + "'";
                        degisken = true;
                    }
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Tarih ";
                    values += "'" + dateTimePicker7.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    filtre += ", Harcanan)";
                    values += ", 0)";
                    filtre += values;
                    SqlCommand komutkaydet = new SqlCommand(filtre, trans);
                    komutkaydet.Transaction = myTransaction;
                    komutkaydet.ExecuteNonQuery();
                    komutkaydet.CommandText = "SELECT MAX(ID) from Ürünler";
                    object ürünid = komutkaydet.ExecuteScalar();
                    int ürün = Convert.ToInt32(ürünid);
                    SqlCommand GünToplam = new SqlCommand("insert into GünToplam (Ürün,Gün, Toplam,Harcanan) " +
                                            "values (@a1, @a2, @a3,0)", trans);
                    GünToplam.Parameters.AddWithValue("@a1", ürün);
                    GünToplam.Parameters.AddWithValue("@a2", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    GünToplam.Parameters.AddWithValue("@a3", textBox9.Text + "." + textBox3.Text);
                    GünToplam.Transaction = myTransaction;
                    GünToplam.ExecuteNonQuery();
                    SqlCommand hareket = new SqlCommand("insert into ÜrünHareketleri (Ürün,EklenenMiktar,Tarih,KullanımMiktarı) " +
                                     "values (@a1, @a2, @a3,@a4)", trans);
                    hareket.Parameters.AddWithValue("@a1", ürün);
                    hareket.Parameters.AddWithValue("@a2", 0);
                    hareket.Parameters.AddWithValue("@a3", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                    hareket.Parameters.AddWithValue("@a4", 0);
                    hareket.Transaction = myTransaction;
                    hareket.ExecuteNonQuery();
                    myTransaction.Commit();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox17.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox9.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox14.Text = "";
                    comboBox9.Text = "";
                    comboBox8.Text = "";
                    comboBox1.Text = "";
                    dateTimePicker7.Value = DateTime.Now;
                }
                catch (Exception A)
                {
                    MessageBox.Show(A.ToString());
                    myTransaction.Rollback();
                }
                finally
                {
                    if (trans.State == ConnectionState.Open)
                        trans.Close();
                }
        }
        }

       
    }

