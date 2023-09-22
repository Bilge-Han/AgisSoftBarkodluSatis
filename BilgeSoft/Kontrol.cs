using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lisans;
namespace BilgeSoft
{
    public class Kontrol
    {
        BarkodDbEntities db = new BarkodDbEntities();
        Lic lic = new Lic();
        Guvenlik guvenlik = new Guvenlik();
        public bool KontrolYap()
        {
            bool durum = false;
            if (!db.Guvenlik.Any())
            {
                LisansFormAc();
            }
            else
            {
                var guvenlik = db.Guvenlik.First();
                // başlangıç tarihi bugünden önce mi?
                if (lic.TarihCoz(guvenlik.baslangic) < DateTime.Now)
                {
                    // tarihi geriye almaları engellemek için başlangıc tarihini bugünün tarihi olarak tekrar atıyorum
                    guvenlik.baslangic = lic.TarihSifrele(DateTime.Now);
                    db.SaveChanges();
                    durum = true;
                }
                // bugünün tarihi başlangıç ve bitiş tarihi arasında mı?
                if (lic.TarihKontrol(lic.TarihCoz(guvenlik.baslangic), lic.TarihCoz(guvenlik.bitis)))
                {
                    durum = true;
                }
                else
                {
                    db.Guvenlik.Remove(guvenlik);
                    db.SaveChanges();
                    durum = false;
                    LisansFormAc();
                }
            }
            return durum;
        }
        public void LisansFormAc()
        {
            fLisans f = new fLisans();
            f.lKontrolNo.Text = lic.EkrandaGoster().ToString();
            f.Show();
        }

        public void Lisanla(string girilenLisansKod)
        {
            int durum = lic.LisansNoKontrolEt(girilenLisansKod);
            switch (durum)
            {
                case 0: //Geçersiz lisan kodu
                    System.Windows.Forms.MessageBox.Show("Geçersiz lisans numarası");
                    break;
                case 1:
                    DemoOlustur();
                    break;
                case 2:
                    YillikOlustur();
                    break;
                default:
                    break;
            }
        }

        private int GuvenlikEkliMi()
        {
            return db.Guvenlik.Count();
        }
        private void GuvenlikEkle(string baslangic, string bitis)
        {
            guvenlik.baslangic = baslangic;
            guvenlik.bitis = bitis;
            db.Guvenlik.Add(guvenlik);
            db.SaveChanges();
        }
        private void GuvenlikGuncelle(string baslangic, string bitis)
        {
            guvenlik.baslangic = baslangic;
            guvenlik.bitis = bitis;
            db.SaveChanges();
        }
        private void DemoOlustur()
        {
            try
            {
                if (GuvenlikEkliMi() == 0)
                {
                    //db ekleme işlemi, bugünün tarihi ile 10 gün sonranın tarihi
                    GuvenlikEkle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.DemoTarihiOlustur()));
                }
                else
                {
                    //db güncelleme işlemi
                    GuvenlikGuncelle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.DemoTarihiOlustur()));
                }
                MessageBox.Show("Program 10 günlük demo olarak kullanıma açılmıştır \n Programı Yeniden Başlatınız.");
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("HATA OLUŞTU");
            }

        }
        private void YillikOlustur()
        {
            try
            {
                if (GuvenlikEkliMi() == 0)
                {
                    //db ekleme işlemi, bugünün tarihi ile 1 yıl sonranın tarihi
                    GuvenlikEkle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.YillikTarihiOlustur()));
                }
                else
                {
                    //db güncelleme işlemi
                    GuvenlikGuncelle(lic.TarihSifrele(DateTime.Now), lic.TarihSifrele(lic.YillikTarihiOlustur()));
                }

                MessageBox.Show("Program 1 yıllık olarak kullanıma açılmıştır \n Programı Yeniden Başlatınız.");
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("HATA OLUŞTU");
            }

        }
    }
}
