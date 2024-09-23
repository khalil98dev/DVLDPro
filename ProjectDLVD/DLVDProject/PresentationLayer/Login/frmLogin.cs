using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{

    public partial class frmLogin : Form
    {

        static public clsUsers CurrentUser { get; private set; }
        public frmLogin()
        {
            InitializeComponent();
            _LoadLoginInformations();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear(object sender, EventArgs e)
        {
            Guna2TextBox tx = (Guna2TextBox)sender;
            tx.Clear();
        }
        private void _SaveLoginInformations(string UserName, string Password)
        {
            StreamWriter writer = new StreamWriter("loginCookies.txt");

            writer.WriteLine($"{UserName}#{Password}");
            writer.Close();

        }

        private void _LoadLoginInformations()
        {
            if (File.Exists("loginCookies.txt"))
            {
                StreamReader reader = new StreamReader("loginCookies.txt");
                string TextReded = reader.ReadLine();
                if (TextReded != string.Empty)
                {
                    string[] LoginInformation = new string[2];
                    LoginInformation = TextReded.Split('#');
                    reader.Close();

                    txbUserName.Text = LoginInformation[0];
                    txbPassword.Text = LoginInformation[1];
                }
                else
                    return;
            }





        }

        bool _CheckUserName()
        {
            return (txbUserName.Text != string.Empty);
        }

        bool _CheckPassword()
        {
            return (txbPassword.Text != string.Empty);

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (chkRemeberme.Checked)
            {
                _SaveLoginInformations(txbUserName.Text, txbPassword.Text);
            }
            else
                _SaveLoginInformations("", "");

            if (!_CheckPassword() || !_CheckUserName())
            {
                lblError.Visible = true;
                lblError.Text = "Please enter correct user name or pâssword format ";
                return;
            }
            else
            {
                lblError.Visible = false;
                txbUserName.FocusedState.BorderColor = Color.Green;
                txbUserName.BorderColor = Color.Green;

                txbPassword.FocusedState.BorderColor = Color.Green;
                txbPassword.BorderColor = Color.Green;
            }

            string Password = txbPassword.Text.Trim();
            string UserName = txbUserName.Text.Trim();
            int ID = 0;
            if ((ID = clsUsers.Login(UserName, Password)) != -1)
            {
                lblError.Visible = false;

                MainMenue users = new MainMenue(ID);
                users.Show();
                this.Visible = false;

                CurrentUser = clsUsers.Find(ID);


            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "invalid user Name or password";
                txbPassword.BorderColor = Color.Red;
                txbUserName.BorderColor = Color.Red;
                txbUserName.Focus();
            }


        }
        bool Password = false;
        private void picPAssword_Click(object sender, EventArgs e)
        {
            if (!Password)
            {
                txbPassword.PasswordChar = '*';
                picPAssword.Image = Resources.hide;
                Password = true;


            }
            else
            {
                txbPassword.PasswordChar = default;
                picPAssword.Image = Resources.eye;
                Password = false;
            }
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbUserName_Leave(object sender, EventArgs e)
        {
            if (!_CheckUserName())
            {
                lblError.Text = "User Name can't be empty";
                lblError.Visible = true;
                txbUserName.Focus();

                txbUserName.BorderColor = Color.Red;
                txbUserName.BorderThickness = 2;


            }
            else
            {
                txbUserName.BorderColor = Color.Green;
                txbUserName.BorderThickness = 2;
                lblError.Visible = false;

            }
        }

        private void txbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbPassword_Leave(object sender, EventArgs e)
        {
            if (!_CheckPassword())
            {
                lblError.Text = "Password can't be empty";
                lblError.Visible = true;
                txbPassword.Focus();

                txbPassword.BorderColor = Color.Red;
                txbPassword.BorderThickness = 2;


            }
            else
            {
                txbPassword.BorderColor = Color.Green;
                txbPassword.BorderThickness = 2;
                lblError.Visible = false;

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
