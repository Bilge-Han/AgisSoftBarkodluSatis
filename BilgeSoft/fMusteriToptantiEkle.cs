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
    public partial class fMusteriToptantiEkle : Form
    {
        BarkodDbEntities _db = new BarkodDbEntities();
        public fMusteriToptantiEkle()
        {
            InitializeComponent();
        }
        private void fMusteriToptantiEkle_Load(object sender, EventArgs e)
        {
            gridListeMusteri.DataSource = _db.Musteriler.OrderByDescending(a => a.MusteriAdSoyad).Take(20).ToList();
            gridListeMusteri.Columns["Alinacak"].Visible = false;
            gridListeMusteri.Columns["Odenen"].Visible = false;
            İslemler.GridDuzenle(gridListeMusteri);
            gridListeToptanci.DataSource = _db.Toptancilar.OrderByDescending(a => a.FirmaAdi).Take(20).ToList();
            gridListeToptanci.Columns["Alinacak"].Visible = false;
            gridListeToptanci.Columns["Verilecek"].Visible = false;
            İslemler.GridDuzenle(gridListeToptanci);
        }
        private void MusteriDoldur()
        {
            using (var db = new BarkodDbEntities())
            {
                if (db.Musteriler.Any())
                {
                    gridListeMusteri.DataSource = db.Musteriler.Select(x => new
                    {
                        x.MusteriID,
                        x.MusteriAdSoyad,
                        x.Telefon,
                        x.Email,
                        x.Adres,
                        x.OzetNotlar
                    }).ToList();
                }
            }
        }
        private void ToptanciDoldur()
        {
            using (var db = new BarkodDbEntities())
            {
                if (db.Toptancilar.Any())
                {
                    gridListeToptanci.DataSource = db.Toptancilar.Select(x => new
                    {
                        x.FirmaID,
                        x.FirmaAdi,
                        x.YetkiliAdSoyad,
                        x.VergiNumarasi,
                        x.Telefon,
                        x.Email,
                        x.Adres,
                        x.OzetNotlar,
                    }).ToList();
                }
            }
        }
        private void MusteriTemizle()
        {
            tMusteriAdSoyad.Clear();
            tMusteriTelefon.Clear();
            tMusteriAdres.Clear();
            tMusteriEposta.Clear();
            tMusteriOzelNot.Clear();
        }
        private void ToptanciTemizle()
        {
            tTFirmaAd.Clear();
            tTYetkiliAdSoyad.Clear();
            tTVergiNo.Clear();
            tTFirmaTelefon.Clear();
            tTEPosta.Clear();
            tTAdres.Clear();
            tTOzelNot.Clear();
        }
        private void tMusteriAra_TextChanged(object sender, EventArgs e)
        {
            if (tMusteriAra.Text != "")
            {
                string musteriAd = tMusteriAra.Text;
                var musteriler = _db.Musteriler.Where(a => a.MusteriAdSoyad.Contains(musteriAd)).ToList();
                gridListeMusteri.DataSource = musteriler;
                //gridMusteriler.Columns["AlisFiyat"].Visible = false;
                //gridMusteriler.Columns["SatisFiyat"].Visible = false;
                //gridMusteriler.Columns["KdvOrani"].Visible = false;
                gridListeMusteri.Columns["Alinacak"].Visible = false;
                gridListeMusteri.Columns["Odenen"].Visible = false;
                İslemler.GridDuzenle(gridListeMusteri);
            }
        }

        private void tToptanciAra_TextChanged(object sender, EventArgs e)
        {
            if (tToptanciAra.Text != "")
            {
                string toptanciAd = tToptanciAra.Text;
                var toptantical = _db.Toptancilar.Where(a => a.FirmaAdi.Contains(toptanciAd)).ToList();
                gridListeToptanci.DataSource = toptantical;
                //gridMusteriler.Columns["AlisFiyat"].Visible = false;
                //gridMusteriler.Columns["SatisFiyat"].Visible = false;
                //gridMusteriler.Columns["KdvOrani"].Visible = false;
                //gridMusteriler.Columns["KdvTutari"].Visible = false;
                gridListeToptanci.Columns["Alinacak"].Visible = false;
                gridListeToptanci.Columns["Verilecek"].Visible = false;
                İslemler.GridDuzenle(gridListeToptanci);
            }
        }

        private void chTumuMusteri_CheckedChanged(object sender, EventArgs e)
        {
            if (chTumuMusteri.Checked)
            {
                gridListeMusteri.DataSource = _db.Musteriler.ToList();
                gridListeMusteri.Columns["Alinacak"].Visible = false;
                gridListeMusteri.Columns["Odenen"].Visible = false;
                İslemler.GridDuzenle(gridListeMusteri);
            }
            else
            {
                gridListeMusteri.DataSource = null;
            }
        }

        private void chTumuToptanci_CheckedChanged(object sender, EventArgs e)
        {
            if (chTumuToptanci.Checked)
            {
                gridListeToptanci.DataSource = _db.Toptancilar.ToList();
                gridListeToptanci.Columns["Alinacak"].Visible = false;
                gridListeToptanci.Columns["Verilecek"].Visible = false;
                İslemler.GridDuzenle(gridListeToptanci);
            }
            else
            {
                gridListeToptanci.DataSource = null;
            }
        }

        private void bMusteriKaydet_Click(object sender, EventArgs e)
        {
            if (bMusteriKaydet.Text == "Kaydet")
            {
                if (tMusteriAdSoyad.Text != "")
                {
                    try
                    {
                        using (var db = new BarkodDbEntities())
                        {
                            if (!db.Musteriler.Any(x => x.MusteriAdSoyad == tMusteriAdSoyad.Text))
                            {
                                Musteriler musteri = new Musteriler();
                                musteri.MusteriAdSoyad = tMusteriAdSoyad.Text;
                                musteri.Alinacak = 0;
                                musteri.Odenen = 0;
                                musteri.Telefon = tMusteriTelefon.Text;
                                musteri.Adres = tMusteriAdres.Text;
                                musteri.OzetNotlar = tMusteriOzelNot.Text;
                                db.Musteriler.Add(musteri);
                                db.SaveChanges();

                                MusteriDoldur();
                                MusteriTemizle();
                            }
                            else
                            {
                                MessageBox.Show("Müşteri zaten kayıtlı!");
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
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad");
                }
            }
            else if (bMusteriKaydet.Text == "Düzenle/Kaydet")
            {
                if (tMusteriAdSoyad.Text != "")
                {
                    int id = Convert.ToInt32(lMusteriId.Text);
                    using (var db = new BarkodDbEntities())
                    {
                        var guncelle = db.Musteriler.Where(x => x.MusteriID == id).FirstOrDefault();
                        guncelle.MusteriAdSoyad = tMusteriAdSoyad.Text;
                        guncelle.Telefon = tMusteriTelefon.Text;
                        guncelle.Adres = tMusteriAdres.Text;
                        guncelle.OzetNotlar = tMusteriOzelNot.Text;
                        db.SaveChanges();
                        MessageBox.Show("Müşteri bilgileri güncellenmiştir");
                        MusteriTemizle();
                        MusteriDoldur();
                        bMusteriKaydet.Text = "Kaydet";
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad");
                }
            }
        }
        private void bToptanciKaydet_Click(object sender, EventArgs e)
        {
            if (bToptanciKaydet.Text == "Kaydet")
            {
                if (tTFirmaAd.Text != "")
                {
                    try
                    {
                        using (var db = new BarkodDbEntities())
                        {
                            if (!db.Toptancilar.Any(x => x.FirmaAdi == tTFirmaAd.Text))
                            {
                                Toptancilar toptanci = new Toptancilar();
                                toptanci.FirmaAdi = tTFirmaAd.Text;
                                toptanci.YetkiliAdSoyad = tTYetkiliAdSoyad.Text;
                                toptanci.Telefon = tTFirmaTelefon.Text;
                                toptanci.Email = tTEPosta.Text;
                                toptanci.Adres = tTAdres.Text;
                                toptanci.VergiNumarasi = tTVergiNo.Text;
                                toptanci.OzetNotlar = tTOzelNot.Text;
                                db.Toptancilar.Add(toptanci);
                                db.SaveChanges();

                                ToptanciDoldur();
                                ToptanciTemizle();
                            }
                            else
                            {
                                MessageBox.Show("Firma zaten kayıtlı!");
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
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad");
                }
            }
            else if (bToptanciKaydet.Text == "Düzenle/Kaydet")
            {
                if (tTFirmaAd.Text != "")
                {
                    int id = Convert.ToInt32(lToptanciID.Text);
                    using (var db = new BarkodDbEntities())
                    {
                        var guncelle = db.Toptancilar.Where(x => x.FirmaID == id).FirstOrDefault();
                        guncelle.FirmaAdi = tTFirmaAd.Text;
                        guncelle.YetkiliAdSoyad = tTYetkiliAdSoyad.Text;
                        guncelle.Telefon = tTFirmaTelefon.Text;
                        guncelle.Email = tTEPosta.Text;
                        guncelle.Adres = tTAdres.Text;
                        guncelle.VergiNumarasi = tTVergiNo.Text;
                        guncelle.OzetNotlar = tTOzelNot.Text;
                        db.SaveChanges();
                        MessageBox.Show("Firma bilgileri güncellenmiştir");
                        ToptanciDoldur();
                        ToptanciTemizle();
                        bToptanciKaydet.Text = "Kaydet";
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu girişleri yapınız" + "\n AdSoyad");
                }
            }
        }

        private void musteriDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListeMusteri.Rows.Count > 0)
            {
                int id = Convert.ToInt32(gridListeMusteri.CurrentRow.Cells["MusteriID"].Value.ToString());
                lMusteriId.Text = id.ToString();
                using (var db = new BarkodDbEntities())
                {
                    var getir = db.Musteriler.Where(x => x.MusteriID == id).FirstOrDefault();
                    tMusteriAdSoyad.Text = getir.MusteriAdSoyad;
                    tMusteriTelefon.Text = getir.Telefon;
                    tMusteriAdres.Text = getir.Adres;
                    tMusteriOzelNot.Text = getir.OzetNotlar;

                    bMusteriKaydet.Text = "Düzenle/Kaydet";
                }
            }
            else
            {
                MessageBox.Show("Müşteri Seçiniz");
            }
        }

        private void musteriSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListeMusteri.Rows.Count > 0)
            {
                int musteriId = Convert.ToInt32(gridListeMusteri.CurrentRow.Cells["MusteriID"].Value.ToString());
                string musteriAd = gridListeMusteri.CurrentRow.Cells["MusteriAdSoyad"].Value.ToString();
                DialogResult onay = MessageBox.Show(musteriAd + " Adlı Müşteriyi Silmek İstiyor Musunuz?", "Ürün Silme İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    using (var db = new BarkodDbEntities())
                    {
                        var musteri = db.Musteriler.Find(musteriId);
                        db.Musteriler.Remove(musteri);
                        db.SaveChanges();
                    }
                    MessageBox.Show(musteriAd + " Adlı Müşteri Silinmiştir");
                    MusteriTemizle();
                    MusteriDoldur();
                    bMusteriKaydet.Text = "Kaydet";
                }
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListeToptanci.Rows.Count > 0)
            {
                int id = Convert.ToInt32(gridListeToptanci.CurrentRow.Cells["FirmaID"].Value.ToString());
                lToptanciID.Text = id.ToString();
                using (var db = new BarkodDbEntities())
                {
                    var getir = db.Toptancilar.Where(x => x.FirmaID == id).FirstOrDefault();
                    tTFirmaAd.Text = getir.FirmaAdi;
                    tTYetkiliAdSoyad.Text = getir.YetkiliAdSoyad;
                    tTFirmaTelefon.Text = getir.Telefon;
                    tTEPosta.Text = getir.Email;
                    tTAdres.Text = getir.Adres;
                    tTVergiNo.Text = getir.VergiNumarasi;
                    tTOzelNot.Text = getir.OzetNotlar;
                    bToptanciKaydet.Text = "Düzenle/Kaydet";
                }
            }
            else
            {
                MessageBox.Show("Firma Seçiniz");
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListeToptanci.Rows.Count > 0)
            {
                int toptancıId = Convert.ToInt32(gridListeToptanci.CurrentRow.Cells["FirmaID"].Value.ToString());
                string toptanciAd = gridListeToptanci.CurrentRow.Cells["FirmaAdi"].Value.ToString();
                DialogResult onay = MessageBox.Show(toptancıId + " Adlı Firmayı Silmek İstiyor Musunuz?", "Ürün Silme İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    using (var db = new BarkodDbEntities())
                    {
                        var toptanci = db.Toptancilar.Find(toptancıId);
                        db.Toptancilar.Remove(toptanci);
                        db.SaveChanges();
                    }
                    MessageBox.Show(toptanciAd + " Adlı Firma Silinmiştir");
                    ToptanciTemizle();
                    ToptanciDoldur();
                    bToptanciKaydet.Text = "Kaydet";
                }
            }
        }
    }
}
