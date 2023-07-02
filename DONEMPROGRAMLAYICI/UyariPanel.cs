using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DONEMPROGRAMLAYICI
{
    public partial class UyariPanel : Form
    {
        int type;
       

        //string message, string caption, string butText
        public UyariPanel(string message, string caption, string butText, int type)
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 19, 19));
            captionLabel.Text = caption;
            messageLabel.Text = message;
            okButton.Text = butText;
            System.Media.SystemSounds.Beep.Play();
            this.type = type;
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
            //Debug.WriteLine(type + ""); //test
            if (type == 0)
                this.Close();
            if (type == 1)
                System.Windows.Forms.Application.Exit();
        }

    }
}
