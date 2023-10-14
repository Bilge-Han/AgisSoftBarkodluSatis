using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeSoft
{
    public partial class fAyarlar : Form
    {
        public fAyarlar()
        {
            InitializeComponent();
        }
        private void fAyarlar_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            İslemler.SabitVarsayilan();
            Doldur();
            Cursor.Current = Cursors.Default;
        }
        private void Temizle()
        {
            tAdSoyad.Clear();
            tTelefon.Clear();
            tEposta.Clear();
            tKullanici.Clear();
            tSifre.Clear();
            tSifreTekrar.Clear();
            chSatisEkrani.Checked = false;
            chRapor.Checked = false;
            chStok.Checked = false;
            chUrunGiris.Checked = false;
            chSatisEkrani.Checked = false;
            chAyarlar.Checked = false;
            chFiyatGuncelle.Checked = false;
            chYedekleme.Checked = false;
        }
        private void Doldur()
        {
            using (var db = new BarkodDbEntities())
            {
                if (db.Kullanici.Any())
                {
                    gridListeKullanici.DataSource = db.Kullanici.Select(x => new { x.Id, x.AdSoyad, x.KullaniciAd, x.Telefon }).ToList();
                }
                //İslemler.SabitVarsayilan();
                var sabitler = db.Sabit.FirstOrDefault();
                chYazmaDurumu.Checked = (bool)(sabitler.Yazici);
                tKartKomisyon.Text = sabitler.KartKomisyon.ToString();

                var teraziOnEk = db.Terazi.ToList();
                cmbTeraziOnEk.DisplayMember = "TeraziOnEk";
                cmbTeraziOnEk.ValueMember = "Id";
                cmbTeraziOnEk.DataSource = teraziOnEk;

                tIsYeriAdSoyad.Text = sabitler.AdSoyad;
                tIsYeriUnvan.Text = sabitler.Unvan;
                tIsYeriAdres.Text = sabitler.Adres;
                tIsYeriTelefon.Text = sabitler.Telefon;
                tIsYeriEposta.Text = sabitler.Eposta;
            }

        }
        private void bKaydet_Click(object sender, EventArgs e)
        {
            if (bKaydet.Text == "Kaydet")
            {
                if (tAdSoyad.Text != "" && tTelefon.Text != "" && tKullanici.Text != "" && tSifre.Text != "" && tSifreTekrar.Text != "")
                {
                    if (tSifre.Text == tSifreTekrar.Text)
                    {
                        try
                        {
                            using (var db = new BarkodDbEntities())
                            {
                                if (!db.Kullanici.Any(x => x.KullaniciAd == tKullanici.Text))
                                {
                                    Kullanici kullanici = new Kullanici();
                                    kullanici.AdSoyad = tAdSoyad.Text;
                                    kullanici.Telefon = tTelefon.Text;
                                    kullanici.EPosta = tEposta.Text;
                                    kullanici.KullaniciAd = tKullanici.Text.Trim();
                                    kullanici.Sifre = tSifre.Text;
                                    kullanici.Satis = chSatisEkrani.Checked;
                                    kullanici.Rapor = chRapor.Checked;
                                    kullanici.Stok = chStok.Checked;
                                    kullanici.UrunGiris = chUrunGiris.Checked;
                                    kullanici.Ayarlar = chAyarlar.Checked;
                                    kullanici.FiyatGuncelle = chFiyatGuncelle.Checked;
                                    kullanici.Yedekleme = chYedekleme.Checked;
                                    db.Kullanici.Add(kullanici);
                                    db.SaveChanges();

                                    Doldur();
                                    Temizle();
                                }
                                else
                                {
                                    MessageBox.Show("Kullanıcı zaten kayıtlı!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata Oluştu : " + ex.ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz şifre bilgileri uyuşmuyor.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad \n Telefon \n Kullanıcı Adı \n Şifre \n Şifre Tekrar");
                }
            }
            else if (bKaydet.Text == "Düzenle/Kaydet")
            {
                if (tAdSoyad.Text != "" && tTelefon.Text != "" && tKullanici.Text != "" && tSifre.Text != "" && tSifreTekrar.Text != "")
                {
                    if (tSifre.Text == tSifreTekrar.Text)
                    {
                        int id = Convert.ToInt32(lKullaniciId.Text);
                        using (var db = new BarkodDbEntities())
                        {
                            var guncelle = db.Kullanici.Where(x => x.Id == id).FirstOrDefault();
                            guncelle.AdSoyad = tAdSoyad.Text;
                            guncelle.Telefon = tTelefon.Text;
                            guncelle.EPosta = tEposta.Text;
                            guncelle.KullaniciAd = tKullanici.Text.Trim();
                            guncelle.Sifre = tSifre.Text;
                            guncelle.Satis = chSatisEkrani.Checked;
                            guncelle.Rapor = chRapor.Checked;
                            guncelle.Stok = chStok.Checked;
                            guncelle.UrunGiris = chUrunGiris.Checked;
                            guncelle.Ayarlar = chAyarlar.Checked;
                            guncelle.FiyatGuncelle = chFiyatGuncelle.Checked;
                            guncelle.Yedekleme = chYedekleme.Checked;
                            db.SaveChanges();
                            MessageBox.Show("Kullanıcı bilgileri güncellenmiştir");
                            Temizle();
                            Doldur();
                            bKaydet.Text = "Kaydet";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz şifre bilgileri uyuşmuyor.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad \n Telefon \n Kullanıcı Adı \n Şifre \n Şifre Tekrar");
                }
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListeKullanici.Rows.Count>0)
            {
                int id = Convert.ToInt32(gridListeKullanici.CurrentRow.Cells["Id"].Value.ToString());
                lKullaniciId.Text = id.ToString();
                using (var db=new BarkodDbEntities())
                {
                    var getir = db.Kullanici.Where(x => x.Id == id).FirstOrDefault();
                    tAdSoyad.Text = getir.AdSoyad;
                    tTelefon.Text = getir.Telefon;
                    tEposta.Text = getir.EPosta;
                    tKullanici.Text = getir.KullaniciAd;
                    tSifre.Text = getir.Sifre;
                    tSifreTekrar.Text = getir.Sifre;
                    chSatisEkrani.Checked = (bool)getir.Satis;
                    chRapor.Checked=(bool)getir.Rapor;
                    chStok.Checked=(bool)getir.Stok;
                    chUrunGiris.Checked=(bool)getir.UrunGiris;
                    chAyarlar.Checked=(bool)getir.Ayarlar;
                    chFiyatGuncelle.Checked= (bool)getir.FiyatGuncelle;
                    chYedekleme.Checked= (bool)getir.Yedekleme;

                    bKaydet.Text = "Düzenle/Kaydet";
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Seçiniz");
            }
        }

        private void chYazmaDurumu_CheckedChanged(object sender, EventArgs e)
        {
            using (var db = new BarkodDbEntities())
            {
                if (chYazmaDurumu.Checked)
                {
                    //İslemler.SabitVarsayilan();
                    var ayarla = db.Sabit.FirstOrDefault();
                    ayarla.Yazici = true;
                    db.SaveChanges();
                    chYazmaDurumu.Text = "Yazma Durumu AKTİF";

                }
                else
                {
                    //İslemler.SabitVarsayilan();
                    var ayarla = db.Sabit.FirstOrDefault();
                    ayarla.Yazici = false;
                    db.SaveChanges();
                    chYazmaDurumu.Text = "Yazma Durumu PASİF";

                }
            }
            
        }

        private void bKartKomisyonAyarla_Click(object sender, EventArgs e)
        {
            if (tKartKomisyon.Text!="")
            {
                using (var db = new BarkodDbEntities())
                {
                    var sabit = db.Sabit.FirstOrDefault();
                    sabit.KartKomisyon = Convert.ToInt16(tKartKomisyon.Text);
                    db.SaveChanges();
                    MessageBox.Show("Kart komisyon ayarlandı.");
                }
            }
            else MessageBox.Show("Kart komisyon bilgisi giriniz.");
        }

        private void bTeraziOnEkKaydet_Click(object sender, EventArgs e)
        {
            if (tTeraziOnEk.Text!="")
            {
                int onEk = Convert.ToInt16(tTeraziOnEk.Text);
                using (var db = new BarkodDbEntities())
                {
                    if (!db.Terazi.Any(x=>x.TeraziOnEk==onEk))
                    {
                        Terazi terazi = new Terazi();
                        terazi.TeraziOnEk = onEk;
                        db.Terazi.Add(terazi);
                        db.SaveChanges();
                        MessageBox.Show(onEk + " ön Eki kaydedilmiştir.");
                        cmbTeraziOnEk.DisplayMember = "TeraziOnEk";
                        cmbTeraziOnEk.ValueMember = "Id";
                        cmbTeraziOnEk.DataSource = db.Terazi.ToList();
                        tTeraziOnEk.Clear();
                    }
                    else MessageBox.Show(onEk.ToString() + " ön eki zaten kayıtlı");
                }
            }
            else MessageBox.Show("Terazi Ön Ek bilgisi giriniz.");
        }

        private void bTeraziOnEkSil_Click(object sender, EventArgs e)
        {
            if (cmbTeraziOnEk.Text != "")
            {
                int onEkId = Convert.ToInt16(cmbTeraziOnEk.SelectedValue);
                DialogResult onay = MessageBox.Show(cmbTeraziOnEk.Text + " ön ekini silmek istiyor musunuz?","Terazi ÖnEk Silme İşlemi", MessageBoxButtons.YesNo);
                if (onay==DialogResult.Yes)
                {
                    using (var db = new BarkodDbEntities())
                    {
                        if (db.Terazi.Any(x => x.Id == onEkId))
                        {
                            var teraziOnEk = db.Terazi.Find(onEkId);
                            db.Terazi.Remove(teraziOnEk);
                            db.SaveChanges();
                            MessageBox.Show(cmbTeraziOnEk.Text + " ön Eki silinmiştir.");
                            cmbTeraziOnEk.DisplayMember = "TeraziOnEk";
                            cmbTeraziOnEk.ValueMember = "Id";
                            cmbTeraziOnEk.DataSource = db.Terazi.ToList();
                            tTeraziOnEk.Clear();
                        }
                    }
                }
              
            }
            else MessageBox.Show("Terazi Ön Ek bilgisi giriniz.");
        }

        private void bIsYeriKaydet_Click(object sender, EventArgs e)
        {
            if (tIsYeriAdSoyad.Text!=""&&tIsYeriUnvan.Text!=""&&tIsYeriAdres.Text!=""&&tIsYeriTelefon.Text!="")
            {
                using (var db = new BarkodDbEntities())
                {
                    var isYeri = db.Sabit.FirstOrDefault();
                    isYeri.AdSoyad = tIsYeriAdSoyad.Text;
                    isYeri.Unvan = tIsYeriUnvan.Text;
                    isYeri.Adres = tIsYeriAdres.Text;
                    isYeri.Telefon = tIsYeriTelefon.Text;
                    isYeri.Eposta = tIsYeriEposta.Text;
                    db.SaveChanges();
                    MessageBox.Show("İşyeri bilgileri kaydedilmiştir.");
                    Doldur();
                }
            }
        }
        private void bYedektenYukle_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\ProgramRestore.exe");
            Application.Exit();
        }
    }
}
