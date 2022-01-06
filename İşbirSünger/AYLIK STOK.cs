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
    public partial class AYLIK_STOK : Form
    {
        public AYLIK_STOK()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
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

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dt.Columns.Add(dataGridView1.Columns[i].HeaderText);
                }
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dt.Rows[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }




                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "GÜNLÜK STOK DURUM");
                    wb.SaveAs(save.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 100; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount ; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var startDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                Boolean bayrak = true;
                komut = new SqlCommand("Select * from GünToplam where Gün<='" + endDate.ToString("yyyyMMdd") + "' and Gün>='"+startDate.ToString("yyyyMMdd")+"'", baglan.baglan());
                SqlDataReader oku4 = komut.ExecuteReader();
                while (oku4.Read())
                {
                    bayrak = false;
                }
                baglan.baglan().Close();
                if (bayrak == false)
                {
                    startDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    dataGridView1.Columns.Clear();
                    query = "Select MAX(ü.ID) 'ÜRÜN ID',MAX(ü.ÜrünTürü) 'ÜRÜN TÜRÜ',MAX(ü.ÜrünKodu) 'ÜRÜN KODU',MAX(ü.ÜrünAdı) 'ÜRÜN ADI',MAX(ü.StokKodu) 'STOK KODU', MAX(ü.StokAdı) 'STOK ADI',SUM(i.EklenenMiktar) 'GELEN',SUM(i.KullanımMiktarı) 'KULLANILAN',(MAX(g.Toplam)-Max(g.Harcanan)) KALAN,MAX(ü.Birim) 'BİRİM' from ÜrünHareketleri i  join Ürünler ü on i.Ürün=ü.ID  join GünToplam g on g.Ürün=ü.ID where i.Tarih>= '" + startDate.ToString("yyyyMMdd") + " 00:00:00' and i.Tarih<='" + endDate.ToString("yyyyMMdd") + " 23:59:59' and g.Gün<='" + endDate.ToString("yyyyMMdd") + "' and g.Gün>='"+ startDate.ToString("yyyyMMdd")+"' group by ü.ÜrünAdı";
                    da = new SqlDataAdapter(query, baglan.baglan());
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "AYLIK_STOK");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.baglan().Close();
                }

                else
                {
                    startDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1); while (bayrak == true)
                    {
                        startDate = startDate.AddMonths(-1);
                        endDate = endDate.AddMonths(-1);
                        komut = new SqlCommand("Select * from GünToplam where Gün<='" + endDate.ToString("yyyyMMdd") + "' and Gün>='" + startDate.ToString("yyyyMMdd") + "'", baglan.baglan());
                        SqlDataReader oku5 = komut.ExecuteReader();
                        while (oku5.Read())
                        {
                            bayrak = false;
                        }
                        baglan.baglan().Close();

                    }
                    dataGridView1.Columns.Clear();
                    query = "Select MAX(ü.ID) 'ÜRÜN ID',MAX(ü.ÜrünTürü) 'ÜRÜN TÜRÜ',MAX(ü.ÜrünKodu) 'ÜRÜN KODU',MAX(ü.ÜrünAdı) 'ÜRÜN ADI',MAX(ü.StokKodu) 'STOK KODU', MAX(ü.StokAdı) 'STOK ADI',SUM(i.EklenenMiktar) 'GELEN',SUM(i.KullanımMiktarı) 'KULLANILAN',(MAX(g.Toplam)-Max(g.Harcanan)) KALAN,MAX(ü.Birim) 'BİRİM' from ÜrünHareketleri i join Ürünler ü on i.Ürün=ü.ID join GünToplam g on g.Ürün=ü.ID where i.Tarih>= '" + startDate.ToString("yyyyMMdd") + " 00:00:00' and i.Tarih<='" + endDate.ToString("yyyyMMdd") + " 23:59:59' and g.Gün<='" + endDate.ToString("yyyyMMdd") + "' and g.Gün>='" + startDate.ToString("yyyyMMdd") + "' group by ü.ÜrünAdı";
                    da = new SqlDataAdapter(query, baglan.baglan());
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "AYLIK_STOK");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.baglan().Close();



                }
            }
            catch { }
        }
        SqlCommand komut;
        private SqlDataAdapter da;
        private SqlCommandBuilder cmdb;
        private DataSet ds;
        String query;
        baglanti baglan = new baglanti();
        private void AYLIK_STOK_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy";
        }
    }
}
