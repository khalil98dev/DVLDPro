namespace PresentationLayer.Forms
{
    partial class MainMenue
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appliocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driverLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replacementForLoseOrDamageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relaiseDetainDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenceApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalDrivingLicenceApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managePeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDriversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showUserDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ressetPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appliocationToolStripMenuItem,
            this.managePeopleToolStripMenuItem,
            this.manageUsersToolStripMenuItem,
            this.manageDriversToolStripMenuItem,
            this.accountSettingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(10);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(20);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1238, 82);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // appliocationToolStripMenuItem
            // 
            this.appliocationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationTypesToolStripMenuItem,
            this.manageTestTypesToolStripMenuItem,
            this.driverLicenceToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem});
            this.appliocationToolStripMenuItem.Name = "appliocationToolStripMenuItem";
            this.appliocationToolStripMenuItem.Size = new System.Drawing.Size(115, 42);
            this.appliocationToolStripMenuItem.Text = "Application";
            // 
            // applicationTypesToolStripMenuItem
            // 
            this.applicationTypesToolStripMenuItem.Name = "applicationTypesToolStripMenuItem";
            this.applicationTypesToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.applicationTypesToolStripMenuItem.Text = "Application Types";
            this.applicationTypesToolStripMenuItem.Click += new System.EventHandler(this.applicationTypesToolStripMenuItem_Click);
            // 
            // manageTestTypesToolStripMenuItem
            // 
            this.manageTestTypesToolStripMenuItem.Name = "manageTestTypesToolStripMenuItem";
            this.manageTestTypesToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.manageTestTypesToolStripMenuItem.Text = "Manage Test Types";
            this.manageTestTypesToolStripMenuItem.Click += new System.EventHandler(this.manageTestTypesToolStripMenuItem_Click);
            // 
            // driverLicenceToolStripMenuItem
            // 
            this.driverLicenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenceToolStripMenuItem,
            this.renewDrivingLicenceToolStripMenuItem,
            this.replacementForLoseOrDamageToolStripMenuItem,
            this.relaiseDetainDrivingLicenceToolStripMenuItem,
            this.retakeTestToolStripMenuItem});
            this.driverLicenceToolStripMenuItem.Name = "driverLicenceToolStripMenuItem";
            this.driverLicenceToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.driverLicenceToolStripMenuItem.Text = "Driver licence Services";
            this.driverLicenceToolStripMenuItem.Click += new System.EventHandler(this.driverLicenceToolStripMenuItem_Click);
            // 
            // newDrivingLicenceToolStripMenuItem
            // 
            this.newDrivingLicenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localLicenceToolStripMenuItem,
            this.internationalLicenceToolStripMenuItem});
            this.newDrivingLicenceToolStripMenuItem.Name = "newDrivingLicenceToolStripMenuItem";
            this.newDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(368, 22);
            this.newDrivingLicenceToolStripMenuItem.Text = "New Driving Licence";
            // 
            // localLicenceToolStripMenuItem
            // 
            this.localLicenceToolStripMenuItem.Name = "localLicenceToolStripMenuItem";
            this.localLicenceToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.localLicenceToolStripMenuItem.Text = "Local Licence";
            this.localLicenceToolStripMenuItem.Click += new System.EventHandler(this.localLicenceToolStripMenuItem_Click);
            // 
            // internationalLicenceToolStripMenuItem
            // 
            this.internationalLicenceToolStripMenuItem.Name = "internationalLicenceToolStripMenuItem";
            this.internationalLicenceToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.internationalLicenceToolStripMenuItem.Text = "International Licence";
            // 
            // renewDrivingLicenceToolStripMenuItem
            // 
            this.renewDrivingLicenceToolStripMenuItem.Name = "renewDrivingLicenceToolStripMenuItem";
            this.renewDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(368, 22);
            this.renewDrivingLicenceToolStripMenuItem.Text = "Renew Driving Licence";
            // 
            // replacementForLoseOrDamageToolStripMenuItem
            // 
            this.replacementForLoseOrDamageToolStripMenuItem.Name = "replacementForLoseOrDamageToolStripMenuItem";
            this.replacementForLoseOrDamageToolStripMenuItem.Size = new System.Drawing.Size(368, 22);
            this.replacementForLoseOrDamageToolStripMenuItem.Text = "Replacement For lost or Damage ";
            this.replacementForLoseOrDamageToolStripMenuItem.Click += new System.EventHandler(this.replacementForLoseOrDamageToolStripMenuItem_Click);
            // 
            // relaiseDetainDrivingLicenceToolStripMenuItem
            // 
            this.relaiseDetainDrivingLicenceToolStripMenuItem.Name = "relaiseDetainDrivingLicenceToolStripMenuItem";
            this.relaiseDetainDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(368, 22);
            this.relaiseDetainDrivingLicenceToolStripMenuItem.Text = "Relaise Detain Driving Licence";
            // 
            // retakeTestToolStripMenuItem
            // 
            this.retakeTestToolStripMenuItem.Name = "retakeTestToolStripMenuItem";
            this.retakeTestToolStripMenuItem.Size = new System.Drawing.Size(368, 22);
            this.retakeTestToolStripMenuItem.Text = "Retake Test";
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenceApplicationsToolStripMenuItem,
            this.internationalDrivingLicenceApplicationsToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLicenceApplicationsToolStripMenuItem
            // 
            this.localDrivingLicenceApplicationsToolStripMenuItem.Name = "localDrivingLicenceApplicationsToolStripMenuItem";
            this.localDrivingLicenceApplicationsToolStripMenuItem.Size = new System.Drawing.Size(436, 22);
            this.localDrivingLicenceApplicationsToolStripMenuItem.Text = "Local Driving Licence Applications";
            this.localDrivingLicenceApplicationsToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenceApplicationsToolStripMenuItem_Click);
            // 
            // internationalDrivingLicenceApplicationsToolStripMenuItem
            // 
            this.internationalDrivingLicenceApplicationsToolStripMenuItem.Name = "internationalDrivingLicenceApplicationsToolStripMenuItem";
            this.internationalDrivingLicenceApplicationsToolStripMenuItem.Size = new System.Drawing.Size(436, 22);
            this.internationalDrivingLicenceApplicationsToolStripMenuItem.Text = "International Driving Licence Applications";
            // 
            // managePeopleToolStripMenuItem
            // 
            this.managePeopleToolStripMenuItem.Name = "managePeopleToolStripMenuItem";
            this.managePeopleToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.managePeopleToolStripMenuItem.Size = new System.Drawing.Size(167, 42);
            this.managePeopleToolStripMenuItem.Text = "Manage People";
            this.managePeopleToolStripMenuItem.Click += new System.EventHandler(this.managePeopleToolStripMenuItem_Click);
            // 
            // manageUsersToolStripMenuItem
            // 
            this.manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            this.manageUsersToolStripMenuItem.Size = new System.Drawing.Size(145, 42);
            this.manageUsersToolStripMenuItem.Text = "Manage Users";
            this.manageUsersToolStripMenuItem.Click += new System.EventHandler(this.manageUsersToolStripMenuItem_Click);
            // 
            // manageDriversToolStripMenuItem
            // 
            this.manageDriversToolStripMenuItem.Name = "manageDriversToolStripMenuItem";
            this.manageDriversToolStripMenuItem.Size = new System.Drawing.Size(158, 42);
            this.manageDriversToolStripMenuItem.Text = "Manage Drivers";
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showUserDetailsToolStripMenuItem,
            this.ressetPasswordToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(166, 42);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            // 
            // showUserDetailsToolStripMenuItem
            // 
            this.showUserDetailsToolStripMenuItem.Name = "showUserDetailsToolStripMenuItem";
            this.showUserDetailsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.showUserDetailsToolStripMenuItem.Text = "Show user Details";
            this.showUserDetailsToolStripMenuItem.Click += new System.EventHandler(this.showUserDetailsToolStripMenuItem_Click);
            // 
            // ressetPasswordToolStripMenuItem
            // 
            this.ressetPasswordToolStripMenuItem.Name = "ressetPasswordToolStripMenuItem";
            this.ressetPasswordToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.ressetPasswordToolStripMenuItem.Text = "Resset Password";
            this.ressetPasswordToolStripMenuItem.Click += new System.EventHandler(this.ressetPasswordToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.Animated = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::PresentationLayer.Properties.Resources.Cancel2;
            this.btnClose.ImageSize = new System.Drawing.Size(50, 50);
            this.btnClose.IndicateFocus = true;
            this.btnClose.Location = new System.Drawing.Point(1157, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(60, 60);
            this.btnClose.TabIndex = 3;
            this.btnClose.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // MainMenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PresentationLayer.Properties.Resources.MainmenuePic;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1238, 578);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenue";
            this.Text = "MainMenue";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainMenue_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem managePeopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageDriversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showUserDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ressetPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private System.Windows.Forms.ToolStripMenuItem appliocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTestTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driverLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replacementForLoseOrDamageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relaiseDetainDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retakeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenceApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalDrivingLicenceApplicationsToolStripMenuItem;
    }
}