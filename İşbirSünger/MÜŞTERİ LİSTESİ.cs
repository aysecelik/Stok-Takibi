using iTextSharp.text;
using iTextSharp.text.pdf;
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
using ClosedXML.Excel;

namespace İşbirSünger
{
    public partial class MÜŞTERİ_LİSTESİ : Form
    {
        public MÜŞTERİ_LİSTESİ()
        {
            InitializeComponent();
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
            da.Fill(ds, "MÜŞTERİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.baglan().Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "GÖRÜNTÜLE";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);

        }
        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON,m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m ";
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
            button5.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "Excel Dosyaları";
            save.DefaultExt = "xlsx";
            save.Filter = "xlsx Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar(*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();

                //Adding the Columns
              
                for (int i = 0; i < dataGridView1.ColumnCount-1; i++)
                {
                    dt.Columns.Add(dataGridView1.Columns[i].HeaderText);
                }
               
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < dataGridView1.ColumnCount-1; j++)
                    {
                        dt.Rows[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                



                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "MÜŞTERİLER");
                    wb.SaveAs(save.FileName);
                }
            }

          


        }
       
        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select m.ID,m.AdSoyad 'MÜŞTERİ',m.Telefon TELEFON,m.Email 'E-POSTA',m.Fax 'FAX',m.Adres 'ADRES' from MÜŞTERİ m where m.AdSoyad='" + textBox6.Text + "'";
            griddoldur();
        }
        int secilen;
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
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
                    button5.Visible = false;


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

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.OverwritePrompt = false;
                save.Title = "PDF Dosyaları";
                save.DefaultExt = "pdf";
                save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 1);

                    // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                    pdfTable.SpacingBefore = 20f;
                    pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                    pdfTable.WidthPercentage = 80; // hücre genişliği
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                    pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                    for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                    {



                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable.AddCell(cell);

                    }
                    try
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                            {
                                pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontTitle));

                            }
                        }


                    }
                    catch (NullReferenceException)
                    {
                    }
                    using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                    {

                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);// sayfa boyutu.
                        PdfWriter.GetInstance(pdfDoc, stream);
                        iTextSharp.text.Font titleFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 20, iTextSharp.text.Font.NORMAL);
                        iTextSharp.text.Font regularFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 15, iTextSharp.text.Font.NORMAL);
                        Paragraph title;
                        Paragraph text;
                        title = new Paragraph(textBox1.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Open();
                        pdfDoc.Add(title);
                        title = new Paragraph(textBox4.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);
                        title = new Paragraph(textBox2.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);

                        pdfDoc.Add(pdfTable);
                        text = new Paragraph("NOT: " + richTextBox1.Text, regularFont);
                        pdfDoc.Add(text);
                        pdfDoc.Close();
                        stream.Close();
                    }
                    panel1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("PDF OLUŞTURMAK İÇİN ÖNCELİKLE ARAMA İŞLEMİ İLE TABLOYU OLUŞTURUNUZ.");
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
                }
                if (radioButton1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.KişiTip= " + "'TÜZEL KİŞİ'";
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

        private void MÜŞTERİ_LİSTESİ_Load(object sender, EventArgs e)
        {

        }
    }
}
