using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmAddNewUser : Form
    {

        private clsUsers _User;
        private int _UserID { get; set; }
        private int _PersonID { get; set; }
        public enum enMode { eAddnew = 1, eUpdate = 2 };
        public enMode Mode { get; set; }
        public frmAddNewUser()
        {
            InitializeComponent();

        }
        public frmAddNewUser(int UserID, int PersonID)
        {
            InitializeComponent();
            _UserID = UserID;
            _PersonID = PersonID;
            Mode = enMode.eUpdate;


        }

        void _ResetToDefaultData()
        {

            if (Mode == enMode.eUpdate)
            {
                _User = clsUsers.Find(_UserID);
                lblMode.Text = "Update User";
                lblID.Text = _UserID.ToString();
                txbConfirmPassword.Text = _User.Password.ToString();
                txbPassword.Text = _User.Password.ToString();
                txbUserName.Text = _User.UserName.ToString();
                ckbisActive.Checked = _User.isActive;

                return;
            }
            lblMode.Text = "Add New User";
            _User = new clsUsers();
            lblID.Text = "[N/A]";
            txbConfirmPassword.Clear();
            txbPassword.Clear();
            txbUserName.Clear();
            ckbisActive.Checked = true;


        }


        private void Clear(object sender, EventArgs e)
        {
            ((Guna2TextBox)sender).Text = string.Empty;
        }






        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        bool Password = false;
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (!Password)
            {
                pcbPaasword.Image = Resources.eye;
                Password = true;
                txbConfirmPassword.PasswordChar = default;
                txbPassword.PasswordChar = default;
            }
            else
            {
                pcbPaasword.Image = Resources.hide;
                Password = false;
                txbConfirmPassword.PasswordChar = '*';
                txbPassword.PasswordChar = '*';
            }
        }

        private bool _UserNameCheck()
        {


            if (txbUserName.Text == string.Empty)
            {
                lblUserNameError.Text = "User Name can't be empty!";
                picCheckUserName.Image = Resources.Error;
                picCheckUserName.Visible = true;
                lblUserNameError.Visible = true;
                //   txbUserName.;
                return false;

            }

            if ((txbUserName.Text != _User.UserName) && clsUsers.IsEXistUser(txbUserName.Text))
            {

                lblUserNameError.Text = "This user already exist,Invalid User";
                picCheckUserName.Image = Resources.Error;
                picCheckUserName.Visible = true;
                lblUserNameError.Visible = true;
                txbUserName.Focus();
                return false;
            }

            return true;

        }

        private bool _CheckPassword()
        {
            if (txbPassword.Text == string.Empty)
            {
                lblPasswordError.Text = "Password can't be empty! ";
                lblPasswordError.Visible = true;

                picheckErrPass.Image = Resources.Error;
                picheckErrPass.Visible = true;
                txbPassword.BorderColor = Color.Red;
                return false;
            }

            if (txbConfirmPassword.Text == string.Empty)
            {
                lblConfirmerror.Text = "Password can't be empty!";
                lblConfirmerror.Visible = true;
                picErrcheckConfirm.Image = Resources.Error;
                picErrcheckConfirm.Visible = true;
                txbConfirmPassword.BorderColor = Color.Red;
                return false;
            }


            if (txbConfirmPassword.Text != txbPassword.Text)
            {
                lblConfirmerror.Text = "Passwords must be matched !";
                lblPasswordError.Text = lblConfirmerror.Text;
                lblPasswordError.Visible = true;
                lblConfirmerror.Visible = true;
                txbConfirmPassword.BorderColor = Color.Red;
                txbPassword.BorderColor = Color.Red;
                picErrcheckConfirm.Image = Resources.Error;
                picheckErrPass.Image = Resources.Error;
                picErrcheckConfirm.Visible = true;
                picheckErrPass.Visible = true;

                return false;
            }


            return true;
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

            if (_UserNameCheck())
            {
                txbUserName.BorderColor = Color.Green;
                lblUserNameError.Text = "User name valid ";
                lblUserNameError.ForeColor = Color.Green;
                picCheckUserName.Image = Resources.valid;
                picCheckUserName.Visible = true;
                lblUserNameError.Visible = true;



            }
        }

        private void txbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (_CheckPassword())
            {
                lblConfirmerror.Text = "Matched Password";

                lblPasswordError.Text = "Password valid";
                lblConfirmerror.ForeColor = Color.Green;

                txbConfirmPassword.BorderColor = Color.Green;
                txbPassword.BorderColor = Color.Green;
                lblPasswordError.ForeColor = Color.Green;

                picErrcheckConfirm.Image = Resources.valid;
                picErrcheckConfirm.Visible = true;
                picheckErrPass.Visible = true;
                picheckErrPass.Image = Resources.valid;


            }
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _AddNewMode()
        {
            lblMode.Text = "Add new User";
            lblMode.ForeColor = Color.Green;
        }

        private void _UpdateMode()
        {
            lblMode.Text = "Update User";
            lblMode.ForeColor = Color.YellowGreen;
        }
        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetToDefaultData();
        }

        private void EmptyFieldValidating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txb = (Guna2TextBox)sender;

            if (txb.Text == "")
            {
                e.Cancel = true;
                errpCheck.SetError(txb, "This field is required!");
                return;
            }
            else
                errpCheck.SetError(txb, null);




        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Error validating, Put curson on red icon to show error ", "Validating", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return;

            }

            _User.UserName = txbUserName.Text.Trim();
            _User.Password = txbPassword.Text.Trim();
            _User.isActive = ckbisActive.Checked;


            if (_User.Save())
            {
                lblID.Text = _User.UserID.ToString();
                lblMode.Text = "Update User";
                Mode = enMode.eUpdate;
                MessageBox.Show("User Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Cannot Save User!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }




    }
}
