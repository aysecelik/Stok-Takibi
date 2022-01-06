using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İşbirSünger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mÜŞTERİEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MÜŞTERİ_EKLE fr= new MÜŞTERİ_EKLE();
            fr.Show();
        }

        private void dOSYADANYÜKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOSYADAN_MÜŞTERİ_YÜKLE fr = new DOSYADAN_MÜŞTERİ_YÜKLE();
            fr.Show();
        }

        private void mÜŞTERİSİLMEGÜNCELLEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MÜŞTERİ_DÜZENLE fr = new MÜŞTERİ_DÜZENLE();
            fr.Show();
        }

        private void mÜŞTERİLİSTELEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MÜŞTERİ_LİSTESİ fr = new MÜŞTERİ_LİSTESİ();
            fr.Show();
        }

        private void tEDARİKÇİEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TEDARİKÇİ_EKLE fr = new TEDARİKÇİ_EKLE();
            fr.Show();
        }

        private void dOSYADANYÜKLEToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DOSYADAN_TEDARİKÇİ_YÜKLE fr = new DOSYADAN_TEDARİKÇİ_YÜKLE();
            fr.Show();
        }

        private void tEDARİKÇİSİLMEGÜNCELLEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TEDARİKÇİ_DÜZENLE fr = new TEDARİKÇİ_DÜZENLE();
            fr.Show();
        }

        private void tEDARİKÇİLİSTELEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TEDARİKÇİ_LİSTESİ fr = new TEDARİKÇİ_LİSTESİ();
            fr.Show();
        }

        private void üRÜNEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_EKLE fr = new ÜRÜN_EKLE();
            fr.Show();
        }

        private void dOSYADANYÜKLEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DOSYADAN_ÜRÜN_EKLE fr = new DOSYADAN_ÜRÜN_EKLE();
            fr.Show();
        }

        private void üRÜNSİLMEGÜNCELLEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_DÜZENLE fr= new ÜRÜN_DÜZENLE();
            fr.Show();

        }

        private void üRÜNLİSTELEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_LİSTESİ fr= new ÜRÜN_LİSTESİ();
            fr.Show();
        }

        private void üRÜNKULLANIMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_KULLANIMI fr = new ÜRÜN_KULLANIMI();
            fr.Show();
        }

        private void üRÜNKULLANIMIGÜNCELLESİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_KULLANIMI_DÜZENLE fr = new ÜRÜN_KULLANIMI_DÜZENLE();
            fr.Show();

        }

        private void üRÜNSATIŞİŞLEMİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_SATIŞI fr = new ÜRÜN_SATIŞI();
            fr.Show();
        }

        private void üRÜNSATIŞGÜNCELLESİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_SATIŞ_DÜZENLEME fr = new ÜRÜN_SATIŞ_DÜZENLEME();
            fr.Show();
        }

        private void üRÜNMİKTAREKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_MİKTAR_EKLE fr = new ÜRÜN_MİKTAR_EKLE();
            fr.Show();
        }

        private void üRÜNMİKTARGÜNCELLESİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÜRÜN_MİKTAR_DÜZENLEME fr = new ÜRÜN_MİKTAR_DÜZENLEME();
            fr.Show();
        }

        private void gÜNLÜKMİKTARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GÜNLÜK_MİKTAR fr = new GÜNLÜK_MİKTAR();
            fr.Show();
        }

        private void aYLIKSTOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AYLIK_STOK fr = new AYLIK_STOK();
            fr.Show();
        }

        private void üRÜNMİKTARLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
