using BusinessLayer;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmUpdateTestType : Form
    {
        public int TestTypeID { get; set; }
        clsTestTypes Test { get; set; }


        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            this.TestTypeID = TestTypeID;
        }






    }
}
