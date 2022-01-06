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
    public partial class ÜRÜN_LİSTESİ : Form
    {
        public ÜRÜN_LİSTESİ()
        {
            InitializeComponent();
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

        private void button6_Click(object sender, EventArgs e)
        {
            //ARAMA
            panel7.Visible = true;
            panel5.Visible = true;
            panel2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM', ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t full outer join Ürünler ü on ü.Tedarikçi=t.ID";
            griddoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ,ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t full outer join Ürünler ü on ü.Tedarikçi=t.ID where ü.ÜrünAdı='" + textBox6.Text + "'";
            griddoldur();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 2)
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
            else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
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
                    textBox18.Text = oku3[5].ToString();

                    comboBox9.Text = oku3[7].ToString();
                    comboBox1.Text = oku3[9].ToString();
                    decimal tutar = (decimal)oku3[6];

                    string s = (tutar).ToString();
                    string[] parts = s.Split(',');
                    int i1 = Convert.ToInt32(parts[0]);
                    int i2 = Convert.ToInt32(parts[1]);
                    textBox14.Text = i1.ToString();
                    textBox11.Text = i2.ToString();
                    decimal tutar2 = (decimal)oku3[8];

                    string s2 = (tutar2).ToString();
                    string[] parts2 = s.Split(',');
                    int i12 = Convert.ToInt32(parts2[0]);
                    int i22 = Convert.ToInt32(parts2[1]);
                    textBox16.Text = i12.ToString();
                    textBox12.Text = i22.ToString();

                    dateTimePicker7.Value = Convert.ToDateTime(oku3[10]);

                }
                baglan.baglan().Close();
                panel7.Visible = true;
                panel5.Visible = true;


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pdf açılma
            panel7.Visible = true;
            panel5.Visible = true;
            panel1.Visible = true;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            //excel
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "Excel Dosyaları";
            save.DefaultExt = "xlsx";
            save.Filter = "xlsx Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar(*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();

                //Adding the Columns

                for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
                {
                    dt.Columns.Add(dataGridView1.Columns[i].HeaderText);
                }
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < dataGridView1.ColumnCount - 2; j++)
                    {
                        dt.Rows[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }




                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "ÜRÜNLER");
                    wb.SaveAs(save.FileName);
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount>1) { 
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
                for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount - 2; j++)
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
                    title = new Paragraph(textBox21.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox22.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox10.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    pdfDoc.Add(pdfTable);
                    text = new Paragraph("NOT: " + richTextBox3.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel7.Visible = false;
            }
        }
            else
            {
                MessageBox.Show("PDF OLUŞTURMAK İÇİN ÖNCELİKLE ARAMA İŞLEMİ İLE TABLOYU OLUŞTURUNUZ.");
            }
        }

        private void ÜRÜN_LİSTESİ_Load(object sender, EventArgs e)
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
                comboBox4.Items.Clear();
                komut = new SqlCommand("Select Tedarikçi,ID from Tedarikçiler order by Tedarikçi", baglan.baglan());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox4.Items.Add(oku[0].ToString());
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
                string filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar-ü.Harcanan KALAN,ü.Birim 'BİRİM' ,ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t full outer join Ürünler ü on ü.Tedarikçi=t.ID where ";

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
                    filtre += " ü.GelişFiyatı between '" +Convert.ToDecimal(textBox5.Text) + "' and '" + Convert.ToDecimal(textBox4.Text) + "'";
                    degisken = true;
                }
                if ((string.IsNullOrEmpty(textBox9.Text) == false && string.IsNullOrEmpty(textBox8.Text) == false) && (radioButton1.Checked==true || radioButton2.Checked==true || radioButton3.Checked==true))
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
                    filtre += " ü.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd HH:mm:ss") + "'";

                    degisken = true;
                }
                if (degisken == false)
                {
                    filtre = "Select ü.ID,ü.ÜrünTürü 'ÜRÜN TÜRÜ',ü.ÜrünKodu 'ÜRÜN KODU',ü.ÜrünAdı 'ÜRÜN ADI',ü.StokKodu 'STOK KODU', ü.StokAdı 'STOK ADI',t.ID 'TEDARİKÇİ ID',t.Tedarikçi 'TEDARİKÇİ',ü.Miktar 'MİKTAR',ü.Harcanan HARCANAN, ü.Miktar - ü.Harcanan KALAN,ü.Birim 'BİRİM', ü.GelişFiyatı 'ÜRÜN FİYATI',ü.Tarih 'TARİH' from Tedarikçiler t full outer join Ürünler ü on ü.Tedarikçi=t.ID";
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          
        }
    }
}
