using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Global_Classes;
using PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLayer.clsTestTypes;

namespace PresentationLayer.Test
{
    public partial class ucSceduleTest : UserControl
    {
        
        public enum eMode { eAddNewTest=0,eUpdate=1} 
        public eMode Mode {  get; set; }    

        private clsTestAppointements _CurrentAppointement {  get; set; }    


        public ucSceduleTest()
        {
            InitializeComponent();
        }
        

        private float _TestFees {  get; set; }  
        private float _TotalFees { get; set; }
        private clsTestTypes.eTestTypes _TestType = eTestTypes.eVisionTest;

        private clsApplications.eApplicationType _ApplicationType = clsApplications.eApplicationType.eNewDrivingLicense;
        public clsApplications.eApplicationType ApplicationType
        {
            get
            {
                return _ApplicationType; 
            }


            set { 
                _ApplicationType = value; 

                switch(_ApplicationType)
                {
                    case clsApplications.eApplicationType.eRetakeTest:
                        grbRetakeText.Enabled = true;
                        _TotalFees = _TestFees + (float)clsApplicaionTypes.Find((int)clsApplications.eApplicationType.eRetakeTest).Fees; 
                        break;
                        case clsApplications.eApplicationType.eNewDrivingLicense:
                        _TotalFees = _TestFees;
                        grbRetakeText.Enabled = false; 
                        break;
                }
            
            
            }
        }



        private void grpTitle_Click(object sender, EventArgs e)
        {

        }
       
        public clsTestTypes.eTestTypes TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestTypes.eTestTypes.eVisionTest:
                        pcTestTypeImage1.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        grpTitle.Text = "Schedule Vision Test";
                        _TestFees = clsTestTypes.Find(clsTestTypes.eTestTypes.eVisionTest).TestFees;
                        break;
                    case clsTestTypes.eTestTypes.eWrittenTes:
                        pcTestTypeImage1.Image = Resources.Written_Test_5121;
                        lblTitle.Text = "Written Test";
                        grpTitle.Text = "Schedule Written Test";
                        _TestFees = clsTestTypes.Find(clsTestTypes.eTestTypes.eWrittenTes).TestFees;

                        break;
                    case clsTestTypes.eTestTypes.eStreetTest:
                        pcTestTypeImage1.Image = Resources.driving_test_512;
                        lblTitle.Text = "Street Test";
                        grpTitle.Text = "Schedule Street Test";
                        _TestFees = clsTestTypes.Find(clsTestTypes.eTestTypes.eStreetTest).TestFees;
                        break;
                }
            }

        }



        private clsLocalDriveingLicenceApplications _LocalDrivingLicenseApplication
        { get; set; }

        public void AddNew()
        {
            _CurrentAppointement = new clsTestAppointements();
            this.Mode = eMode.eAddNewTest; 


        }

        public void Update(int AppointementID)
        {
            _CurrentAppointement = clsTestAppointements.Find(AppointementID); 
            Mode = eMode.eUpdate;

            
        }



        public  void FillData(int LDLAppID, clsTestTypes.eTestTypes TestType,
            clsApplications.eApplicationType ApplicationType)
        {


            if ((_LocalDrivingLicenseApplication = clsLocalDriveingLicenceApplications.Find(LDLAppID)) == null)
            {
               
                _ResetDefault();
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Caption = "Not Found",
                    Buttons = MessageDialogButtons.OK,
                    Style =   MessageDialogStyle.Dark,
                    Icon =    MessageDialogIcon.Error,
                    Text =   $"This Application with ID : {LDLAppID} not found"
                };
                messageDialog.Show();
                return;
            }
           
            _TestType = TestType;
            _ApplicationType = ApplicationType;
            dtpAppointementDate.MinDate = DateTime.Now;
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text =             _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName.ToString();
            lblFullName.Text =                 _LocalDrivingLicenseApplication.ApplicantFullName.ToString();
            lblFees.Text =                     _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblTrial.Text = clsLocalDriveingLicenceApplications.TotalTrilaPerTest(LDLAppID, _TestType).ToString();
            
            if(ApplicationType == clsApplications.eApplicationType.eRetakeTest)
            {
                lblRetakeAppFees.Text = _TestFees.ToString();
                
            }

            lblTotalFees.Text = _TotalFees.ToString(); 

            
            //ApplicationID after Save
        }

        void _ResetDefault()
        {
            lblLocalDrivingLicenseAppID.Text = "[??]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblFees.Text = "[$$$$]";
            lblTrial.Text = "[????]";

        }
        private void ucSceduleTest_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_LocalDrivingLicenseApplication == null) 
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Caption = "Not Found",
                    Buttons = MessageDialogButtons.OK,
                    Style = MessageDialogStyle.Dark,
                    Icon = MessageDialogIcon.Error,
                    Text = $"This Application don't Exist"
                };
                messageDialog.Show();
                return; 
            }

            _CurrentAppointement.TestTypeID = _TestType;
            _CurrentAppointement.PaidFees = _TotalFees;
            _CurrentAppointement.AppointementDate = dtpAppointementDate.Value;
            _CurrentAppointement.LocalDrivingLicenseID = _LocalDrivingLicenseApplication.LicenseClassID; 
            _CurrentAppointement.CreatedByUserID = (clsGlobal.CurrentUser==null)?1: clsGlobal.CurrentUser.UserID;
            _CurrentAppointement.isLocked = false;


            if(_CurrentAppointement.Save())
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Caption = "Save",
                    Buttons = MessageDialogButtons.OK,
                    Style = MessageDialogStyle.Dark,
                    Icon = MessageDialogIcon.Information,
                    Text = $"The New Schedule Test Added with ID = {_CurrentAppointement.TestID}"
                };
                messageDialog.Show();

                Mode = eMode.eUpdate; 
            }else
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Caption = "Error",
                    Buttons = MessageDialogButtons.OK,
                    Style = MessageDialogStyle.Dark,
                    Icon = MessageDialogIcon.Information,
                    Text = $"The new Schedule can't be create"
                };
                messageDialog.Show();

                return; 
            }




        }
    }
}
