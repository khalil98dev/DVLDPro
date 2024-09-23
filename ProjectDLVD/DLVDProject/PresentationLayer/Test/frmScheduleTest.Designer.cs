namespace PresentationLayer.Test
{
    partial class frmScheduleTest
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
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pcTestTypeImage1 = new System.Windows.Forms.PictureBox();
            this.ucSceduleTest1 = new PresentationLayer.Test.ucSceduleTest();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(8, 34);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(173, 11);
            this.guna2Separator1.TabIndex = 13;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(45, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(136, 22);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Schedule Test";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(508, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 36);
            this.button1.TabIndex = 15;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pcTestTypeImage1
            // 
            this.pcTestTypeImage1.Image = global::PresentationLayer.Properties.Resources.Vision_512;
            this.pcTestTypeImage1.Location = new System.Drawing.Point(8, 9);
            this.pcTestTypeImage1.Name = "pcTestTypeImage1";
            this.pcTestTypeImage1.Size = new System.Drawing.Size(31, 24);
            this.pcTestTypeImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcTestTypeImage1.TabIndex = 14;
            this.pcTestTypeImage1.TabStop = false;
            // 
            // ucSceduleTest1
            // 
            this.ucSceduleTest1.ApplicationType = BusinessLayer.clsApplications.eApplicationType.eNewDrivingLicense;
            this.ucSceduleTest1.Location = new System.Drawing.Point(8, 51);
            this.ucSceduleTest1.Mode = PresentationLayer.Test.ucSceduleTest.eMode.eAddNewTest;
            this.ucSceduleTest1.Name = "ucSceduleTest1";
            this.ucSceduleTest1.Size = new System.Drawing.Size(542, 772);
            this.ucSceduleTest1.TabIndex = 16;
            this.ucSceduleTest1.TestType = BusinessLayer.clsTestTypes.eTestTypes.eVisionTest;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 831);
            this.Controls.Add(this.ucSceduleTest1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pcTestTypeImage1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScheduleTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmScheduleTest";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcTestTypeImage1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcTestTypeImage1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button button1;
        private ucSceduleTest ucSceduleTest1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}