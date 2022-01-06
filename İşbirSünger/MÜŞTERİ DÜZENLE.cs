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
namespace İşbirSünger
{
    public partial class MÜŞTERİ_DÜZENLE : Form
    {
        public MÜŞTERİ_DÜZENLE()
        {
            InitializeComponent();
        }
        baglanti baglan = new baglanti();
        SqlDataAdapter da;
        SqlCommandBuilder cmdb;
        DataSet ds;
        String query;
        private void button2_Click(object sender, EventArgs e)
        {
           
            query = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON,m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m where m.AdSoyad='" + textBox6.Text + "'";
            griddoldur();
        }
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            
            da = new SqlDataAdapter(query, baglan.baglan());
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MÜŞTERİ");
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
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount-2)
                {
                    secilen = dataGridView1.CurrentCell.RowIndex;
                        komut = new SqlCommand("select * from Müşteri where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
                        SqlDataReader oku3 = komut.ExecuteReader();
                        while (oku3.Read())
                        {
 
                            textBox15.Text = oku3[1].ToString();
                            textBox10.Text = oku3[7].ToString();
                            textBox13.Text = oku3[4].ToString();

                            textBox3.Text = oku3[2].ToString();
                            richTextBox2.Text = oku3[9].ToString();
                            textBox8.Text = oku3[8].ToString();
                            textBox12.Text = oku3[6].ToString();
                            maskedTextBox1.Text = oku3[3].ToString();
                            maskedTextBox2.Text = oku3[5].ToString();
                            if (oku3[10].ToString() == "GERÇEK KİŞİ")
                            {
                                panel5.Visible = true;
                                panel2.Visible = true;
                                maskedTextBox3.Text = oku3[11].ToString();
                                radioButton2.Checked = true;

                            }
                            else if (oku3[10].ToString() == "TÜZEL KİŞİ")
                            {
                                panel5.Visible = true;
                                panel2.Visible = false;
                                textBox7.Text = oku3[13].ToString();
                                textBox1.Text = oku3[12].ToString();
                                radioButton1.Checked = true;


                            }

                        }
                        baglan.baglan().Close();
                        panel7.Visible = true;
                        button11.Visible = true;
                        button5.Visible = false;
                    

                }
                else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "MÜŞTERİ SİLME", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " İSİMLİ MÜŞTERİYİ  TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "MÜŞTERİ SİLME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                string sql = "DELETE  FROM Müşteri WHERE ID=@id";
                                komut = new SqlCommand(sql, baglan.baglan());
                                komut.Parameters.AddWithValue("@id", id);
                                komut.ExecuteNonQuery();
                                baglan.baglan().Close();
                                MessageBox.Show("İŞLEM BAŞARILI");
                                griddoldur();

                            }
                            catch (Exception a)
                            {
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
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
            finally
            {
                if (baglan.baglan().State == ConnectionState.Open)
                    baglan.baglan().Close();
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox15.Text = "";
            maskedTextBox3.Text = "";
            textBox10.Text = "";
            textBox13.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            richTextBox2.Text = "";
            textBox8.Text = "";
            textBox12.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            panel5.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = false;
                string filtre = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON,m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m where";
                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.AdSoyad= " + "'" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Email= " + "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Telefon= " + "'" + maskedTextBox1.Text.Replace(" ", "") + "'";
                    degisken = true;
                }
                if (maskedTextBox2.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.IBAN= " + "'" + maskedTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Fax= " + "'" + textBox13.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox12.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.İl= " + "'" + textBox12.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.İlçe= " + "'" + textBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Mahalle= " + "'" + textBox8.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Adres= " + "'" + richTextBox2.Text + "'";
                    degisken = true;
                }
                if (maskedTextBox3.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.TcKimlik=" + "'" + maskedTextBox3.Text + "'";
                    degisken = true;
                }
                if (radioButton2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.KişiTip= " + "'GERÇEK KİŞİ'";
                    degisken = true;

                }
                if (radioButton1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.KişiTip= " + "'TÜZEL KİŞİ'";
                    degisken = true;

                }
                if (degisken == false)
                {
                    filtre = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON, m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m";
                }
                query = filtre;
                griddoldur();
                textBox15.Text = "";
                maskedTextBox3.Text = "";
                textBox10.Text = "";
                textBox13.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                richTextBox2.Text = "";
                textBox8.Text = "";
                textBox12.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                panel5.Visible = false;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label30.Text = "FİRMA ADI";
                panel5.Visible = true;
                panel2.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label30.Text = "ADI SOYADI";
                panel2.Visible = true;
                panel5.Visible = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
              
                    bool degisken = false;
                    string filtre = "update   Müşteri set ";
                   
                  
                    if (string.IsNullOrEmpty(textBox15.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " AdSoyad='" + textBox15.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Email='" + textBox3.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Telefon='" + maskedTextBox1.Text.Replace(" ", "") + "'";
                        degisken = true;
                    }
                    if (maskedTextBox2.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " IBAN='" + maskedTextBox2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox13.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Fax='" + textBox13.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " İl='" + textBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " İlçe='" + textBox10.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Mahalle='" + textBox8.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " Adres='" + richTextBox2.Text + "'";
                        degisken = true;
                    }
                if (radioButton1.Checked == true)
                {
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " VergiNo='" + textBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " VergiDairesi='" + textBox7.Text + "'";
                        degisken = true;
                    }
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " KişiTip='TÜZEL KİŞİ'";
                }
                if (radioButton2.Checked == true)
                {
                    if (maskedTextBox3.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " TcKimlik='" + maskedTextBox3.Text + "'";
                        degisken = true;
                    }

                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " KişiTip='GERÇEK KİŞİ'";
                }

                    filtre += "where ID='"+dataGridView1.Rows[secilen].Cells[0].Value.ToString()+"'";
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan.baglan());
                    komutkaydet.ExecuteNonQuery();
                    baglan.baglan().Close();
                    MessageBox.Show("İŞLEM BAŞARILI");
                    griddoldur();
                    textBox15.Text = "";
                    maskedTextBox3.Text = "";
                    textBox10.Text = "";
                    textBox13.Text = "";
                    textBox7.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                    richTextBox2.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
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

        private void button6_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            button5.Visible = true;
            button11.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON,m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m ";
            griddoldur();
        }

        private void MÜŞTERİ_DÜZENLE_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }
    }
}
