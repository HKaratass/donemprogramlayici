namespace DONEMPROGRAMLAYICI
{
    partial class UyariPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UyariPanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cikisBut = new System.Windows.Forms.Button();
            this.captionLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Controls.Add(this.cikisBut);
            this.panel1.Controls.Add(this.captionLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 40);
            this.panel1.TabIndex = 0;
            // 
            // cikisBut
            // 
            this.cikisBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cikisBut.BackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.BorderSize = 0;
            this.cikisBut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cikisBut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RosyBrown;
            this.cikisBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cikisBut.Image = global::DONEMPROGRAMLAYICI.Properties.Resources.close_30_pink;
            this.cikisBut.Location = new System.Drawing.Point(351, 2);
            this.cikisBut.Margin = new System.Windows.Forms.Padding(2);
            this.cikisBut.Name = "cikisBut";
            this.cikisBut.Size = new System.Drawing.Size(35, 35);
            this.cikisBut.TabIndex = 10;
            this.cikisBut.UseVisualStyleBackColor = false;
            this.cikisBut.Click += new System.EventHandler(this.cikisBut_Click);
            // 
            // captionLabel
            // 
            this.captionLabel.AutoSize = true;
            this.captionLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.captionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.captionLabel.Location = new System.Drawing.Point(4, 2);
            this.captionLabel.Name = "captionLabel";
            this.captionLabel.Size = new System.Drawing.Size(152, 30);
            this.captionLabel.TabIndex = 1;
            this.captionLabel.Text = "GİRDİ HATASI";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.messageLabel);
            this.panel2.Controls.Add(this.okButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 142);
            this.panel2.TabIndex = 1;
            // 
            // messageLabel
            // 
            this.messageLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.messageLabel.Location = new System.Drawing.Point(4, 10);
            this.messageLabel.MaximumSize = new System.Drawing.Size(380, 95);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(380, 95);
            this.messageLabel.TabIndex = 2;
            this.messageLabel.Text = resources.GetString("messageLabel.Text");
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.LightGray;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Location = new System.Drawing.Point(263, 104);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(120, 33);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Tamam";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.cikisBut_Click);
            // 
            // UyariPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 182);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UyariPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button cikisBut;
        private Button okButton;
        private Label captionLabel;
        private Label messageLabel;
    }
}