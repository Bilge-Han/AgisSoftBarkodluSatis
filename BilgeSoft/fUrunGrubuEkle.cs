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
    public partial class fUrunGrubuEkle : Form
    {
        public fUrunGrubuEkle()
        {
            InitializeComponent();
        }
        BarkodDbEntities db = new BarkodDbEntities();
        private void fUrunGrubuEkle_Load(object sender, EventArgs e)
        {
            GrupDoldur();
        }

        private void bEkle_Click(object sender, EventArgs e)
        {
            if (tUrunGrupAd.Text!="")
            {
                UrunGrup ug = new UrunGrup();
                ug.UrunGrupAd = tUrunGrupAd.Text;
                db.UrunGrup.Add(ug);
                db.SaveChanges();
                GrupDoldur();
                tUrunGrupAd.Clear();
                MessageBox.Show("Ürün Grubu Eklenmiştir.");
                // Açılmış form üzerinde işlem yapmak için
                fUrunGiris f = (fUrunGiris)Application.OpenForms["fUrunGiris"];
                if (f!=null)
                {
                    f.GrupDoldur();
                }
                
            }
            else MessageBox.Show("Grup Bilgisi Ekleyiniz.");
        }
        private void GrupDoldur()
        {
            listUrunGrup.DisplayMember = "UrunGrupAd";
            listUrunGrup.ValueMember = "Id";
            listUrunGrup.DataSource = db.UrunGrup.OrderBy(a => a.UrunGrupAd).ToList();
        }

        private void bSil_Click(object sender, EventArgs e)
        {
            int grupId = Convert.ToInt32(listUrunGrup.SelectedValue.ToString());
            string grupAd = listUrunGrup.Text;
            DialogResult onay = MessageBox.Show(grupAd + " Grubunu Silmek İstiyor Musunuz?","Silme İşlemi",MessageBoxButtons.YesNo);
            if (onay==DialogResult.Yes)
            {
                var grup = db.UrunGrup.FirstOrDefault(x => x.Id == grupId);
                db.UrunGrup.Remove(grup);
                db.SaveChanges();
                GrupDoldur();          
                fUrunGiris f = (fUrunGiris)Application.OpenForms["fUrunGiris"];
                if (f != null)
                {
                    f.GrupDoldur();
                }
                tUrunGrupAd.Focus();
                MessageBox.Show(grupAd + " Ürün Grubu Silindi");
            }
        }
    }
}
