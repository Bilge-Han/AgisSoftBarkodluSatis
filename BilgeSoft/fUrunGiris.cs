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
    public partial class fUrunGiris : Form
    {
        public fUrunGiris()
        {
            InitializeComponent();
        }
        private void fUrunGiris_Load(object sender, EventArgs e)
        {
            KullaniciAta();
            tUrunSayisi.Text = db.Urun.Count().ToString();
            gridUrunler.DataSource = db.Urun.OrderByDescending(a => a.UrunId).Take(20).ToList();
            İslemler.GridDuzenle(gridUrunler);
            GrupDoldur();
            BirimDoldur();
        }
        private void KullaniciAta()
        {
            fSatis f = (fSatis)Application.OpenForms["fSatis"];
            if (f != null)
            {
                lKullanici.Text = f.lKullanici.Text;
            }
        }
        public void HizliButonDoldur()
        {
            fSatis f = (fSatis)Application.OpenForms["fSatis"];
            var hizliUrun = db.HizliButon.ToList();
            foreach (var item in hizliUrun)
            {
                Button bH = f.Controls.Find("bHizli" + item.Id, true).FirstOrDefault() as Button;
                if (bH != null)
                {
                    double fiyat = İslemler.DoubleYap(item.Fiyat.ToString());
                    bH.Text = item.UrunAd + "\n" + fiyat.ToString("C2");
                }
            }
        }
        BarkodDbEntities db = new BarkodDbEntities();
        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod = tBarkod.Text.Trim();
                if (db.Urun.Any(a => a.Barkod == barkod))
                {
                    var urun = db.Urun.Where(a => a.Barkod == barkod).SingleOrDefault();
                    tUrunAdi.Text = urun.UrunAd;
                    tAciklama.Text = urun.Aciklama;
                    cmbUrunGrubu.Text = urun.UrunGrup;
                    cmbBirim.Text = urun.Birim;
                    tAlisFiyati.Text = urun.AlisFiyat.ToString();
                    tSatisFiyati.Text = urun.SatisFiyat.ToString();
                    tMiktar.Text = urun.Miktar.ToString();
                    tKdvOrani.Text = urun.KdvOrani.ToString();
                    if (urun.Birim != "Adet")
                    {
                        chUrunTipi.Checked = true;
                    }
                    else
                    {
                        chUrunTipi.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Ürün Kayıtlı Değil, Kaydedebilirsiniz.");
                }
            }
        }

        private void bKaydet_Click(object sender, EventArgs e)
        {
            if (tBarkod.Text != "" && tUrunAdi.Text != "" && cmbUrunGrubu.Text != "" && tAlisFiyati.Text != "" && tSatisFiyati.Text != "" && tKdvOrani.Text != "" && tMiktar.Text != "")
            {
                if (db.Urun.Any(a => a.Barkod == tBarkod.Text))
                {
                    var guncelle = db.Urun.Where(a => a.Barkod == tBarkod.Text).SingleOrDefault();
                    guncelle.UrunAd = tUrunAdi.Text;
                    guncelle.Aciklama = tAciklama.Text;
                    guncelle.UrunGrup = cmbUrunGrubu.Text;
                    guncelle.AlisFiyat = Convert.ToDouble(tAlisFiyati.Text);
                    guncelle.SatisFiyat = Convert.ToDouble(tSatisFiyati.Text);
                    guncelle.KdvOrani = Convert.ToInt16(tKdvOrani.Text);
                    guncelle.KdvTutari = Math.Round(İslemler.DoubleYap(tSatisFiyati.Text) * Convert.ToInt16(tKdvOrani.Text) / 100, 2);
                    guncelle.Miktar += Convert.ToDouble(tMiktar.Text);
                    if (!chUrunTipi.Checked)
                    {
                        chUrunTipi.ShadowDecoration.Color = Color.Black;
                        cmbBirim.Text = "Adet";
                        guncelle.Birim = cmbBirim.Text;
                    }
                    else
                    {
                        chUrunTipi.ShadowDecoration.Color = Color.Sienna;
                        cmbBirim.Text = "Kg";
                        guncelle.Birim = cmbBirim.Text;
                    }
                    //guncelle.Tarih = DateTime.Now;
                    guncelle.Kullanici = lKullanici.Text;
                    db.SaveChanges();

                }
                else
                {

                    Urun urun = new Urun();
                    urun.Barkod = tBarkod.Text;
                    urun.UrunAd = tUrunAdi.Text;
                    urun.Aciklama = tAciklama.Text;
                    urun.UrunGrup = cmbUrunGrubu.Text;
                    urun.AlisFiyat = Convert.ToDouble(tAlisFiyati.Text);
                    urun.SatisFiyat = Convert.ToDouble(tSatisFiyati.Text);
                    urun.KdvOrani = Convert.ToInt16(tKdvOrani.Text);
                    urun.KdvTutari = Math.Round(İslemler.DoubleYap(tSatisFiyati.Text) * Convert.ToInt16(tKdvOrani.Text) / 100, 2);
                    urun.Miktar = Convert.ToDouble(tMiktar.Text);
                    if (!chUrunTipi.Checked)
                    {
                        cmbBirim.Text = "Adet";
                        urun.Birim = cmbBirim.Text;
                    }
                    else
                    {
                        cmbBirim.Text = "Kg";
                        urun.Birim = cmbBirim.Text;
                    }

                    urun.Tarih = DateTime.Now;
                    urun.Kullanici = lKullanici.Text;
                    db.Urun.Add(urun);
                    db.SaveChanges();
                    if (tBarkod.Text.Length == 8)
                    {
                        var ozelBarkod = db.Barkod.First();
                        ozelBarkod.BarkodNo += 1;
                        db.SaveChanges();
                    }
                }
                İslemler.StokHareket(tBarkod.Text, tUrunAdi.Text, cmbBirim.Text, Convert.ToDouble(tMiktar.Text), cmbUrunGrubu.Text, lKullanici.Text);
                Temizle();
                gridUrunler.DataSource = db.Urun.OrderByDescending(a => a.UrunId).Take(20).ToList();
                İslemler.GridDuzenle(gridUrunler);
                tUrunSayisi.Text = gridUrunler.Rows.Count.ToString();

            }
            else
            {
                MessageBox.Show("Bilgi Girişlerini Kontrol Ediniz.");
                tBarkod.Focus();
            }
        }
        private void chUrunTipi_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chUrunTipi.Checked)
            {
                cmbBirim.Text = "Kg";
                chUrunTipi.Text = "Gramajlı Ürün İşlemi";
                bBarkodOlustur.Enabled = false;
            }
            else
            {
                cmbBirim.Text = "Adet";
                chUrunTipi.Text = "Barkodlu Ürün İşlemi";
                bBarkodOlustur.Enabled = true;
            }
        }
        private void tUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (tUrunAra.Text.Length >= 2)
            {
                string urunAd = tUrunAra.Text;
                gridUrunler.DataSource = db.Urun.Where(a => a.UrunAd.Contains(urunAd)).ToList();
                İslemler.GridDuzenle(gridUrunler);
            }
        }
        private void Temizle()
        {
            tBarkod.Clear();
            tUrunAdi.Clear();
            tAciklama.Clear();
            tAlisFiyati.Text = "0";
            tSatisFiyati.Text = "0";
            tKdvOrani.Text = "8";
            tMiktar.Text = "0";
            tBarkod.Focus();
        }
        private void bIptal_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void bUrunGrubuEkle_Click(object sender, EventArgs e)
        {
            fUrunGrubuEkle f = new fUrunGrubuEkle();
            f.ShowDialog();
        }
        public void GrupDoldur()
        {
            cmbUrunGrubu.DisplayMember = "UrunGrupAd";
            cmbUrunGrubu.ValueMember = "Id";
            cmbUrunGrubu.DataSource = db.UrunGrup.OrderBy(a => a.UrunGrupAd).ToList();
        }
        private void BirimDoldur()
        {
            cmbBirim.DisplayMember = "BirimAd";
            cmbBirim.ValueMember = "Id";
            cmbBirim.DataSource = db.Birim.OrderBy(a => a.BirimAd).ToList();
        }
        private void bBarkodOlustur_Click(object sender, EventArgs e)
        {
            var barkodNo = db.Barkod.First();
            int karakter = barkodNo.BarkodNo.ToString().Length;
            string sifirSayisi = string.Empty;
            for (int i = 0; i < 8 - karakter; i++)
            {
                sifirSayisi = sifirSayisi + "0";
            }
            string olusanBarkodNo = sifirSayisi + barkodNo.BarkodNo.ToString();
            tBarkod.Text = olusanBarkodNo;
            tUrunAdi.Focus();


        }

        private void tAlisFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44 && e.KeyChar != (char)45)
            {
                e.Handled = true;
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridUrunler.Rows.Count > 0)
            {
                int urunId = Convert.ToInt32(gridUrunler.CurrentRow.Cells["UrunId"].Value.ToString());
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                DialogResult onay = MessageBox.Show(urunAd + " Ürününü Silmek İstiyor Musunuz?", "Ürün Silme İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    var urun = db.Urun.Find(urunId);
                    db.Urun.Remove(urun);
                    db.SaveChanges();
                    if (db.HizliButon.Any(x => x.Barkod == barkod))
                    {
                        var hizliUrun = db.HizliButon.Where(x => x.Barkod == barkod).SingleOrDefault();
                        if (hizliUrun!=null)
                        {
                            hizliUrun.Barkod = "-";
                            hizliUrun.UrunAd = "-";
                            hizliUrun.Fiyat = 0;
                            db.SaveChanges();
                            HizliButonDoldur();
                        }
                        
                    }
                    MessageBox.Show("Ürün Silinmiştir");
                    gridUrunler.DataSource = db.Urun.OrderByDescending(a => a.UrunId).Take(20).ToList();
                    tBarkod.Focus();
                }
            }


        }

        private void gridUrunler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int urunId = Convert.ToInt32(gridUrunler.CurrentRow.Cells["UrunId"].Value.ToString());
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();

                DialogResult onay = MessageBox.Show(urunAd + " Ürününü Silmek İstiyor Musunuz?", "Ürün Silme İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {

                    var urun = db.Urun.Find(urunId);
                    db.Urun.Remove(urun);
                    db.SaveChanges();
                    if (db.HizliButon.Any(x => x.Barkod == barkod))
                    {
                        var hizliUrun = db.HizliButon.Where(x => x.Barkod == barkod).SingleOrDefault();
                        if (hizliUrun!=null)
                        {
                            hizliUrun.Barkod = "-";
                            hizliUrun.UrunAd = "-";
                            hizliUrun.Fiyat = 0;
                            db.SaveChanges();
                            HizliButonDoldur();
                        }
                    }
                    MessageBox.Show("Ürün Silinmiştir");
                    gridUrunler.DataSource = db.Urun.OrderByDescending(a => a.UrunId).Take(20).ToList();
                    tBarkod.Focus();
                }
            }

        }

        private void gridUrunler_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridUrunler.Rows.Count>0)
            {
                tBarkod.Text = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                tUrunAdi.Text = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                tAciklama.Text= gridUrunler.CurrentRow.Cells["Aciklama"].Value.ToString();
                cmbUrunGrubu.Text= gridUrunler.CurrentRow.Cells["UrunGrup"].Value.ToString();
                tAlisFiyati.Text= gridUrunler.CurrentRow.Cells["AlisFiyat"].Value.ToString();
                tSatisFiyati.Text= gridUrunler.CurrentRow.Cells["SatisFiyat"].Value.ToString();
                tKdvOrani.Text= gridUrunler.CurrentRow.Cells["KdvOrani"].Value.ToString();
                tMiktar.Text= gridUrunler.CurrentRow.Cells["Miktar"].Value.ToString();
                cmbBirim.Text = gridUrunler.CurrentRow.Cells["Birim"].Value.ToString();
                if (cmbBirim.Text == "Adet") chUrunTipi.Checked = false;
                else chUrunTipi.Checked = true;
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridUrunler.Rows.Count > 0)
            {
                tBarkod.Text = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                tUrunAdi.Text = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                tAciklama.Text = gridUrunler.CurrentRow.Cells["Aciklama"].Value.ToString();
                cmbUrunGrubu.Text = gridUrunler.CurrentRow.Cells["UrunGrup"].Value.ToString();
                tAlisFiyati.Text = gridUrunler.CurrentRow.Cells["AlisFiyat"].Value.ToString();
                tSatisFiyati.Text = gridUrunler.CurrentRow.Cells["SatisFiyat"].Value.ToString();
                tKdvOrani.Text = gridUrunler.CurrentRow.Cells["KdvOrani"].Value.ToString();
                tMiktar.Text = gridUrunler.CurrentRow.Cells["Miktar"].Value.ToString();
                cmbBirim.Text = gridUrunler.CurrentRow.Cells["Birim"].Value.ToString();
                if (cmbBirim.Text == "Adet") chUrunTipi.Checked = false;
                else chUrunTipi.Checked = true;
            }
        }
    }
}
