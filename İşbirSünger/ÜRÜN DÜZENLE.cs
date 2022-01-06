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
    public partial class ÜRÜN_DÜZENLE : Form
    {
        public ÜRÜN_DÜZENLE()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }
        baglanti baglan = new baglanti();
        SqlDataAdapter da;
        SqlCommandBuilder cmdb;
        DataSet ds;
        String query;
        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ,ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID where ü.ÜrünAdı='" + textBox6.Text + "'";
            griddoldur();
        }
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
            dgvBtn3.HeaderText = "DÜZENLE";
            dgvBtn3.Text = "DÜZENLE";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
            DataGridViewButtonColumn dgvbtn = new DataGridViewButtonColumn();
            dgvbtn.HeaderText = "SİL";
            dgvbtn.Text = "SİL";
            dgvbtn.UseColumnTextForButtonValue = true;
            dgvbtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn.Width = 70;
            dataGridView1.Columns.Add(dgvbtn);

        }
        SqlCommand komut;

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ,ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID";
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //arama kısmı
            panel7.Visible = true;
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox15.Text = "";
            textBox13.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            richTextBox2.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            panel7.Visible = false;
        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 3)
            {
                secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("select * from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    textBox15.Text = oku3[1].ToString();
                    textBox3.Text = oku3[4].ToString();
                    textBox13.Text = oku3[3].ToString();
                    richTextBox2.Text = oku3[6].ToString();
                    maskedTextBox1.Text = oku3[2].ToString();
                    maskedTextBox2.Text = oku3[5].ToString();
                    textBox7.Text = oku3[7].ToString();
                    textBox1.Text = oku3[8].ToString();

                }
                baglan.baglan().Close();
                panel7.Visible = true;
                panel5.Visible = false;


            }
            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 2)
            {
                secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("select * from Ürünler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox8.Text = comboBox8.Items[id.IndexOf((int)oku3[1])].ToString();
                    textBox17.Text = oku3[2].ToString();
                    textBox20.Text = oku3[3].ToString();
                    textBox19.Text = oku3[4].ToString();
                    comboBox9.Text = oku3[6].ToString();
                    comboBox1.Text = oku3[8].ToString();
                    decimal tutar = (decimal)oku3[5];
                   
                    string s = (tutar).ToString();
                    string[] parts = s.Split(',');
                    int i1 = Convert.ToInt32(parts[0]);
                    int i2 = Convert.ToInt32(parts[1]);
                    textBox14.Text = i1.ToString();
                    textBox11.Text = i2.ToString();
                    decimal tutar2 = (decimal)oku3[7];

                    string s2 = (tutar2).ToString();
                    string[] parts2 = s.Split(',');
                    int i12 = Convert.ToInt32(parts2[0]);
                    int i22 = Convert.ToInt32(parts2[1]);
                    textBox16.Text = i12.ToString();
                    textBox12.Text = i22.ToString();

                    dateTimePicker7.Value = Convert.ToDateTime(oku3[9]);

                }
                baglan.baglan().Close();
                panel7.Visible = true;
                panel5.Visible = true;


            }
            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                DialogResult result = MessageBox.Show("ÜRÜN SİLİNİRKEN ÜRÜN HAREKETLERİ DE SİLİNMEKTEDİR VE SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "ÜRÜN SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " KODLU ÜRÜNÜ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "ÜRÜN SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            trans.Open();
                            Transaction = trans.BeginTransaction();
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM Ürünler WHERE ID=@id";
                            komut = new SqlCommand(sql, trans);
                            komut.Parameters.AddWithValue("@id", id);
                            komut.Transaction = Transaction;
                            komut.ExecuteNonQuery();
                            string sql2 = "DELETE  FROM ÜrünHareketleri WHERE ID=@id";
                            SqlCommand komut2 = new SqlCommand(sql2, trans);
                            komut2.Parameters.AddWithValue("@id", id);
                            komut2.Transaction = Transaction;
                            komut2.ExecuteNonQuery();
                            string sql3 = "DELETE  FROM GünToplam WHERE Ürün=@id";
                            SqlCommand komut3 = new SqlCommand(sql3, trans);
                            komut3.Parameters.AddWithValue("@id", id);
                            komut3.Transaction = Transaction;
                            komut3.ExecuteNonQuery();
                            Transaction.Commit();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            griddoldur();

                        }
                        catch (Exception a)
                        {
                            Transaction.Rollback();
                            MessageBox.Show(a.ToString());
                        }
                        finally
                        {
                            if (trans.State == ConnectionState.Open)
                                trans.Close();
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            textBox17.Text = "";
            textBox20.Text = "";
            textBox19.Text = "";
            textBox18.Text = "";
            textBox16.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox14.Text = "";
            comboBox9.Text = "";
            comboBox8.Text = "";
            comboBox1.Text = "";
            dateTimePicker7.Value = DateTime.Now;

        }
        List<int> id = new List<int>();
        private SqlTransaction mytransaction;
        List<decimal> toplam = new List<decimal>();
        List<DateTime> gün = new List<DateTime>();
        private SqlTransaction Transaction;

        private void button12_Click(object sender, EventArgs e)
        {
            //ürün düzenleme kısımı
            if ( string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(textBox12.Text) == true)
            {
                MessageBox.Show("ÜRÜN SEÇİMİ YAPILMALI MİKTAR GİRİLMELİDİR.");
            }
           
            try
            {
                Boolean bayrak = true;
                komut = new SqlCommand("Select Ürün from ÜrünHareketleri where Ürün='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and ÜrünHareketi='EKLEME'", baglan.baglan());
                SqlDataReader oku4 = komut.ExecuteReader();
                while (oku4.Read())
                {
                    bayrak = false;
                    
                }
                baglan.baglan().Close();
                trans.Open();
                mytransaction = trans.BeginTransaction();
                bool degisken = false;
                string filtre = "update   Ürünler  set ";


                if (comboBox8.Text!=dataGridView1.Rows[secilen].Cells[7].Value.ToString())
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " Tedarikçi='" + id[comboBox8.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox17.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " ÜrünKodu='" + textBox17.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox20.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " ÜrünAdı='" + textBox20.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox19.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " StokKodu='" + textBox19.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox18.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " StokAdı='" + textBox18.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox14.Text) == false && string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " GelişFiyatı='" + textBox14.Text + "." + textBox11.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " ÜrünTürü='" + comboBox9.Text + "'";
                    degisken = true;
                }
                if (bayrak == false)
                {
                    MessageBox.Show("BU ÜRÜNÜN MİKTARINI DEĞİŞTİREMEZSİNİZ. ÇÜNKÜ ÜRÜN ÜZERİNE MİKTAR EKLENMESİ YAPILMIŞTIR. ÜRÜN MİKTAR EKLE KISIMINDAN BU İŞLEMİ SİLERSENİZ ÜRÜN MİKTARINI GÜNCELLEYEBİLİRSİNİZ.");

                }
                else if (bayrak == true)
                {
                    if (string.IsNullOrEmpty(textBox16.Text) == false && string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Miktar='" + textBox16.Text + "." + textBox12.Text + "'";
                        degisken = true;
                    }
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " Birim='" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (degisken == true)
                {
                    filtre += " , ";
                }
                filtre += " Tarih='" + dateTimePicker7.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                filtre += " where ID='"+dataGridView1.Rows[secilen].Cells[0].Value.ToString() +"'";
                SqlCommand komutkaydet = new SqlCommand(filtre, trans);
                komutkaydet.Transaction = mytransaction;
                komutkaydet.ExecuteNonQuery();
                if (bayrak == true)
                {
                    SqlCommand komutkaydet3 = new SqlCommand("update  GünToplam Toplam=@p1,Gün=@p2 set where Ürün='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Gün='" + dateTimePicker7.Value.ToString("yyyyMMdd") + "'", trans);
                    komutkaydet3.Parameters.AddWithValue("@p1", textBox16.Text + "." + textBox12.Text);
                    komutkaydet3.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    komutkaydet3.Transaction = mytransaction;
                    komutkaydet3.ExecuteNonQuery();
                }

                mytransaction.Commit();
                griddoldur();
                MessageBox.Show("İŞLEM BAŞARILI");
                textBox17.Text = "";
                textBox20.Text = "";
                textBox19.Text = "";
                textBox18.Text = "";
                textBox16.Text = "";
                textBox12.Text = "";
                textBox11.Text = "";
                textBox14.Text = "";
                comboBox9.Text = "";
                comboBox8.Text = "";
                comboBox1.Text = "";
                dateTimePicker7.Value = DateTime.Now;



            

            }
            catch (Exception A)
            {
                mytransaction.Rollback();
                MessageBox.Show(A.ToString());
            }
            finally
            {
                if (trans.State == ConnectionState.Open)
                    trans.Close();
            }
        }
        SqlConnection trans = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");
        List<int> ürün = new List<int>();
        List<decimal> miktar = new List<decimal>();
        List<decimal> kullanılan = new List<decimal>();
        private void ÜRÜN_DÜZENLE_Load(object sender, EventArgs e)
        {
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            try
            {

                id.Clear();
                comboBox8.Items.Clear();
                komut = new SqlCommand("Select Tedarikçi,ID from Tedarikçiler order by Tedarikçi", baglan.baglan());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox8.Items.Add(oku[0].ToString());
                    comboBox4.Items.Add(oku[0].ToString());

                    id.Add((int)oku[1]);

                }
                baglan.baglan().Close();
                comboBox1.Items.Clear();
                komut = new SqlCommand("Select MiktarBirimi from MiktarBirimi order by MiktarBirimi", baglan.baglan());
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    comboBox1.Items.Add(oku2[0].ToString());
                    comboBox2.Items.Add(oku2[0].ToString());

                }
                baglan.baglan().Close();
                comboBox9.Items.Clear();
                komut = new SqlCommand("Select ÜrünTürü from ÜrünTürü order by ÜrünTürü", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox9.Items.Add(oku3[0].ToString());
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

        private void button20_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox26.Text = "";
            textBox2.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = false;
                string filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ,ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID where ";

                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tedarikçi= " + "'" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox26.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ID= " + "'" + textBox26.Text + "'";
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
                if (string.IsNullOrEmpty(textBox25.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.ÜrünAdı= " + "'" + textBox25.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox24.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.StokKodu= " + "'" + textBox24.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox23.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ü.StokAdı= " + "'" + textBox23.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false && string.IsNullOrEmpty(textBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";

                    }
                    filtre += " ü.GelişFiyatı between '" + Convert.ToDecimal(textBox7.Text) + "' and '" + Convert.ToDecimal(textBox4.Text) + "'";
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
                    filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar - ü.Harcanan KALAN,ü.Birim 'BİRİM', ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t right join Ürünler ü on ü.Tedarikçi=t.ID";
                }


                query = filtre;
                griddoldur();
                textBox26.Text = "";
                textBox2.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox5.Text = "";
                textBox4.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
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
    }
}
