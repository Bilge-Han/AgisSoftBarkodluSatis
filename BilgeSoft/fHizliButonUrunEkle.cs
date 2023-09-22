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
    public partial class fHizliButonUrunEkle : Form
    {
        BarkodDbEntities _db = new BarkodDbEntities();
        public fHizliButonUrunEkle()
        {
            InitializeComponent();
        }

        private void tUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (tUrunAra.Text!="")
            {
                string urunAd = tUrunAra.Text;
                var urunler = _db.Urun.Where(a => a.UrunAd.Contains(urunAd)).ToList();
                gridUrunler.DataSource = urunler;
                gridUrunler.Columns["AlisFiyat"].Visible = false;
                gridUrunler.Columns["SatisFiyat"].Visible = false;
                gridUrunler.Columns["KdvOrani"].Visible = false;
                gridUrunler.Columns["KdvTutari"].Visible = false;
                gridUrunler.Columns["Miktar"].Visible = false;
                İslemler.GridDuzenle(gridUrunler);
            }
        }

        private void gridUrunler_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridUrunler.Rows.Count>0)
            {
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                double fiyat =Convert.ToDouble(gridUrunler.CurrentRow.Cells["SatisFiyat"].Value.ToString());
                int id = Convert.ToInt16(lButonId.Text);

                //Find 1.anahtarda arama yapar
                var guncellenecekButon = _db.HizliButon.Find(id);
                guncellenecekButon.Barkod = barkod;
                guncellenecekButon.UrunAd = urunAd;
                guncellenecekButon.Fiyat = fiyat;
                _db.SaveChanges();
                MessageBox.Show("Buton tanımlanmıştır.");
                fSatis f = (fSatis)Application.OpenForms["fSatis"];
                if (f!=null)
                {
                    Button b = f.Controls.Find("bHizli" + id,true).FirstOrDefault() as Button;
                    //Veritabanından çeker güncelleyipte yapabiliriz burdan direktte güncelleyebiliriz.
                    b.Text = urunAd + "\n" + fiyat.ToString("C2");
                }
            }
        }

        private void chTumu_CheckedChanged(object sender, EventArgs e)
        {
            if (chTumu.Checked)
            {
                gridUrunler.DataSource = _db.Urun.ToList();
                gridUrunler.Columns["AlisFiyat"].Visible = false;
                gridUrunler.Columns["SatisFiyat"].Visible = false;
                gridUrunler.Columns["KdvOrani"].Visible = false;
                gridUrunler.Columns["KdvTutari"].Visible = false;
                gridUrunler.Columns["Miktar"].Visible = false;
                İslemler.GridDuzenle(gridUrunler);
            }
            else
            {
                gridUrunler.DataSource = null;
            }
        }

    }
}
