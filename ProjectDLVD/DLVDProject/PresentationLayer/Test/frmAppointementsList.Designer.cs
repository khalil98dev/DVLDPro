namespace PresentationLayer.Test
{
    partial class frmAppointementsList
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
            this.components = new System.ComponentModel.Container();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFrandTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAppointements = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.pcTestTypeImage1 = new System.Windows.Forms.PictureBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.pcTestTypeImage = new System.Windows.Forms.PictureBox();
            this.ucLocalDrivingApplicationinfos1 = new PresentationLayer.Applications.LocalDrivingLicenseApplication.ucLocalDrivingApplicationinfos();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1014, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(43, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 22);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Vision Test Appointement";
            // 
            // lblFrandTitle
            // 
            this.lblFrandTitle.AutoSize = true;
            this.lblFrandTitle.Font = new System.Drawing.Font("Montserrat", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrandTitle.Location = new System.Drawing.Point(325, 26);
            this.lblFrandTitle.Name = "lblFrandTitle";
            this.lblFrandTitle.Size = new System.Drawing.Size(411, 40);
            this.lblFrandTitle.TabIndex = 2;
            this.lblFrandTitle.Text = "Vision Test Appointement";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 782);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Appointements";
            // 
            // dgvAppointements
            // 
            this.dgvAppointements.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAppointements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointements.Location = new System.Drawing.Point(23, 814);
            this.dgvAppointements.Name = "dgvAppointements";
            this.dgvAppointements.Size = new System.Drawing.Size(1026, 236);
            this.dgvAppointements.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 1065);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "# Records: ";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(127, 1065);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(70, 26);
            this.lblRecords.TabIndex = 7;
            this.lblRecords.Text = "[????]";
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(6, 35);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(275, 10);
            this.guna2Separator1.TabIndex = 10;
            // 
            // pcTestTypeImage1
            // 
            this.pcTestTypeImage1.Image = global::PresentationLayer.Properties.Resources.Vision_512;
            this.pcTestTypeImage1.Location = new System.Drawing.Point(3, 10);
            this.pcTestTypeImage1.Name = "pcTestTypeImage1";
            this.pcTestTypeImage1.Size = new System.Drawing.Size(31, 24);
            this.pcTestTypeImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcTestTypeImage1.TabIndex = 11;
            this.pcTestTypeImage1.TabStop = false;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.BorderRadius = 31;
            this.guna2Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Empty;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = global::PresentationLayer.Properties.Resources.AddIcon;
            this.guna2Button1.ImageSize = new System.Drawing.Size(36, 36);
            this.guna2Button1.Location = new System.Drawing.Point(982, 744);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(67, 64);
            this.guna2Button1.TabIndex = 8;
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // pcTestTypeImage
            // 
            this.pcTestTypeImage.Image = global::PresentationLayer.Properties.Resources.Vision_512;
            this.pcTestTypeImage.Location = new System.Drawing.Point(396, 69);
            this.pcTestTypeImage.Name = "pcTestTypeImage";
            this.pcTestTypeImage.Size = new System.Drawing.Size(281, 141);
            this.pcTestTypeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcTestTypeImage.TabIndex = 3;
            this.pcTestTypeImage.TabStop = false;
            // 
            // ucLocalDrivingApplicationinfos1
            // 
            this.ucLocalDrivingApplicationinfos1.Location = new System.Drawing.Point(12, 216);
            this.ucLocalDrivingApplicationinfos1.Name = "ucLocalDrivingApplicationinfos1";
            this.ucLocalDrivingApplicationinfos1.Size = new System.Drawing.Size(1037, 522);
            this.ucLocalDrivingApplicationinfos1.TabIndex = 4;
            // 
            // frmAppointementsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 1100);
            this.Controls.Add(this.pcTestTypeImage1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvAppointements);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucLocalDrivingApplicationinfos1);
            this.Controls.Add(this.pcTestTypeImage);
            this.Controls.Add(this.lblFrandTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAppointementsList";
            this.Text = "frmSheduleTest";
            this.Load += new System.EventHandler(this.frmSheduleTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFrandTitle;
        private System.Windows.Forms.PictureBox pcTestTypeImage;
        private Applications.LocalDrivingLicenseApplication.ucLocalDrivingApplicationinfos ucLocalDrivingApplicationinfos1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvAppointements;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecords;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.PictureBox pcTestTypeImage1;
    }
}