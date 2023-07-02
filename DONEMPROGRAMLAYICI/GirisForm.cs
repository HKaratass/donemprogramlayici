using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DONEMPROGRAMLAYICI
{
    public partial class GirisForm : Form
    {
        
        BackEnd backEnd;
        public GirisForm()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 19, 19));
            
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



        private void cikisBut_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void winBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://login.live.com/") { UseShellExecute = true });
        }
        private void fbBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://www.facebook.com/") { UseShellExecute = true });
        }
        private void linkedinBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://www.linkedin.com/") { UseShellExecute = true });
        }
        private void twitterBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://twitter.com/i/flow/login") { UseShellExecute = true });
        }
        private void appleBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://appleid.apple.com/") { UseShellExecute = true });
        }
        private void kayitOlLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("https://accounts.google.com/signup") { UseShellExecute = true });
        }

        private void connectBut_MouseEnter(object sender, EventArgs e)
        {
            this.mainPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.altBanner2;
            this.connectBut.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.buttonb;
            this.ustPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.ustBanner2;
            this.appleBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_apple_logo_50;
            this.rememberMe.BackColor = System.Drawing.Color.White;
            this.rememberMe.ForeColor = System.Drawing.Color.Black;
            this.kayitOlLink.BackColor = System.Drawing.Color.White;
            this.withSocialMediaLbl.ForeColor = System.Drawing.Color.Black;
        }
        private void connectBut_MouseLeave(object sender, EventArgs e)
        {
            this.mainPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.altBanner;
            this.connectBut.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.button2s;
            this.ustPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.ustBanner;
            this.appleBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_apple_logo_30_beyaz;
            this.rememberMe.BackColor = System.Drawing.Color.Black;
            this.rememberMe.ForeColor = System.Drawing.Color.White;
            this.kayitOlLink.BackColor = System.Drawing.Color.Black;
            this.withSocialMediaLbl.ForeColor = System.Drawing.Color.White;
        }
        private void connectBut_Click(object sender, EventArgs e)
        {
            GoogleSQL google = new GoogleSQL();
            if (google.connect()) this.Hide();
        }

        private void connectButLbl_Click(object sender, EventArgs e)
        {
            connectBut_Click(sender, e);
        }
        private void connectButLbl_MouseEnter(object sender, EventArgs e)
        {
            connectBut_MouseEnter(sender, e);
        }
        private void connectButLbl_MouseLeave(object sender, EventArgs e)
        {
            connectBut_MouseLeave(sender, e);
        }

        private void rememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (rememberMe.Checked)
            {
                for(int i=0; i<30; i++)
                {
                    kayitOlLink.Height += 1;
                    int y = kayitOlLink.Location.Y + 1;
                    kayitOlLink.Location = new Point(0,y);
                    uyariLabel.Height += 1;
                    Thread.Sleep(3);
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    uyariLabel.Height -= 1;
                    int y = kayitOlLink.Location.Y - 1;
                    kayitOlLink.Location = new(0, y);
                    kayitOlLink.Height -= 1;
                    Thread.Sleep(3);
                }
            }
        }
    
    
    
    
    }
}
