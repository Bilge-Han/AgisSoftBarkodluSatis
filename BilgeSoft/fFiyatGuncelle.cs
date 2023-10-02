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
    public partial class fFiyatGuncelle : Form
    {
        public fFiyatGuncelle()
        {
            InitializeComponent();
        }

        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                using (var db=new BarkodDbEntities())
                {
                    if (db.Urun.Any(x=>x.Barkod==tBarkod.Text.Trim()))
                    {
                        var getir = db.Urun.Where(x => x.Barkod == tBarkod.Text).SingleOrDefault();
                        lBarkod.Text = getir.Barkod;
                        lUrunAdi.Text = getir.UrunAd;
                        double mevcutFiyat =Convert.ToDouble(getir.SatisFiyat);
                        lMevcutFiyat.Text = mevcutFiyat.ToString("C2");
                    }
                    else
                    {
                        MessageBox.Show("Ürün Kayıtlı Değil");
                    }
                }
            }
        }

        private void bYeniFiyat_Click(object sender, EventArgs e)
        {
            if (tYeniFiyat.Text!=""&&lBarkod.Text!="")
            {
                using (var db = new BarkodDbEntities())
                {
                    var guncelle = db.Urun.Where(x => x.Barkod == lBarkod.Text).SingleOrDefault();
                    guncelle.SatisFiyat = İslemler.DoubleYap(tYeniFiyat.Text);
                    int kdvOrani = Convert.ToInt16(guncelle.KdvOrani);
                    guncelle.KdvTutari = Math.Round(İslemler.DoubleYap(tYeniFiyat.Text) * kdvOrani / 100, 2);
                    db.SaveChanges();
                    MessageBox.Show("Fiyat Güncellenmiştir.");
                    Temizle();
                }
            }
            else
            {
                MessageBox.Show("Ürün Okutunuz");
                tBarkod.Focus();
            }
        }
        private void Temizle()
        {
            tBarkod.Clear();
            lBarkod.Text = "";
            lUrunAdi.Text = "";
            lMevcutFiyat.Text = "";
            tYeniFiyat.Clear();
            tBarkod.Focus();
        }
        private void fFiyatGuncelle_Load(object sender, EventArgs e)
        {
            Temizle();
        }

        private void lStandart5_Click(object sender, EventArgs e)
        {

        }
    }
}
