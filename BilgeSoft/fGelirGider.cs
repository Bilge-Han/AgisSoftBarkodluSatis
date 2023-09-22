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
    public partial class fGelirGider : Form
    {
        public fGelirGider()
        {
            InitializeComponent();
        }
        public string gelirgider { get; set; }
        public string kullanici { get; set; }
        private void fGelirGider_Load(object sender, EventArgs e)
        {
            lGelirGider.Text = gelirgider + " İŞLEMİ YAPILIYOR";
        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOdemeTuru.SelectedIndex==0)
            {
                tNakit.Enabled = true;
                tKart.Enabled = false;
            }
            else if (cmbOdemeTuru.SelectedIndex==1)
            {
                tNakit.Enabled = false;
                tKart.Enabled = true;
            }
            else if (cmbOdemeTuru.SelectedIndex==2)
            {
                tNakit.Enabled = true;
                tKart.Enabled = true;
            }
            tNakit.Text = "0";
            tKart.Text = "0";
        }

        private void bEkle_Click(object sender, EventArgs e)
        {
            if (cmbOdemeTuru.Text!="")
            {
                if (tNakit.Text!=""&&tKart.Text!="")
                {
                    using (var db=new BarkodDbEntities())
                    {
                        IslemOzet islemOzet = new IslemOzet();
                        islemOzet.IslemNo = 0;
                        islemOzet.Iade = false;
                        islemOzet.OdemeSekli = cmbOdemeTuru.Text;
                        islemOzet.Nakit = İslemler.DoubleYap(tNakit.Text);
                        islemOzet.Kart = İslemler.DoubleYap(tKart.Text);
                        if (gelirgider=="GELİR")
                        {
                            islemOzet.Gelir = true;
                            islemOzet.Gider = false;
                        }
                        else
                        {
                            islemOzet.Gelir = false;
                            islemOzet.Gider = true;
                        }
                        islemOzet.AlisFiyatToplam = 0;
                        islemOzet.Aciklama = gelirgider +" İşlemi - "+ tAciklama.Text;
                        islemOzet.Tarih = dtTarih.Value;
                        islemOzet.Kullanici = kullanici;
                        db.IslemOzet.Add(islemOzet);
                        db.SaveChanges();
                        MessageBox.Show(gelirgider+" İşlemi Kaydedildi");
                        tNakit.Text = "0";
                        tKart.Text = "0";
                        tAciklama.Clear();
                        cmbOdemeTuru.Text = "";
                        fRapor f = (fRapor)Application.OpenForms["fRapor"];
                        if (f!=null)
                        {
                            f.bGoster_Click(null,null);
                        }
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Ödeme Türünü Belirleyiniz");
            }
        }
    }
}
