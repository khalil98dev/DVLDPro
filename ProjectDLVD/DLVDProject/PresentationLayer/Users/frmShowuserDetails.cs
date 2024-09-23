using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmShowuserDetails : Form
    {
        public int userID { get; set; }

        public frmShowuserDetails(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void personCard1_Load(object sender, EventArgs e)
        {

        }

        private void frmShowuserDetails_Load(object sender, EventArgs e)
        {


        }
    }
}
