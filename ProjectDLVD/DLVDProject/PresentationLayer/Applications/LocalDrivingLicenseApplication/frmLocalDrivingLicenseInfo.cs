using BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Applications.LocalDrivingLicenseApplication
{
    public partial class frmLocalDrivingLicenseInfo : Form
    {
        private int _LocalDrivingApplicationID {  get; set; }   
        clsLocalDriveingLicenceApplications _LocalDriveingLicenceApplications { get; set; }
        public frmLocalDrivingLicenseInfo()
        {
            InitializeComponent();
        }
        public frmLocalDrivingLicenseInfo(int LocalDringApplicationID)
        {
            InitializeComponent();
            _LocalDrivingApplicationID = LocalDringApplicationID;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmLocalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            _LocalDriveingLicenceApplications = clsLocalDriveingLicenceApplications.Find(_LocalDrivingApplicationID); 
            if(_LocalDriveingLicenceApplications == null)
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Text = $"This Local Driving Application with ID {_LocalDrivingApplicationID} not exist",
                    Caption = "Error",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Error
                };

                this.Close();
                return; 
            }

            ucLocalDrivingApplicationinfos1.FillDataByLocalDrivingApplicationID(_LocalDrivingApplicationID);
            
        }
    }
}
