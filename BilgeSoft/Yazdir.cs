using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace BilgeSoft
{
    class Yazdir
    {
        public int? IsleMno { get; set; }
        public int? ZNo { get; set; }
        public Yazdir(int? islemNo,int? zNo)
        {
            IsleMno = islemNo;
            ZNo = zNo;
        }
        PrintDocument pd = new PrintDocument();
        public void YazdirmayaBasla()
        {
            try
            {
                pd.PrintPage += Pd_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            BarkodDbEntities db = new BarkodDbEntities();
            var isYeri = db.Sabit.FirstOrDefault();
            var satisListe = db.Satis.Where(x => x.IslemNo == IsleMno).ToList();
            if (isYeri!=null&&satisListe!=null)
            {
                int kagitUzunluk = 120;
                for (int i = 0; i < satisListe.Count; i++)
                {
                    kagitUzunluk += 15;
                }
                PaperSize ps58 = new PaperSize("58mm termal", 220, kagitUzunluk + 120);
                pd.DefaultPageSettings.PaperSize = ps58;
                Font fontBaslik = new Font("Calibri", 10, FontStyle.Bold);
                Font fontBilgi = new Font("Calibri", 8, FontStyle.Bold);
                Font fontIcerikBaslik = new Font("Calibri", 8, FontStyle.Underline);
                StringFormat ortala = new StringFormat(StringFormatFlags.FitBlackBox);
                ortala.Alignment = StringAlignment.Center;
                RectangleF rcUnvanKonum = new RectangleF(0, 20, 220, 20);
                e.Graphics.DrawString(isYeri.Unvan, fontBaslik, Brushes.Black, rcUnvanKonum,ortala);
                e.Graphics.DrawString("Z No : " + ZNo.ToString(), fontBilgi, Brushes.Black, new Point(5,45));
                e.Graphics.DrawString("İşlem No : " + IsleMno.ToString(), fontBilgi, Brushes.Black, new Point(5, 60));
                e.Graphics.DrawString("Tarih : " + DateTime.Now, fontBilgi, Brushes.Black, new Point(5, 75));
                e.Graphics.DrawString("------------------------------------------------------", fontBilgi,Brushes.Black, new Point(5, 90));
                e.Graphics.DrawString("Ürün Adı", fontIcerikBaslik, Brushes.Black, new Point(5, 105));
                e.Graphics.DrawString("Miktar", fontIcerikBaslik, Brushes.Black, new Point(100, 105));
                e.Graphics.DrawString("Fiyat", fontIcerikBaslik, Brushes.Black, new Point(140, 105));
                e.Graphics.DrawString("Tutar", fontIcerikBaslik, Brushes.Black, new Point(180, 105));
                int yukseklik = 120;
                double genelToplam = 0;
                foreach (var item in satisListe)
                {
                    e.Graphics.DrawString(item.UrunAd, fontBilgi, Brushes.Black, new Point(5, yukseklik));
                    e.Graphics.DrawString(item.Miktar.ToString(), fontBilgi, Brushes.Black, new Point(115, yukseklik));
                    e.Graphics.DrawString(Convert.ToDouble(item.SatisFiyat).ToString("C2"), fontBilgi, Brushes.Black, new Point(140, yukseklik));
                    e.Graphics.DrawString(Convert.ToDouble(item.Toplam).ToString("C2"), fontBilgi, Brushes.Black, new Point(180, yukseklik));
                    yukseklik += 15;
                    genelToplam += Convert.ToDouble(item.Toplam);
                }
                e.Graphics.DrawString("------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5, yukseklik));
                e.Graphics.DrawString("TOPLAM : " + genelToplam.ToString("C2"),fontBaslik,Brushes.Black,new Point(5,yukseklik+20));
                e.Graphics.DrawString("------------------------------------------------------", fontBilgi, Brushes.Black, new Point(5, yukseklik + 40));
                e.Graphics.DrawString("(Mali Değeri Yoktur)", fontBilgi, Brushes.Black, new Point(5, yukseklik + 60));
                
                
            }
        }
    }
}
