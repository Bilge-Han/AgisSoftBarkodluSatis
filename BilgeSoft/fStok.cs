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
    public partial class fStok : Form
    {
        public fStok()
        {
            InitializeComponent();
        }

        private void bAra_Click(object sender, EventArgs e)
        {
            // grid'te bir sürüna tıklandığında onu sıralayacağız
            gridListe.DataSource = null;
            using (var db = new BarkodDbEntities())
            {
                if (cmbIslemTuru.Text != "")
                {
                    string urunGrubu = cmbUrunGrubu.Text;
                    if (cmbIslemTuru.SelectedIndex == 0) // Stok Durumu İse
                    {
                        if (rdTumu.Checked) //hepsi listelenecek
                        {
                            db.Urun.OrderBy(x => x.Miktar).Load();
                            gridListe.DataSource = db.Urun.Local.ToBindingList();
                        }
                        else if (rdUrunGrubu.Checked)
                        {
                            if (cmbUrunGrubu.Text != "")
                            {
                                db.Urun.Where(x => x.UrunGrup == urunGrubu).OrderBy(x => x.Miktar).Load();
                                gridListe.DataSource = db.Urun.Local.ToBindingList();
                            }
                            else { MessageBox.Show("Lütfen Urun Grubu Seçiniz"); }
                        }
                        else { MessageBox.Show("Lütfen Filtreleme Türünü Seçiniz"); }
                    }
                    else if (cmbIslemTuru.SelectedIndex == 1)
                    {
                        DateTime baslangic = DateTime.Parse(dateBaslangic.Value.ToShortDateString());
                        DateTime bitis = DateTime.Parse(dateBitis.Value.ToShortDateString());
                        bitis = bitis.AddDays(1);
                        if (rdTumu.Checked)
                        {
                            db.StokHareket.OrderByDescending(x => x.Tarih).Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).Load();
                            gridListe.DataSource = db.StokHareket.Local.ToBindingList();
                        }
                        else if (rdUrunGrubu.Checked)
                        {
                            if (cmbUrunGrubu.Text != "")
                            {
                                db.StokHareket.OrderByDescending(x => x.Tarih).Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.UrunGrup.Equals(urunGrubu)).Load();
                                gridListe.DataSource = db.StokHareket.Local.ToBindingList();
                            }
                            else { MessageBox.Show("Lütfen Urun Grubu Seçiniz"); }
                        }
                        else { MessageBox.Show("Lütfen Filtreleme Türünü Seçiniz"); }
                    }
                    İslemler.GridDuzenle(gridListe);
                }
                else { MessageBox.Show("Lütfen İşlem Türünü Seçiniz"); }
            }
        }
        BarkodDbEntities dbx = new BarkodDbEntities();
        private void fStok_Load(object sender, EventArgs e)
        {
            cmbUrunGrubu.DisplayMember = "UrunGrupAd";
            cmbUrunGrubu.ValueMember = "Id";
            cmbUrunGrubu.DataSource = dbx.UrunGrup.ToList();
        }

        private void tUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (tUrunAra.Text.Length >= 2)
            {
                string urunAd = tUrunAra.Text;
                using (var db = new BarkodDbEntities())
                {
                    if (cmbIslemTuru.SelectedIndex == 0)
                    {
                        db.Urun.Where(x => x.UrunAd.Contains(urunAd)).Load();
                        gridListe.DataSource = db.Urun.Local.ToBindingList();
                    }
                    else if (cmbIslemTuru.SelectedIndex == 1)
                    {
                        db.StokHareket.Where(x => x.UrunAd.Contains(urunAd)).Load();
                        gridListe.DataSource = db.StokHareket.Local.ToBindingList();
                    }
                }
                İslemler.GridDuzenle(gridListe);
            }
        }

        private void bRaporAl_Click(object sender, EventArgs e)
        {
            if (cmbIslemTuru.SelectedIndex==0)
            {
                Raporlar.Baslik = cmbIslemTuru.Text + " RAPORU";
                // Buraya raporun alındığı tarihi yazabilirim
                Raporlar.TarihBaslangic = dateBaslangic.Value.ToShortDateString();
                Raporlar.TarihBitis = dateBitis.Value.ToShortDateString();
                Raporlar.StokRaporu(gridListe);
            }
            else if (cmbIslemTuru.SelectedIndex==1)
            {
                Raporlar.Baslik = cmbIslemTuru.Text + " RAPORU";
                Raporlar.TarihBaslangic = dateBaslangic.Value.ToShortDateString();
                Raporlar.TarihBitis = dateBitis.Value.ToShortDateString();
                Raporlar.StokIzlemeRaporu(gridListe);
            }
        }
    }
}
