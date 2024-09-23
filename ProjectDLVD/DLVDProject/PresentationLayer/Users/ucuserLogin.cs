using BusinessLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer.UserControles
{
    public partial class ucuserLogin : UserControl
    {
        public ucuserLogin()
        {
            InitializeComponent();
        }
        public int UserID { get; set; }

        public void SetData(int ID)
        {
            clsUsers User = clsUsers.Find(ID);
            int PersonID = User.PersonID;

            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName.ToString();
            lblisUserActive.Text = (User.isActive) ? "Yes" : "No";






        }
        private void ucuserLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
