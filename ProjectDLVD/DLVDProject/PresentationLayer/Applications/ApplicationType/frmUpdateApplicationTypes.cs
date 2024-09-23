using BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmUpdateApplicationTypes : Form
    {
        public int AppID { get; set; }
        clsApplicaionTypes App { get; set; }
        public frmUpdateApplicationTypes(int appID)
        {
            InitializeComponent();
            AppID = appID;

        }
        void _SetData()
        {

            App = clsApplicaionTypes.Find(AppID);
            if (App == null)
            {
                MessageBox.Show("This Menue must be Closed");
                return;
            }
            lblAppID.Text = AppID.ToString();
            txbFees.Text = App.Fees.ToString();
            txbTitle.Text = App.Title.ToString();


        }
        private void txbCurrentPassword_IconRightClick(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).Clear();
        }

        void _Load()
        {
            App.Fees = Convert.ToDouble(txbFees.Text);
            App.Title = txbTitle.Text;
        }
        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _SetData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            _Load();
            if (App.Update())
            {
                MessageBox.Show("Data Updated");
            }
            else
                MessageBox.Show("Error");
        }
    }
}
