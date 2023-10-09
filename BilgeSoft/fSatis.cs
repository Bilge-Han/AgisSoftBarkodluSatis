using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace BilgeSoft
{
    public partial class fSatis : Form
    {
        BarkodDbEntities _db = new BarkodDbEntities();
        public fSatis()
        {
            InitializeComponent();
        }
        private void fSatis_Load(object sender, EventArgs e)
        {
            HizliButonDoldur();
            tGenelToplam.Text = 0.ToString("C2");
            tOdenen.Text = 0.ToString("C2");
            tParaUstu.Text = 0.ToString("C2");
            b5.Text = 5.ToString("C2");
            b10.Text = 10.ToString("C2");
            b20.Text = 20.ToString("C2");
            b50.Text = 50.ToString("C2");
            b100.Text = 100.ToString("C2");
            b200.Text = 200.ToString("C2");

            var sabit = _db.Sabit.FirstOrDefault();
            chYazdirma.Checked = (bool)sabit.Yazici;
        }
        public void HizliButonDoldur()
        {
            var hizliUrun = _db.HizliButon.ToList();
            foreach (var item in hizliUrun)
            {
                Guna2Button bH = this.Controls.Find("bHizli" + item.Id, true).FirstOrDefault() as Guna2Button;
                if (bH != null)
                {
                    double fiyat = İslemler.DoubleYap(item.Fiyat.ToString());
                    bH.Text = item.UrunAd + "\n" + fiyat.ToString("C2");
                }
            }
        }
        private void HizliButonClick(object sender, EventArgs e)
        {
            Guna2Button bt = (Guna2Button)sender;
            int butonId = Convert.ToInt16(bt.Name.ToString().Substring(6, bt.Name.Length - 6));
            if (bt.Text.StartsWith("-"))
            {
                fHizliButonUrunEkle f = new fHizliButonUrunEkle();
                f.lButonId.Text = butonId.ToString();
                f.ShowDialog();
            }
            else
            {
                var urunBarkod = _db.HizliButon.Where(a => a.Id == butonId).Select(a => a.Barkod).FirstOrDefault();
                var urun = _db.Urun.Where(a => a.Barkod == urunBarkod).FirstOrDefault();
                UrunListele(urun, urunBarkod, Convert.ToDouble(tMiktar.Text));
                GenelToplamHesap();
            }

        }

        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod = tBarkod.Text.Trim();
                if (barkod.Length <= 2)
                {
                    tMiktar.Text = barkod;
                    tBarkod.Clear();
                    tBarkod.Focus();
                }
                else
                {
                    BarkodOkuma(barkod);
                }
            }
        }
        private void BarkodOkuma(string barkod)
        {
            //Eğer ürün varsa
            if (_db.Urun.Any(a => a.Barkod == barkod)) //Any true false dönüyor, barkod var mı ?
            {
                var urun = _db.Urun.Where(a => a.Barkod == barkod).FirstOrDefault(); //varsa ürünü al bu değişkene ata.
                UrunListele(urun, barkod, Convert.ToDouble(tMiktar.Text));
            }
            //Eğer ürün yoksa
            else
            {
                if (barkod.Length == 8 || barkod.Length == 13)
                {
                    //Gelen etiketteki verinin ilk 2 karakterini aldım
                    int onEk = Convert.ToInt16(barkod.Substring(0, 2));
                    //Bunu terazi kodu mu diye soruyorum.
                    if (_db.Terazi.Any(a => a.TeraziOnEk == onEk))
                    {
                        string teraziUrunNo = barkod.Substring(2, 5);
                        // Eğer terazi ürünü varsa
                        if (_db.Urun.Any(a => a.Barkod == teraziUrunNo))
                        {
                            var urunTerazi = _db.Urun.Where(a => a.Barkod == teraziUrunNo).FirstOrDefault();
                            double miktarKg = Convert.ToDouble(barkod.Substring(7, 5)) / 1000; //gramı kiloya çevirmek için
                            UrunListele(urunTerazi, teraziUrunNo, miktarKg);
                        }
                        // Eğer terazi ürünü yoksa
                        else
                        {
                            Console.Beep(900, 500);
                            MessageBox.Show("Kg Ürün Ekleme Sayfası");
                        }
                    }
                    else
                    {
                        Console.Beep(900, 500);
                        fUrunGiris f = new fUrunGiris();
                        f.tBarkod.Text = barkod;
                        f.ShowDialog();
                    }
                }
                else
                {
                    Console.Beep(900, 500);
                    MessageBox.Show("Geçersiz Barkod");
                }
            }
            gridSatisListesi.ClearSelection();
            GenelToplamHesap();
        }
        private void UrunListele(Urun urun, string barkod, double miktar)
        {
            int satirSayisi = gridSatisListesi.Rows.Count;
            //double miktar = Convert.ToDouble(tMiktar.Text);
            bool eklenmisMi = false;
            if (satirSayisi > 0)
            {
                for (int i = 0; i < satirSayisi; i++)
                {
                    if (gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        gridSatisListesi.Rows[i].Cells["Miktar"].Value = miktar + (double)gridSatisListesi.Rows[i].Cells["Miktar"].Value;
                        gridSatisListesi.Rows[i].Cells["Toplam"].Value = Math.Round((double)(gridSatisListesi.Rows[i].Cells["Fiyat"].Value) * (double)(gridSatisListesi.Rows[i].Cells["Miktar"].Value), 2);
                        double dblKdvTutari = (double)urun.KdvTutari;
                        gridSatisListesi.Rows[i].Cells["KdvTutari"].Value = Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * dblKdvTutari;
                        eklenmisMi = true;
                    }
                }
            }
            if (!eklenmisMi)
            {
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirSayisi].Cells["Barkod"].Value = barkod;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunAdi"].Value = urun.UrunAd;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunGrup"].Value = urun.UrunGrup;
                gridSatisListesi.Rows[satirSayisi].Cells["Birim"].Value = urun.Birim;
                gridSatisListesi.Rows[satirSayisi].Cells["Fiyat"].Value = urun.SatisFiyat;
                gridSatisListesi.Rows[satirSayisi].Cells["Miktar"].Value = miktar;
                gridSatisListesi.Rows[satirSayisi].Cells["Toplam"].Value = Math.Round((double)miktar * (double)urun.SatisFiyat, 2);
                gridSatisListesi.Rows[satirSayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
                gridSatisListesi.Rows[satirSayisi].Cells["Tarih"].Value = urun.Tarih;
                gridSatisListesi.Rows[satirSayisi].Cells["AlisFiyat"].Value = urun.AlisFiyat;

            }
        }
        private void GenelToplamHesap()
        {
            string eskiGenelToplam = tGenelToplam.Text;
            if (gridSatisListesi.Rows.Count >= 0)
            {
                double toplam = 0;
                for (int i = 0; i < gridSatisListesi.Rows.Count; i++)
                {
                    toplam += Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Toplam"].Value);
                }
                tGenelToplam.Text = toplam.ToString("C2");
                tMiktar.Text = "1";
                tBarkod.Clear();
                tBarkod.Focus();
                ParaUstuGuncelle(tOdenen.Text, tGenelToplam.Text);
            }
        }

        private void gridSatisListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridSatisListesi.Columns.Count - 1)
            {
                gridSatisListesi.Rows.Remove(gridSatisListesi.CurrentRow);
                gridSatisListesi.ClearSelection();
                GenelToplamHesap();
                tBarkod.Focus();
            }
        }

        private void bh_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Guna2Button b = (Guna2Button)sender;
                if (!b.Text.StartsWith("-"))
                {
                    int butonId = Convert.ToInt16(b.Name.ToString().Substring(6, b.Name.Length - 6));
                    ContextMenuStrip s = new ContextMenuStrip();
                    ToolStripMenuItem sil = new ToolStripMenuItem();
                    sil.Text = "Temizle - Buton No: " + butonId.ToString();
                    sil.Click += Sil_Click;
                    s.Items.Add(sil);
                    this.ContextMenuStrip = s;
                }
            }
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            int butonId = Convert.ToInt16(sender.ToString().Substring(19, sender.ToString().Length - 19));
            var guncelle = _db.HizliButon.Find(butonId);
            guncelle.Barkod = "-";
            guncelle.UrunAd = "-";
            guncelle.Fiyat = 0;
            _db.SaveChanges();
            double fiyat = 0;
            Guna2Button b = this.Controls.Find("bHizli" + butonId, true).FirstOrDefault() as Guna2Button;
            b.Text = "-" + "\n" + fiyat.ToString("C2");
        }

        private void bNx_Click(object sender, EventArgs e)
        {
            Guna2Button b = (Guna2Button)sender;
            if (b.Text == ",")
            {
                int virgul = tNumarator.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    tNumarator.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (tNumarator.Text.Length > 0)
                {
                    tNumarator.Text = tNumarator.Text.Substring(0, tNumarator.Text.Length - 1);
                }
            }
            else
            {
                tNumarator.Text += b.Text;
            }
        }

        private void bAdet_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                tMiktar.Text = tNumarator.Text;
                tNumarator.Clear();
                tBarkod.Clear();
                tBarkod.Focus();
            }
            else
            {
                Console.Beep(900, 500);
                MessageBox.Show("Lütfen Kaç Adet Olduğunu Giriniz.");
            }
        }

        private void bOdenen_Click(object sender, EventArgs e)
        {
            OdenenButonMiktar(tNumarator.Text);
            #region eski
            //double genelToplam = İslemler.DoubleYap(tGenelToplam.Text);
            //if (genelToplam!=0&&genelToplam>0)
            //{
            //    if (tNumarator.Text != "")
            //    {
            //        double odenenPara = İslemler.DoubleYap(tNumarator.Text);
            //        //double sonuc = odenenPara - İslemler.DoubleYap(tGenelToplam.Text);
            //        tOdenen.Text = odenenPara.ToString("C2");
            //        ParaUstuGuncelle(tNumarator.Text, tGenelToplam.Text);
            //        //ParaUstuHesap(sonuc);
            //    }
            //}
            //else
            //{
            //    tNumarator.Clear();
            //    MessageBox.Show("Şu anda bir satış bulunmuyor.");
            //}
            #endregion
        }


        private void OdenenHizli_Click(object sender, EventArgs e)
        {
            Guna2Button b = (Guna2Button)sender;
            OdenenButonMiktar(b.Text);
            #region eski
            //double genelToplam = İslemler.DoubleYap(tGenelToplam.Text);
            //if (genelToplam != 0 && genelToplam > 0)
            //{
            //    if (b.Text != "")
            //    {
            //        double odenenPara = İslemler.DoubleYap(b.Text);
            //        //double sonuc = odenenPara - İslemler.DoubleYap(tGenelToplam.Text);
            //        tOdenen.Text = odenenPara.ToString("C2");
            //        ParaUstuGuncelle(b.Text, tGenelToplam.Text);
            //        //ParaUstuHesap(sonuc);
            //    }
            //}
            //else
            //{
            //    Console.Beep(900, 500);
            //    MessageBox.Show("Şu anda bir satış bulunmuyor.");
            //}
            #endregion
        }
        private void OdenenButonMiktar(string odenenMiktar)
        {
            double genelToplam = İslemler.DoubleYap(tGenelToplam.Text);
            if (genelToplam != 0 && genelToplam > 0)
            {
                if (odenenMiktar != "")
                {
                    double odenenPara = İslemler.DoubleYap(odenenMiktar);
                    //double sonuc = odenenPara - İslemler.DoubleYap(tGenelToplam.Text);
                    tOdenen.Text = odenenPara.ToString("C2");
                    ParaUstuGuncelle(odenenMiktar, tGenelToplam.Text);
                    //ParaUstuHesap(sonuc);
                }
            }
            else
            {
                tNumarator.Clear();
                Console.Beep(900, 500);
                MessageBox.Show("Şu anda bir satış bulunmuyor.");
            }
        }
        private void ParaUstuHesap(double sonuc)
        {
            if (gridSatisListesi.Rows.Count > 0)
            {
                double odenen = İslemler.DoubleYap(tOdenen.Text);
                if (odenen != 0 && odenen > 0)
                {
                    if (sonuc >= 0)
                    {
                        tParaUstu.Text = sonuc.ToString("C2");
                        tNumarator.Clear();
                        tBarkod.Focus();
                    }
                    else
                    {
                        Console.Beep(900, 500);
                        MessageBox.Show("Yetersiz Tutar");
                        tNumarator.Clear();
                    }
                }
            }
            else
            {
                tOdenen.Text = 0.ToString("C2");
                tParaUstu.Text = 0.ToString("C2");
            }
        }

        private void ParaUstuGuncelle(string odenenText, string genelToplamText)
        {
            double guncelParaUstu = İslemler.DoubleYap(odenenText) - İslemler.DoubleYap(genelToplamText);
            ParaUstuHesap(guncelParaUstu);
        }
        private void bBarkod_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                if (tNumarator.Text.Length > 2 && tNumarator.Text.Length < 14)
                {
                    BarkodOkuma(tNumarator.Text);
                }
                else
                {
                    Console.Beep(500, 900);
                    MessageBox.Show("Geçersiz Barkod.");
                }
            }
            else
            {
                Console.Beep(900, 500);
                MessageBox.Show("Lütfen Bir Barkod Değeri Giriniz.");
            }
            tNumarator.Clear();
            gridSatisListesi.ClearSelection();
            GenelToplamHesap();
        }

        private void bDigerUrun_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                int satirSayisi = gridSatisListesi.Rows.Count;
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirSayisi].Cells["Barkod"].Value = "1111111111116";
                gridSatisListesi.Rows[satirSayisi].Cells["UrunAdi"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirSayisi].Cells["UrunGrup"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirSayisi].Cells["Birim"].Value = "Adet";
                gridSatisListesi.Rows[satirSayisi].Cells["Miktar"].Value = 1;
                gridSatisListesi.Rows[satirSayisi].Cells["AlisFiyat"].Value = 0;
                gridSatisListesi.Rows[satirSayisi].Cells["Fiyat"].Value = Convert.ToDouble(tNumarator.Text);
                gridSatisListesi.Rows[satirSayisi].Cells["KdvTutari"].Value = 0;
                gridSatisListesi.Rows[satirSayisi].Cells["Toplam"].Value = Convert.ToDouble(tNumarator.Text);
                tNumarator.Text = "";
                tBarkod.Focus();
                GenelToplamHesap();
            }
            else
            {
                Console.Beep(900, 500);
                MessageBox.Show("Lütfen Bir Değer Giriniz.");
            }
        }

        private void bIade_Click(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Checked = false;
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Checked = true;
                chSatisIadeIslemi.Text = "İade Yapılıyor";
            }
        }

        private void bTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        public void Temizle()
        {

            tMiktar.Text = "1";
            tBarkod.Clear();
            tGenelToplam.Text = 0.ToString("C2");
            tOdenen.Text = 0.ToString("C2");
            tParaUstu.Text = 0.ToString("C2");
            chSatisIadeIslemi.Checked = false;
            tNumarator.Text = "";
            gridSatisListesi.Rows.Clear();
            tBarkod.Focus();
        }

        public void SatisYap(string odemeSekli)
        {
            int satirSayisi = gridSatisListesi.Rows.Count;
            bool satisIade = chSatisIadeIslemi.Checked;
            double alisFiyatToplam = 0;
            if (satirSayisi > 0)
            {
                int? islemNo = _db.Islem.First().IslemNo; // 0 olabilir bu yüzden ? koyduk
                int? zNumara = _db.Zno.First().ZNumara;
                Satis satis = new Satis(); //Satış modeli oluşturdum.
                for (int i = 0; i < satirSayisi; i++)
                {
                    satis.IslemNo = islemNo;
                    satis.ZNumara = zNumara;
                    satis.UrunAd = gridSatisListesi.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.Barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.UrunGrup = gridSatisListesi.Rows[i].Cells["UrunGrup"].Value.ToString();
                    satis.Birim = gridSatisListesi.Rows[i].Cells["Birim"].Value.ToString();
                    // Alış fiyatta diğer ürün alınca alış fiyatı olmadığı için onu doldurmamız gerekiyor hata!!!!!!!!!!!!
                    satis.AlisFiyat = İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString()); //TL simgesi olduğundan convert değil
                    satis.SatisFiyat = İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Fiyat"].Value.ToString()); //parse yapmamız gerekti.
                    satis.Miktar = İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam = İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Toplam"].Value.ToString());
                    satis.KdvTutari = İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["KdvTutari"].Value.ToString()) *
                        İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemeSekli;
                    satis.Iade = satisIade;
                    satis.Tarih = DateTime.Now;
                    satis.Kullanici = lKullanici.Text;
                    _db.Satis.Add(satis); // bu modeli tabloya ekle
                    _db.SaveChanges();

                    // eğer iade değilse, yani satıldı ise stoktan azalt
                    if (!satisIade)
                    {
                        İslemler.StokAzalt(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(),
                            İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    // iade ise stoğa ekle
                    else
                    {
                        İslemler.StokArttır(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(),
                            İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    alisFiyatToplam += (İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString()) * İslemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                }

                IslemOzet islemOzet = new IslemOzet();
                islemOzet.IslemNo = islemNo;
                islemOzet.ZNumara = zNumara;
                islemOzet.Iade = satisIade;
                islemOzet.AlisFiyatToplam = alisFiyatToplam;
                islemOzet.Gelir = false;
                islemOzet.Gider = false;
                if (!satisIade)
                {
                    islemOzet.Aciklama = odemeSekli + " Satış";
                    Console.Beep(900, 500);
                    MessageBox.Show("Satış Başarılı");
                }
                else
                {
                    islemOzet.Aciklama = "İade İşlemi (" + odemeSekli + ")";
                    Console.Beep(900, 500);
                    MessageBox.Show("İade Başarılı");
                }
                islemOzet.OdemeSekli = odemeSekli;
                islemOzet.Kullanici = lKullanici.Text;
                islemOzet.Tarih = DateTime.Now;
                switch (odemeSekli)
                {
                    case "Nakit":
                        islemOzet.Nakit = İslemler.DoubleYap(tGenelToplam.Text);
                        islemOzet.Kart = 0; break;
                    case "Kart":
                        islemOzet.Nakit = 0;
                        islemOzet.Kart = İslemler.DoubleYap(tGenelToplam.Text); break;
                    case "Kart-Nakit":
                        islemOzet.Nakit = İslemler.DoubleYap(lNakit.Text);
                        islemOzet.Kart = İslemler.DoubleYap(lKart.Text); break;

                }
                _db.IslemOzet.Add(islemOzet);
                _db.SaveChanges();
                var islemNoArtir = _db.Islem.First();
                islemNoArtir.IslemNo += 1;
                _db.SaveChanges();
                if (chYazdirma.Checked)
                {
                    Yazdir yazdir = new Yazdir(islemNo, zNumara);
                    yazdir.YazdirmayaBasla();
                }
                Temizle();
            }
            else
            {
                Console.Beep(900, 50);
                MessageBox.Show("Şu anda bir satış bulunmuyor");
            }
        }
        private void bZRaporAl_Click(object sender, EventArgs e)
        {
            var zNoArtir = _db.Zno.First();
            zNoArtir.ZNumara += 1;
            _db.SaveChanges();
        }
        private void bNakit_Click(object sender, EventArgs e)
        {
            double odenen = İslemler.DoubleYap(tOdenen.Text);
            if (odenen > 0)
            {
                SatisYap("Nakit");
            }
            else
            {
                Console.Beep(900, 50);
                MessageBox.Show("Lütfen ödenen miktarı giriniz.");
            }
        }

        private void bKart_Click(object sender, EventArgs e)
        {
            SatisYap("Kart");
        }

        private void bKartNakit_Click(object sender, EventArgs e)
        {
            int satirSayisi = gridSatisListesi.Rows.Count;
            if (satirSayisi > 0)
            {
                fNakitKart f = new fNakitKart();
                f.ShowDialog();
            }
            else
            {
                Console.Beep(900, 50);
                MessageBox.Show("Şu anda bir satış bulunmuyor");
            }
        }

        private void tBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void tMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void fSatis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) SatisYap("Nakit");
            if (e.KeyCode == Keys.F2) SatisYap("Kart");
            if (e.KeyCode == Keys.F3)
            {
                fNakitKart f = new fNakitKart();
                f.ShowDialog();
            }
        }

        private void bIslemBeklet_Click(object sender, EventArgs e)
        {
            if (bIslemBeklet.Text == "İşlem Beklet")
            {
                Bekle();
            }
            else
            {
                BeklemedenCik();
            }
            GenelToplamHesap();
        }

        private void Bekle()
        {
            int satirSayisi = gridSatisListesi.Rows.Count;
            int sutunSayisi = gridSatisListesi.Columns.Count;
            if (satirSayisi > 0)
            {
                bIslemBeklet.BackColor = System.Drawing.Color.OrangeRed;
                bIslemBeklet.Text = "İşlem Bekliyor";
                for (int i = 0; i < satirSayisi; i++)
                {
                    gridBekle.Rows.Add();
                    for (int j = 0; j < sutunSayisi - 1; j++) // -1 dedim çünkü sil sütununu eklemeyeceğim
                    {
                        gridBekle.Rows[i].Cells[j].Value = gridSatisListesi.Rows[i].Cells[j].Value;
                    }
                }
                gridSatisListesi.Rows.Clear();
            }
            else
            {
                Console.Beep(900, 50);
                MessageBox.Show("Bekletilecek işlem bulunamadı");
            }
        }

        private void BeklemedenCik()
        {
            int satirSayisi = gridBekle.Rows.Count;
            int sutunSayisi = gridBekle.Columns.Count;
            int islemdekiSatirSayisi = gridSatisListesi.Rows.Count;
            if (islemdekiSatirSayisi == 0)
            {
                if (satirSayisi > 0)
                {
                    bIslemBeklet.BackColor = System.Drawing.Color.MediumTurquoise;
                    bIslemBeklet.Text = "İşlem Beklet";
                    for (int i = 0; i < satirSayisi; i++)
                    {
                        gridSatisListesi.Rows.Add();
                        for (int j = 0; j < sutunSayisi - 1; j++) // -1 dedim çünkü sil sütununu eklemeyeceğim
                        {
                            gridSatisListesi.Rows[i].Cells[j].Value = gridBekle.Rows[i].Cells[j].Value;
                        }
                    }
                    gridBekle.Rows.Clear();
                }
            }
            else
            {
                Console.Beep(900, 50);
                MessageBox.Show("Lütfen işlemi tamamlayın.");
            }
        }

        private void chSatisIadeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Text = "İade Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
