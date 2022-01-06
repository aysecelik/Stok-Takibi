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
    public partial class TEDARİKÇİ_EKLE : Form
    {
        public TEDARİKÇİ_EKLE()
        {
            InitializeComponent();
        }
        baglanti baglan = new baglanti();
        private void button11_Click(object sender, EventArgs e)
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
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Email ";
                    values += "'" + textBox3.Text + "'";
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
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " VergiNo ";
                    values += "'" + textBox1.Text + "'";
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
    }
}
