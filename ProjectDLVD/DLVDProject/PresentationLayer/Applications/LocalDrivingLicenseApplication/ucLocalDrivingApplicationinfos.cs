using BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PresentationLayer.Applications.LocalDrivingLicenseApplication
{
    public partial class ucLocalDrivingApplicationinfos : UserControl
    {
       
     
        private clsLocalDriveingLicenceApplications _LocalDrivingApplication {  get; set; }   

        private int _LocalDrivingApplicationID { get; set; }

        public int LocalDrivingApplicationID { get;}


        public ucLocalDrivingApplicationinfos()
        {
            InitializeComponent();
        }

        void _ResetToDefault()
        {
            lblLocalDrivingLicenseApplicationID.Text = "[???]";
            lblAppliedFor.Text = "[???]";
            lblPassedTests.Text = "0/3";
            ucApplicationInfos1.ResetDefaultData(); 

            
        }


        void _FillData()
        {
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = _LocalDrivingApplication.ApplicantFullName.ToString();
            lblPassedTests.Text = clsLocalDriveingLicenceApplications.GetPassedTestCount(LocalDrivingApplicationID) +"/3";
            ucApplicationInfos1.FillDataByApplicationID(_LocalDrivingApplication.ApplicationID);
        }

        void _FillDataBy(Func<clsLocalDriveingLicenceApplications> Fill)
        {
            _LocalDrivingApplication = Fill(); 

            if (_LocalDrivingApplication == null)
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Text = $"This Local Driving Application with ID {_LocalDrivingApplication.ApplicationID} not exist",
                    Caption = "Error",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Error
                };
                messageDialog.Show();
                _ResetToDefault();
                return;

            }
            _FillData();

        }
        public void FillDataByLocalDrivingApplicationID(int LocalDrivingApplicationID)
        {
            _FillDataBy(() => { return clsLocalDriveingLicenceApplications.Find(LocalDrivingApplicationID); });
        }

        public void FillDataBApplicationID(int ApplicationID)
        {
            _FillDataBy( () => {return clsLocalDriveingLicenceApplications.FindByApplicationID(ApplicationID); });
        }



        private void ucApplicationInfos_Load(object sender, EventArgs e)
        {
            
        }

        private void ucApplicationInfos1_Load(object sender, EventArgs e)
        {

        }
    }
}
