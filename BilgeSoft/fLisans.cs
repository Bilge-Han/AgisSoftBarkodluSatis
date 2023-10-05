using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lisans;
namespace BilgeSoft
{
    public partial class fLisans : Form
    {
        Lic lic = new Lic();
        public fLisans()
        {
            InitializeComponent();
        }

        private void bTamam_Click(object sender, EventArgs e)
        {
            if (tLisans.Text!="")
            {
                Kontrol kontrol = new Kontrol();
                kontrol.Lisanla(tLisans.Text);
            }
        }
    }
}
