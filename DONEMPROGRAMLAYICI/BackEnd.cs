using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

using System.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DONEMPROGRAMLAYICI
{
    internal class BackEnd
    {
        GoogleSQL database = new GoogleSQL();

        public void dosyaOlustur()
        {
            database.ADDTODRIVE(Program.uygulamaDosyaName, 0); // 0->FOLDER
        }

        public void yeniDonemOlustur()
        {
            database.ADDTODRIVE("DONEM-" + (Program.toplamDonemSayisi + 1), 1); // 1->SPREAD
        }



        public bool kurulumDonemOlustur(int donemSayisiGirdi, DateTime donemIlkGun, DateTime finalSonGun)
        {
            for(int i=0; i<donemSayisiGirdi; i++)
            {
                this.yeniDonemOlustur();
                Program.toplamDonemSayisi++;
            }
            database.sonDonemId(); // id yi program.cs içindeki genel değişkene tanımlamak için

            this.SETUP_OF_SPREAD(donemIlkGun, finalSonGun);

            return true;
        }

        string[] uygulamaSheets = { Program.dersProgSheet, Program.dersCizelgeSheet,
                                    Program.sinavTakvimSheet, Program.sinavSonucSheet,
                                     Program.devamDurumuSheet, Program.extraNotSheet, Program.settingsSheet };
        public void SETUP_OF_SPREAD(DateTime donemIlkGun, DateTime finalSonGun)
        {
            foreach (string i in uygulamaSheets)
            {
                database.ADD_NEW_SHEET_TO_SPREAD(i);
            }
            database.DELETE_SHEET_FROM_SPREAD(0); // -> sayfa1 siler sayfa1 oto grid=0 alır


            TimeSpan gunFarkiZamansal = finalSonGun - donemIlkGun;
            int gunFarki = gunFarkiZamansal.Days + 1;

            var dersCizelgeLeftHead = new List<IList<object>> { };
            var headOfDersCizelgeLeftHead = new List<object>() {"HAFTAGUNU", "TARIH"};
            dersCizelgeLeftHead.Add(headOfDersCizelgeLeftHead);
            DateTime donemIlkGunForCizelge = donemIlkGun;
            for (int i = 0; i < gunFarki; i++)
            {
                var satir = new List<object>() { $"=HAFTANINGÜNÜ(B{i + 2};2)", donemIlkGunForCizelge.ToShortDateString() };
                donemIlkGunForCizelge = donemIlkGunForCizelge.AddDays(1);//1 gün ilerletecek
                dersCizelgeLeftHead.Add(satir);
            }
            this.INSERT(Program.dersCizelgeSheet, dersCizelgeLeftHead, "A1:B");
            this.INSERT(Program.devamDurumuSheet, dersCizelgeLeftHead, "A1:B"); //aynısını devam durumundada kullanalım

            string[] sinavTakvimLeftValue =
            {
                "VIZE_TARIH",
                "VIZE_SINIF",
                "FINAL_TARIH",
                "FINAl_SINIF",
                "EXTRA1_AD",
                "EXTRA1_TARIH",
                "EXTRA1_SINIF",
                "EXTRA2_AD",
                "EXTRA2_TARIH",
                "EXTRA2_SINIF",
                "EXTRA3_AD",
                "EXTRA3_TARIH",
                "EXTRA3_SINIF",
                "EXTRA4_AD",
                "EXTRA4_TARIH",
                "EXTRA4_SINIF",
                "EXTRA5_AD",
                "EXTRA5_TARIH",
                "EXTRA5_SINIF",
                "EXTRA_SINAV_SAYISI"
            };
            var sinavTakvimLeftHead = new List<IList<object>> { };
            for(int i=0; i< sinavTakvimLeftValue.Length; i++)
            {
                var satir = new List<object>() { sinavTakvimLeftValue[i] };
                sinavTakvimLeftHead.Add(satir);
            }
            this.INSERT(Program.sinavTakvimSheet, sinavTakvimLeftHead, "A2:A");

            string[] sinavSonucLeftValue =
            {
                "VIZE_NOT",
                "VIZE_ETKISI",
                "VIZE_ORT",
                "FINAL_NOT",
                "FINAL_ETKISI",
                "TAHMINI_FINAL_ORT",
                "EXTRA1_AD",
                "EXTRA1_NOT",
                "EXTRA1_ETKISI",
                "EXTRA1_ORT",
                "EXTRA2_AD",
                "EXTRA2_NOT",
                "EXTRA2_ETKISI",
                "EXTRA2_ORT",
                "EXTRA3_AD",
                "EXTRA3_NOT",
                "EXTRA3_ETKISI",
                "EXTRA3_ORT",
                "EXTRA4_AD",
                "EXTRA4_NOT",
                "EXTRA4_ETKISI",
                "EXTRA4_ORT",
                "EXTRA5_AD",
                "EXTRA5_NOT",
                "EXTRA5_ETKISI",
                "EXTRA5_ORT",
                "EXTRA_DEGERLENDIRME_SAYISI"
            };
            var sinavSonucLeftHead = new List<IList<object>> { };
            for(int i=0; i<sinavSonucLeftValue.Length; i++)
            {
                var satir = new List<object>() { sinavSonucLeftValue[i] };
                sinavSonucLeftHead.Add(satir);
            }
            this.INSERT(Program.sinavSonucSheet, sinavSonucLeftHead, "A2:A");


            var dersProgHeadSatir = new List<object>() {
                                                         "DERS_TAM_AD",
                                                         "KISA_AD",
                                                         "DERS_GUNU",
                                                         "SINIF",
                                                         "ARA_SAYISI",
                                                         "TAHMINI_ARA_SURESI(dk)",
                                                         "EN_ERKEN_BASLANGIC(24h->dk)",
                                                         "BASLANGIC_SAAT",
                                                         "BASLANGIC_DAKIKA",
                                                         "EN_SON_BITIS(24h->dk)",
                                                         "BITIS_SAAT",
                                                         "BITIS_DAKIKA",
                                                         "HOCASI",
                                                         "HOCA_YOKLAMA_STILI",
                                                         "DEVAM_ZORUNLULUK",
                                                         "DERS_COLOR_INDEX",
                                                         "DERS_AKTS",
                                                         "EK_DERS",
                                                         "SUBE",
                                                         "DEVAMSIZLIK_YUZDESI",
                                                         "BLOK_YOKLAMA",
                                                         "DERS_SAYISI",
                                                         "HOCA_GEC_GIRENE_TEPKI"
                                                       };
            this.INSERT(Program.dersProgSheet, dersProgHeadSatir, "A1:1");


            var extraNotHead = new List<object>() { "NO", "GIRDI_TARIH", "NOT", "NOT_BASLIK" };
            this.INSERT(Program.extraNotSheet, extraNotHead, "A1:1");


            var settingsTaslak = new List<IList<object>> { };
            settingsTaslak.Add(new List<object>() { "TOTAL_NOT_SAYISI", "->", "=BAĞ_DEĞ_DOLU_SAY(ExtraNot!A2:A)" });
            settingsTaslak.Add(new List<object>() { "TOTAL_DERS_SAYISI", "->", "=BAĞ_DEĞ_DOLU_SAY(DersProg!A2:A)" });
            settingsTaslak.Add(new List<object>() { "DARK_MODE_LEVEL", "->", "0" });
            settingsTaslak.Add(new List<object>() { "GOZ_SAGLIGI", "->", "0" });
            settingsTaslak.Add(new List<object>() {});
            settingsTaslak.Add(new List<object>() { "DONEM_GUN_SAYISI", "->", "=BAĞ_DEĞ_DOLU_SAY(DersCizelge!A2:A)" });
            settingsTaslak.Add(new List<object>() {});
            settingsTaslak.Add(new List<object>() { "DERSPROG_CMTS_OPEN", "->", "1" });
            settingsTaslak.Add(new List<object>() { "DERSPROG_PZR_OPEN", "->", "1" });
            settingsTaslak.Add(new List<object>() { "DERSPROG_ILK_SAAT", "->", "=DÜŞEYARA(B19;DersProg!G2:I;2;0)" });
            settingsTaslak.Add(new List<object>() { "DERSPROG_ILK_DK",   "->", "=DÜŞEYARA(B19;DersProg!G2:I;3;0)" });
            settingsTaslak.Add(new List<object>() { "DERSPROG_SON_SAAT", "->", "=DÜŞEYARA(B20;DersProg!J2:L;2;0)" });
            settingsTaslak.Add(new List<object>() { "DERSPROG_SON_DK",   "->", "=DÜŞEYARA(B20;DersProg!J2:L;3;0)" });
            settingsTaslak.Add(new List<object>() {});
            settingsTaslak.Add(new List<object>() {});
            settingsTaslak.Add(new List<object>() { "!DERSPROG_ILKDERS_24HTODK", "=MİN(DersProg!G2:G)" });
            settingsTaslak.Add(new List<object>() { "!DERSPROG_ILKDERS_24HTODK", "=MAK(DersProg!J2:J)" });
            this.INSERT(Program.settingsSheet, settingsTaslak, "A4:C");


        }

        //var tekSatir = new List<object>() { "a", "b", "c", "d", }; //ÖRNEK
        public void INSERT(string sheetName, List<object> tekSatir, string range)
        {
            var eklenenSatirlar = new List<IList<object>> { };
            eklenenSatirlar.Add(tekSatir);

            database.INSERT(sheetName, eklenenSatirlar, range);
        }

        public void INSERT(string sheetName, List<IList<object>> eklenenSatirlar, string range)
        {
            database.INSERT(sheetName, eklenenSatirlar, range);
        }

        public void UPDATE(string sheetName, List<object> tekSatir, string range)
        {
            var eklenenSatirlar = new List<IList<object>> { };
            eklenenSatirlar.Add(tekSatir);

            database.UPDATE(sheetName, eklenenSatirlar, range);
        }

        public void UPDATE(string sheetName, List<IList<object>> eklenenSatirlar, string range)
        {
            database.UPDATE(sheetName, eklenenSatirlar, range);
        }


        public IList<IList<object>> READCELL(string sheetName, string aralik)
        {
            IList<IList<object>> veriAxBformat = database.READ(sheetName, aralik);
            return veriAxBformat;
        }

        public void dersProgVeri()
        {
            database.dersProgVeriCek();
        }
        public void dersCizelgeVeri()
        {
            database.dersCizelgeVeriCek();
        }

        public void settingsVeri()
        {
            database.settingsVeriCek();
        }
        public void devamDurumuVeri()
        {
            database.devamDurumuVeriCek();
        }
        public void extraNotVeri()
        {
            database.extraNotVeriCek();
        }
        public void sinavTavkimVeri()
        {
            database.sinavTakvimVeriCek();
        }
        public void sinavSonucVeri()
        {
            database.sinavSonucVeriCek();
        }


        public System.Drawing.Color[] dersRenkleri =
        {
            System.Drawing.Color.FromArgb(110, 104, 118),
            System.Drawing.Color.FromArgb(54, 80, 113),
            System.Drawing.Color.FromArgb(38, 70, 83),
            System.Drawing.Color.FromArgb(182, 101, 118),
            System.Drawing.Color.FromArgb(109, 90, 122),
            System.Drawing.Color.FromArgb(144, 112, 1),
            System.Drawing.Color.FromArgb(160, 65, 9),
            System.Drawing.Color.FromArgb(80, 98, 98),
            System.Drawing.Color.FromArgb(1, 98, 117),
            System.Drawing.Color.FromArgb(167, 64, 78)
        };
        public System.Drawing.Color[] fontColor =
        {
            System.Drawing.Color.Black,
            System.Drawing.Color.White,
            System.Drawing.Color.FromArgb(253, 244, 220)
        };
        /// head unuttuk sıkıntı çıkarıyor head yazımı elle - olarak kaydedeceğiz
        public void programaDersEkle(Panel gelenPanel, string dersAdi, 
                                    int baslangicSaat, int baslangicDakika, 
                                    int bitisSaat, int bitisDakika,
                                    byte backColorIndex, string sinif)
        {
            ///
            /// 
            ///
            ///ders paneli
            ///ilk ders saat 6:30
            ///son ders bitiş saat gün sonu
            ///toplam 17,5 saat 1050 dk
            ///
            ///panel.height / 1050 = dk başı pixel(dbpx)
            ///baslangic saati ilk ders saati farkı dk ya çevir örnek saat 9-> 9 - 6,5 = 2,5-> 2,5 * 60 = 150 dk
            ///dbpx *150 = gelen ders saati baslangic pixeli
            ///bitis saati baslangic saati ile farkı dk ya çevir örnek saat 10-> 10 - 9 = 1-> 1 * 60 = 60dk
            /// dbpx *60 = gelen ders saati bitiş pixeli
            /// 
            ///
           
            
            int headHeight = 39; // @@@@@@@@@@@@@ DİKKAT @@@@@@@@@@@@
            int gunDakikaFarki = ((Settings.dersProgSonBitenDersSaati - Settings.dersProgIlkBaslayanDersSaati) * 60)
                                    + (Settings.dersProgSonBitenDersDakika - Settings.dersProgIlkBaslayanDersDakika);
            double dbpx = (double)(gelenPanel.Height-headHeight) / (double)gunDakikaFarki;
            
            int baslangicPx = (int)(dbpx * (((baslangicSaat - Settings.dersProgIlkBaslayanDersSaati) *60)
                                            + (baslangicDakika-Settings.dersProgIlkBaslayanDersDakika)));
            // DersKaç Dakika Sürecek
            int bitisPx = (int)(dbpx * (((bitisSaat - baslangicSaat)*60)
                                        + (bitisDakika-baslangicDakika)));
            //Debug.WriteLine("Başlangıç:BitixPx -> " + baslangicPx + ":" + bitisPx + "   dbpx-> " + dbpx); //TEST
            Panel p = new Panel();


            p.BackColor = dersRenkleri[backColorIndex];
            p.Location = new System.Drawing.Point(10, baslangicPx+headHeight);//yordu beni -> gelenPanel.Location.X + 10
            p.Margin = new System.Windows.Forms.Padding(2);
            p.Size = new System.Drawing.Size(gelenPanel.Width - 20, bitisPx);



            ////////////////  ÇALIŞMALARA DEVAM  /////////////// hala hata var
            Label tester = new Label();
            tester.Text = dersAdi;

            int maxLenght = 8;
            if (tester.Width<p.Width+10 || dersAdi.Length<maxLenght)//p.witdh-10 -> izin verilen en yüksek genişlik for lbl
            maxLenght = dersAdi.Length;


            Label lbl = new Label();
            string startSaat = ""+baslangicSaat; if (baslangicSaat < 10) startSaat = "0" + startSaat;
            string startDakika = "" + baslangicDakika; if (baslangicDakika < 10) startDakika = "0" + startDakika;
            string finishSaat = "" + bitisSaat; if (bitisSaat < 10) finishSaat = "0" + finishSaat;
            string finishDakika = "" + bitisDakika; if (bitisDakika < 10) finishDakika = "0" + finishDakika;
            lbl.Text = startSaat + ":"+ startDakika + "\r\n"
                                                +dersAdi.Substring(0,maxLenght)+"\r\n"
                                                + finishSaat + ":"+ finishDakika + " - "+sinif;
            lbl.MaximumSize = new System.Drawing.Size(p.Width-10, 0);
            lbl.Size = new System.Drawing.Size(p.Width - 10, 45);
            lbl.Location = new System.Drawing.Point(((p.Width-lbl.Width)/2), ((p.Height-lbl.Height)/2));
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.ForeColor = System.Drawing.Color.White;

            p.Controls.Add(lbl);
            gelenPanel.Controls.Add(p);


        }








    }
}
