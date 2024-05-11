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
    public partial class fMusteriRapor : Form
    {

        BarkodDbEntities _db = new BarkodDbEntities();
        public fMusteriRapor()
        {
            InitializeComponent();
        }

        private void fMusteriRapor_Load(object sender, EventArgs e)
        {
            dateBaslangic.Enabled = false;
            dateBitis.Enabled = false;
            gridListeMusteri.DataSource = _db.Musteriler.OrderByDescending(a => a.MusteriAdSoyad).Take(20).ToList();
            gridListeMusteri.Columns["Telefon"].Visible = false;
            gridListeMusteri.Columns["Email"].Visible = false;
            gridListeMusteri.Columns["Adres"].Visible = false;
            gridListeMusteri.Columns["OzetNotlar"].Visible = false;
            gridListeMusteri.Columns["Alinacak"].Visible = false;
            gridListeMusteri.Columns["Odenen"].Visible = false;
            İslemler.GridDuzenle(gridListeMusteri);
        }
        private void tMusteriAra_TextChanged(object sender, EventArgs e)
        {
            if (tMusteriAra.Text != "")
            {
                string musteriAd = tMusteriAra.Text;
                var musteriler = _db.Musteriler.Where(a => a.MusteriAdSoyad.Contains(musteriAd)).ToList();
                gridListeMusteri.DataSource = musteriler;
                //gridListeMusteri.Columns["MusteriID"].Visible = false;
                gridListeMusteri.Columns["Telefon"].Visible = false;
                gridListeMusteri.Columns["Email"].Visible = false;
                gridListeMusteri.Columns["Adres"].Visible = false;
                gridListeMusteri.Columns["OzetNotlar"].Visible = false;
                gridListeMusteri.Columns["Alinacak"].Visible = false;
                gridListeMusteri.Columns["Odenen"].Visible = false;
                İslemler.GridDuzenle(gridListeMusteri);
            }
        }

        private void bGoster_Click(object sender, EventArgs e)
        {
            gridRaporMusteri.DataSource = null;
            int musteriId = Convert.ToInt32(gridListeMusteri.CurrentRow.Cells["MusteriID"].Value.ToString());
            Cursor.Current = Cursors.WaitCursor;
            using (var db = new BarkodDbEntities())
            {
                if (db.Musteriler.Any(x => x.MusteriID == musteriId))
                {
                    double alinacak = İslemler.DoubleYap(db.Musteriler.Where(x => x.MusteriID == musteriId).Sum(x => x.Alinacak).ToString());
                    double odenen = İslemler.DoubleYap(db.Musteriler.Where(x => x.MusteriID == musteriId).Sum(x => x.Odenen).ToString());
                    tAlinacak.Text = alinacak.ToString("C2");
                    tOdenen.Text = odenen.ToString("C2");
                    tGenelHesap.Text = (alinacak - odenen).ToString("C2");

                    gridRaporMusteri.DataSource = db.Satis.Select(s => new { s.IslemNo, s.MusteriID, s.UrunAd, s.UrunGrup, s.Miktar, s.Toplam,s.Tarih })
                   .Where(x => x.MusteriID == musteriId).ToList();

                    //db.IslemOzet.Where(x => x.MusteriID == musteriId).OrderByDescending(x => x.Tarih).Load();
                    //var islemOzet = db.IslemOzet.Local.ToBindingList();
                    //gridRaporMusteri.DataSource = islemOzet;
                    İslemler.GridDuzenle(gridRaporMusteri);
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
