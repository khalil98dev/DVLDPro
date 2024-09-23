using BusinessLayer;
using PresentationLayer.Application.LocalDrivingLicenseApplication;
using PresentationLayer.Applications.LocalDrivingLicenseApplication;
using PresentationLayer.Test;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmManageLoacalDrivingLicenseApplications : Form
    {
        public frmManageLoacalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        
        void SubscribeToNewApplicationCreated()
        {
          frmAddNewLocalDrivingLicense.Action +=_Refresh;
        }
        void _LoadData()
        {
            dtgvLDLicenseApp.Rows.Clear();
            DataTable dt = clsApplications.GetLocalDrivingLicenceInfo();

            foreach (DataRow dr in dt.Rows)
            {
                dtgvLDLicenseApp.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);

            }

        }
        void _Refresh()
        {
            _LoadData();
            lblRecordsCount.Text = dtgvLDLicenseApp.RowCount.ToString();

        }
        private void frmLoacalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
          
            _Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        void Edit(int ApplicationID)
        {

        }
        private void dtgvLDLicenseApp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ApplicationID = (int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value;
            Edit(ApplicationID);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense NewlocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            SubscribeToNewApplicationCreated();
            NewlocalDrivingLicense.ShowDialog();

        }
        
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
           

        }


        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsApplications.FindApplicationID((int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value);

            //if ((DialogResult = MessageBox.Show($"Are you sure you want to cancel application with ID [{ApplicationID}]? ", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))== DialogResult.Yes)
            //{


            //}
            //else
            //{
            //    MessageBox.Show($" Application  ID [{ApplicationID}] not cancelled", "Cancel menue", MessageBoxButtons.OK,
            //            MessageBoxIcon.Information);
            //}

            _Refresh();
        }
        bool Delete(int ApplicationID)
        {
            return clsApplications.Delete(ApplicationID);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           
        }

        private void cmtLocal_Opening(object sender, CancelEventArgs e)
        {
            int ApplicationID = clsApplications.FindApplicationID((int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value);
            clsApplications App = clsApplications.Find(ApplicationID);



        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value;
            frmLocalDrivingLicenseInfo frm = new frmLocalDrivingLicenseInfo(ID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LLicenseAppID = (int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value;
            frmAddNewLocalDrivingLicense NewLiceseApplication =
                new frmAddNewLocalDrivingLicense(LLicenseAppID);
            SubscribeToNewApplicationCreated();

            NewLiceseApplication.ShowDialog();
            _Refresh();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsApplications.FindApplicationID((int)dtgvLDLicenseApp.CurrentRow.Cells[0].Value);
            if (Delete(ApplicationID))
            {
                MessageBox.Show("Application Deleted.", "Delete Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Application not Deleted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _Refresh();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID =(int) dtgvLDLicenseApp.CurrentRow.Cells[0].Value;

            frmAppointementsList NewAppointement = new frmAppointementsList(DLAppID,
                clsTestTypes.eTestTypes.eVisionTest); 
            NewAppointement.ShowDialog();   
        }
    }
}
