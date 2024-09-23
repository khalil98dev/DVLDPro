using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Global_Classes;
using PresentationLayer.People;
using PresentationLayer.People.Controles;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PresentationLayer.Application.LocalDrivingLicenseApplication
{
    public partial class frmAddNewLocalDrivingLicense : Form
    {
        static public Action Action { get; set; }
         public Action<int> SendPersonID { get; set; }

        void OnFillData(int PersonID)
        {
            Action<int> Handler = SendPersonID; 
            if(Handler != null)
                Handler(PersonID);

        }
        void OnNewApplicationSaved()
        {
            Action handler = Action;    
            if (handler != null)
            {
                handler?.Invoke();  
            }
        }

        

        public enum eMode { eAddNew =1,eUppdate=2}; 
        public eMode Mode { get; set; }

        protected clsLocalDriveingLicenceApplications _CurrentLocalDrivingApplication { get; set; }    

        int _LocalDrivingLicenseApplicationID { get; set; }

        int _PersonSelectedID = -1; 



        public frmAddNewLocalDrivingLicense()
        {
            InitializeComponent();
            Mode = eMode.eAddNew;   
        }

        public frmAddNewLocalDrivingLicense(int LDApllicationID)
        {
            InitializeComponent();
            Mode = eMode.eUppdate;
            _LocalDrivingLicenseApplicationID = LDApllicationID;
           

        }

        void LoadedDataOnLoadForm()
        {
            PermetApplication();
            _FillLisecesClassesDataToComboBox();
            lblApplicationDate.Text = clsFormatting.DateFormating(DateTime.Now);
            lblApplicationFees.Text = clsApplicaionTypes.Find((int)clsApplications.eApplicationType.eNewDrivingLicense).Fees.ToString();
            clsGlobal.CurrentUser = clsUsers.Find(1);
            cmbLisenceClasses.SelectedIndex = 2;
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        void FillData()
        {
            lblApplicationDate.Text = _CurrentLocalDrivingApplication.ApplicationDate.ToString();
            lblApplicationFees.Text = _CurrentLocalDrivingApplication.PaidFees.ToString();
            lblApplicationID.Text = _CurrentLocalDrivingApplication.ApplicationID.ToString();
            cmbLisenceClasses.SelectedIndex = _CurrentLocalDrivingApplication.LicenseClassID-1;
            lblCreatedByUser.Text = _CurrentLocalDrivingApplication.CreatedByUserInfo.UserName;
            _PersonSelectedID = _CurrentLocalDrivingApplication.PersonID; 
            ucFindPersonWithFilter1.LoadDataToPersonWithID( _PersonSelectedID );
           // ucFindPersonWithFilter1.Enabled = false;
            
            


        }
        void ResetToDefaultData()
        {

            switch (Mode)
            {
                
                case eMode.eAddNew:
                    lblMode.Text = "Add New";
                    this.Text = "New Local Driving License";
                    btnSave.Text = "Save";
                    btnSave.Width = 110;
                    _CurrentLocalDrivingApplication = new clsLocalDriveingLicenceApplications();

                    break;
                case eMode.eUppdate:
                    lblMode.Text = "Update";
                    this.Text = "Update Local Driving License";
                    btnSave.Text = "Update";
                    btnSave.Width = 142;
                    _CurrentLocalDrivingApplication =clsLocalDriveingLicenceApplications.Find(_LocalDrivingLicenseApplicationID);
                    FillData();
                    break; 
                default: 
                    break;
            }

        }
        void _FillLisecesClassesDataToComboBox()
        {
            DataTable LicenseClasses = clsLicenseClasses.GetAllClasses();
            foreach(DataRow Row in LicenseClasses.Rows)
            {
                cmbLisenceClasses.Items.Add(Row["ClassName"]);
            }


        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
       
        private void frmAddNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            LoadedDataOnLoadForm();
            ResetToDefaultData();
        }
        void PermetApplication()
        {
            grbDetails.Enabled = (_PersonSelectedID != -1);
            btnSave.Enabled = (_PersonSelectedID != -1);
        }
        private void ucFindPersonWithFilter1_PersonSelector(int obj)
        {
            _PersonSelectedID = obj;
            PermetApplication();


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
         
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

           

            // function => LicenseClassID ,PersonID => 
            int FoundID = -1;
            if ( (FoundID = clsApplications.GetAactiveApplicationIDForLicenseClas(_PersonSelectedID,cmbLisenceClasses.SelectedIndex+1,clsApplications.eApplicationType.eNewDrivingLicense))!=-1)
            {
                Guna2MessageDialog Message = new Guna2MessageDialog()
                {
                    Text = $"This Person already has an active application with ID: {FoundID}",
                    Caption = "Error",
                    Style = MessageDialogStyle.Dark,
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    
                };

                Message.Show();

               return;
            }
            
            if( (FoundID = clsLicense.GetActivateLicenseIDByPersonID(_PersonSelectedID, (cmbLisenceClasses.SelectedIndex + 1)))!=-1)
            {
                Guna2MessageDialog Message = new Guna2MessageDialog()
                {
                    Text = $"This Person already has an active License with ID: {FoundID}",
                    Caption = "Error",
                    Style = MessageDialogStyle.Dark,
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,

                };

                Message.Show();
                return; 
            }


            _CurrentLocalDrivingApplication.ApplicationDate = DateTime.Now;
            _CurrentLocalDrivingApplication.PaidFees =Convert.ToInt64(lblApplicationFees.Text);
            _CurrentLocalDrivingApplication.LicenseClassID = cmbLisenceClasses.SelectedIndex+1;
            _CurrentLocalDrivingApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _CurrentLocalDrivingApplication.PersonID = _PersonSelectedID;
            _CurrentLocalDrivingApplication.ApplicationTypeID =(int) clsApplications.eApplicationType.eNewDrivingLicense;

            if(_CurrentLocalDrivingApplication.Save())
            {
               switch(Mode)
                {
                    case eMode.eAddNew:
                        MessageBox.Show("New Application Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Mode = eMode.eUppdate;
                        lblMode.Text = "Update";
                        lblApplicationID.Text = _CurrentLocalDrivingApplication.ApplicationID.ToString(); 
                        break;
                    case eMode.eUppdate:
                        MessageBox.Show("Application Updated ", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;


                }
               
                OnNewApplicationSaved();

            }
            else
            {
                MessageBox.Show("Failed!, can't save or update this application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblApplicationID_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationDate_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0; 
            Mode = eMode.eAddNew;
            ucFindPersonWithFilter1.Clear();
            ResetToDefaultData();

        }

        private void ucFindPersonWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}
