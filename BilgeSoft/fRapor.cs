using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeSoft
{
    public partial class fRapor : Form
    {
        public fRapor()
        {
            InitializeComponent();
        }

        public void bGoster_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime baslangic = DateTime.Parse(dtBaslangic.Value.ToShortDateString());
            DateTime bitis = DateTime.Parse(dtBitis.Value.ToShortDateString());
            bitis = bitis.AddDays(1);
            using (var db = new BarkodDbEntities())
            {
                if (rbTumu.Checked)  //Tümünü Getir
                {
                    db.IslemOzet.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).OrderByDescending(x => x.Tarih).Load();
                    var islemOzet = db.IslemOzet.Local.ToBindingList();
                    gridListe.DataSource = islemOzet;

                    //Satışların kart ve nakit toplamları
                    tSatisNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Nakit)).ToString("C2");
                    tSatisKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Kart)).ToString("C2");

                    //İadelerin kart ve nakit toplamları
                    tIadeNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Nakit)).ToString("C2");
                    tIadeKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Kart)).ToString("C2");

                    //Gelirlerin kart ve nakit toplamları
                    tGelirNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Nakit)).ToString("C2");
                    tGelirKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Kart)).ToString("C2");

                    //Giderlerin kart ve nakit toplamları
                    tGiderNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Nakit)).ToString("C2");
                    tGiderKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Kart)).ToString("C2");

                    //Kdv toplamı hesabı, !(Burada islem özete kdvtutari eklemedğim için satıştan çekiyorum ilerde tabloları düzenlerken burayı da halledebilirsni)!
                    db.Satis.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).Load();
                    var satisTablosu = db.Satis.Local.ToBindingList();
                    double kdvTutariSatis = İslemler.DoubleYap(satisTablosu.Where(x => x.Iade == false).Sum(x => x.KdvTutari).ToString());
                    double kdvTutariIade = İslemler.DoubleYap(satisTablosu.Where(x => x.Iade == true).Sum(x => x.KdvTutari).ToString());
                    tKdvToplam.Text = (kdvTutariSatis - kdvTutariIade).ToString("C2");
                }
                else if (rbSatis.Checked) //Satışlar
                {
                    db.IslemOzet.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Iade == false && x.Gelir == false && x.Gider == false).Load();
                    var islemOzet = db.IslemOzet.Local.ToBindingList();
                    gridListe.DataSource = islemOzet;
                }
                else if (rbIade.Checked) //İade
                {
                    db.IslemOzet.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Iade == true).Load();
                    //var islemOzet = db.IslemOzet.Local.ToBindingList();
                    gridListe.DataSource = db.IslemOzet.Local.ToBindingList();
                }
                else if (rbGelir.Checked) //Gelir
                {
                    db.IslemOzet.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gelir == true).Load();
                    //var islemOzet = db.IslemOzet.Local.ToBindingList();
                    gridListe.DataSource = db.IslemOzet.Local.ToBindingList();
                }
                else if (rbGider.Checked) //Gider
                {
                    db.IslemOzet.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gider == true).Load();
                    //var islemOzet = db.IslemOzet.Local.ToBindingList();
                    gridListe.DataSource = db.IslemOzet.Local.ToBindingList();
                }
            }
            İslemler.GridDuzenle(gridListe);
            Cursor.Current = Cursors.Default;
        }

        private void fRapor_Load(object sender, EventArgs e)
        {
            dtBaslangic.Value = DateTime.Now;
            dtBitis.Value = DateTime.Now;
            guna2GradientPanel2.BorderRadius = 30;
            bGoster.BorderRadius = 15;
            bGelirEkle.Text = "";
            bGiderEkle.Text = "";
            rbTumu.Checked=true;
            tKartKomisyon.Text = "%"+İslemler.KartKomisyon().ToString();
            bGoster_Click(null, null);
        }

        private void gridListe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex==2||e.ColumnIndex==6||e.ColumnIndex==7)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Evet" : "Hayır";
                    e.FormattingApplied = true;
                }
            }
        }

        private void bGelirEkle_Click(object sender, EventArgs e)
        {
            fGelirGider f = new fGelirGider();
            f.gelirgider = "GELİR";
            f.kullanici = lKullanici.Text;
            f.ShowDialog();
        }

        private void bGiderEkle_Click(object sender, EventArgs e)
        {
            fGelirGider f = new fGelirGider();
            f.gelirgider = "GİDER";
            f.kullanici = lKullanici.Text;
            f.ShowDialog();
        }

        private void detayGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListe.Rows.Count>0)
            {
                int islemNo = Convert.ToInt32(gridListe.CurrentRow.Cells["IslemNo"].Value.ToString());
                if (islemNo!=0)
                {
                    fDetayGoster f = new fDetayGoster();
                    f.islemNo = islemNo;
                    f.ShowDialog();
                }
            }
        }

        private void bRaporAl_Click(object sender, EventArgs e)
        {
            Raporlar.Baslik = "GENEL RAPOR";
            Raporlar.TarihBaslangic = dtBaslangic.Value.ToShortDateString();
            Raporlar.TarihBitis = dtBitis.Value.ToShortDateString();
            Raporlar.SatisNakit = tSatisNakit.Text;
            Raporlar.SatisKart=tSatisKart.Text;
            Raporlar.IadeNakit = tIadeNakit.Text;
            Raporlar.IadeKart = tIadeKart.Text;
            Raporlar.GelirNakit = tGelirNakit.Text;
            Raporlar.GelirKart = tGelirKart.Text;
            Raporlar.GiderNakit = tGiderNakit.Text;
            Raporlar.GiderKart = tGiderKart.Text;
            Raporlar.KdvToplam = tKdvToplam.Text;
            Raporlar.KartKomisyon = tKartKomisyon.Text;
            Raporlar.RaporSayfasiRaporu(gridListe);
        }
    }
}
