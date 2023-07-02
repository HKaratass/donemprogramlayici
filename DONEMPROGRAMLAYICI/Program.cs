using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

using System.Diagnostics;



namespace DONEMPROGRAMLAYICI
{

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        static public string appName = "Donem Programlayici";
        static public string clientId = "<<secret>>";
        static public string clientSecret = "<<secret>>";
        static public string uygulamaDosyaName = "DONEMPROGRAMLAYICI";

        static public SheetsService? sheetsService; // ? otomatik null alabilecek ilan uyarý vermemesi için
        static public DriveService? driveService;
        static public string? uygulamaDosyaId = null;
        static public string? sonDonemId = null;
        static public int toplamDonemSayisi = 0;

        static public string sayfa1Sheet = "Sayfa1"; static public int sayfa1SheetID = 0;
        static public string dersProgSheet = "DersProg"; static public int dersProgSheetID = 1;
        static public string dersCizelgeSheet = "DersCizelge"; static public int dersCizelgeSheetID = 2;
        static public string sinavTakvimSheet = "SinavTakvim"; static public int sinavTakvimSheetID = 3;
        static public string sinavSonucSheet = "SinavSonuc"; static public int sinavSonucSheetID = 4;
        static public string extraNotSheet = "ExtraNot"; static public int extraNotSheetID = 5;
        static public string devamDurumuSheet = "DevamDurumu"; static public int devamDurumuSheetID = 6;
        static public string settingsSheet = "Settings"; static public int settingsSheetID = 7;


        [STAThread]
        static void Main()
        {
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new GirisForm());
            //Application.Run(new FirstUse());
            //Application.Run(new MainForm());
        }



    }
}