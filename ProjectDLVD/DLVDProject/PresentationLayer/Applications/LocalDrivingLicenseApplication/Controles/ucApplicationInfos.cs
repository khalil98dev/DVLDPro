using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Forms;
using PresentationLayer.People;
using System;
using System.Windows.Forms;

namespace PresentationLayer.UserControles
{
    public partial class ucApplicationInfos : UserControl
    {
      
        private clsApplications _CurrentApplication {  get; set; }  

        private int _AppilcationID { get; set; }

        public int AppilcationID
        { get; }

        public enum eStatus { eNew = 1, eCancelled = 2, eCompleted = 3 }
        public eStatus Status  { get; set; }
        public ucApplicationInfos()
        {
            InitializeComponent();
        }

  

        public void ResetDefaultData()
        {
            lblID.Text = "[????]";
            lblCreatedBy.Text = "[????]";
            lblAppliant.Text = "[????]";
            lblDate.Text = "[????]";
            lblDateStatus.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblStatus.Text = "[????]";

        }
        void _FillData()
        {

            lblID.Text =         _CurrentApplication.ApplicationID.ToString();
            lblCreatedBy.Text =  _CurrentApplication.CreatedByUserInfo.UserName;
            lblAppliant.Text =   _CurrentApplication.ApplicantFullName;
            lblDate.Text =       _CurrentApplication.ApplicationDate.ToString();
            lblDateStatus.Text = _CurrentApplication.LastStatusDate.ToString();
            lblFees.Text =       _CurrentApplication.PaidFees.ToString();
            lblType.Text =       _CurrentApplication.ApplicationTypeInfo.Title.ToString();
            lblStatus.Text = _CurrentApplication.StatusText.ToString();

        }
      
        public void FillDataByApplicationID(int ApplicationID)
        {


            _CurrentApplication = clsApplications.Find(ApplicationID);

            if (_CurrentApplication == null)
            {
                Guna2MessageDialog messageDialog = new Guna2MessageDialog()
                {
                    Text = $"This Application with ID {ApplicationID} not exist",
                    Caption ="Error",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Error
                };
                messageDialog.Show();
                ResetDefaultData();
                return;
            }

            _FillData();



        }

        private void grbInfos_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInformations PersonDetails = new frmPersonInformations(_CurrentApplication.PersonID);
            PersonDetails.ShowDialog(); 
        }
    }
}
