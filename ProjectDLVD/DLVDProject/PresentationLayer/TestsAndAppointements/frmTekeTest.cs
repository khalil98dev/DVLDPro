using BusinessLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmTekeTest : Form
    {
        public short TestTypeID { get; set; }
        public int AppointementID { get; set; }
        public short Trial { get; set; }
        public bool RetakeTest { get; set; }

        public frmTekeTest(int AppointementID, short Trail, short TestypeID, bool RetakeTest = false)
        {
            InitializeComponent();
            this.AppointementID = AppointementID;
            this.Trial = Trail;
            this.TestTypeID = TestypeID;
            this.RetakeTest = RetakeTest;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grbInformations_Click(object sender, EventArgs e)
        {

        }




        private void frmTekeTest_Load(object sender, EventArgs e)
        {

        }

        private void FullData()
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            clsTests Taketest = new clsTests();
            Taketest.CreatedByUserID = frmLogin.CurrentUser.UserID;
            Taketest.TestAppointID = AppointementID;
            Taketest.Notes = txbNotes.Text;


            string Message = (Taketest.Save()) ? "Test Saved" : "Error,Check your Code";
            MessageBoxIcon Icon = (Taketest.Save()) ? MessageBoxIcon.Information : MessageBoxIcon.Error;

            MessageBox.Show(Message, "Save", MessageBoxButtons.OK, Icon);




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
