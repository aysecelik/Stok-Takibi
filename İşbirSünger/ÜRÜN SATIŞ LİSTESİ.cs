using ClosedXML.Excel;
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

namespace İşbirSünger
{
    public partial class ÜRÜN_SATIŞ_LİSTESİ : Form
    {
        public ÜRÜN_SATIŞ_LİSTESİ()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ürün adına göre ara
            query = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.KullanımMiktarı 'KULLANILAN MİKTAR',ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ü.ÜrünAdı='" + textBox6.Text + "' and ÜrünHareketi='SATIŞ'";
            griddoldur();
        }
        String query;
        baglanti baglan = new baglanti();
        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.KullanımMiktarı 'KULLANILAN MİKTAR',ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ÜrünHareketi='SATIŞ'";
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Arama işlemi
            panel2.Visible = true;
            panel1.Visible = false;
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
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "GÖRÜNTÜLE";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
        

        }
        SqlCommand komut;
        private SqlDataAdapter da;
        private SqlCommandBuilder cmdb;
        private DataSet ds;
        int ürünid;
        decimal harcanan;
        decimal kalan;
        int secilen;
        decimal son;

        private void button4_Click(object sender, EventArgs e)
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

                for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                {
                    dt.Columns.Add(dataGridView1.Columns[i].HeaderText);
                }

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                secilen = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                ürünid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString());
                komut = new SqlCommand("Select ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.KullanımMiktarı 'KULLANILAN MİKTAR',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT', ü.Miktar, ü.Harcanan,ü.Harcanan-i.KullanımMiktarı from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan.baglan());
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {

                    comboBox5.Text = oku3[0].ToString();
                    textBox7.Text = oku3[1].ToString();
                    textBox14.Text = oku3[2].ToString();
                    textBox13.Text = oku3[3].ToString();
                    textBox12.Text = oku3[4].ToString();
                    richTextBox1.Text = oku3[7].ToString();

                    decimal tutar = (decimal)oku3[5];

                    string s = (tutar).ToString();
                    string[] parts = s.Split(',');
                    int i1 = Convert.ToInt32(parts[0]);
                    int i2 = Convert.ToInt32(parts[1]);
                    textBox11.Text = i1.ToString();
                    textBox10.Text = i2.ToString();
                    textBox1.Text = oku3[8].ToString();
                    textBox3.Text = oku3[10].ToString();
                    textBox4.Text = oku3[9].ToString();
                    harcanan = (decimal)oku3[10];


                    dateTimePicker4.Value = Convert.ToDateTime(oku3[6]);

                }
                baglan.baglan().Close();
                panel2.Visible = true;
                panel1.Visible = true;
                panel4.Visible = true;



            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel4.Visible = true;
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
                string filtre = "Select i.ID 'İŞLEM ID',ü.ID 'ÜRÜN ID',ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',i.KullanımMiktarı 'KULLANILAN MİKTAR', ü.Birim 'BİRİM',i.Tarih 'İŞLEM TARİHİ',i.[Not] 'NOT' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID where ÜrünHareketi='SATIŞ' ";


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
                        filtre += " i.KullanımMiktarı between '" + Convert.ToDecimal(textBox9.Text) + "' and '" + Convert.ToDecimal(textBox8.Text) + "'";
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
            textBox4.Text = "";
            textBox10.Text = "";
            dateTimePicker4.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }

        private void button11_Click(object sender, EventArgs e)
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
                        title = new Paragraph(textBox27.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Open();
                        pdfDoc.Add(title);
                        title = new Paragraph(textBox28.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);
                        title = new Paragraph(textBox22.Text, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);

                        pdfDoc.Add(pdfTable);
                        text = new Paragraph("NOT: " + richTextBox3.Text, regularFont);
                        pdfDoc.Add(text);
                        pdfDoc.Close();
                        stream.Close();
                    }
                    panel2.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("PDF OLUŞTURMAK İÇİN ÖNCELİKLE ARAMA İŞLEMİ İLE TABLOYU OLUŞTURUNUZ.");
            }
        }

        private void ÜRÜN_SATIŞ_LİSTESİ_Load(object sender, EventArgs e)
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
