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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DONEMPROGRAMLAYICI
{
    public partial class FirstUse : Form
    {
        BackEnd backEnd = new BackEnd();


        public FirstUse()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 19, 19));
            yuklenenPanel.Width = 0;
        }

        //------------------  DRAGGING USTPANEL  -------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ustPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //------------------------------------------------------------------\\

        //------------- Elips -------------------------
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
             int nLeftRect, // sol üst köşenin x koordinatı
             int nTopRect, // sol üst köşenin y kordinatı
             int nRightRect, // sağ alt köşenin x kordinatı
             int nBottomRect, // sağ alt köşenin y kordinatı
             int nWidthEllipse, // height of ellipse
             int nHeightEllipse // elipsin genişliği
            );
        private void textBoxAltiCiz(Panel p, PaintEventArgs e, System.Drawing.Color c)
        {
            Pen blackPen = new Pen(c, 2);

            int x1 = 10;
            int x2 = p.Width - 10;

            int y = p.Height - 4; // düz çizgi için tek y yeterli

            e.Graphics.DrawLine(blackPen, x1, y, x2, y);

            p.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, p.Width, p.Height, 18, 18));
        }
        //------------------------------------------------------------\\

        private void startKurulum_Click(object sender, EventArgs e)
        {
            DateTime ilkgun = donemBasiPick.Value;
            DateTime songun = finalSonPick.Value;
            byte donemSayisi;
            try
            {
                
                ilkgun = Convert.ToDateTime("" + ilkgun.ToShortDateString()); // saati hesaptan çıkarmak için
                TimeSpan gunFarkiTarihsel = songun - ilkgun;
                int gunFarki = gunFarkiTarihsel.Days + 1;
                if (kacinciDonemTextBox.Text.Trim() == string.Empty) throw new CustomExeption("Boşlukları uygun şekilde doldurunuz.");
                donemSayisi = Convert.ToByte(kacinciDonemTextBox.Text);
                if (donemSayisi < 1 || donemSayisi > 20) throw new CustomExeption("Dönem sayısı", 1, 20);
                if (gunFarki < 5 || gunFarki > 200) throw new CustomExeption("Tarih farkı", 5, 200);
            }
            catch (CustomExeption hata)
            {
                //MessageBox.Show(hata.Message, "GİRDİ HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new UyariPanel(hata.Message, "GİRDİ HATASI", "Tamam", 0).ShowDialog();

                return;
            }


            backEnd.dosyaOlustur();
            bool kontrol = backEnd.kurulumDonemOlustur(donemSayisi, ilkgun, songun);

            if (kontrol)
            {
                backEnd.settingsVeri();
                Settings.fill();
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
        }




        private void cikisBut_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void kacinciDonemTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {   
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(textPanel, e, System.Drawing.Color.Black);
        }
    }
}
