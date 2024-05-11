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
    public partial class fBaslangic : Form
    {
        public fBaslangic()
        {
            InitializeComponent();
        }

        private void bSatisIslemi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fSatis f = new fSatis();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bGenelRapor_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fRapor f = new fRapor();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bStok_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fStok f = new fStok();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bUrunGiris_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fUrunGiris f = new fUrunGiris();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bFiyatGuncelle_Click(object sender, EventArgs e)
        {
            fFiyatGuncelle f = new fFiyatGuncelle();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
        }

        private void bAyarlar_Click(object sender, EventArgs e)
        {
            fAyarlar f = new fAyarlar();
            f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
        }

        private void bYedekleme_Click(object sender, EventArgs e)
        {
            İslemler.BackUp();
        }

        private void bKullaniciDegistir_Click(object sender, EventArgs e)
        {
            fLogin login = new fLogin();
            login.Show();
            this.Hide();
        }

        private void bMusteriToptanciEkle_Click(object sender, EventArgs e)
        {
            fMusteriToptantiEkle f = new fMusteriToptantiEkle();
            //f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
        }

        private void bToptanciRapor_Click(object sender, EventArgs e)
        {
            fToptanciRapor f = new fToptanciRapor();
            //f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
        }

        private void bMusteriRapor_Click(object sender, EventArgs e)
        {
            fMusteriRapor f = new fMusteriRapor();
            //f.lKullanici.Text = lKullanici.Text;
            f.ShowDialog();
        }
    }
}
