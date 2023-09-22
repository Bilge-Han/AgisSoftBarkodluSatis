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
    public partial class fDetayGoster : Form
    {
        public fDetayGoster()
        {
            InitializeComponent();
        }

        public int islemNo { get; set; }
        private void fDetayGoster_Load(object sender, EventArgs e)
        {
            lIslemNo.Text ="İşlem No :  " +islemNo.ToString();
            using(var db =new BarkodDbEntities())
            {
                gridListe.DataSource = db.Satis.Select(s=>new {s.IslemNo,s.UrunAd,s.UrunGrup,s.Miktar,s.Toplam})
                    .Where(x => x.IslemNo == islemNo).ToList();
            }
            İslemler.GridDuzenle(gridListe);
        }
    }
}
