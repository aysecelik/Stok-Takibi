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
    public partial class ÜRÜN_MİKTAR_EKLE : Form
    {
        public ÜRÜN_MİKTAR_EKLE()
        {
            InitializeComponent();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox13.Text = "";
            textBox2.Text = "";
            textBox12.Text = "";
            textBox10.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            comboBox3.Text = "";
            comboBox5.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }
        List<int> id = new List<int>();
        baglanti baglan = new baglanti();
        SqlDataAdapter da;
        SqlCommandBuilder cmdb;
        DataSet ds;
        String query;
        SqlCommand komut;
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            da = new SqlDataAdapter(query, baglan.baglan());
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÜRÜNLER");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.baglan().Close();
            DataGridViewButtonColumn dgvBtn1 = new DataGridViewButtonColumn();
            dgvBtn1.HeaderText = "TEDARİKÇİ";
            dgvBtn1.Text = "DETAY";
            dgvBtn1.UseColumnTextForButtonValue = true;
            dgvBtn1.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn1.Width = 70;
            dataGridView1.Columns.Add(dgvBtn1);
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "GÖRÜNTÜLE";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
            DataGridViewButtonColumn dgvBtn4 = new DataGridViewButtonColumn();
            dgvBtn4.HeaderText = "SEÇ";
            dgvBtn4.Text = "SEÇ";
            dgvBtn4.UseColumnTextForButtonValue = true;
            dgvBtn4.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn4.Width = 70;
            dataGridView1.Columns.Add(dgvBtn4);
        }
        int secilen;
        decimal harcanan;
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = false;
                string filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID where ";

                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tedarikçi= " + "'" + comboBox5.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ID= " + "'" + textBox13.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ÜrünTürü= " + "'" + comboBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.Birim= " + "'" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ÜrünKodu= " + "'" + textBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox12.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ÜrünAdı= " + "'" + textBox12.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.StokKodu= " + "'" + textBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.StokAdı= " + "'" + textBox7.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text) == false && string.IsNullOrEmpty(textBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";

                    }
                    filtre += " ü.GelişFiyatı between '" + Convert.ToDecimal(textBox1.Text) + "' and '" + Convert.ToDecimal(textBox4.Text) + "'";
                    degisken = true;
                }
                if ((string.IsNullOrEmpty(textBox9.Text) == false && string.IsNullOrEmpty(textBox8.Text) == false) && (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";

                    }
                    if (radioButton1.Checked == true)
                    {
                        filtre += " ü.Miktar between '" + Convert.ToDecimal(textBox9.Text) + "' and '" + Convert.ToDecimal(textBox8.Text) + "'";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        filtre += " ü.Harcanan between '" + Convert.ToDecimal(textBox9.Text) + "' and '" + Convert.ToDecimal(textBox8.Text) + "'";
                    }
                    else if (radioButton3.Checked == true)
                    {
                        filtre += " ü.Miktar-ü.Harcanan between '" + Convert.ToDecimal(textBox9.Text) + "' and '" + Convert.ToDecimal(textBox8.Text) + "'";
                    }
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";

                    }
                    filtre += " ü.Tarih between '" + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy HH:mm:ss") + "' and '" + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy HH:mm:ss") + "'";

                    degisken = true;
                }
                if (degisken == false)
                {
                    filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar - ü.Harcanan KALAN,ü.Birim 'BİRİM' ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID";
                }


                query = filtre;
                griddoldur();
                textBox13.Text = "";
                textBox2.Text = "";
                textBox12.Text = "";
                textBox10.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox4.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                comboBox3.Text = "";
                comboBox5.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                panel7.Visible = false;




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

        private void button4_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox32.Text = "";
            textBox37.Text = "";
            textBox36.Text = "";
            textBox35.Text = "";
            textBox32.Text = "";
            textBox31.Text = "";
            textBox33.Text = "";
            textBox30.Text = "";
            dateTimePicker7.Value = DateTime.Now;
            comboBox10.Text = "";
            comboBox9.Text = "";
            comboBox8.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox40.Text = "";
            textBox41.Text = "";
            textBox42.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            richTextBox2.Text = "";
            textBox39.Text = "";
            textBox38.Text = "";
        }

        private void textBox43_Click(object sender, EventArgs e)
        {
            textBox43.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü where ü.ÜrünAdı='" + textBox43.Text + "'";
            griddoldur();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID";
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel5.Visible = true;
            panel3.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 3)
            {
                komut = new SqlCommand("select * from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    textBox40.Text = oku3[1].ToString();
                    textBox41.Text = oku3[4].ToString();
                    textBox42.Text = oku3[3].ToString();
                    richTextBox2.Text = oku3[6].ToString();
                    maskedTextBox1.Text = oku3[2].ToString();
                    maskedTextBox2.Text = oku3[5].ToString();
                    textBox39.Text = oku3[7].ToString();
                    textBox38.Text = oku3[8].ToString();

                }
                baglan.baglan().Close();
                panel7.Visible = true;
                panel5.Visible = false;


            }
            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 2)
            {
                komut = new SqlCommand("select * from Ürünler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox10.Text = comboBox8.Items[id.IndexOf((int)oku3[1])].ToString();
                    textBox32.Text = oku3[2].ToString();
                    textBox37.Text = oku3[3].ToString();
                    textBox36.Text = oku3[4].ToString();
                    textBox35.Text = oku3[5].ToString();

                    comboBox9.Text = oku3[7].ToString();
                    comboBox8.Text = oku3[9].ToString();
                    decimal tutar = (decimal)oku3[6];

                    string s = (tutar).ToString();
                    string[] parts = s.Split(',');
                    int i1 = Convert.ToInt32(parts[0]);
                    int i2 = Convert.ToInt32(parts[1]);
                    textBox33.Text = i1.ToString();
                    textBox30.Text = i2.ToString();
                    decimal tutar2 = (decimal)oku3[8];

                    string s2 = (tutar2).ToString();
                    string[] parts2 = s.Split(',');
                    int i12 = Convert.ToInt32(parts2[0]);
                    int i22 = Convert.ToInt32(parts2[1]);
                    textBox34.Text = i12.ToString();
                    textBox31.Text = i22.ToString();

                    dateTimePicker7.Value = Convert.ToDateTime(oku3[10]);

                }
                baglan.baglan().Close();
                panel7.Visible = true;
                panel5.Visible = true;
                panel3.Visible = false;
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                secilen = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                label16.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + " - " + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString();
                harcanan = Convert.ToDecimal(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString());
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox6.Text) == true)
            {
                MessageBox.Show("ÜRÜN SEÇİMİ YAPILMALI MİKTAR GİRİLMELİDİR.");
            }
          
            else
            {
                try
                {
                    trans.Open();
                    myTransaction = trans.BeginTransaction();
                    SqlCommand komutkaydet = new SqlCommand("insert into  ÜrünHareketleri (Ürün,EklenenMiktar,Tarih,[Not],ÜrünHareketi) values (@p1, @p2, @p3, @p4,'EKLEME')", trans);
                    komutkaydet.Parameters.AddWithValue("@p1", secilen);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox5.Text + "." + textBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd HH:mm:ss"));
                    komutkaydet.Parameters.AddWithValue("@p4", richTextBox1.Text);
                    komutkaydet.Transaction = myTransaction;
                    komutkaydet.ExecuteNonQuery();
                    trans.Close();
                    SqlCommand komutkaydet2 = new SqlCommand("update  Ürünler set Miktar=@p1  where ID='" + secilen + "'", trans);
                    komutkaydet2.Parameters.AddWithValue("@p1", harcanan + Convert.ToDecimal(textBox5.Text + "," + textBox6.Text));
                    komutkaydet2.Transaction = myTransaction;
                    komutkaydet2.ExecuteNonQuery();               
                    SqlCommand komutkaydet3 = new SqlCommand("update  GünToplam set Toplam=@p1,Gün=@p2  where Ürün='" + secilen + "' and Gün='"+ dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'", trans);
                    komutkaydet3.Parameters.AddWithValue("@p1", harcanan + Convert.ToDecimal(textBox5.Text + "," + textBox6.Text));
                    komutkaydet3.Parameters.AddWithValue("@p2", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet3.Transaction = myTransaction;
                    komutkaydet3.ExecuteNonQuery();
                    myTransaction.Commit();
                    MessageBox.Show("Kayıt Başarılı");
                    dateTimePicker3.Value = DateTime.Now;
                    label16.Text = "";
                    textBox3.Text = "";
                    richTextBox1.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "00000";
                    dateTimePicker3.Value = DateTime.Now;

                }
                catch (Exception a)
                {
                    myTransaction.Rollback();
                    MessageBox.Show("HATA." + a.ToString());
                }
                finally
                {

                    if (trans.State == ConnectionState.Open)
                        trans.Close();
                }
            }
        }
        List<int> ürün = new List<int>();
        List<decimal> miktar = new List<decimal>();
        List<decimal> kullanılan = new List<decimal>();

        private SqlTransaction myTransaction;
        SqlConnection trans = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");
        private void ÜRÜN_MİKTAR_EKLE_Load(object sender, EventArgs e)
        {
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            try
            {

                id.Clear();
                comboBox5.Items.Clear();
                komut = new SqlCommand("Select Tedarikçi,ID from Tedarikçiler order by Tedarikçi", baglan.baglan());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox5.Items.Add(oku[0].ToString());
                    id.Add((int)oku[1]);

                }
                baglan.baglan().Close();
                comboBox2.Items.Clear();
                komut = new SqlCommand("Select MiktarBirimi from MiktarBirimi order by MiktarBirimi", baglan.baglan());
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    comboBox2.Items.Add(oku2[0].ToString());
                }
                baglan.baglan().Close();
                comboBox3.Items.Clear();
                komut = new SqlCommand("Select ÜrünTürü from ÜrünTürü order by ÜrünTürü", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox3.Items.Add(oku3[0].ToString());
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
                                  "values (@a1, @a2, @a3,@a4)", baglan.baglan());
                        hareket.Parameters.AddWithValue("@a1", ürün[i]);
                        hareket.Parameters.AddWithValue("@a2", 0);
                        hareket.Parameters.AddWithValue("@a3", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        hareket.Parameters.AddWithValue("@a4", 0);

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
    }
}
