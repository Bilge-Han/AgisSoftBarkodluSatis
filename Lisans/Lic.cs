using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace Lisans
{
    public class Lic
    {
        public string gunKarakter = "NDSAOIFQWHNCDLSŞAEHIOWAVNDSALVCNEWAIOCVNLASŞCNEAWIOCNDASLKTRWPOA";
        public string ayKarakter = "ZXCASDMGJLKIEWQDASFDWQCSACXZUYOYLRJK";
        public string yilKarakter = "DFJSAFDTJWEVCDNASFSAFSDAXZLMNG";
        public string CpuNo()
        {
            string cpuId = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From WIN32_Processor");
            ManagementObjectCollection objects = searcher.Get();
            foreach (ManagementObject item in objects)
            {
                cpuId = item["ProcessorId"].ToString();
            }
            return cpuId;
        }

        // Cpu numarasının ascıı karşılığını alıp toplayıp sayı elde etme
        public int CpuCharTotal()
        {
            int toplam = 0;
            foreach (char item in CpuNo().ToCharArray())
            {
                toplam += (int)item;
            }
            return toplam;
        }

        //0- hatalı şifre, 1-demo , 2-yıllık
        public int LisansNoKontrolEt(string girilenLisans)
        {
            Cursor.Current = Cursors.WaitCursor;

            double girilenLisansNo = Convert.ToDouble(girilenLisans);
            //3 ve 13 opsiyonel bize kalmış, demo ve yıllık ayırt edebilmek için
            bool demoMu = girilenLisansNo == CpuCharTotal() * TarihFonk() * 3;
            bool yillikMi = girilenLisansNo == CpuCharTotal() * TarihFonk() * 13;
            int durum = 0;
            if (demoMu)
            {
                durum = 1; // demo kurulumu için
            }
            else if (yillikMi)
            {
                durum = 2;
            }
            else
            {
                durum = 0;
            }
            Cursor.Current = Cursors.Default;
            return durum;
        }

        public bool TarihKontrol(DateTime baslangic, DateTime bitis)
        {
            return baslangic < DateTime.Now && DateTime.Now < bitis;
        }
        public DateTime DemoTarihiOlustur()
        {
            DateTime tarih = DateTime.Now.AddDays(10);
            return tarih;
        }
        public DateTime YillikTarihiOlustur()
        {
            DateTime tarih = DateTime.Now.AddYears(1);
            return tarih;
        }

        public DateTime TarihCoz(string sifreliTarih)
        {
            string strGun = sifreliTarih.Substring(0, 2);
            string strAy = sifreliTarih.Substring(2, 2);
            string strYil = sifreliTarih.Substring(4, 2);

            int gun = Gunler().Where(x => x.Ad == strGun).FirstOrDefault().Numara;
            int ay = Aylar().Where(x => x.Ad == strAy).FirstOrDefault().Numara;
            int yil = Yillar().Where(x => x.Ad == strYil).FirstOrDefault().Numara;

            DateTime tarih = new DateTime(yil, ay, gun);
            return tarih;
        }
        public string TarihSifrele(DateTime tarih)
        {
            int gun = tarih.Day;
            int ay = tarih.Month;
            int yil = tarih.Year;

            string strGun = Gunler().Where(x => x.Numara == gun).FirstOrDefault().Ad;
            string strAy = Aylar().Where(x => x.Numara == ay).FirstOrDefault().Ad;
            string strYil = Yillar().Where(x => x.Numara == yil).FirstOrDefault().Ad;

            string sifreliTarih = strGun + strAy + strYil;
            return sifreliTarih;
        }
        public class Sablon
        {
            public int Numara { get; set; }
            public string Ad { get; set; }
        }
        public List<Sablon> Gunler()
        {
            List<Sablon> listGun = new List<Sablon>();
            for (int i = 0; i < 31; i++)
            {
                listGun.Add(new Sablon { Numara = i + 1, Ad = gunKarakter.ToString().Substring(i * 2, 2) });
            }
            return listGun;
        }
        public List<Sablon> Aylar()
        {
            List<Sablon> listAy = new List<Sablon>();
            for (int i = 0; i < 12; i++)
            {
                listAy.Add(new Sablon { Numara = i + 1, Ad = ayKarakter.ToString().Substring(i * 2, 2) });
            }
            return listAy;
        }
        public List<Sablon> Yillar()
        {
            List<Sablon> listYil = new List<Sablon>();
            int karakterSayisi = 0;
            for (int i = 2023; i < 2034; i++)
            {
                listYil.Add(new Sablon { Numara = i, Ad = yilKarakter.ToString().Substring(karakterSayisi, 2) });
                karakterSayisi += 2;
            }
            return listYil;
        }

        public long EkrandaGoster()
        {
            long kontrolNo = CpuCharTotal() * TarihFonk();
            return kontrolNo;
        }

        // bize o tarihte mi o numarayı vermiş pc, 
        // tarihin günü + ayı * yıl yapalım
        // karşı taraf tarihle engellemiş olmasını engellemek için
        public long TarihFonk()
        {
            return (DateTime.Now.Day + DateTime.Now.Month) * DateTime.Now.Year;
        }
    }
}
