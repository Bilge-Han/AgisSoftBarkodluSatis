using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeSoft
{
   static class Raporlar
    {
        public static string Baslik { get; set; }
        public static string TarihBaslangic { get; set; }
        public static string TarihBitis { get; set; }
        public static string SatisNakit { get; set; }
        public static string SatisKart { get; set; }
        public static string IadeNakit { get; set; }
        public static string IadeKart { get; set; }
        public static string GelirNakit { get; set; }
        public static string GelirKart { get; set; }
        public static string GiderNakit { get; set; }
        public static string GiderKart { get; set; }
        public static string KdvToplam { get; set; }
        public static string KartKomisyon { get; set; }

        public static void RaporSayfasiRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<IslemOzet> list = new List<IslemOzet>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new IslemOzet { 
                IslemNo=Convert.ToInt32(dgv.Rows[i].Cells["IslemNo"].Value.ToString()),
                Iade=Convert.ToBoolean(dgv.Rows[i].Cells["Iade"].Value),
                OdemeSekli= dgv.Rows[i].Cells["OdemeSekli"].Value.ToString(),
                Nakit=İslemler.DoubleYap(dgv.Rows[i].Cells["Nakit"].Value.ToString()),
                Kart=İslemler.DoubleYap(dgv.Rows[i].Cells["Kart"].Value.ToString()),
                Gelir=Convert.ToBoolean(dgv.Rows[i].Cells["Gelir"].Value.ToString()),
                Gider=Convert.ToBoolean(dgv.Rows[i].Cells["Gider"].Value.ToString()),
                AlisFiyatToplam=İslemler.DoubleYap( dgv.Rows[i].Cells["AlisFiyatToplam"].Value.ToString()),
                Aciklama=dgv.Rows[i].Cells["Aciklama"].Value.ToString(),
                Tarih=Convert.ToDateTime(dgv.Rows[i].Cells["Tarih"].Value.ToString()),
                Kullanici=dgv.Rows[i].Cells["Kullanici"].Value.ToString()

                });
            }
            ReportDataSource rs = new ReportDataSource();
            rs.Name = "dsGenelRapor";
            rs.Value = list;
            fRaporGoster f = new fRaporGoster();
            f.reportViewer1.LocalReport.DataSources.Clear();
            f.reportViewer1.LocalReport.DataSources.Add(rs);
            f.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpGenelRapor.rdlc";

            ReportParameter[] rPrm = new ReportParameter[13];
            rPrm[0] = new ReportParameter("Baslik", Baslik);
            rPrm[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            rPrm[2] = new ReportParameter("TarihBitis", TarihBitis);
            rPrm[3] = new ReportParameter("SatisNakit", SatisNakit);
            rPrm[4] = new ReportParameter("SatisKart", SatisKart);
            rPrm[5] = new ReportParameter("IadeNakit", IadeNakit);
            rPrm[6] = new ReportParameter("IadeKart", IadeKart);
            rPrm[7] = new ReportParameter("GelirNakit", GelirNakit);
            rPrm[8] = new ReportParameter("GelirKart", GelirKart);
            rPrm[9] = new ReportParameter("GiderNakit", GiderNakit);
            rPrm[10] = new ReportParameter("GiderKart", GiderKart);
            rPrm[11] = new ReportParameter("KdvToplam", KdvToplam);
            rPrm[12] = new ReportParameter("KartKomisyon", KartKomisyon);
            f.reportViewer1.LocalReport.SetParameters(rPrm);
            f.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            f.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            f.ShowDialog();

            Cursor.Current = Cursors.Default;
        }


        public static void StokRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<Urun> list = new List<Urun>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new Urun
                {
                    Barkod= dgv.Rows[i].Cells["Barkod"].Value.ToString(),
                    UrunAd= dgv.Rows[i].Cells["UrunAd"].Value.ToString(),
                    UrunGrup = dgv.Rows[i].Cells["UrunGrup"].Value.ToString(),
                    Birim = dgv.Rows[i].Cells["Birim"].Value.ToString(),
                    SatisFiyat=İslemler.DoubleYap(dgv.Rows[i].Cells["SatisFiyat"].Value.ToString()),
                    Miktar=İslemler.DoubleYap(dgv.Rows[i].Cells["Miktar"].Value.ToString()),
                    Aciklama = dgv.Rows[i].Cells["Aciklama"].Value.ToString(),
                });
            }
            ReportDataSource rs = new ReportDataSource();
            rs.Name = "dsStokUrun";
            rs.Value = list;
            fRaporGoster f = new fRaporGoster();
            f.reportViewer1.LocalReport.DataSources.Clear();
            f.reportViewer1.LocalReport.DataSources.Add(rs);
            f.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpStokUrun.rdlc";

            ReportParameter[] rPrm = new ReportParameter[3];
            rPrm[0] = new ReportParameter("Baslik", Baslik);
            rPrm[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            rPrm[2] = new ReportParameter("TarihBitis", TarihBitis);
            f.reportViewer1.LocalReport.SetParameters(rPrm);
            f.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            f.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            f.ShowDialog();

            Cursor.Current = Cursors.Default;
        }

        public static void StokIzlemeRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<StokHareket> list = new List<StokHareket>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new StokHareket
                {
                    Barkod = dgv.Rows[i].Cells["Barkod"].Value.ToString(),
                    UrunAd = dgv.Rows[i].Cells["UrunAd"].Value.ToString(),
                    UrunGrup = dgv.Rows[i].Cells["UrunGrup"].Value.ToString(),
                    Birim = dgv.Rows[i].Cells["Birim"].Value.ToString(),
                    Miktar = İslemler.DoubleYap(dgv.Rows[i].Cells["Miktar"].Value.ToString()),
                    Kullanici = dgv.Rows[i].Cells["Kullanici"].Value.ToString(),
                    Tarih=Convert.ToDateTime( dgv.Rows[i].Cells["Tarih"].Value.ToString()),
                });
            }
            ReportDataSource rs = new ReportDataSource();
            rs.Name = "dsStokIzleme";
            rs.Value = list;
            fRaporGoster f = new fRaporGoster();
            f.reportViewer1.LocalReport.DataSources.Clear();
            f.reportViewer1.LocalReport.DataSources.Add(rs);
            f.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpStokIzleme.rdlc";

            ReportParameter[] rPrm = new ReportParameter[3];
            rPrm[0] = new ReportParameter("Baslik", Baslik);
            rPrm[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            rPrm[2] = new ReportParameter("TarihBitis", TarihBitis);
            f.reportViewer1.LocalReport.SetParameters(rPrm);
            f.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            f.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            f.ShowDialog();

            Cursor.Current = Cursors.Default;
        }
    }
}
