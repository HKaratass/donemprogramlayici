namespace DONEMPROGRAMLAYICI
{
    partial class GirisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisForm));
            this.kayitOlLink = new System.Windows.Forms.LinkLabel();
            this.ustPanel = new System.Windows.Forms.Panel();
            this.cikisBut = new System.Windows.Forms.Button();
            this.connectBut = new System.Windows.Forms.Button();
            this.connectButLbl = new System.Windows.Forms.Label();
            this.withSocialMediaLbl = new System.Windows.Forms.Label();
            this.appleBut = new System.Windows.Forms.Button();
            this.fbBut = new System.Windows.Forms.Button();
            this.linkedinBut = new System.Windows.Forms.Button();
            this.twitterBut = new System.Windows.Forms.Button();
            this.winBut = new System.Windows.Forms.Button();
            this.rememberMe = new System.Windows.Forms.CheckBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.uyariLabel = new System.Windows.Forms.Label();
            this.ustPanel.SuspendLayout();
            this.connectBut.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kayitOlLink
            // 
            this.kayitOlLink.ActiveLinkColor = System.Drawing.Color.Blue;
            this.kayitOlLink.BackColor = System.Drawing.Color.Black;
            this.kayitOlLink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.kayitOlLink.Location = new System.Drawing.Point(0, 246);
            this.kayitOlLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kayitOlLink.Name = "kayitOlLink";
            this.kayitOlLink.Size = new System.Drawing.Size(380, 20);
            this.kayitOlLink.TabIndex = 3;
            this.kayitOlLink.TabStop = true;
            this.kayitOlLink.Text = "Google Kayıt Ol";
            this.kayitOlLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.kayitOlLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.kayitOlLink_LinkClicked);
            // 
            // ustPanel
            // 
            this.ustPanel.BackColor = System.Drawing.Color.White;
            this.ustPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.ustBanner;
            this.ustPanel.Controls.Add(this.cikisBut);
            this.ustPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ustPanel.Location = new System.Drawing.Point(0, 0);
            this.ustPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ustPanel.Name = "ustPanel";
            this.ustPanel.Size = new System.Drawing.Size(380, 60);
            this.ustPanel.TabIndex = 0;
            this.ustPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ustPanel_MouseDown);
            // 
            // cikisBut
            // 
            this.cikisBut.BackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.BorderSize = 0;
            this.cikisBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RosyBrown;
            this.cikisBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cikisBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.close_30_pink;
            this.cikisBut.Location = new System.Drawing.Point(334, 11);
            this.cikisBut.Margin = new System.Windows.Forms.Padding(2);
            this.cikisBut.Name = "cikisBut";
            this.cikisBut.Size = new System.Drawing.Size(35, 35);
            this.cikisBut.TabIndex = 0;
            this.cikisBut.TabStop = false;
            this.cikisBut.UseVisualStyleBackColor = false;
            this.cikisBut.Click += new System.EventHandler(this.cikisBut_Click);
            // 
            // connectBut
            // 
            this.connectBut.BackColor = System.Drawing.Color.Moccasin;
            this.connectBut.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.button2s;
            this.connectBut.Controls.Add(this.connectButLbl);
            this.connectBut.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.connectBut.FlatAppearance.BorderSize = 0;
            this.connectBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectBut.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.connectBut.ForeColor = System.Drawing.Color.Black;
            this.connectBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_google_96;
            this.connectBut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectBut.Location = new System.Drawing.Point(63, 116);
            this.connectBut.Margin = new System.Windows.Forms.Padding(2);
            this.connectBut.Name = "connectBut";
            this.connectBut.Size = new System.Drawing.Size(254, 103);
            this.connectBut.TabIndex = 1;
            this.connectBut.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.connectBut.UseVisualStyleBackColor = false;
            this.connectBut.Click += new System.EventHandler(this.connectBut_Click);
            this.connectBut.MouseEnter += new System.EventHandler(this.connectBut_MouseEnter);
            this.connectBut.MouseLeave += new System.EventHandler(this.connectBut_MouseLeave);
            // 
            // connectButLbl
            // 
            this.connectButLbl.BackColor = System.Drawing.Color.Transparent;
            this.connectButLbl.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.connectButLbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.connectButLbl.Location = new System.Drawing.Point(94, 0);
            this.connectButLbl.Name = "connectButLbl";
            this.connectButLbl.Size = new System.Drawing.Size(160, 103);
            this.connectButLbl.TabIndex = 0;
            this.connectButLbl.Text = "Google Hesabı\r\nile\r\nGiriş Yap";
            this.connectButLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.connectButLbl.Click += new System.EventHandler(this.connectButLbl_Click);
            this.connectButLbl.MouseEnter += new System.EventHandler(this.connectButLbl_MouseEnter);
            this.connectButLbl.MouseLeave += new System.EventHandler(this.connectButLbl_MouseLeave);
            // 
            // withSocialMediaLbl
            // 
            this.withSocialMediaLbl.BackColor = System.Drawing.Color.Transparent;
            this.withSocialMediaLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.withSocialMediaLbl.ForeColor = System.Drawing.Color.White;
            this.withSocialMediaLbl.Location = new System.Drawing.Point(0, 435);
            this.withSocialMediaLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.withSocialMediaLbl.Name = "withSocialMediaLbl";
            this.withSocialMediaLbl.Size = new System.Drawing.Size(380, 21);
            this.withSocialMediaLbl.TabIndex = 10;
            this.withSocialMediaLbl.Text = "Social Media ile Giriş Yap";
            this.withSocialMediaLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // appleBut
            // 
            this.appleBut.BackColor = System.Drawing.Color.Transparent;
            this.appleBut.FlatAppearance.BorderSize = 0;
            this.appleBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.appleBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.appleBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.appleBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appleBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_apple_logo_30_beyaz;
            this.appleBut.Location = new System.Drawing.Point(169, 458);
            this.appleBut.Margin = new System.Windows.Forms.Padding(2);
            this.appleBut.Name = "appleBut";
            this.appleBut.Size = new System.Drawing.Size(35, 35);
            this.appleBut.TabIndex = 6;
            this.appleBut.UseVisualStyleBackColor = false;
            this.appleBut.Click += new System.EventHandler(this.appleBut_Click);
            // 
            // fbBut
            // 
            this.fbBut.BackColor = System.Drawing.Color.Transparent;
            this.fbBut.FlatAppearance.BorderSize = 0;
            this.fbBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.fbBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.fbBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.fbBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fbBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_facebook_481;
            this.fbBut.Location = new System.Drawing.Point(95, 458);
            this.fbBut.Margin = new System.Windows.Forms.Padding(2);
            this.fbBut.Name = "fbBut";
            this.fbBut.Size = new System.Drawing.Size(35, 35);
            this.fbBut.TabIndex = 4;
            this.fbBut.UseVisualStyleBackColor = false;
            this.fbBut.Click += new System.EventHandler(this.fbBut_Click);
            // 
            // linkedinBut
            // 
            this.linkedinBut.BackColor = System.Drawing.Color.Transparent;
            this.linkedinBut.FlatAppearance.BorderSize = 0;
            this.linkedinBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.linkedinBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.linkedinBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.linkedinBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.linkedinBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_linkedin_48;
            this.linkedinBut.Location = new System.Drawing.Point(206, 458);
            this.linkedinBut.Margin = new System.Windows.Forms.Padding(2);
            this.linkedinBut.Name = "linkedinBut";
            this.linkedinBut.Size = new System.Drawing.Size(35, 35);
            this.linkedinBut.TabIndex = 7;
            this.linkedinBut.UseVisualStyleBackColor = false;
            this.linkedinBut.Click += new System.EventHandler(this.linkedinBut_Click);
            // 
            // twitterBut
            // 
            this.twitterBut.BackColor = System.Drawing.Color.Transparent;
            this.twitterBut.FlatAppearance.BorderSize = 0;
            this.twitterBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.twitterBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.twitterBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.twitterBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.twitterBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_twitter_48;
            this.twitterBut.Location = new System.Drawing.Point(132, 458);
            this.twitterBut.Margin = new System.Windows.Forms.Padding(2);
            this.twitterBut.Name = "twitterBut";
            this.twitterBut.Size = new System.Drawing.Size(35, 35);
            this.twitterBut.TabIndex = 5;
            this.twitterBut.UseVisualStyleBackColor = false;
            this.twitterBut.Click += new System.EventHandler(this.twitterBut_Click);
            // 
            // winBut
            // 
            this.winBut.BackColor = System.Drawing.Color.Transparent;
            this.winBut.FlatAppearance.BorderSize = 0;
            this.winBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.winBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.winBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.winBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.winBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.icons8_windows_10_30;
            this.winBut.Location = new System.Drawing.Point(243, 458);
            this.winBut.Margin = new System.Windows.Forms.Padding(2);
            this.winBut.Name = "winBut";
            this.winBut.Size = new System.Drawing.Size(35, 35);
            this.winBut.TabIndex = 8;
            this.winBut.UseVisualStyleBackColor = false;
            this.winBut.Click += new System.EventHandler(this.winBut_Click);
            // 
            // rememberMe
            // 
            this.rememberMe.AutoSize = true;
            this.rememberMe.BackColor = System.Drawing.Color.Black;
            this.rememberMe.ForeColor = System.Drawing.Color.White;
            this.rememberMe.Location = new System.Drawing.Point(62, 224);
            this.rememberMe.Name = "rememberMe";
            this.rememberMe.Size = new System.Drawing.Size(87, 19);
            this.rememberMe.TabIndex = 2;
            this.rememberMe.Text = "Beni Hatırla";
            this.rememberMe.UseVisualStyleBackColor = false;
            this.rememberMe.CheckedChanged += new System.EventHandler(this.rememberMe_CheckedChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.LightGray;
            this.mainPanel.BackgroundImage = global::DONEMPROGRAMLAYICI.Properties.Resources.altBanner;
            this.mainPanel.Controls.Add(this.uyariLabel);
            this.mainPanel.Controls.Add(this.connectBut);
            this.mainPanel.Controls.Add(this.rememberMe);
            this.mainPanel.Controls.Add(this.kayitOlLink);
            this.mainPanel.Controls.Add(this.withSocialMediaLbl);
            this.mainPanel.Controls.Add(this.winBut);
            this.mainPanel.Controls.Add(this.appleBut);
            this.mainPanel.Controls.Add(this.twitterBut);
            this.mainPanel.Controls.Add(this.fbBut);
            this.mainPanel.Controls.Add(this.linkedinBut);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 60);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(380, 500);
            this.mainPanel.TabIndex = 15;
            // 
            // uyariLabel
            // 
            this.uyariLabel.BackColor = System.Drawing.Color.Transparent;
            this.uyariLabel.ForeColor = System.Drawing.Color.Red;
            this.uyariLabel.Location = new System.Drawing.Point(62, 246);
            this.uyariLabel.Name = "uyariLabel";
            this.uyariLabel.Size = new System.Drawing.Size(252, 0);
            this.uyariLabel.TabIndex = 15;
            this.uyariLabel.Text = "Uyarı: Sadece kişisel bilgisayarınızda bu özelliği\r\naktif edin.";
            // 
            // GirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 560);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ustPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(380, 560);
            this.MinimumSize = new System.Drawing.Size(380, 560);
            this.Name = "GirisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GİRİŞ PANEL";
            this.ustPanel.ResumeLayout(false);
            this.connectBut.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private LinkLabel kayitOlLink;
        private Panel ustPanel;
        private Button connectBut;
        private Button cikisBut;
        private Label withSocialMediaLbl;
        private Button appleBut;
        private Button fbBut;
        private Button linkedinBut;
        private Button twitterBut;
        private Button winBut;
        private CheckBox rememberMe;
        private Panel mainPanel;
        private Label connectButLbl;
        private Label uyariLabel;
    }
}