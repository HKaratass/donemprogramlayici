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
    internal class EskiKodlar
    {

        ////////////    ESKİ İŞLEVSİZ KODLAR - MAİN FORM       ///////////////////////////

        //////////////  MAİN FRAME BAŞI ->  PROGRAM.CS TAŞINDI     //////////////
        //string appName = "Donem Programlayici";
        ////string clientId = "223354323516-b78v4l3hjkd4nsk51vvtatbiva3a53oj.apps.googleusercontent.com";
        //string clientId = Program.clientId;
        //string clientSecret = "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";
        //string uygulamaDosyaName = "DONEMPROGRAMLAYICI";

        //SheetsService? sheetsService; // ? otomatik null alabilecek ilan uyarı vermemesi için
        //DriveService? driveService;
        //string? uygulamaDosyaId = null;
        //string? sonDonemId = null;
        //int? toplamDonemSayisi = null;



        //////////  KULLANIMA GEREK KALMADI - FORMLAR ARASI BAĞ    ////////
        //private void hesabaBaglan()
        //{
        //    string[] scopes = new string[] {DriveService.Scope.Drive,
        //                                    DriveService.Scope.DriveFile,
        //                                    SheetsService.Scope.Spreadsheets};

        //    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
        //    {
        //        ClientId = clientId,
        //        ClientSecret = clientSecret
        //    }, scopes,
        //    Environment.UserName, CancellationToken.None, new FileDataStore("DonemProgramlayiciToken")).Result;

        //    driveService = new DriveService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = appName,

        //    });

        //    sheetsService = new SheetsService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = appName,

        //    });

        //    uygulamaDosyaIdCheck(); //hesaba bağlanır bağlanmaz dosya id sini çekerek uygulama dosyasına giriş yapıyor
        //                                // dosya yoksa null atayarak uygulama ona göre şekil alacak
        //    if(uygulamaDosyaId != null)
        //    {
        //        sonDonemSorgula();
        //    }
        //}



        /////////   MAİNFRAME->BACKEND+GOOGLE SQL  olarak taşındı  ////////////
        //public void uygulamaDosyaIdCheck()
        //{
        //    string? pageToken = null;
        //    string query = "name = \"" + Program.uygulamaDosyaName + "\"" + " and " +
        //                   "mimeType = \"application/vnd.google-apps.folder\"";
        //    var driveDosyalar = Program.driveService.Files.List(); //request
        //    driveDosyalar.Q = query;
        //    driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
        //    driveDosyalar.PageToken = pageToken;

        //    var result = driveDosyalar.Execute(); // sorgu sonuçları
        //    //sysout("BULUNAN DOSYA SAYISI = " + result.Files.Count); //TEST
        //    if (result.Files.Count > 0)
        //    {
        //        foreach (Google.Apis.Drive.v3.Data.File bulunanDosya in result.Files)
        //        {
        //            Program.uygulamaDosyaId = bulunanDosya.Id;
        //        }
        //    }
        //    else
        //    {
        //        Program.uygulamaDosyaId = null;
        //    }
        //    //sysout("DOSYA ID: " + uygulamaDosyaId); //TEST

        //}




        ///// MAİN FRAME -> BACK END + GOOGLE SQL uzun yapıdan fonksiyonel yapıya donüştürüldü //////////
        //public void sonDonemSorgula()
        //{
        //    try
        //    {
        //        string? pageToken = null;
        //        string query = "name contains \"" + "DONEM" + "\"" + " and " +
        //                       "mimeType = \"application/vnd.google-apps.spreadsheet\"" + " and " +
        //                       "\"" + Program.uygulamaDosyaId + "\" in parents";
        //        var driveDosyalar = Program.driveService.Files.List(); //request
        //        driveDosyalar.Q = query;
        //        driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
        //        driveDosyalar.PageToken = pageToken;

        //        // var result
        //        Google.Apis.Drive.v3.Data.FileList result = driveDosyalar.Execute(); // sorgu sonuçları

        //        Program.toplamDonemSayisi = result.Files.Count;
        //        //Debug.WriteLine("TOPLAM DONEM SAYISI = " + toplamDonemSayisi); //TEST YAZIMI

        //        query = "name contains \"" + "DONEM-" + Program.toplamDonemSayisi + "\"" + " and " +
        //                "mimeType = \"application/vnd.google-apps.spreadsheet\"" + " and " +
        //                "\"" + Program.uygulamaDosyaId + "\" in parents";
        //        driveDosyalar = Program.driveService.Files.List();
        //        driveDosyalar.Q = query;
        //        driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
        //        driveDosyalar.PageToken = pageToken;
        //        result = driveDosyalar.Execute();
        //        //sysout("RESULT TİPİ = " + result.GetType()); // TEST tip öğrenme

        //        foreach (Google.Apis.Drive.v3.Data.File bulunanExcel in result.Files)
        //        {
        //            string bulunanExcelID = bulunanExcel.Id;
        //            Program.sonDonemId = bulunanExcelID;
        //            Debug.WriteLine("Bulunan Dosya ID = " + bulunanExcelID); //TEST YAZIMI
        //            //return bulunanExcelID;

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("HATA:" + ex);
        //    }

        //    //return "0";
        //}



















        //////////   ESKİ RECURSİVE DONEM SORGULAMA - MAİN FRAME  //////////////
        //int donemNo;
        //private string sonDonemSorgula(string dosyaAdi, string parentFolder) //ESKİ İLK YAPTIĞIM UZUN SÜRÜYOR
        //{
        //    string recursiveReturn = ""; //iç içe arama yapınca bulunan değeri ilk fonksiyona kadar taşıyacak
        //    try
        //    {
        //        string? pageToken = null;
        //        string query = "name = \"" + dosyaAdi + "\"" + " and " +
        //                       "mimeType = \"application/vnd.google-apps.spreadsheet\"" + " and " +
        //                       "\"" + parentFolder + "\" in parents";
        //        var driveDosyalar = Program.driveService.Files.List(); //request
        //        driveDosyalar.Q = query;
        //        driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
        //        driveDosyalar.PageToken = pageToken;

        //        var result = driveDosyalar.Execute(); // sorgu sonuçları

        //        if (result.Files.Count > 0) //dönem sheet daha önce oluşturulmuş mu ? 0->oluşturulmamış; 1->oluşturulmuş
        //        {
        //            foreach (Google.Apis.Drive.v3.Data.File bulunanExcel in result.Files)
        //            {
        //                string bulunanExcelD = bulunanExcel.Id;
        //                Debug.WriteLine("Bulunan Dosya ID = " + bulunanExcelD);
        //                return bulunanExcelD;

        //            }
        //        }
        //        else
        //        {
        //            donemNo--;
        //            string yeniDonemDosyaAdi = "DONEM-" + donemNo;
        //            if (donemNo == 0) return "0";
        //            recursiveReturn = sonDonemSorgula(yeniDonemDosyaAdi, parentFolder);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("HATA:" + ex);
        //    }

        //    return recursiveReturn;
        //}






        // MAİNFORM FONKSİYONU İÇİNDE derssayisi != 0 if in içinde yer alan
        // tümleşik kod bunun yerine bölünerek sol panel butonlarına atandı
        //
        //for (int i = 0; i < Settings.dersSayisi + 1; i++) //tarih satırı dahil
        //{
        //    var sutun = new DataGridViewTextBoxColumn();
        //    sutun.HeaderText = GoogleSQL.dersCizelgeVeri[0][i + 1].ToString();
        //    sutun.MinimumWidth = 8;
        //    sutun.ReadOnly = true;
        //    sutun.Width = 150;
        //    sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    var sutunDevam = new DataGridViewTextBoxColumn();
        //    sutunDevam.HeaderText = GoogleSQL.dersCizelgeVeri[0][i + 1].ToString();
        //    sutunDevam.MinimumWidth = 8;
        //    sutunDevam.ReadOnly = true;
        //    sutunDevam.Width = 150;
        //    sutunDevam.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    dersCizelge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { sutun });
        //    devamsizlikCizelge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { sutunDevam });

        //    for (int j = 0; j < Settings.donemGunSayisi; j++)
        //    {
        //        if (dersCizelge.Rows.Count != Settings.donemGunSayisi) dersCizelge.Rows.Add();
        //        dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGray;


        //        if (i > 0)
        //        {
        //            dersCizelge.Rows[j].Cells[i].Style.BackColor = backEnd.renkler[Convert.ToByte(GoogleSQL.dersProgVeri[i][15])];
        //        }
        //        dersCizelge.Rows[j].Cells[i].Value = GoogleSQL.dersCizelgeVeri[j + 1][i + 1].ToString();
        //        if (GoogleSQL.dersCizelgeVeri[j + 1][i + 1].Equals("!EMPTY"))
        //        {
        //            dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Black;
        //            dersCizelge.Rows[j].Cells[i].Value = "";
        //        }

        //        if (devamsizlikCizelge.Rows.Count != Settings.donemGunSayisi) devamsizlikCizelge.Rows.Add();
        //        devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGray;
        //        if (i == 0) devamsizlikCizelge.Rows[j].Cells[i].Value = GoogleSQL.devamDurumuVeri[j + 1][i + 1].ToString();

        //        if (GoogleSQL.dersCizelgeVeri[j + 1][i + 1].ToString().Equals(DateTime.Now.ToShortDateString)) //önce satırlar oluşsun
        //        {
        //            dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Turquoise;
        //            dersCizelge.Rows[j].Cells[i].Selected = true;
        //            devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Turquoise;
        //            devamsizlikCizelge.Rows[j].Cells[i].Selected = true;
        //        }

        //        if (i > 0 && j > -1)
        //        {
        //            switch (Convert.ToSByte(GoogleSQL.devamDurumuVeri[j + 1][i + 1]))
        //            {
        //                case 0:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.White;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "HENÜZ İŞLENMEDİ";
        //                    break;
        //                case -11: devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Black; break;
        //                case -1:
        //                case -2:
        //                case -3:
        //                case -4:
        //                case -5:
        //                case -6:
        //                case -7:
        //                case -8:
        //                case -9:
        //                case -10:
        //                case -12:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "DEVAMSIZ";
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                    break;
        //                case 1:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Green;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "DEVAMLI";
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                    break;
        //                case 2:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGreen;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "TEKRAR İZLENDİ";
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                    break;
        //                case 3:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Blue;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "TATİL";
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                    break;
        //                case 4:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Blue;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI YOK";
        //                    break;
        //                case 5:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI DEVAMSIZ";
        //                    break;
        //                case 6:
        //                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Green;
        //                    devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI DEVAMLI";
        //                    break;

        //                    //default: devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red; break;
        //            }
        //        }




        //    }


        //}





        // HALA KULLANIMDA ELİPS FORM İÇİN
        // SADECE AÇIKLAMAR YER KAPLAMAMASI İÇİN AÇIKLAMASIZ HALİ KODDA YER ALIYOR
        //private static extern IntPtr CreateRoundRectRgn(
        //     int nLeftRect, // sol üst köşenin x koordinatı
        //     int nTopRect, // sol üst köşenin y kordinatı
        //     int nRightRect, // sağ alt köşenin x kordinatı
        //     int nBottomRect, // sağ alt köşenin y kordinatı
        //     int nWidthEllipse, // height of ellipse
        //     int nHeightEllipse // elipsin genişliği
        //    );





    }
}







//WndProc Gelişmiş Version ama TitleBar sıkıntısı var
// sorumatik projesinde örneği var
