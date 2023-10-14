using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace BilgeSoft
{
    public partial class fNakitKart : Form
    {
        public fNakitKart()
        {
            InitializeComponent();
        }

        private void tNakit_KeyDown(object sender, KeyEventArgs e)
        {
            if (tNakit.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Hesapla();
                }
            }
        }
        private void Hesapla()
        {
            fSatis f = (fSatis)Application.OpenForms["fSatis"];
            double nakit = İslemler.DoubleYap(tNakit.Text);
            double genelToplam = İslemler.DoubleYap(f.tGenelToplam.Text);
            double kart = genelToplam - nakit;
            if (kart > 0)
            {
                f.lNakit.Text = nakit.ToString("C2");
                f.lKart.Text = kart.ToString("C2");
                f.SatisYap("Kart-Nakit");
                f.tGenelToplam.Text = genelToplam.ToString("C2");
                f.Temizle();
                this.Hide();
            }
            else if (kart == 0) { MessageBox.Show("Girdiğiniz nakit değeri toplam tutar ile eşit. Lütfen nakit satış yapınız."); }
            else { MessageBox.Show("Girdiğiniz nakit değeri toplam tutarın üzerinde. Girdiğiniz değerleri kontrol ediniz."); }
        }
        private void bNx_Click(object sender, EventArgs e)
        {
            Guna2Button b = (Guna2Button)sender;
            if (b.Text == ",")
            {
                int virgul = tNakit.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    tNakit.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (tNakit.Text.Length > 0)
                {
                    tNakit.Text = tNakit.Text.Substring(0, tNakit.Text.Length - 1);
                }
            }
            else
            {
                tNakit.Text += b.Text;
            }
        }

        private void bNakit_Click(object sender, EventArgs e)
        {
            if (tNakit.Text != "")
            {
                Hesapla();
            }
        }

        private void tNakit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }
    }
}
