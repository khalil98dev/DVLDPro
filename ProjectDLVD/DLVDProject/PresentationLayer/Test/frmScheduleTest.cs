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
    public partial class frmScheduleTest : Form
    {
        private int _DLAppID {  get; set; } 

        private clsTestTypes.eTestTypes _TestTypes { get; set; }   

        private clsApplications.eApplicationType _ApplicationType { get; set; }

        public frmScheduleTest()
        {
            InitializeComponent();
        }
        public frmScheduleTest(int DlAppID, clsTestTypes.eTestTypes TestTypes,
            clsApplications.eApplicationType ApplicationType)
        {
            InitializeComponent();
            ucSceduleTest1.AddNew();

            _DLAppID = DlAppID;
            _TestTypes = TestTypes;
            _ApplicationType = ApplicationType;

        }
        public frmScheduleTest(int AppointID, int DlAppID, clsTestTypes.eTestTypes TestTypes,
            clsApplications.eApplicationType ApplicationType)
        {
            InitializeComponent();
            ucSceduleTest1.Update(AppointID);

            _DLAppID = DlAppID;
            _TestTypes = TestTypes;
            _ApplicationType = ApplicationType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            
            ucSceduleTest1.FillData(_DLAppID, _TestTypes, _ApplicationType);
        }
    }
}
