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
    public partial class TEDARİKÇİ_DÜZENLE : Form
    {
        public TEDARİKÇİ_DÜZENLE()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.Telefon TELEFON,t.Email 'E-POSTA',t.Fax 'FAX',t.IBAN 'IBAN',t.Adres 'ADRES',t.VergiDairesi 'VERGİ DAİRESİ',t.VergiNo 'VERGİ NUMARASI' from Tedarikçiler t where t.Tedarikçi='" + textBox6.Text + "'";
            griddoldur();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.Telefon TELEFON,t.Email 'E-POSTA',t.Fax 'FAX',t.IBAN 'IBAN',t.Adres 'ADRES',t.VergiDairesi 'VERGİ DAİRESİ',t.VergiNo 'VERGİ NUMARASI' from Tedarikçiler t";
            griddoldur();
        }
        baglanti baglan = new baglanti();
        SqlDataAdapter da;
        SqlCommandBuilder cmdb;
        DataSet ds;
        String query;
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            da = new SqlDataAdapter(query, baglan.baglan());
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "TEDARİKÇİ");
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

        private void button6_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            button5.Visible = true;
            button11.Visible = false;
       
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = false;
                string filtre = "Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.Telefon TELEFON,t.Email 'E-POSTA',t.Fax 'FAX',t.IBAN 'IBAN',t.Adres 'ADRES',t.VergiDairesi 'VERGİ DAİRESİ',t.VergiNo 'VERGİ NUMARASI' from Tedarikçiler t where ";

                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tedarikçi= " + "'" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Email= " + "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Telefon= " + "'" + maskedTextBox1.Text.Replace(" ", "") + "'";
                    degisken = true;
                }
                if (maskedTextBox2.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.IBAN= " + "'" + maskedTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.FAX= " + "'" + textBox13.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Adres= " + "'" + richTextBox2.Text + "'";
                    degisken = true;
                }
                if (degisken == false)
                {
                    filtre += "Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.Telefon TELEFON,t.Email 'E-POSTA',t.Fax 'FAX',t.IBAN 'IBAN',t.Adres 'ADRES',t.VergiDairesi 'VERGİ DAİRESİ',t.VergiNo 'VERGİ NUMARASI' from Tedarikçiler t ";
                }
                query = filtre;
                griddoldur();
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "update  Tedarikçiler set ";

              
                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " Tedarikçi='" + textBox15.Text + "'";
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
                    filtre += " FAX='" + textBox13.Text + "'";
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
                filtre += "where ID='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'";
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan.baglan());
                komutkaydet.ExecuteNonQuery();
                baglan.baglan().Close();
                MessageBox.Show("İŞLEM BAŞARILI");
                griddoldur();
                textBox15.Text = "";
                textBox13.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                richTextBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
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
        int secilen;
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 2)
                {
                    secilen = dataGridView1.CurrentCell.RowIndex;
                    komut = new SqlCommand("select * from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
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
                    button5.Visible = false;


                }
                else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "TEDARİKÇİ SİLME", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " İSİMLİ TEDARİKÇİ  TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "TEDARİKÇİ SİLME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                string sql = "DELETE  FROM Tedarikçiler WHERE ID=@id";
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

        private void TEDARİKÇİ_DÜZENLE_Load(object sender, EventArgs e)
        {

        }
    }
}
