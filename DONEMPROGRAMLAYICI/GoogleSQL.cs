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
    internal class GoogleSQL
    {
        public void sysout(string a)
        {
            Debug.WriteLine(a);
        }

        static public IList<IList<object>> dersProgVeri;
        static public IList<IList<object>> dersCizelgeVeri;
        static public IList<IList<object>> sinavTakvimVeri;
        static public IList<IList<object>> sinavSonucVeri;
        static public IList<IList<object>> devamDurumuVeri;
        static public IList<IList<object>> extraNotVeri;
        static public IList<IList<object>> settingsVeri;

        public void dersProgVeriCek()
        {
            dersProgVeri = READ(Program.dersProgSheet, "A:Z");
        }
        public void dersCizelgeVeriCek()
        {
            dersCizelgeVeri = READ(Program.dersCizelgeSheet, "A:Z");
        }
        public void settingsVeriCek()
        {
            settingsVeri = READ(Program.settingsSheet, "A:Z");
        }
        public void devamDurumuVeriCek()
        {
            devamDurumuVeri = READ(Program.devamDurumuSheet, "A:Z");
        }
        public void extraNotVeriCek()
        {
            extraNotVeri = READ(Program.extraNotSheet, "A:Z");
        }
        public void sinavTakvimVeriCek()
        {
            sinavTakvimVeri = READ(Program.sinavTakvimSheet, "A:Z");
        }
        public void sinavSonucVeriCek()
        {
            sinavSonucVeri = READ(Program.sinavSonucSheet, "A:Z");
        }

        public void dosyaId()
        {
            string sql = "name = \"" + Program.uygulamaDosyaName + "\"" + " and " +
                          "mimeType = \"application/vnd.google-apps.folder\"";
            uygulamaDosyaId(arama(sql));
        }

        public void sonDonemId()
        {
            string sql = "name contains \"" + "DONEM" + "\"" + " and " +
                         "mimeType = \"application/vnd.google-apps.spreadsheet\"" + " and " +
                         "\"" + Program.uygulamaDosyaId + "\" in parents";
            sonDonemId(arama(sql));
        }


        public bool connect()
        {
            string[] scopes = new string[] {DriveService.Scope.Drive,
                                            DriveService.Scope.DriveFile,
                                            SheetsService.Scope.Spreadsheets};


            Google.Apis.Auth.OAuth2.UserCredential credential = null;
            try
            {
                //Google.Apis.Auth.OAuth2.UserCredential -> type
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
                {
                    ClientId = Program.clientId,
                    ClientSecret = Program.clientSecret
                }, scopes,
                    Environment.UserName, CancellationToken.None, new FileDataStore("DonemProgramlayiciToken")).Result;
            }
            catch (Exception e)
            {
                //Debug.WriteLine("HATA\n--------------\n" + e);
                new UyariPanel("ERİŞİM REDDEDİLDİ\r\nUYGULAMA KAPANIYOR", "ERİŞİM HATASI", "TAMAM", 1).ShowDialog();
                //DialogResult result = MessageBox.Show("HESABA ERİŞİLEMEDİ", "ERİŞİM REDDEDİLDİ", MessageBoxButtons.OK);
                //System.Windows.Forms.Application.Exit(); //TAM TANIM DEVAM EDİYOR FONKSİYON
                return false; //FONKSİYONDAN ÇIKMAK İÇİN
            }
            //Debug.WriteLine("credential tip -> " + credential.GetType()); //TİP İÇİN TEST YAZIMI



            Program.driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Program.appName,

            });

            Program.sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Program.appName,

            });

            dosyaId();


            if (Program.uygulamaDosyaId != null)
            {
                sonDonemId();
                settingsVeri = READ(Program.settingsSheet, "A:Z");
                Settings.fill();
                MainForm mainForm = new MainForm();
                mainForm.Show();
                return true;
            }
            else
            {
                FirstUse firstUseForm = new FirstUse();
                firstUseForm.Show();
                return true;
            }


        }



        public void ADDTODRIVE(string name, int type) // 0->FOLDER, 1->SHEET
        {
            Google.Apis.Drive.v3.Data.File eklenecek = new Google.Apis.Drive.v3.Data.File();
            eklenecek.Name = name;
            string mimeType = "";
            var parents = "";
            switch (type)
            {
                case 0: 
                    mimeType = "application/vnd.google-apps.folder"; break;
                case 1: 
                    mimeType = "application/vnd.google-apps.spreadsheet";
                    parents = Program.uygulamaDosyaId;
                    eklenecek.Parents = new List<string> { parents }; // Parent folder                
                    break;
            }
            eklenecek.MimeType = mimeType;
            Google.Apis.Drive.v3.FilesResource.CreateRequest createRequest;
            createRequest = Program.driveService.Files.Create(eklenecek);
           
            
            
            createRequest.Fields = "id";

            var olusan = createRequest.Execute();

            if(type == 0)
            {
                Program.uygulamaDosyaId = olusan.Id;
                sysout("DOSYA OLUSTURULDU"); // TEST YAZIMI
            }
            else
            {
                sysout("SHEET OLUSTURULDU"); //TEST YAZIMI
            }


        }

        public Google.Apis.Drive.v3.Data.FileList arama(string sql)
        {
            string? pageToken = null;
            var driveDosyalar = Program.driveService.Files.List(); //request
            driveDosyalar.Q = sql;
            driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
            driveDosyalar.PageToken = pageToken;

            var result = driveDosyalar.Execute();
            return result;
        }

        public void arama(int type) // 0->FOLDER, 1->SHEETS
        {
            string? pageToken = null;
            string mimeType = "";
            string name = "";
            string ustDosyaQuery = "";
            string containORequal = "";

            switch (type)
            {
                case 0: 
                    mimeType = "application/vnd.google-apps.folder";
                    name = Program.uygulamaDosyaName;
                    containORequal = "=";
                    break;
                case 1: 
                    mimeType = "application/vnd.google-apps.spreadsheet";
                    name = "DONEM";
                    containORequal = "contains";
                    ustDosyaQuery = " and \"" + Program.uygulamaDosyaId + "\" in parents";
                    break;
            }

            string query = "name "+containORequal+" \"" + name + "\"" + " and " +
                           "mimeType = \"" + mimeType + "\"" + ustDosyaQuery;

            var driveDosyalar = Program.driveService.Files.List(); //request
            driveDosyalar.Q = query;
            driveDosyalar.Fields = "nextPageToken, files(id, name,parents,mimeType)";
            driveDosyalar.PageToken = pageToken;

            var result = driveDosyalar.Execute();

            if(result.Files.Count > 0)
            {
                foreach(Google.Apis.Drive.v3.Data.File bulunanDosya in result.Files)
                {
                    Program.uygulamaDosyaId = bulunanDosya.Id;
                }
            }

        }

        public void uygulamaDosyaId(Google.Apis.Drive.v3.Data.FileList result)
        {
            if (result.Files.Count > 0)
            {
                foreach (Google.Apis.Drive.v3.Data.File bulunanDosya in result.Files)
                {
                    Program.uygulamaDosyaId = bulunanDosya.Id;
                }
            }
            else
            {
                Program.uygulamaDosyaId = null;
            }
        }

        public void sonDonemId(Google.Apis.Drive.v3.Data.FileList result)
        {
            Program.toplamDonemSayisi = result.Files.Count;
            string sql = "name = \"" + "DONEM-" + Program.toplamDonemSayisi + "\"" + " and " +
                        "mimeType = \"application/vnd.google-apps.spreadsheet\"" + " and " +
                        "\"" + Program.uygulamaDosyaId + "\" in parents";
            result = arama(sql);

            foreach(var bulunanExcel in result.Files)
            {
                string bulunanExcelID = bulunanExcel.Id;
                Program.sonDonemId = bulunanExcelID;
                sysout("SON DONEM ID = " + bulunanExcelID); // KONTROL TEST YAZIMI
            }

        }

        public void INSERT(string sheetName, List<IList<object>> eklenenSatirlar, string rangeAralik)
        {
            var range = $"{sheetName}!" + rangeAralik; //A2:A  DEĞİŞİKLİK YAPILDI SONUNDA +""; vardı sildim 
            var valueRange = new ValueRange();

            valueRange.Values = eklenenSatirlar;

            var appendRequest = Program.sheetsService.Spreadsheets.Values.Append(valueRange, Program.sonDonemId, range); //ekleme talebi
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();

            sysout("YAZMA İŞLEMİ TAMAMLANDI"); // BİLDİRİM TEST 
        }
        public void UPDATE(string sheetName, List<IList<object>> eklenenSatirlar, string rangeAralik)
        {
            var range = $"{sheetName}!"+rangeAralik;
            var valueRange = new ValueRange();

            valueRange.Values = eklenenSatirlar;

            var updateRequest = Program.sheetsService.Spreadsheets.Values.Update(valueRange, Program.sonDonemId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateReponse = updateRequest.Execute();

            sysout("GÜNCELLEME İŞLEMİ TAMAMLANDI"); // BİLDİRİM TEST
        }

        public IList<IList<object>> READ(string sheetName, string aralik)
        {
            var range = $"{sheetName}!{aralik}";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    Program.sheetsService.Spreadsheets.Values.Get(Program.sonDonemId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            return values;
        }

        byte eklenenSheetGrid = 1; // kodlama dışı yazım
        // çoklu sheet ekleme incele tek request ile
        public void ADD_NEW_SHEET_TO_SPREAD(string sheetName)
        {
            var addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = new SheetProperties();
            addSheetRequest.Properties.Title = sheetName;
            addSheetRequest.Properties.SheetId = eklenenSheetGrid;
            eklenenSheetGrid++;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request
            {
                AddSheet = addSheetRequest
            });

            var batchUpdateRequest =
                Program.sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, Program.sonDonemId);

            batchUpdateRequest.Execute();

            sysout("ADD_NEW_SHEET_TO_SPREAD TAMAMLANDI"); // SONUC BİLDİRME TEST YAZIMI
        }

        public void DELETE_SHEET_FROM_SPREAD(int gridNo)
        {
            var deleteSheetRequest = new DeleteSheetRequest();
            deleteSheetRequest.SheetId = gridNo;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request
            {
                DeleteSheet = deleteSheetRequest
            });

            var batchUpdateRequest =
                Program.sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, Program.sonDonemId);

            batchUpdateRequest.Execute();

            sysout("DELETE_SHEET_FROM_SPREAD TAMAMLANDI"); // SONUC BİLDİRME TEST YAZIMI
        }











    }
}
