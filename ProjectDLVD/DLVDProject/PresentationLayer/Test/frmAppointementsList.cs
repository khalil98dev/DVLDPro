using BusinessLayer;
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
    public partial class frmAppointementsList : Form
    {
        private int _LocalDrinvingLicanseApllicationID {  get; set; }   

        private clsTestTypes.eTestTypes _TestType {  get; set; }    

        private clsApplications.eApplicationType _ApplicationType { get; set; }

        public frmAppointementsList(int LDLAppID, clsTestTypes.eTestTypes TestType)
        {
            InitializeComponent();
        
            _LocalDrinvingLicanseApllicationID = LDLAppID;
            _TestType = TestType;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        void _Refresh()
        {
            lblRecords.Text = dgvAppointements.Rows.Count.ToString();   
        }

        void LoadData()
        {

            ucLocalDrivingApplicationinfos1.FillDataByLocalDrivingApplicationID(_LocalDrinvingLicanseApllicationID); 

        }
        private void frmSheduleTest_Load(object sender, EventArgs e)
        {
            _Refresh();
            LoadData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmScheduleTest Schedule  = new frmScheduleTest(_LocalDrinvingLicanseApllicationID,
                _TestType,clsApplications.eApplicationType.eNewDrivingLicense);
            Schedule.ShowDialog();
        }
    }
}
