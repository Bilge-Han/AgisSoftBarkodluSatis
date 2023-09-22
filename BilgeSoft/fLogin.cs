using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lisans;


namespace BilgeSoft
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }
        private void GirisYap()
        {
            if (tKullaniciAdi.Text != "" && tSifre.Text != "")
            {
                try
                {
                    using (var db = new BarkodDbEntities())
                    {
                        if (db.Kullanici.Any())
                        {
                            var bak = db.Kullanici.Where(x => x.KullaniciAd == tKullaniciAdi.Text && x.Sifre == tSifre.Text).FirstOrDefault();
                            if (bak != null)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                Kontrol kontrol = new Kontrol();
                                // Lisans Kontrolü
                                if (kontrol.KontrolYap())
                                {
                                    fBaslangic f = new fBaslangic();
                                    f.bSatisIslemi.Enabled = (bool)bak.Satis;
                                    f.bGenelRapor.Enabled = (bool)bak.Rapor;
                                    f.bStok.Enabled = (bool)bak.Stok;
                                    f.bUrunGiris.Enabled = (bool)bak.UrunGiris;
                                    f.bAyarlar.Enabled = (bool)bak.Ayarlar;
                                    f.bFiyatGuncelle.Enabled = (bool)bak.FiyatGuncelle;
                                    f.bYedekleme.Enabled = (bool)bak.Yedekleme;
                                    f.lKullanici.Text = bak.AdSoyad;
                                    var isYeri = db.Sabit.FirstOrDefault();
                                    f.lIsYeri.Text = isYeri.Unvan;
                                    f.Show();
                                    this.Hide();
                                }
                                Cursor.Current = Cursors.Default;
                            }
                            else
                            {
                                MessageBox.Show("Hatalı Giriş");
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            Kullanici kullanici = new Kullanici();
                            kullanici.AdSoyad = "admin";
                            kullanici.Telefon = "admin";
                            kullanici.EPosta = "admin";
                            kullanici.KullaniciAd = "admin";
                            kullanici.Sifre = "admin";
                            kullanici.Satis = true;
                            kullanici.Rapor = true;
                            kullanici.Stok = true;
                            kullanici.UrunGiris = true;
                            kullanici.Ayarlar = true;
                            kullanici.FiyatGuncelle = true;
                            kullanici.Yedekleme = true;
                            db.Kullanici.Add(kullanici);
                            db.SaveChanges();

                            var bak = db.Kullanici.Where(x => x.KullaniciAd == tKullaniciAdi.Text && x.Sifre == tSifre.Text).FirstOrDefault();
                            fBaslangic f = new fBaslangic();
                            f.bSatisIslemi.Enabled = (bool)bak.Satis;
                            f.bGenelRapor.Enabled = (bool)bak.Rapor;
                            f.bStok.Enabled = (bool)bak.Stok;
                            f.bUrunGiris.Enabled = (bool)bak.UrunGiris;
                            f.bAyarlar.Enabled = (bool)bak.Ayarlar;
                            f.bFiyatGuncelle.Enabled = (bool)bak.FiyatGuncelle;
                            f.bYedekleme.Enabled = (bool)bak.Yedekleme;
                            f.lKullanici.Text = bak.AdSoyad;
                            var isYeri = db.Sabit.FirstOrDefault();
                            f.lIsYeri.Text = isYeri.Unvan;
                            f.Show();
                            this.Hide();
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void bGiris_Click(object sender, EventArgs e)
        {
            GirisYap();  
        }

        private void fLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                GirisYap();
            }
        }

        private void bLisans_Click(object sender, EventArgs e)
        {
            Lic license = new Lic();

        }
    }
}
