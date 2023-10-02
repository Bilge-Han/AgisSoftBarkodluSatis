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

        private void fLisans_Load(object sender, EventArgs e)
        {

        }

        private void bTamam_Click(object sender, EventArgs e)
        {
            if (tLisans.Text!="")
            {
                Kontrol kontrol = new Kontrol();
                kontrol.Lisanla(tLisans.Text);
            }
        }

        private void lStandart2_Click(object sender, EventArgs e)
        {

        }

        private void bTamam_Click_1(object sender, EventArgs e)
        {

        }

        private void lKontrolNo_Click(object sender, EventArgs e)
        {

        }

        private void tLisans_TextChanged(object sender, EventArgs e)
        {

        }

        private void lStandart1_Click(object sender, EventArgs e)
        {

        }
    }
}
