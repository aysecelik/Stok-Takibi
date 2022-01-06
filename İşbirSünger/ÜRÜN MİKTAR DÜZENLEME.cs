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
    public partial class ÜRÜN_MİKTAR_DÜZENLEME : Form
    {
        public ÜRÜN_MİKTAR_DÜZENLEME()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.EklenenMiktar 'EKLENEN MİKTAR',ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ü.ÜrünAdı='" + textBox6.Text + "' and ÜrünHareketi='EKLEME'";
            griddoldur();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.EklenenMiktar 'EKLENEN MİKTAR',ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ÜrünHareketi='EKLEME'";
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Arama işlemi
            panel2.Visible = true;
            panel1.Visible = false;
        }
        String query;
        baglanti baglan = new baglanti();
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            da = new SqlDataAdapter(query, baglan.baglan());
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÜRÜNLER");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.baglan().Close();
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
        private SqlDataAdapter da;
        private SqlCommandBuilder cmdb;
        private DataSet ds;
        decimal son;
        DateTime date;
        decimal tutar;
        List<decimal> toplam = new List<decimal>();
        List<DateTime> gün = new List<DateTime>();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 2)
            {
                secilen = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                ürünid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString());
                komut = new SqlCommand("Select ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.EklenenMiktar 'EKLENEN MİKTAR',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT', ü.Miktar, ü.Harcanan,ü.Miktar-i.EklenenMiktar from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {

                    comboBox5.Text = oku3[0].ToString();
                    textBox7.Text = oku3[1].ToString();
                    textBox14.Text = oku3[2].ToString();
                    textBox13.Text = oku3[3].ToString();
                    textBox12.Text = oku3[4].ToString();
                    richTextBox1.Text = oku3[7].ToString();

                    tutar = (decimal)oku3[5];

                    string s = (tutar).ToString();
                    string[] parts = s.Split(',');
                    int i1 = Convert.ToInt32(parts[0]);
                    int i2 = Convert.ToInt32(parts[1]);
                    textBox11.Text = i1.ToString();
                    textBox10.Text = i2.ToString();
                    textBox1.Text = oku3[8].ToString();
                    textBox3.Text = oku3[10].ToString();
                    harcanan = (decimal)oku3[10];


                    dateTimePicker4.Value = Convert.ToDateTime(oku3[6]);
                    date = Convert.ToDateTime(oku3[6]);
                }
                baglan.baglan().Close();
                panel2.Visible = true;
                panel1.Visible = true;


            }

            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "ÜRÜN SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip İŞLEMİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "ÜRÜN SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            myTransaction = baglan.baglan().BeginTransaction();
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            date = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[9].Value.ToString());
                            string sql = "DELETE  FROM ÜrünHareketleri WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan.baglan());
                            komut.Parameters.AddWithValue("@id", id);
                            komut.Transaction = myTransaction;
                            komut.ExecuteNonQuery();

                            komut.CommandText = "Select ü.Miktar-i.EklenenMiktar from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where i.ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";
                            object ürün = komut.ExecuteScalar();
                            son = Convert.ToDecimal(ürün);
                            SqlCommand komutkaydet1 = new SqlCommand("update  Ürünler Miktar=@p1 set where ID='" + ürünid + "'", baglan.baglan());
                            komutkaydet1.Parameters.AddWithValue("@p1", son);
                            komutkaydet1.Transaction = myTransaction;
                            komutkaydet1.ExecuteNonQuery();
                            SqlCommand komutkaydet3 = new SqlCommand("update  GünToplam Toplam=@p1 set where Ürün='" + ürünid + "' and Gün='" + date.ToString("yyyyMMdd") + "'", baglan.baglan());
                            komutkaydet3.Parameters.AddWithValue("@p1", son);
                            komutkaydet3.Transaction = myTransaction;
                            komutkaydet3.ExecuteNonQuery();
                            komut = new SqlCommand("Select Toplam,Gün from GünToplam where Gün>'" + DateTime.Now.ToString("yyyyMMdd") + "' and Ürün='" + ürünid + "'", baglan.baglan());
                            SqlDataReader oku4 = komut.ExecuteReader();
                            while (oku4.Read())
                            {
                                toplam.Add((decimal)oku4[0]);
                                gün.Add((DateTime)oku4[1]);
                            }
                            komut.Transaction = myTransaction;
                            komut.CommandText = "Select i.EklenenMiktar from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where i.ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";
                            object ürünek = komut.ExecuteScalar();
                            decimal ek = Convert.ToDecimal(ürünek);
                            for (int i = 0; i < toplam.Count; i++)
                            {
                                SqlCommand komutkaydet4 = new SqlCommand("update  GünToplam Toplam=@p1 set where Ürün='" + ürünid + "' and Gün='" + gün[i].ToString("yyyyMMdd") + "'", baglan.baglan());
                                komutkaydet4.Parameters.AddWithValue("@p1", toplam[i] - ek);

                                komutkaydet4.Transaction = myTransaction;
                                komutkaydet4.ExecuteNonQuery();
                            }

                            myTransaction.Commit();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            griddoldur();

                        }
                        catch (Exception a)
                        {
                            myTransaction.Rollback();
                            MessageBox.Show(a.ToString());
                        }
                        finally
                        {
                            if (baglan.baglan().State == ConnectionState.Open)
                                baglan.baglan().Close();
                        }

                    }
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox26.Text = "";
            textBox2.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";

            textBox9.Text = "";
            textBox8.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = true;
                string filtre = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.EklenenMiktar 'EKLENEN MİKTAR', ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ÜrünHareketi='EKLEME' ";


                if (string.IsNullOrEmpty(textBox26.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " İ.ID= " + "'" + textBox26.Text + "'";
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
                    else if (radioButton3.Checked == true)
                    {
                        filtre += " i.EklenenMiktar between '" + Convert.ToDecimal(textBox9.Text) + "' and '" + Convert.ToDecimal(textBox8.Text) + "'";
                    }
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";

                    }
                    filtre += " i.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd HH:mm:ss") + "'";

                    degisken = true;
                }



                query = filtre;
                griddoldur();
                textBox26.Text = "";
                textBox2.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                comboBox3.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                panel2.Visible = false;




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

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            comboBox5.Text = "";
            textBox7.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            dateTimePicker4.Value = DateTime.Now;
        }
        int secilen;
        int ürünid;
        decimal harcanan;
        private SqlTransaction myTransaction;

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true)
            {
                MessageBox.Show("ÜRÜN SEÇİMİ YAPILMALI VE KALAN MİKTARDAN DAHA FAZLA KULLANIM YAPILMASI MÜMKÜN DEĞİLDİR.");
            }
            else
            {
                try
                {
                    trans.Open();
                    myTransaction = trans.BeginTransaction();
                    SqlCommand komutkaydet2 = new SqlCommand("update  ÜrünHareketleri EklenenMiktar=@p1,Tarih=@p2 set where ID='" + secilen + "'", trans);
                    komutkaydet2.Parameters.AddWithValue("@p1", textBox11.Text + "." + textBox10.Text);
                    komutkaydet2.Parameters.AddWithValue("@p2", dateTimePicker4.Value.ToString("yyyyMMdd HH:mm:ss"));
                    komutkaydet2.Transaction = myTransaction;
                    komutkaydet2.ExecuteNonQuery();
                    trans.Close();
                    SqlCommand komutkaydet1 = new SqlCommand("update  Ürünler Miktar=@p1 set where ID='" + ürünid + "'", trans);
                    komutkaydet1.Parameters.AddWithValue("@p1", harcanan + Convert.ToDecimal(textBox11.Text + "," + textBox10.Text));
                    komutkaydet1.Transaction = myTransaction;
                    komutkaydet1.ExecuteNonQuery();
                    SqlCommand komutkaydet3 = new SqlCommand("update  GünToplam Toplam=@p1,Gün=@p2 set where Ürün='" + ürünid + "' and Gün='" + date.ToString("yyyyMMdd") + "'", trans);
                    komutkaydet3.Parameters.AddWithValue("@p1", harcanan + Convert.ToDecimal(textBox11.Text + "," + textBox10.Text));
                    komutkaydet3.Parameters.AddWithValue("@p2", dateTimePicker4.Value.ToString("yyyyMMdd"));
                    komutkaydet3.Transaction = myTransaction;
                    komutkaydet3.ExecuteNonQuery();
                    komut = new SqlCommand("Select Toplam,Gün from GünToplam where Gün>'" + dateTimePicker4.Value.ToString("yyyyMMdd") + "' and Ürün='" + ürünid + "'", baglan.baglan());
                    SqlDataReader oku4 = komut.ExecuteReader();
                    while (oku4.Read())
                    {
                        toplam.Add((decimal)oku4[0]);
                        gün.Add((DateTime)oku4[1]);
                    }
                    komut.Transaction = myTransaction;
                    komut.CommandText = "Select i.EklenenMiktar from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where i.ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";
                    object ürünek = komut.ExecuteScalar();
                    decimal ek = Convert.ToDecimal(ürünek);

                    for (int i = 0; i < toplam.Count; i++)
                    {
                        SqlCommand komutkaydet4 = new SqlCommand("update  GünToplam Toplam=@p1 set where Ürün='" + ürünid + "' and Gün='" + gün[i].ToString("yyyyMMdd") + "'", baglan.baglan());
                        komutkaydet4.Parameters.AddWithValue("@p1", toplam[i] - tutar + ek);

                        komutkaydet4.Transaction = myTransaction;
                        komutkaydet4.ExecuteNonQuery();
                    }



                    myTransaction.Commit();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    comboBox5.Text = "";
                    textBox7.Text = "";
                    textBox14.Text = "";
                    textBox13.Text = "";
                    textBox12.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                    textBox11.Text = "";
                    textBox10.Text = "";
                    dateTimePicker4.Value = DateTime.Now;
                    panel2.Visible = false;



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
        SqlConnection trans = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");
      
        private void ÜRÜN_MİKTAR_DÜZENLEME_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            try
            {

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
