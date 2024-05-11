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
    public partial class fMusteriSec : Form
    {
        BarkodDbEntities _db = new BarkodDbEntities();
        public fMusteriSec()
        {
            InitializeComponent();
        }
        private void fMusteriSec_Load(object sender, EventArgs e)
        {
            gridMusteriler.DataSource = _db.Musteriler.OrderByDescending(a => a.MusteriAdSoyad).Take(20).ToList();
            gridMusteriler.Columns["Alinacak"].Visible = false;
            gridMusteriler.Columns["Odenen"].Visible = false;
            İslemler.GridDuzenle(gridMusteriler);
        }
        private void chTumu_CheckedChanged(object sender, EventArgs e)
        {
            if (chTumu.Checked)
            {
                gridMusteriler.DataSource = _db.Musteriler.ToList();
                İslemler.GridDuzenle(gridMusteriler);
            }
            else
            {
                gridMusteriler.DataSource = null;
            }
        }

        private void tMusteriAra_TextChanged(object sender, EventArgs e)
        {
            if (tMusteriAra.Text != "")
            {
                string musteriAd = tMusteriAra.Text;
                var urunler = _db.Musteriler.Where(a => a.MusteriAdSoyad.Contains(musteriAd)).ToList();
                gridMusteriler.DataSource = urunler;
                //gridMusteriler.Columns["AlisFiyat"].Visible = false;
                //gridMusteriler.Columns["SatisFiyat"].Visible = false;
                //gridMusteriler.Columns["KdvOrani"].Visible = false;
                //gridMusteriler.Columns["KdvTutari"].Visible = false;
                //gridMusteriler.Columns["Miktar"].Visible = false;
                İslemler.GridDuzenle(gridMusteriler);
            }
        }

        private void gridMusteriler_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
            fSatis f = (fSatis)Application.OpenForms["fSatis"];
            if (f != null) { f.lMusteri.Text = gridMusteriler.CurrentRow.Cells["MusteriAdSoyad"].Value.ToString(); }
        }


    }
}
