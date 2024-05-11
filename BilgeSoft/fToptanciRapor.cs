using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeSoft
{
    public partial class fToptanciRapor : Form
    {

        BarkodDbEntities _db = new BarkodDbEntities();
        public fToptanciRapor()
        {
            InitializeComponent();
        }
        private void fToptanciRapor_Load(object sender, EventArgs e)
        {
            dateBaslangic.Enabled = false;
            dateBitis.Enabled = false;
            gridListeToptanci.DataSource = _db.Toptancilar.OrderByDescending(a => a.FirmaAdi).Take(20).ToList();
            gridListeToptanci.Columns["YetkiliAdSoyad"].Visible = false;
            gridListeToptanci.Columns["Telefon"].Visible = false;
            gridListeToptanci.Columns["Email"].Visible = false;
            gridListeToptanci.Columns["Adres"].Visible = false;
            gridListeToptanci.Columns["VergiNumarasi"].Visible = false;
            gridListeToptanci.Columns["Alinacak"].Visible = false;
            gridListeToptanci.Columns["Verilecek"].Visible = false;
            İslemler.GridDuzenle(gridListeToptanci);
        }
        private void tMusteriAra_TextChanged(object sender, EventArgs e)
        {
            if (tToptanciAra.Text != "")
            {
                string toptanciAd = tToptanciAra.Text;
                var musteriler = _db.Toptancilar.Where(a => a.FirmaAdi.Contains(toptanciAd)).ToList();
                gridListeToptanci.DataSource = musteriler;
                //gridListeMusteri.Columns["MusteriID"].Visible = false;
                gridListeToptanci.Columns["YetkiliAdSoyad"].Visible = false;
                gridListeToptanci.Columns["Telefon"].Visible = false;
                gridListeToptanci.Columns["Email"].Visible = false;
                gridListeToptanci.Columns["Adres"].Visible = false;
                gridListeToptanci.Columns["VergiNumarasi"].Visible = false;
                gridListeToptanci.Columns["Alinacak"].Visible = false;
                gridListeToptanci.Columns["Verilecek"].Visible = false;
                İslemler.GridDuzenle(gridListeToptanci);
            }
        }

        private void bGoster_Click(object sender, EventArgs e)
        {
            gridRaporToptanci.DataSource = null;
            int firmaId = Convert.ToInt32(gridListeToptanci.CurrentRow.Cells["FirmaID"].Value.ToString());
            Cursor.Current = Cursors.WaitCursor;
            using (var db = new BarkodDbEntities())
            {
                if (rdTarihSec.Checked)
                {
                    if (db.Toptancilar.Any(x => x.FirmaID == firmaId))
                    {
                        // ALINAN VERİLENİ TARİHE GÖRE AYARLA !!!!!!!
                        DateTime baslangic = DateTime.Parse(dateBaslangic.Value.ToShortDateString());
                        DateTime bitis = DateTime.Parse(dateBitis.Value.ToShortDateString());
                        bitis = bitis.AddDays(1);
                        double alinacak = İslemler.DoubleYap(db.Toptancilar.Where(x => x.FirmaID == firmaId).Sum(x => x.Alinacak).ToString());
                        double verilecek = İslemler.DoubleYap(db.Toptancilar.Where(x => x.FirmaID == firmaId).Sum(x => x.Verilecek).ToString());
                        tAlinacak.Text = alinacak.ToString("C2");
                        tVerilecek.Text = verilecek.ToString("C2");
                        tGenelHesap.Text = (alinacak - verilecek).ToString("C2");

                        gridRaporToptanci.DataSource = db.StokHareket.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).Select(s => new { s.Barkod, s.UrunAd, s.Birim, s.Miktar, s.UrunGrup, s.ToptancıID, s.Tarih })
                       .Where(x => x.ToptancıID == firmaId).ToList();

                        İslemler.GridDuzenle(gridRaporToptanci);
                    }
                }
                else
                {
                    if (db.Toptancilar.Any(x => x.FirmaID == firmaId))
                    {
                        DateTime baslangic = DateTime.Parse(dateBaslangic.Value.ToShortDateString());
                        DateTime bitis = DateTime.Parse(dateBitis.Value.ToShortDateString());
                        bitis = bitis.AddDays(1);
                        double alinacak = İslemler.DoubleYap(db.Toptancilar.Where(x => x.FirmaID == firmaId).Sum(x => x.Alinacak).ToString());
                        double verilecek = İslemler.DoubleYap(db.Toptancilar.Where(x => x.FirmaID == firmaId).Sum(x => x.Verilecek).ToString());
                        tAlinacak.Text = alinacak.ToString("C2");
                        tVerilecek.Text = verilecek.ToString("C2");
                        tGenelHesap.Text = (alinacak - verilecek).ToString("C2");

                        gridRaporToptanci.DataSource = db.StokHareket.Select(s => new { s.Barkod, s.UrunAd, s.Birim, s.Miktar, s.UrunGrup, s.ToptancıID, s.Tarih })
                       .Where(x => x.ToptancıID == firmaId).ToList();

                        İslemler.GridDuzenle(gridRaporToptanci);
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }
        private void rdTarihSec_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTarihSec.Checked != false)
            {
                dateBaslangic.Enabled = true;
                dateBitis.Enabled = true;
            }
        }
    }
}
