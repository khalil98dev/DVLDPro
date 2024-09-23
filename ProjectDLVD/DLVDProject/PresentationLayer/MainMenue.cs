using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class MainMenue : Form
    {
        public int UserID { get; set; }
        public MainMenue(int ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        private void MainMenue_Load(object sender, EventArgs e)
        {
            btnClose.Location = new System.Drawing.Point(this.Width - 70, 10);


        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        frmPeople people = new frmPeople();
        private void managePeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            people.MdiParent = this;
            people.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowuserDetails Details = new frmShowuserDetails(UserID);
            Details.MdiParent = this;
            Details.Show();
        }

        private void ressetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not embeded yet");
        }



        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers users = new frmUsers();
            users.MdiParent = this;
            users.Show();
        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        frmApplicationTypes frmApplicationTypes = new frmApplicationTypes();
        private void applicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmApplicationTypes.MdiParent = this;
            frmApplicationTypes.Show();
        }
        frmManageTestType Testfrm = new frmManageTestType();
        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Testfrm.MdiParent = this;
            Testfrm.Show();

        }

        private void driverLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void replacementForLoseOrDamageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        frmManageLoacalDrivingLicenseApplications ManagerLDL = new frmManageLoacalDrivingLicenseApplications();
        private void localDrivingLicenceApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ManagerLDL.ShowDialog();
            // ManagerLDL.Parent = this;
        }

        private void localLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }
    }
}
