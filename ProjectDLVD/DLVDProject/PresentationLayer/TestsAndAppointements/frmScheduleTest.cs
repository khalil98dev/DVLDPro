using BusinessLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmScheduleTest : Form
    {

        // clsAppointementTest appointementTest = new clsAppointementTest();
        public int ApplicationID { get; set; }
        public int DlAppID { get; set; }
        public int TestTypeID { get; set; }
        public short Trial { get; set; }

        public int AppoinID { get; set; }
        public enum eMode { eAddnew = 1, eUpdate = 2 }
        public eMode Mode { get; set; }
        public frmScheduleTest(int DlAppID, int ApplicationID, short Trial, int TestTypeID, bool RetakeTest = false)
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        public void LoadData()
        {

            clsBasicAppilcationInfo Infos = clsBasicAppilcationInfo.Find(ApplicationID);
            clsDrivingLicenseApplicationInfos DlInfos = clsDrivingLicenseApplicationInfos.Find(DlAppID);
            //clsTestTypes TestT = clsTestTypes.Find(TestTypeID);

            lblApplicationID.Text = DlAppID.ToString();
            lblName.Text = Infos.ApplicantName;
            lblDrivingLicense.Text = DlInfos.LicenseClass.ToString();

            //lblFees.Text = TestT.TestFees.ToString();

            lblTrial.Text = Trial.ToString();
            Mode = eMode.eAddnew;
            lblTotalFees.Text = lblFees.Text.ToString();
        }

        public void UpdateData()
        {
            //    Mode = eMode.eUpdate;
            //  //  appointementTest = clsAppointementTest.Find(AppoinID);

            //    lblApplicationID.Text = appointementTest.DLAppID.ToString();
            //    lblName.Text = appointementTest.Name.ToString(); ;
            //    lblDrivingLicense.Text = appointementTest.DClass.ToString();

            //    lblFees.Text = appointementTest.PaidFees.ToString();
            //    dateTimePicker1.Value = appointementTest.AppointementDate;
            //    lblTrial.Text = Trial.ToString();

            //    lblTotalFees.Text = (Convert.ToDouble(lblFees.Text)+
            //        Convert.ToDouble(lblTotalFees.Text)).ToString();


        }

        bool _Save()
        {
            return false;
            //switch (Mode)
            //{
            //    case eMode.eAddnew:

            //        appointementTest.PaidFees = Convert.ToDouble(lblFees.Text);
            //        appointementTest.DLAppID = this.DlAppID;
            //        appointementTest.Name = lblName.Text;
            //        appointementTest.DClass = lblDrivingLicense.Text;
            //        appointementTest.AppointementDate = dateTimePicker1.Value;
            //        appointementTest.TestTypeID = TestTypeID;
            //        appointementTest.CreatedByUserID = frmLogin.CurrentUser.UserID;
            //        Mode = eMode.eUpdate;
            //        return appointementTest.Save();

            //    case eMode.eUpdate:

            //        if (appointementTest != null)
            //        {
            //            appointementTest.AppointementDate = dateTimePicker1.Value;
            //            return appointementTest.Save();

            //        }
            //        else return false;



            //    default: return false;
            //}





        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Message = "";

            switch (Mode)
            {
                case eMode.eAddnew:
                    Message = (_Save()) ? "New appointement added, enjoy" : "Error,Check your code";
                    break;
                case eMode.eUpdate:
                    Message = (_Save()) ? " appointement Updated, enjoy" : "Error,Check your code";
                    break;

            }

            MessageBox.Show(Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
