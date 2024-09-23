using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Test
{
    public partial class ucScheduledTest : UserControl
    {
        private int _TestID = -1; 

        public  int TestID { get; } 

        private clsTestTypes.eTestTypes _TestType
        {
            get
            {
                return _TestType;
            }
            set { 
                _TestType = value;
                switch(_TestType)
                {
                    case clsTestTypes.eTestTypes.eVisionTest:
                        pcTestTypeImage1.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        grpTitle.Text = "Schedule Vision Test";
                            break;
                    case clsTestTypes.eTestTypes.eWrittenTes:
                        pcTestTypeImage1.Image = Resources.Written_Test_5121;
                        lblTitle.Text = "Written Test";
                        grpTitle.Text = "Schedule Written Test";

                        break;
                    case clsTestTypes.eTestTypes.eStreetTest:
                        pcTestTypeImage1.Image = Resources.Test_32;
                        lblTitle.Text = "Street Test";
                        grpTitle.Text = "Schedule Street Test";

                        break;
                }
            }

        }

      

        private clsLocalDriveingLicenceApplications _LocalDrivingLicenseApplication { get; set; }  

        public void LoadData(int LDLAppID, clsTestTypes.eTestTypes TestType)
        {
            if((_LocalDrivingLicenseApplication = clsLocalDriveingLicenceApplications.Find(LDLAppID))==null)
            {
                _ResetDefault();
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Caption = "Not Found",
                    Buttons = MessageDialogButtons.OK,
                    Style = MessageDialogStyle.Dark,
                    Icon = MessageDialogIcon.Error,
                    Text = $"This Application with ID : {LDLAppID} not found" 
                };
                return;
            }
            _TestType = TestType;
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString(); 
            lblDrivingClass.Text             = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName.ToString();
            lblFullName.Text                 = _LocalDrivingLicenseApplication.ApplicantFullName.ToString();
            lblFees.Text                     = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblTrial.Text                    = clsLocalDriveingLicenceApplications.TotalTrilaPerTest(LDLAppID,_TestType).ToString();

            if(_TestID != -1)
                lblTestID.Text = _TestID.ToString();    
        }

        void _ResetDefault()
        {
            lblLocalDrivingLicenseAppID.Text = "[??]";
           
            lblDrivingClass.Text =             "[????]";
            lblFullName.Text =                 "[????]";
            lblFees.Text =                     "[$$$$]";
            lblTrial.Text =                    "[????]";

        }

        public ucScheduledTest()
        {
            InitializeComponent();
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
