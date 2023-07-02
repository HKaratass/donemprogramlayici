using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DONEMPROGRAMLAYICI
{
    public partial class MainForm : Form
    {
        BackEnd backEnd = new BackEnd();
        private int borderSize = 4;
        private Size formSize;
        public void sysout(string a)
        {
            Debug.WriteLine(a);
        } //java konsol çýktý kýsaltmasý

        char[] kolon = { 'A' , 'B' , 'C' , 'D' , 'E' , 'F' , 'G' , 'H' , 'I' ,
                        'J' , 'K' , 'L' , 'M' , 'N', 'O' , 'P' , 'Q' , 'R' ,
                        'S' , 'T' , 'U' , 'V', 'W' , 'X', 'Y' , 'Z'}; // index bazlý excel kolon sutun harfleri
        RadioButton[] renkRadioButtons;
        private void panelAc(Panel p)
        {
            dersProgramPanel.Visible = false;
            dersCizelgePanel.Visible = false;
            devamDurumuPanel.Visible = false;
            sinavTakvimPanel.Visible = false;
            sinavSonucPanel.Visible = false;
            extraNotPanel.Visible = false;
            dersEklePanel.Visible = false;
            dersNotuPanel.Visible = false;
            notEklePanel.Visible = false;
            kaynakcaPanel.Visible = false;

            if (p == dersProgramPanel)
            {
                haftaSonuToolStripMenuItem.Visible = true;
            }
            else
            {
                haftaSonuToolStripMenuItem.Visible = false;
            }
            if(p == sinavTakvimPanel)
            {
                bitenSinavlarToolStripMenuItem.Visible = true;
            }
            else
            {
                bitenSinavlarToolStripMenuItem.Visible = false;
            }

            p.Visible = true;
        }
        private void solPanelRest()
        {
            dersProgBut.Size = new System.Drawing.Size(211, dersProgBut.Size.Height);
            sinavTakvimBut.Size = new System.Drawing.Size(211, sinavTakvimBut.Size.Height);
            extraNotBut.Size = new System.Drawing.Size(211, extraNotBut.Size.Height);
            dersEkleBut.Visible = false;
            extraNotEkleBut.Visible = false;
            sinavEkleBut.Visible = false;
        }



        public MainForm()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            mainPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 1, mainPanel.Width - 8, mainPanel.Height - 8, 20, 20));
            
            //DERS EKLE RENK RADÝO BUTON ATAMA
            renkRadioButtons = new RadioButton[] {renkButton0, renkButton1, renkButton2, renkButton3, renkButton4,
                                                    renkButton5, renkButton6, renkButton7, renkButton8, renkButton9};

            for(int i=0; i<10; i++) //ilk renk atama
            {
                renkRadioButtons[i].BackColor = backEnd.dersRenkleri[i];
            }
             

            //First Panel
            panelAc(dersProgramPanel);

            backEnd.dersCizelgeVeri();
            backEnd.dersProgVeri();
            backEnd.devamDurumuVeri();
            backEnd.extraNotVeri();
            if (Settings.dersSayisi != 0)
            {
                

            }
            else
            {
                dersCizelgeBut.Enabled = false;
                devamDurumBut.Enabled = false;
                sinavTakvimBut.Enabled = false;
                sinavSonucBut.Enabled = false;
                extraNotBut.Enabled = false;
            }


            //--- DERSPROG HAFTASONU AC/KAPA ---
            haftaSonuToolStripMenuItem.Visible = false;
            if (Settings.dersProgCmts == 1) cumartesiPanel.Visible = true; else cumartesiPanel.Visible = false;
            if (Settings.dersProgPzr == 1) pazarPanel.Visible = true; else pazarPanel.Visible = false;

            //--- ComboBox DOLDURMA ---
            Object[] dakika = new object[60];
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    string j = "0" + i;
                    dakika[i] = j;
                }
                else
                {
                    dakika[i] = i;
                }    
            }   
            ekleDersBaslangicDakikaComboBox.Items.AddRange(dakika);
            ekleDersBitisDakikaComboBox.Items.AddRange(dakika);
            araSuresiDakikaComboBox.Items.AddRange(dakika);
            sinavEkleDakikaComboBox.Items.AddRange(dakika);

            


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
        
        // FORM SÝMGEDEN KÜÇÜLTME (ÝÞLEVSELLÝÐÝ ÝSPATLANMADI)
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- Minimize borderless form from taskbar
                return cp;
            }
        }

        // FORM SÝZEABLE
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
        
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }

            base.WndProc(ref m);
        }




        //-------------  TextBox Elips+Pen  --------------------------
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect,int nTopRect,int nRightRect,
                                                        int nBottomRect,int nWidthEllipse,int nHeightEllipse);
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

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new System.Windows.Forms.Padding(0, 8, 8, 0);
                    this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 18, 18));
                    mainPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 1, mainPanel.Width, mainPanel.Height, 20, 20));
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new System.Windows.Forms.Padding(borderSize);
                    //this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 18, 18));
                    //mainPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 1, mainPanel.Width, mainPanel.Height, 20, 20));
                    break;

            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }


        //-----DERS CIZELGE SELECT CELL ----
        string selectedCell = "";
        System.Drawing.Color selectedCellColor = System.Drawing.Color.DarkOliveGreen;
        private void dersCizelge_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex > 0)
            {
                string tarih = dersCizelge.Rows[e.RowIndex].Cells[0].Value.ToString();
                string ders = dersCizelge.Columns[e.ColumnIndex].HeaderText;

                if (dersCizelge.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == System.Drawing.Color.FromArgb(38, 38, 38) ||
                    dersCizelge.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == System.Drawing.Color.DarkRed)
                {
                    
                    if(dersCizelge.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == System.Drawing.Color.DarkRed)
                    {
                        new UyariPanel(ders + " dersi " + tarih + " tarihinde yok.\r\n"+ tarih+" tarihi hafta sonuna denk gelmektedir.",
                                            "DERS YOK", "TAMAM", 0).ShowDialog();
                    }
                    else
                    {
                        new UyariPanel(ders + " dersi " + tarih + " tarihinde yok.", "DERS YOK", "TAMAM", 0).ShowDialog();
                    }

                    //MessageBox.Show("Bu ders bu tarihte yok.");
                }
                else
                {
                    selectedCellColor = dersCizelge.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                    dersNotuPanel.BackColor = selectedCellColor;
                    dersNotTarihLabel.Text = "Tarih: " + tarih;
                    dersNotDersAdLabel.Text = "Ders: " + ders;
                    panelAc(dersNotuPanel);
                    selectedCell = kolon[e.ColumnIndex + 1] + "" + (e.RowIndex + 2); //A1
                    if (!dersCizelge.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(" "))
                    {
                        //sysout((e.RowIndex + 1) + ":" + (e.ColumnIndex + 1)); //TEST YAZIMI
                        dersNotuTextBox.Text = GoogleSQL.dersCizelgeVeri[e.RowIndex + 1][e.ColumnIndex + 1].ToString();
                    }
                    else
                    {
                        dersNotuTextBox.Text = "";
                        dersNotDevamDurumuComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                        dersNotDevamDurumuComboBox.SelectedIndex = -1;
                        dersNotDevamDurumuComboBox.Text = "- Seçim Yapýnýz -";
                    }
                }
            }


        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 5000, 5000, 18, 18));
            mainPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 1, 5000, 5000, 20, 20));
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 18, 18));
            mainPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 1, mainPanel.Width, mainPanel.Height, 20, 20));
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

        }



        private void haftaiciPanel_SizeChanged(object sender, EventArgs e)
        {
            pazartesiPanel.Width = haftaiciPanel.Width / 5;
            saliPanel.Width = haftaiciPanel.Width / 5;
            carsambaPanel.Width = haftaiciPanel.Width / 5;
            persembePanel.Width = haftaiciPanel.Width / 5;
            cumaPanel.Width = haftaiciPanel.Width / 5;
            dersProgBut_Click(sender, e);

        }


        private void dersProgBut_Click(object sender, EventArgs e)
        {
            backEnd.dersProgVeri();
            Panel[] gun = { pazartesiPanel, saliPanel, carsambaPanel, persembePanel, cumaPanel, cumartesiPanel, pazarPanel };
            Panel[] gunBaslik = { pazartesiBaslikPanel, saliBaslikPanel, carsambaBaslikPanel, persembeBaslikPanel,
                                    cumaBaslikPanel, cumartesiBaslikPanel, pazarBaslikPanel};

            switch (DateTime.Today.DayOfWeek.ToString())
            {
                case "Monday": pazartesiBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Tuesday": saliBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Wednesday": carsambaBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Thursday": persembeBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Friday": cumaBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Saturday": cumartesiBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
                case "Sunday": pazarBaslikPanel.BackColor = System.Drawing.Color.FromArgb(84, 87, 117); break;
            }

            for (int i = 0; i < 7; i++)
            {
                gun[i].Controls.Clear();
                gun[i].Controls.Add(gunBaslik[i]);
            }


            for (int i = 0; i < Settings.dersSayisi; i++)
            {
                if (Settings.dersSayisi != 0)
                {
                    backEnd.programaDersEkle(gun[Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][2])],
                                                Convert.ToString(GoogleSQL.dersProgVeri[i + 1][1]),
                                                Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][7]),
                                                Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][8]),
                                                Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][10]),
                                                Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][11]),
                                                Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][15]),
                                                Convert.ToString(GoogleSQL.dersProgVeri[i + 1][3]));
                }

            }

            panelAc(dersProgramPanel);
            solPanelRest();
            dersProgBut.Size = new System.Drawing.Size(140, dersProgBut.Size.Height);
            dersEkleBut.Visible = true;

        }

        private void dersCizelgeBut_Click(object sender, EventArgs e)
        {
            backEnd.dersCizelgeVeri();
            dersCizelge.RowCount = 0;
            dersCizelge.ColumnCount = 0;
            dersCizelge.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dersCizelge.EnableHeadersVisualStyles = false;
            for (int i = 0; i < Settings.dersSayisi + 1; i++) //tarih satýrý dahil
            {
                var sutun = new DataGridViewTextBoxColumn();
                sutun.HeaderText = GoogleSQL.dersCizelgeVeri[0][i + 1].ToString();
                sutun.MinimumWidth = 8;
                sutun.ReadOnly = true;
                sutun.Width = 150;
                sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
                dersCizelge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { sutun });

                for (int j = 0; j < Settings.donemGunSayisi; j++)
                {
                    //satýrlarý tek sefer eklemek için kontrol
                    if (dersCizelge.Rows.Count != Settings.donemGunSayisi) dersCizelge.Rows.Add();
                    dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGray;


                    if (i > 0) //dersin rengine göre renklendirme
                    {
                        dersCizelge.Rows[j].Cells[i].Style.BackColor = backEnd.dersRenkleri[Convert.ToByte(GoogleSQL.dersProgVeri[i][15])];
                        dersCizelge.Rows[j].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                    }
                    // ders içerik bilgisini iþleme
                    dersCizelge.Rows[j].Cells[i].Value = GoogleSQL.dersCizelgeVeri[j + 1][i + 1].ToString();
                    // eðer ders boþsa o hücreyi iptal etme
                    if (GoogleSQL.dersCizelgeVeri[j + 1][i + 1].Equals("!EMPTY"))
                    {
                        dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.FromArgb(38, 38, 38); //38 38 38
                        dersCizelge.Rows[j].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        dersCizelge.Rows[j].Cells[i].Value = ""; //YOK
                    }
                    if (GoogleSQL.dersCizelgeVeri[j + 1][i + 1].Equals("!WEEKEND"))
                    {
                        dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.DarkRed;
                        dersCizelge.Rows[j].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        dersCizelge.Rows[j].Cells[i].Value = ""; //HAFTASONU
                    }


                    if (GoogleSQL.dersCizelgeVeri[j + 1][i + 1].ToString().Equals(DateTime.Now.ToShortDateString())) //önce satýrlar oluþsun
                    {
                        dersCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Turquoise;
                        dersCizelge.Rows[j].Cells[i].Selected = true;
                    }

                }


            }

            dersCizelge.Columns[0].Frozen = true;
            panelAc(dersCizelgePanel);
            solPanelRest();
        }



        private void devamDurumBut_Click(object sender, EventArgs e)
        {
            backEnd.devamDurumuVeri();
            devamsizlikCizelge.RowCount = 0;
            devamsizlikCizelge.ColumnCount = 0;
            devamsizlikCizelge.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            devamsizlikCizelge.EnableHeadersVisualStyles = false;
            for (int i = 0; i < Settings.dersSayisi + 1; i++) //tarih satýrý dahil
            {
                var sutunDevam = new DataGridViewTextBoxColumn();
                sutunDevam.HeaderText = GoogleSQL.devamDurumuVeri[0][i + 1].ToString();
                sutunDevam.MinimumWidth = 8;
                sutunDevam.ReadOnly = true;
                sutunDevam.Width = 150;
                sutunDevam.SortMode = DataGridViewColumnSortMode.NotSortable;
                devamsizlikCizelge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { sutunDevam });

                for (int j = 0; j < Settings.donemGunSayisi; j++)
                {
                    if (devamsizlikCizelge.Rows.Count != Settings.donemGunSayisi) devamsizlikCizelge.Rows.Add();
                    devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGray;
                   
                    if (i == 0) devamsizlikCizelge.Rows[j].Cells[i].Value = GoogleSQL.devamDurumuVeri[j + 1][i + 1].ToString();

                    if (GoogleSQL.devamDurumuVeri[j + 1][i + 1].ToString().Equals(DateTime.Now.ToShortDateString())) //önce satýrlar oluþsun
                    {
                        devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Turquoise;
                        devamsizlikCizelge.Rows[j].Cells[i].Selected = true;
                    }

                    //DEVAMSIZLIK NOsuna GORE YAZACAKLAR SWÝTCHi
                    if (i > 0 && j > -1)
                    {
                        switch (Convert.ToSByte(GoogleSQL.devamDurumuVeri[j + 1][i + 1]))
                        {
                            case 0:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGray;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "HENÜZ ÝÞLENMEDÝ";
                                break;
                            case -11:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);
                                break;
                            case -1:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "1S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -2:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "2S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -3:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "3S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -4:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "4S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -5:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "5S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -6:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "6S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -7:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "7S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -8:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "8S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -9:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "9S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -10:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "10S DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case -12:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "DEVAMSIZ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case 1:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Green;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "DEVAMLI";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case 2:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.LightGreen;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "TEKRAR ÝZLENDÝ";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case 3:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Blue;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "TATÝL";
                                devamsizlikCizelge.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case 4:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Blue;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI YOK";
                                break;
                            case 5:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI DEVAMSIZ";
                                break;
                            case 6:
                                devamsizlikCizelge.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Green;
                                devamsizlikCizelge.Rows[j].Cells[i].Value = "DERS IPTAL TELAFI DEVAMLI";
                                break;
                        }
                    }




                }


            }




            devamsizlikCizelge.Columns[0].Frozen = true;
            panelAc(devamDurumuPanel);
            solPanelRest();
        }

        

        private void sinavSonucBut_Click(object sender, EventArgs e)
        {
            panelAc(sinavSonucPanel);
            solPanelRest();
        }

        

        //-------- NAVÝGASYON BUTONLARI --------//
        private void cikisBut_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void maxBut_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void minBut_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //-------------------------------------//

        //-------- HAFTA SONU UST_MENU SHOW --------//
        private void hesaptanCýkýsYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var token = Path.Combine(Environment.GetFolderPath(
                         Environment.SpecialFolder.ApplicationData), "DonemProgramlayiciToken");
            if (System.IO.Directory.Exists(token))
            {
                Directory.Delete(token, true);
                //sysout("dosya var"); // TEST YAZIMI
                System.Windows.Forms.Application.Exit();
            }
        }
        private void haftaSonuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cumartesiPanel.Visible)
            {
                cumartesiPanel.Visible = false;
                pazarPanel.Visible = false;
                Settings.dersProgCmts = 0;
                Settings.dersProgPzr = 0;
                string[] update =
                {
                    "0",
                    "0"
                };
                var updateAll = new List<IList<object>> { };
                for (int i = 0; i < update.Length; i++)
                {
                    var satir = new List<object>() { update[i] };
                    updateAll.Add(satir);
                }
                backEnd.UPDATE(Program.settingsSheet, updateAll, "C11:C12");

            }
            else
            {
                cumartesiPanel.Visible = true;
                pazarPanel.Visible = true;
                Settings.dersProgCmts = 1;
                Settings.dersProgPzr = 1;
                string[] update =
                {
                    "1",
                    "1"
                };
                var updateAll = new List<IList<object>> { };
                for (int i = 0; i < update.Length; i++)
                {
                    var satir = new List<object>() { update[i] };
                    updateAll.Add(satir);
                }
                backEnd.UPDATE(Program.settingsSheet, updateAll, "C11:C12");

            }
        }
        private void cumartesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cumartesiPanel.Visible)
            {
                cumartesiPanel.Visible = false;
                backEnd.UPDATE(Program.settingsSheet, new List<object>() { "0" }, "C11");
                Settings.dersProgCmts = 0;
            }
            else
            {
                cumartesiPanel.Visible = true;
                backEnd.UPDATE(Program.settingsSheet, new List<object>() { "1" }, "C11");
                Settings.dersProgCmts = 1;
            }
        }
        private void pazarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pazarPanel.Visible)
            {
                pazarPanel.Visible = false;
                backEnd.UPDATE(Program.settingsSheet, new List<object>() { "0" }, "C12");
                Settings.dersProgPzr = 0;
            }
            else
            {
                pazarPanel.Visible = true;
                backEnd.UPDATE(Program.settingsSheet, new List<object>() { "1" }, "C12");
                Settings.dersProgPzr = 1;
            }

        }
        private void uygulamaHakkýndaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UyariPanel("Visiual Studio Proje\r\nHazýrlayan: Hasan Karataþ\r\nFree Version: v0.1", "HAKKINDA", "Tamam", 0).ShowDialog();
        }
        private void bitenSinavlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gecmisSinavlarGozuksun) gecmisSinavlarGozuksun = false;
            else gecmisSinavlarGozuksun = true;
            sinavTakvimBut_Click(sender, e);
        }
        private void kapaliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaChanger(0);
        }
        private void acikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaChanger(1);
        }
        private void oLEDEnerjiTasarrufuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaChanger(2);
        }
        //-----------------------------------------//





        private void dersCizelgePanel_SizeChanged(object sender, EventArgs e)
        {
            dersCizelge.Width = dersCizelgePanel.Width - 11;
            dersCizelge.Height = dersCizelgePanel.Height - 11;
        }

        private void dersEkleTest_Click(object sender, EventArgs e)
        {
            //MECBURUÝ KESÝN
            araSayisiComboBox.SelectedIndex = 0;
            araSuresiDakikaComboBox.SelectedIndex = 0;
            dersSayisiComboBox.SelectedIndex = 0;

            //CLEAR
            ekDersCheckBox.Checked = false;
            ekleDersAdiTextBox.Text = String.Empty;
            ekleDersKisaltmaTextBox.Text = String.Empty;
            ekleDersHocasiTextBox.Text = String.Empty;
            ekleDersSinifiTextBox.Text = String.Empty;
            ekleDersAKTSTextBox.Text = String.Empty;

            ekleDersSubeComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            ekleDersSubeComboBox.SelectedIndex = -1;
            ekleDersSubeComboBox.Text = "Þube";

            ekleDersBaslangicSaatComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            ekleDersBaslangicSaatComboBox.SelectedIndex = -1;
            ekleDersBaslangicSaatComboBox.Text = "Saat";

            ekleDersBaslangicDakikaComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            ekleDersBaslangicDakikaComboBox.SelectedIndex = -1;
            ekleDersBaslangicDakikaComboBox.Text = "Dakika";

            ekleDersBitisSaatComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            ekleDersBitisSaatComboBox.SelectedIndex = -1;
            ekleDersBitisSaatComboBox.Text = "Saat";

            ekleDersBitisDakikaComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            ekleDersBitisDakikaComboBox.SelectedIndex = -1;
            ekleDersBitisDakikaComboBox.Text = "Dakika";

            devamZorunluCheckBox.Checked = false;

            yoklamaStiliComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            yoklamaStiliComboBox.SelectedIndex = -1;
            yoklamaStiliComboBox.Text = "- Seçim Yapýnýz -";

            blokYoklamaCheckBox.Checked = false;

            gecGireneTepkiComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            gecGireneTepkiComboBox.SelectedIndex = -1;
            gecGireneTepkiComboBox.Text = "- Seçim Yapýnýz -";

            dersGunuComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            dersGunuComboBox.SelectedIndex = -1;
            dersGunuComboBox.Text = "- Seçim Yapýnýz -";


            for (int i = 0; i < renkRadioButtons.Length; i++)
                renkRadioButtons[i].Checked = false;



            panelAc(dersEklePanel);
        }


        //-----------------------------------------//
        //-----------------------------------------//
        //-----------DATABASE DERS EKLE------------//
        //-----------------------------------------//
        //-----------------------------------------//
        private void ekleDersNow_Click(object sender, EventArgs e)
        {
            sbyte dersRenkKod = -1;
            for(int i=0; i<10; i++)
            {
                if (renkRadioButtons[i].Checked)
                    dersRenkKod = (sbyte)i;
            }
            if (ekleDersAdiTextBox.Text.Trim() == string.Empty ||
                ekleDersKisaltmaTextBox.Text.Trim() == string.Empty ||
                ekleDersHocasiTextBox.Text.Trim() == string.Empty ||
                ekleDersAKTSTextBox.Text.Trim() == string.Empty ||
                ekleDersSinifiTextBox.Text.Trim() == string.Empty ||
                ekleDersSubeComboBox.SelectedIndex == -1 ||
                ekleDersBaslangicSaatComboBox.SelectedIndex == -1 ||
                ekleDersBitisSaatComboBox.SelectedIndex == -1 ||
                ekleDersBaslangicDakikaComboBox.SelectedIndex == -1 ||
                ekleDersBitisDakikaComboBox.SelectedIndex == -1 ||
                dersSayisiComboBox.SelectedIndex == -1 ||
                dersGunuComboBox.SelectedIndex == -1 ||
                yoklamaStiliComboBox.SelectedIndex == -1 ||
                dersRenkKod == -1
                )
            {
                new UyariPanel("Boþluklarý Eksiksiz ve Uygun Biçimde Doldurun", "GÝRDÝ HATA", "Tamam", 0).ShowDialog();
                //MessageBox.Show("Lütfen Boþluklarý Eksiksiz Doldurun");
                return;
            }
            string devamZ = "0";
            string ekDers = "0";
            string blokYoklama = "0";
            string devamYuzde = "0";
            if (devamYuzdeComboBox.SelectedIndex != -1) 
                devamYuzde = devamYuzdeComboBox.Text.Substring(1, (devamYuzdeComboBox.Text.Length - 1));
            if (devamZorunluCheckBox.Checked) devamZ = "1";
            if (ekDersCheckBox.Checked) ekDers = "1";
            if (blokYoklamaCheckBox.Checked) blokYoklama = "1"; 

                
            var tekSatir = new List<object>() {
                ekleDersAdiTextBox.Text,
                ekleDersKisaltmaTextBox.Text,
                dersGunuComboBox.SelectedIndex.ToString(),
                ekleDersSinifiTextBox.Text,
                araSayisiComboBox.Text,
                araSuresiDakikaComboBox.Text,
                $"=H{Settings.dersSayisi+2}*60+I{Settings.dersSayisi+2}",
                ekleDersBaslangicSaatComboBox.Text,
                ekleDersBaslangicDakikaComboBox.Text,
                $"=K{Settings.dersSayisi+2}*60+L{Settings.dersSayisi+2}",
                ekleDersBitisSaatComboBox.Text,
                ekleDersBitisDakikaComboBox.Text,
                ekleDersHocasiTextBox.Text,
                yoklamaStiliComboBox.SelectedIndex.ToString(),
                devamZ,
                dersRenkKod,
                ekleDersAKTSTextBox.Text,
                ekDers,
                ekleDersSubeComboBox.Text,
                devamYuzde,
                blokYoklama,
                dersSayisiComboBox.Text,
                gecGireneTepkiComboBox.SelectedIndex.ToString()
            };

            //for(int i=0; i<tekSatir.Count; i++) //TEST YAZIMI
            //{
            //    sysout(tekSatir[i].ToString());
            //}


            backEnd.INSERT(Program.dersProgSheet, tekSatir, $"A{Settings.dersSayisi + 2}:{Settings.dersSayisi + 2}");
            GoogleSQL.dersProgVeri.Add(tekSatir);


            var sinavTakvimSutun = new List<IList<object>> { };
            var sinavTakvimSatir = new List<object>() { $"=DersProg!A{Settings.dersSayisi + 2}" };
            sinavTakvimSutun.Add(sinavTakvimSatir);
            for(int i=0; i<19; i++) //boþ satýr ekleme
                sinavTakvimSutun.Add(new List<object>() { });
            sinavTakvimSutun.Add(new List<object>() { 
                $"=BAÐ_DEÐ_DOLU_SAY({kolon[Settings.dersSayisi + 1]}6:{kolon[Settings.dersSayisi + 1]}20)/3" });// extra toplam satýrý
            backEnd.INSERT(Program.sinavTakvimSheet, sinavTakvimSutun, $"{kolon[Settings.dersSayisi + 1]}1:{kolon[Settings.dersSayisi + 1]}");
            //GoogleSQL.sinavTakvimVeri[0].Add(textBox1.Text);

            var sinavSonucSutun = new List<IList<object>> { };
            var sinavSonucSatir = new List<object>() { $"=DersProg!A{Settings.dersSayisi + 2}" };
            sinavSonucSutun.Add(sinavSonucSatir);
            for (int i = 0; i < 26; i++) //boþ satýr ekleme
                sinavSonucSutun.Add(new List<object>() { });
            sinavSonucSutun.Add(new List<object>() { 
                $"=BAÐ_DEÐ_DOLU_SAY({kolon[Settings.dersSayisi + 1]}8:{kolon[Settings.dersSayisi + 1]}27)/4" }); //extra toplam satýrý
            backEnd.INSERT(Program.sinavSonucSheet, sinavSonucSutun, $"{kolon[Settings.dersSayisi + 1]}1:{kolon[Settings.dersSayisi + 1]}");
            //GoogleSQL.sinavSonucVeri[0].Add(textBox1.Text);

            var dersCizelgeSutun = new List<IList<object>> { };
            var dersCizelgeDersAdi = new List<object>() { $"=DersProg!A{Settings.dersSayisi + 2}" };
            dersCizelgeSutun.Add(dersCizelgeDersAdi);
            for (int i = 0; i < Settings.donemGunSayisi; i++)
            {
                sysout(i+". iþlem");
                if ((dersGunuComboBox.SelectedIndex + 1) != Convert.ToByte(GoogleSQL.dersCizelgeVeri[i + 1][0]))
                {
                    if(Convert.ToByte(GoogleSQL.dersCizelgeVeri[i + 1][0]) == 6 ||
                        Convert.ToByte(GoogleSQL.dersCizelgeVeri[i + 1][0]) == 7)
                    {
                        var satir = new List<object>() { "!WEEKEND" };
                        //GoogleSQL.dersCizelgeVeri[i+1].Add("!EMPTY");
                        dersCizelgeSutun.Add(satir);
                    }
                    else
                    {
                        var satir = new List<object>() { "!EMPTY" };
                        //GoogleSQL.dersCizelgeVeri[i+1].Add("!EMPTY");
                        dersCizelgeSutun.Add(satir);
                    }
                }
                else
                {
                    var satir = new List<object>() { " " };
                    dersCizelgeSutun.Add(satir);
                }
                //GoogleSQL.dersCizelgeVeri[0].Add(textBox1.Text);
            }
            backEnd.INSERT(Program.dersCizelgeSheet, dersCizelgeSutun, $"{kolon[Settings.dersSayisi + 2]}1:{kolon[Settings.dersSayisi + 2]}");
            //GoogleSQL.dersCizelgeVeri[0].Add(textBox1.Text);

            var devamDurumuSutun = new List<IList<object>> { };
            var devamDurumuDersAdi = new List<object>() { $"=DersProg!A{Settings.dersSayisi + 2}" };
            devamDurumuSutun.Add(devamDurumuDersAdi);
            for (int i = 0; i < Settings.donemGunSayisi; i++)
            {
                if ((dersGunuComboBox.SelectedIndex + 1) != Convert.ToByte(GoogleSQL.dersCizelgeVeri[i + 1][0])) //ayni sayilar tek cekim
                {
                    var satir = new List<object>() { "-11" };
                    devamDurumuSutun.Add(satir);
                }
                else
                {
                    var satir = new List<object>() { "0" };
                    devamDurumuSutun.Add(satir);

                }
            }
            backEnd.INSERT(Program.devamDurumuSheet, devamDurumuSutun, $"{kolon[Settings.dersSayisi + 2]}1:{kolon[Settings.dersSayisi + 2]}");
            //GoogleSQL.devamDurumuVeri[0].Add(textBox1.Text);

            if (Settings.dersSayisi == 0)
            {
                dersCizelgeBut.Enabled = true;
                devamDurumBut.Enabled = true;
                sinavTakvimBut.Enabled = true;
                sinavSonucBut.Enabled = true;
                extraNotBut.Enabled = true;
            }


            backEnd.settingsVeri(); Settings.fill();

        }
        private void ekDersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ekDersCheckBox.Checked)
            {
                ekleDersAKTSTextBox.Text = "0";
                ekleDersAKTSTextBox.Enabled = false;
            }
            else
            {
                ekleDersAKTSTextBox.Text = "";
                ekleDersAKTSTextBox.Enabled = true;
            }
        }

        private void dersNotuEkleBut_Click(object sender, EventArgs e)
        {
            if (dersNotuTextBox.Text.Equals("") || dersNotDevamDurumuComboBox.SelectedIndex == -1)
            {
                new UyariPanel("Lütfen boþluklarý uygun þekilde doldurunuz.", "UYARI", "TAMAM", 0).ShowDialog();
                //MessageBox.Show("Lütfen boþluklarý uygun þekilde doldurunuz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cell = new List<object>() { dersNotuTextBox.Text };
            backEnd.UPDATE(Program.dersCizelgeSheet, cell, selectedCell);
            sbyte devamDurumuIndex = 0;
            switch (dersNotDevamDurumuComboBox.SelectedIndex)
            {
                case 0: devamDurumuIndex = 0; break;
                case 1: devamDurumuIndex = -12; break;
                case 2: devamDurumuIndex = 1; break;
                case 3: devamDurumuIndex = 2; break;
                case 4: devamDurumuIndex = 3; break;
                case 5: devamDurumuIndex = 4; break;
                case 6: devamDurumuIndex = 5; break;
                case 7: devamDurumuIndex = 6; break;
            }
            var devamCell = new List<object>() { devamDurumuIndex };
            backEnd.UPDATE(Program.devamDurumuSheet, devamCell, selectedCell);

            dersProgBut_Click(sender, e);
        }
        private void araSayisiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (araSayisiComboBox.SelectedIndex == 0)
            {
                ortAraSureLabel.Visible = false;
                araSuresiDakikaComboBox.Visible = false;
                araSuresiDakikaComboBox.SelectedIndex = 0;
                araSuresiDakikaComboBox.Enabled = false;
            }
            else
            {
                araSuresiDakikaComboBox.Enabled = true;
                ortAraSureLabel.Visible = true;
                araSuresiDakikaComboBox.Visible = true;
            }
        }

        private void devamZorunluCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (devamZorunluCheckBox.Checked)
            {
                devamYuzdeComboBox.Enabled = true;
                devamsizlikHakkiLabel.Visible = true;
                devamYuzdeComboBox.SelectedIndex = 0;
            }
            else
            {
                devamYuzdeComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                devamYuzdeComboBox.SelectedIndex = -1;
                devamYuzdeComboBox.Enabled = false;
                devamYuzdeComboBox.Text = "% 0";
                devamsizlikHakkiLabel.Visible = false;
            }
            tahminiDevamsizlik();
        }

        private void blokYoklamaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tahminiDevamsizlik();
        }

        private void dersSayisiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tahminiDevamsizlik();
        }

        private void ekleDersAKTSTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void devamYuzdeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tahminiDevamsizlik();
        }

        public void tahminiDevamsizlik()
        {
            double devamYuzde = 0;
            if (devamYuzdeComboBox.SelectedIndex != -1)
                devamYuzde = Convert.ToInt32(devamYuzdeComboBox.Text.Substring(1, (devamYuzdeComboBox.Text.Length - 1)));
            double dersSayisi = 0;
            if (dersSayisiComboBox.SelectedIndex != -1)
                dersSayisi = Convert.ToInt32(dersSayisiComboBox.Text);
            double donemGunSayisi = Settings.donemGunSayisi; //c# problem iki inti bölünce double çeviremiyor
            int yaklasikHafta = (int)Math.Ceiling((donemGunSayisi) / 7); // ünilerde tüm dersler ayný hafta sayýsýna sahiptir
                                    //Math.Ceiling yukarý yuvarlama
            double haftaBazli = (yaklasikHafta * (100-devamYuzde)) / 100;
            double dersBazli = haftaBazli * dersSayisi;
            //sysout(yaklasikHafta + ""); sysout(haftaBazli + ""); sysout(dersBazli + ""); //TEST
            if (blokYoklamaCheckBox.Checked)
            {
                devamsizlikHakkiLabel.Text = "Tahmini Devamsýzlýk Hakký: Döneminiz yaklaþýk " + string.Format("{0:0.##}", yaklasikHafta) + " haftadan oluþmaktadýr.\r\n " +
                                             "%" + devamYuzde + " devam zorunluluðu ile yaklaþýk " +
                                             string.Format("{0:0.##}", haftaBazli) + " hafta derse katýlmama hakkýnýz vardýr.\r\n" +
                                             "(Tatiller, sýnav haftalarý dahil olmamakla beraber girilen dönem tarihleri arasý farkýn\r\n" +
                                             "haftanýn tüm günlere bölümü ile oluþturulmuþtur.)";
            }
            else
            {

                devamsizlikHakkiLabel.Text = "Tahmini Devamsýzlýk Hakký: Döneminiz yaklaþýk " + string.Format("{0:0.##}", yaklasikHafta) + " haftadan oluþmaktadýr.\r\n " +
                                             "%" + devamYuzde + " devam zorunluluðu ile yaklaþýk " +
                                             string.Format("{0:0.##}", dersBazli) + " derse katýlmama hakkýnýz vardýr.\r\n" +
                                             "(Tatiller, sýnav haftalarý dahil olmamakla beraber girilen dönem tarihleri arasý farkýn\r\n" +
                                             "haftanýn tüm günlere bölümü ile oluþturulmuþtur.)";
            }
        }


        


        //EXTRA NOTE EKLE PANEL KONTROL
        //EXTRA NOTE EKLE PANEL KONTROL
        //EXTRA NOTE EKLE PANEL KONTROL
        private void extraNotBut_Click(object sender, EventArgs e) //--- SOL PANEL EXTRA NOT BUTON
        {
            extraNotPanel.Controls.Clear();
            //backEnd.notCek(extraNotPanel);
            notCek(extraNotPanel);
            if (extraNotPanel.Controls.Count == 0)
            {
                extraNotPanel.Controls.Add(notGirdiYokLabel);
                notGirdiYokLabel.Visible = true;
            }
            panelAc(extraNotPanel);
            solPanelRest();
            extraNotBut.Size = new System.Drawing.Size(140, extraNotBut.Size.Height);
            extraNotEkleBut.Visible = true;

        }
        //----- DATABASEDEN NOTLARI ÇEKER VE BUTON OLUÞTURUR ---
        public void notCek(Panel notePanel) 
        {
            notLocationNo = -1;
            for (int i = 0; i < Settings.notSayisi; i++)
            {
                if (GoogleSQL.extraNotVeri[i + 1][0].Equals("!DELETED")) continue;
                int noteNo = Convert.ToInt32(GoogleSQL.extraNotVeri[i + 1][0]);
                notLocationNo++;
                notButton(notePanel, noteNo);

            }
        }
        //--- DÝNAMÝK BUTON EKLEME NOT BUTTON ---
        int notLocationNo = -1;
        public void notButton(Panel notePanel, int notNo)
        {
            //panel autoscroll iken hareketten etkilenen panele but konumu deðiþiyor
            //buton oluþtur fonksiyon
            Button b = new Button();
            b.Name = "not" + notNo;
            b.Location = new System.Drawing.Point(10, 10 + ((notLocationNo) * 70));
            b.Size = new System.Drawing.Size((extraNotPanel.Width-29), 65); //765,65
            b.Text = notNo + " - " + GoogleSQL.extraNotVeri[notNo][3].ToString();
            b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            b.UseVisualStyleBackColor = true;
            b.Tag = notNo;


            //ÇALIÞIYOR
            Label lbl = new Label();
            lbl.Text = GoogleSQL.extraNotVeri[notNo][1].ToString();
            lbl.Location = new System.Drawing.Point(b.Width-75, 0);
            lbl.Size = new System.Drawing.Size(65, 65);
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.BackColor = System.Drawing.Color.Transparent;
            b.Controls.Add(lbl);

            notePanel.Controls.Add(b);
            b.Click += new System.EventHandler(notAc);
        }
        //--- DÝNAMÝK BUTON CLÝCK FONKSÝYON ---
        int currentNotNo = 0;
        public void notAc(object s, EventArgs e)
        {
            Button button = s as Button;
            //Debug.WriteLine(button.Tag); //TEST YAZIMI
            currentNotNo = Convert.ToInt32(button.Tag);
            notBaslikTextBox.Text = GoogleSQL.extraNotVeri[currentNotNo][3].ToString();
            notTextBox.Text = GoogleSQL.extraNotVeri[currentNotNo][2].ToString();
            extraNotEkleTarihLabel.Text = "Girdi Tarih: " + GoogleSQL.extraNotVeri[currentNotNo][1].ToString();
            currentNotUpdateBut.Visible = true;
            currentNotEkleBut.Visible = false;
            panelAc(notEklePanel);
        }
        //--- GÜNCELLE BUTONU NOTU GÜNCELLE  DATABASE NOT GÜNCELLER ---
        private void currentNotUpdateBut_Click(object sender, EventArgs e)
        {
            var satir = new List<object>() { notTextBox.Text, notBaslikTextBox.Text };
            backEnd.UPDATE(Program.extraNotSheet, satir, "C" + (currentNotNo+1)+":D" + (currentNotNo+1));
            backEnd.extraNotVeri();
        }
        //--- NOTU SÝL BUTONU TRASH ---
        private void currentNotDeleteBut_Click(object sender, EventArgs e)
        {
            if (currentNotEkleBut.Visible)
            {
                notBaslikTextBox.Text = "";
                notTextBox.Text = "";
                extraNotBut_Click(sender, e);
            }
            else
            {
                backEnd.UPDATE(Program.extraNotSheet, new List<object>() { "!DELETED" }, "A" + (currentNotNo + 1));
                backEnd.extraNotVeri();
                extraNotBut_Click(sender, e);
            }
        }
        //--- NOT PANEL SÝZE DEÐÝÞÝM SONUCU ---
        private void extraNotPanel_SizeChanged(object sender, EventArgs e)
        {
            extraNotBut_Click(sender, e);
        }
        //--- NOT EKLE BUTONU DATABASE NOT EKLER ---
        private void currentNotEkleBut_Click(object sender, EventArgs e)
        {
            if(notTextBox.Text.Trim() == string.Empty || notBaslikTextBox.Text.Trim() == string.Empty)
            {
                new UyariPanel("Boþluklarý uygun þekilde doldurunuz.", "UYARI", "TAMAM", 0).ShowDialog();
                return;
            }


            var satir = new List<object>() { Settings.notSayisi+1,
                                             DateTime.Now.ToShortDateString(),
                                             notTextBox.Text,
                                             notBaslikTextBox.Text
                                            };
            backEnd.INSERT(Program.extraNotSheet, satir, "A2:Z");
            backEnd.settingsVeri(); Settings.fill();
            backEnd.extraNotVeri();
            extraNotBut_Click(sender, e);
        }
        //--- NOT BASLIK TEXT PANEL ---
        private void notBaslikPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(System.Drawing.Color.Black, 2);

            int x1 = 10;
            int x2 = 5000;

            int y = notBaslikPanel.Height - 4; // düz çizgi için tek y yeterli

            e.Graphics.DrawLine(blackPen, x1, y, x2, y);

            notBaslikPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, notBaslikPanel.Width, notBaslikPanel.Height, 18, 18));
        }
        //--- NOT EKLE SOL PANEL BUTON ---
        private void extraNotEkleBut_Click(object sender, EventArgs e)
        {

            extraNotEkleTarihLabel.Text = "Girdi Tarih: " + DateTime.Now.ToShortDateString();
            currentNotEkleBut.Visible = true;
            currentNotUpdateBut.Visible = false;
            notBaslikTextBox.Text = "";
            notTextBox.Text = "";
            panelAc(notEklePanel);

        }
        //--- NOT EKLE PANEL SÝZECHANGED ---
        private void notEklePanel_SizeChanged(object sender, EventArgs e)
        {
            notTextBox.Size = new Size(notEklePanel.Width - 256, notEklePanel.Height - 92);
            notBaslikPanel.Size = new Size(notEklePanel.Width - 256, 51);
            notBaslikTextBox.Size = new Size(notBaslikPanel.Width - 165, 15);
            notBaslikPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, notBaslikPanel.Width, notBaslikPanel.Height, 18, 18));
        }




        //SINAV TAKVÝM PANEL KONTROL SINAV TAKVÝM SOL PANEL BUT
        //SINAV TAKVÝM PANEL KONTROL
        //SINAV TAKVÝM PANEL KONTROL
        private void sinavTakvimBut_Click(object sender, EventArgs e)
        {
            sinavEklePanel.Visible = false;
            sinavListPanel.Controls.Clear();
            backEnd.sinavSonucVeri();
            backEnd.sinavTavkimVeri();
            panelAc(sinavTakvimPanel);
            sinavPanel();
            solPanelRest();
            sinavTakvimBut.Size = new System.Drawing.Size(140, sinavTakvimBut.Size.Height);
            sinavEkleBut.Visible = true;
        }
        //--- SINAV EKLE SOL PANEL BUT + COMBOBOXa DERSLERÝ EKLER ---
        private void sinavEkleBut_Click(object sender, EventArgs e)
        {
            Object[] dersler = new Object[Settings.dersSayisi];
            for(int i=0; i<Settings.dersSayisi; i++)
            {
                dersler[i] = GoogleSQL.dersProgVeri[i + 1][0];
            }
            sinavEkleDerslerComboBox.Items.Clear();
            sinavEkleDerslerComboBox.Items.AddRange(dersler);
            sinavListPanel.Controls.Clear();
            sinavEklePanel.Visible = true;
            sinavPanel();//TEST DENEME
            
        }
        //--- YENÝ SINAV TÜRÜ EKLEME ---
        private void sinavEkleSinavTuruComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sinavEkleSinavTuruComboBox.SelectedIndex == 2)
            {
                this.sinavEkleTarihPicker.Location = new System.Drawing.Point(25, 172);
                this.sinavEkleTarihPicker.Size = new System.Drawing.Size(200, 23);
                etkiLabel.Location = new System.Drawing.Point(25, 208);
                etkiLabel.Size = new System.Drawing.Size(47, 15);
                sinavEkleSinavEtkiTextPanel.Location = new System.Drawing.Point(72, 201);
                sinavEkleSinavEtkiTextPanel.Size = new System.Drawing.Size(52, 33);
                extraSinavTurLabel.Visible = true;
                sinavEkleSinavTuruTextPanel.Visible = true;
                ekleSinifLabel.Location = new System.Drawing.Point(25, 237);
                ekleSinifLabel.Size = new System.Drawing.Size(36, 15);
                ekleSinavSinifTextPanel.Location = new System.Drawing.Point(57, 233);
                ekleSinavSinifTextPanel.Size = new System.Drawing.Size(72, 25);

                ekleSinavSaatiLabel.Location = new System.Drawing.Point(25, 270);
                ekleSinavSaatiLabel.Size = new System.Drawing.Size(36, 15);
                sinavEkleSaatComboBox.Location = new System.Drawing.Point(92, 267);
                sinavEkleSaatComboBox.Size = new System.Drawing.Size(48, 23);
                sinavEkleDakikaComboBox.Location = new System.Drawing.Point(146, 267);
                sinavEkleDakikaComboBox.Size = new System.Drawing.Size(48, 23);
            }
            else
            {
                extraSinavTurLabel.Visible = false;
                sinavEkleSinavTuruTextPanel.Visible = false;
                this.sinavEkleTarihPicker.Location = new System.Drawing.Point(25, 112);
                this.sinavEkleTarihPicker.Size = new System.Drawing.Size(200, 23);
                etkiLabel.Location = new System.Drawing.Point(25, 148);
                etkiLabel.Size = new System.Drawing.Size(47, 15);
                sinavEkleSinavEtkiTextPanel.Location = new System.Drawing.Point(72, 141);
                sinavEkleSinavEtkiTextPanel.Size = new System.Drawing.Size(52, 33);
                ekleSinifLabel.Location = new System.Drawing.Point(25, 177);
                ekleSinifLabel.Size = new System.Drawing.Size(36, 15);
                ekleSinavSinifTextPanel.Location = new System.Drawing.Point(57, 173);
                ekleSinavSinifTextPanel.Size = new System.Drawing.Size(72, 25);

                ekleSinavSaatiLabel.Location = new System.Drawing.Point(25, 210);
                ekleSinavSaatiLabel.Size = new System.Drawing.Size(36, 15);
                sinavEkleSaatComboBox.Location = new System.Drawing.Point(92, 207);
                sinavEkleSaatComboBox.Size = new System.Drawing.Size(48, 23);
                sinavEkleDakikaComboBox.Location = new System.Drawing.Point(146, 207);
                sinavEkleDakikaComboBox.Size = new System.Drawing.Size(48, 23);
            }
        }
        //--- TEXTBOX PANEL PAÝNT ---
        private void sinavEkleSinavTuruTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(sinavEkleSinavTuruTextPanel, e, System.Drawing.Color.Blue);
        }
        private void sinavEkleSinavEtkiTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(sinavEkleSinavEtkiTextPanel, e, System.Drawing.Color.Blue);
        }
        private void ekleSinavSinifTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(ekleSinavSinifTextPanel, e, System.Drawing.Color.Blue);
        }
        //--- SÝNAV ETKÝ NUMPAD KEYPRESS ---
        private void sinavEkleEtkiTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //----------- DATABASE SINAV KAYDI -----------
        private void sinavEkleNowBut_Click(object sender, EventArgs e)
        {
            sysout(sinavEkleDerslerComboBox.SelectedIndex + "");
            char kolon = this.kolon[(sinavEkleDerslerComboBox.SelectedIndex + 1)];
            if (sinavEkleSinavTuruTextPanel.Visible) //new deðerlendirme
            {
                int extraSayisi = Convert.ToInt32(GoogleSQL.sinavTakvimVeri[20][(sinavEkleDerslerComboBox.SelectedIndex + 1)]);
                int extraSayisiSonuc = Convert.ToInt32(GoogleSQL.sinavSonucVeri[27][(sinavEkleDerslerComboBox.SelectedIndex + 1)]);
                var newSinavSutun = new List<IList<object>> { };
                newSinavSutun.Add(new List<object>() { newSinavTuruNameTextBox.Text });
                newSinavSutun.Add(new List<object>() { sinavEkleTarihPicker.Value.ToShortDateString()+" - "+sinavEkleSaatComboBox.Text+":"
                                                                                                    +sinavEkleDakikaComboBox.Text+":00" });
                newSinavSutun.Add(new List<object>() { ekleSinavSinifTextBox.Text });
                var sinavSonucSutun = new List<IList<object>> { };
                sinavSonucSutun.Add(new List<object>() { newSinavTuruNameTextBox.Text });
                sinavSonucSutun.Add(new List<object>() { "-1" });
                sinavSonucSutun.Add(new List<object>() { sinavEkleEtkiTextBox.Text });
                sinavSonucSutun.Add(new List<object>() { "-1" });
                backEnd.UPDATE(Program.sinavTakvimSheet, newSinavSutun,
                    $"{kolon}" + ((extraSayisi * 3) + 6) + ":" +
                    $"{kolon}" + ((extraSayisi * 3) + 8));
                backEnd.UPDATE(Program.sinavSonucSheet, sinavSonucSutun,
                    $"{kolon}" + ((extraSayisiSonuc * 4) + 8) + ":"+
                    $"{kolon}" + ((extraSayisiSonuc * 4) + 11));
            }
            else
            {
                var newSinavSutun = new List<IList<object>> { };
                newSinavSutun.Add(new List<object>() { sinavEkleTarihPicker.Value.ToShortDateString()+" - "+sinavEkleSaatComboBox.Text+":"
                                                                                                    +sinavEkleDakikaComboBox.Text+":00" });
                newSinavSutun.Add(new List<object>() { ekleSinavSinifTextBox.Text });
                var etki = new List<object> { sinavEkleEtkiTextBox.Text };

                if (sinavEkleSinavTuruComboBox.SelectedIndex == 0) //vize
                {
                    backEnd.UPDATE(Program.sinavTakvimSheet, newSinavSutun, $"{kolon}2:{kolon}3");
                    backEnd.UPDATE(Program.sinavSonucSheet, etki, $"{kolon}3");
                }
                else // geriye final kaldý
                {
                    backEnd.UPDATE(Program.sinavTakvimSheet, newSinavSutun, $"{kolon}4:{kolon}5");
                    backEnd.UPDATE(Program.sinavSonucSheet, etki, $"{kolon}6");
                }
            }
            sinavTakvimBut_Click(sender, e);
        }
        //--- SINAV PANELI CEKME ---
        int sinavLocationNo = 0; // ayrýca panel no
        bool gecmisSinavlarGozuksun = true;
        List<Panel> sinavlar = new List<Panel>();
        public void sinavPanel()
        {
            sinavlar.Clear();
            sinavLocationNo = 0;
            sinavListPanel.Controls.Clear();
            byte[] sinavSatirNo = { 1, 3, 5, 8, 11, 14, 17 };
            backEnd.sinavTavkimVeri();
            backEnd.sinavSonucVeri();
            for (int i = 0; i < Settings.dersSayisi; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    try
                    {
                        if (GoogleSQL.sinavTakvimVeri[sinavSatirNo[j]][i + 1] != null && GoogleSQL.sinavTakvimVeri[sinavSatirNo[j]][i + 1].ToString() != String.Empty)
                        {
                            Panel p = new Panel();
                            p.Name = "sinav" + sinavLocationNo;
                            p.Location = new System.Drawing.Point(10, 10 + ((sinavLocationNo) * 80));
                            p.Size = new System.Drawing.Size((sinavListPanel.Width - 29), 75);
                            p.BackColor = backEnd.dersRenkleri[Convert.ToByte(GoogleSQL.dersProgVeri[i + 1][15])];


                            // newSinavAKTSLabel
                            Label newSinavAKTSLabel = new Label();
                            newSinavAKTSLabel.AutoSize = true;
                            newSinavAKTSLabel.Location = new System.Drawing.Point(p.Width - 119, 10);
                            newSinavAKTSLabel.Size = new System.Drawing.Size(72, 15);
                            newSinavAKTSLabel.TabIndex = 1;
                            newSinavAKTSLabel.Text = "Ders AKTS: " + GoogleSQL.dersProgVeri[i + 1][16].ToString();
                            // newSinavSinifLabel
                            Label newSinavSinifLabel = new Label();
                            newSinavSinifLabel.AutoSize = true;
                            newSinavSinifLabel.Location = new System.Drawing.Point(10, 50);
                            newSinavSinifLabel.Size = new System.Drawing.Size(116, 15);
                            newSinavSinifLabel.TabIndex = 1;
                            newSinavSinifLabel.Text = "Sýnav Sýnýfý: " + GoogleSQL.sinavTakvimVeri[sinavSatirNo[j] + 1][i + 1].ToString();
                            // newSinavEtkiLabel
                            Label newSinavEtkiLabel = new Label();
                            newSinavEtkiLabel.AutoSize = true;
                            newSinavEtkiLabel.Location = new System.Drawing.Point(p.Width - 119, 30);
                            newSinavEtkiLabel.Size = new System.Drawing.Size(109, 15);
                            newSinavEtkiLabel.TabIndex = 1;
                            newSinavEtkiLabel.Text = "Sýnavýn Etkisi: %100";
                            // newSinavTarihSaatLabel
                            Label newSinavTarihSaatLabel = new Label();
                            newSinavTarihSaatLabel.AutoSize = true;
                            newSinavTarihSaatLabel.Location = new System.Drawing.Point(10, 30);
                            newSinavTarihSaatLabel.Size = new System.Drawing.Size(188, 15);
                            newSinavTarihSaatLabel.TabIndex = 1;
                            newSinavTarihSaatLabel.Text = "Sýnav Tarih-Saat: " + GoogleSQL.sinavTakvimVeri[sinavSatirNo[j]][i + 1].ToString();
                            // newSinavAdLabel
                            Label newSinavAdLabel = new Label();
                            newSinavAdLabel.Location = new System.Drawing.Point(10, 10);
                            newSinavAdLabel.Size = new System.Drawing.Size(154, 15);
                            newSinavAdLabel.AutoSize = true;
                            newSinavAdLabel.MaximumSize = new System.Drawing.Size(154, 15);
                            newSinavAdLabel.TabIndex = 1;
                            newSinavAdLabel.Text = GoogleSQL.sinavTakvimVeri[0][i + 1].ToString();
                            // newSinavTurLabel
                            Label newSinavTurLabel = new Label();
                            newSinavTurLabel.AutoSize = true;
                            newSinavTurLabel.Location = new System.Drawing.Point(159, 10);
                            newSinavTurLabel.Size = new System.Drawing.Size(74, 15);
                            newSinavTurLabel.TabIndex = 1;
                            newSinavTurLabel.Text = "- Final Sýnavý";

                            string sinavTarihString = GoogleSQL.sinavTakvimVeri[sinavSatirNo[j]][i + 1].ToString(); //tarih karþýlaþtýrma için
                            switch (sinavSatirNo[j])
                            {
                                case 1:
                                    newSinavTurLabel.Text = "- Vize Sýnavý";
                                    newSinavEtkiLabel.Text = "Sýnavýn Etkisi: %" + GoogleSQL.sinavSonucVeri[2][i + 1].ToString(); break;
                                case 3:
                                    newSinavTurLabel.Text = "- Final Sýnavý";
                                    newSinavEtkiLabel.Text = "Sýnavýn Etkisi: %" + GoogleSQL.sinavSonucVeri[5][i + 1].ToString(); break;
                                case 5:
                                case 8:
                                case 11:
                                case 14:
                                case 17:
                                    newSinavTarihSaatLabel.Text = "Sýnav Tarih-Saat: " + GoogleSQL.sinavTakvimVeri[sinavSatirNo[j] + 1][i + 1].ToString();
                                    newSinavTurLabel.Text = "- " + GoogleSQL.sinavTakvimVeri[sinavSatirNo[j]][i + 1].ToString();
                                    newSinavSinifLabel.Text = "Sýnav Sýnýfý: " + GoogleSQL.sinavTakvimVeri[sinavSatirNo[j] + 2][i + 1].ToString();
                                    newSinavEtkiLabel.Text = "Sýnavýn Etkisi: %" + GoogleSQL.sinavSonucVeri[sinavSatirNo[j] + 4 + (j - 2)][i + 1].ToString();
                                    sinavTarihString = GoogleSQL.sinavTakvimVeri[sinavSatirNo[j] + 1][i + 1].ToString(); break;
                            }


                            sinavTarihString = sinavTarihString.Replace("-", "").Replace("  ", " ");
                            DateTime sinavTarih = Convert.ToDateTime(sinavTarihString);

                            if (DateTime.Compare(DateTime.Now, sinavTarih) > 0)
                            {
                                p.BackColor = System.Drawing.Color.Red;
                                // ekleSinavBittiLabel
                                Label ekleSinavBittiLabel = new Label();
                                ekleSinavBittiLabel.AutoSize = true;
                                ekleSinavBittiLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                ekleSinavBittiLabel.Location = new System.Drawing.Point(243, 33);
                                ekleSinavBittiLabel.Name = "ekleSinavBittiLabel";
                                ekleSinavBittiLabel.Size = new System.Drawing.Size(152, 32);
                                ekleSinavBittiLabel.TabIndex = 1;
                                ekleSinavBittiLabel.Text = "SINAV BÝTTÝ";
                                p.Controls.Add(ekleSinavBittiLabel);
                            }

                            if (gecmisSinavlarGozuksun || p.BackColor != System.Drawing.Color.Red)
                            {
                                p.Controls.Add(newSinavAKTSLabel);
                                p.Controls.Add(newSinavTurLabel);
                                p.Controls.Add(newSinavSinifLabel);
                                p.Controls.Add(newSinavEtkiLabel);
                                p.Controls.Add(newSinavTarihSaatLabel);
                                p.Controls.Add(newSinavAdLabel);


                                sinavlar.Add(p);
                                //sinavListPanel.Controls.Add(p);
                                sinavLocationNo++;
                            }



                            for (int k = 0; k < sinavlar.Count; k++)
                                sinavListPanel.Controls.Add(sinavlar[k]);

                        }
                    }
                    catch (Exception)
                    {
                        //BOS VERI HATASINI TUTACAK
                    }




                }
            }


        }
        private void sinavListPanel_SizeChanged(object sender, EventArgs e)
        {
            //sinavTakvimBut_Click(sender, e);
        }





        public void temaChanger(byte index)
        {
            Button[] b =
            {
                dersProgBut,
                dersEkleBut,
                dersCizelgeBut,
                devamDurumBut,
                sinavTakvimBut,
                sinavEkleBut,
                sinavSonucBut,
                extraNotBut,
                extraNotEkleBut,
                currentNotEkleBut
            };
            switch (index)
            {
                // LÝGHT MODE
                case 0:
                    Settings.darkModeSeviye = 0;
                    this.BackColor = System.Drawing.Color.White; //form color
                    ustMenu.BackColor = System.Drawing.Color.White;
                    ustPanel.BackColor = System.Drawing.Color.White;
                    ustMenu.ForeColor = System.Drawing.Color.Black;
                    solPanel.BackColor = System.Drawing.Color.AntiqueWhite;
                    break;
                // DARK MODE
                case 1:
                    Settings.darkModeSeviye = 1;
                    this.BackColor = System.Drawing.Color.DimGray; //form color
                    ustMenu.BackColor = System.Drawing.Color.DimGray;
                    ustPanel.BackColor = System.Drawing.Color.DimGray;
                    ustMenu.ForeColor = System.Drawing.Color.Black;
                    solPanel.BackColor = System.Drawing.Color.SlateGray;

                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i].BackColor = System.Drawing.Color.Transparent;
                        b[i].FlatAppearance.BorderSize = 1;
                        b[i].FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                        b[i].FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                        b[i].ForeColor = System.Drawing.SystemColors.ControlText;
                    }


                    break;
                // OLED MODE
                case 2:
                    Settings.darkModeSeviye = 2;
                    this.BackColor = System.Drawing.Color.Black; //form color
                    ustMenu.BackColor = System.Drawing.Color.Black;
                    ustPanel.BackColor = System.Drawing.Color.Black;
                    ustMenu.ForeColor = System.Drawing.Color.White;
                    solPanel.BackColor = System.Drawing.Color.Black;

                    for(int i=0; i<b.Length; i++)
                    {
                        b[i].BackColor = System.Drawing.Color.DimGray;
                        b[i].FlatAppearance.BorderSize = 0;
                        b[i].FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
                        b[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        b[i].ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }


                    pazartesiPanel.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);
                    saliPanel.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                    carsambaPanel.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);
                    persembePanel.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                    cumaPanel.BackColor = System.Drawing.Color.FromArgb(38, 38, 38);
                    cumartesiPanel.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                    pazarPanel.BackColor = System.Drawing.Color.FromArgb(166, 166, 166);

                    System.Drawing.Color[] dersRenkleri = { 
                        System.Drawing.Color.FromArgb(224, 62, 82),
                        System.Drawing.Color.FromArgb(28,82,83),
                        System.Drawing.Color.FromArgb(84,85,108),
                        System.Drawing.Color.FromArgb(17,93,118),
                        System.Drawing.Color.FromArgb(211,182,41),
                        System.Drawing.Color.FromArgb(45,91,107),
                        System.Drawing.Color.FromArgb(196,122,83),
                        System.Drawing.Color.FromArgb(143,71,49),
                        System.Drawing.Color.FromArgb(82,73,76),
                        System.Drawing.Color.FromArgb(123,125,42),
                    };
                    backEnd.dersRenkleri = dersRenkleri;

                    



                        //161313(22, 19, 19)
                        //060605(6, 6, 5)
                        //0c180f(12, 24, 15)
                        //0d151a(13, 21, 26)
                        //160314(22, 3, 20)
                    sinavEklePanel.BackColor = System.Drawing.Color.Black;
                    sinavListPanel.BackColor = System.Drawing.Color.Black;
                    extraNotPanel.BackColor = System.Drawing.Color.FromArgb(22, 3, 20);
                    notEklePanel.BackColor = System.Drawing.Color.FromArgb(22, 3, 20);
                    notBaslikPanel.BackColor = System.Drawing.Color.FromArgb(90, 12, 82);
                    notBaslikLabel.ForeColor = System.Drawing.Color.White;
                    notBaslikTextBox.BackColor = System.Drawing.Color.FromArgb(90, 12, 82);
                    notBaslikTextBox.ForeColor = System.Drawing.Color.White;
                    extraNotEkleTarihLabel.ForeColor = System.Drawing.Color.White;
                    notTextBox.BackColor = System.Drawing.Color.DimGray;
                    notTextBox.ForeColor = System.Drawing.Color.White;
                    sinavSonucPanel.BackColor = System.Drawing.Color.Black;

                    break;
            }

            
        }

        public void temaChangerforGozSagligi()
        {

        }




        private void ComboBox_Enter(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox.DropDownStyle != ComboBoxStyle.DropDownList)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.SelectedIndex = 0;
                comboBox.DroppedDown = true;
            }
                
        }
        //DERS EKLE TEXT BOX ELLÝPS
        private void dersAdiTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(dersAdiTextPanel, e, System.Drawing.Color.Black);
        }
        private void dersSinifTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(dersSinifTextPanel, e, System.Drawing.Color.Black);
        }
        private void dersHocaTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(dersHocaTextPanel, e, System.Drawing.Color.Black);
        }
        private void dersAdiKisaltmaTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(dersAdiKisaltmaTextPanel, e, System.Drawing.Color.Black);
        }
        private void dersAKTSTextPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxAltiCiz(dersAKTSTextPanel, e, System.Drawing.Color.Black);
        }
        // / / / / / / / / // ////////////////////////////////////////////


        //SAAT CHANGE ile DAKÝKA ATAMA
        private void ekleDersBaslangicSaatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ekleDersBaslangicDakikaComboBox.DropDownStyle != ComboBoxStyle.DropDownList)
            {
                ekleDersBaslangicDakikaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                ekleDersBaslangicDakikaComboBox.SelectedIndex = 0;
            }
        }
        private void ekleDersBitisSaatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ekleDersBitisDakikaComboBox.DropDownStyle != ComboBoxStyle.DropDownList)
            {
                ekleDersBitisDakikaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                ekleDersBitisDakikaComboBox.SelectedIndex = 0;
            }
        }

        private void dersNotuPanel_SizeChanged(object sender, EventArgs e)
        {
            dersNotuTextBox.Size = new Size(dersNotuPanel.Width - 256, dersNotuPanel.Height - 40);
        }

        private void kaynakcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAc(kaynakcaPanel);
        }
    }
}