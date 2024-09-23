using BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmUsers : Form
    {
        static private DataTable _AllUsers = clsUsers.GetAllUsers();

        static private DataTable _Users = _AllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "FullName",
                                                                            "UserName", "IsActive");

        public frmUsers()
        {
            InitializeComponent();
        }



        private void _Refresh()
        {

            _AllUsers = clsUsers.GetAllUsers();

            _Users = _AllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "FullName",
                                                       "UserName", "IsActive");

            dgvUsers.DataSource = _Users;
            lblNumberOfUsers.Text = dgvUsers.RowCount.ToString();


        }
        private void frmUsers_Load(object sender, EventArgs e)
        {
            dgvUsers.DataSource = _Users;

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 100;

            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[1].Width = 100;

            dgvUsers.Columns[2].HeaderText = "Full Name";
            dgvUsers.Columns[2].Width = 150;

            dgvUsers.Columns[3].HeaderText = "User Name";
            dgvUsers.Columns[3].Width = 120;

            dgvUsers.Columns[4].HeaderText = "is Active";
            dgvUsers.Columns[4].Width = 80;




            lblNumberOfUsers.Text = dgvUsers.RowCount.ToString();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser User = new frmAddNewUser();
            User.ShowDialog();
            _Refresh();
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmShowuserDetails frmdtls = new frmShowuserDetails(ID);
            frmdtls.ShowDialog();
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterValue = "";
            txbSearch.Clear();

            switch (cmbSelect.Text)
            {
                case "None.":
                    FilterValue = "None";
                    break;
                case "User ID":
                    FilterValue = "UserID";
                    break;
                case "Person ID":
                    FilterValue = "PersonID";
                    break;
                case "Ful Name":
                    FilterValue = "FullName";
                    break;


            }
            if (txbSearch.Text == "" || FilterValue == "None")
            {
                dgvUsers.DataSource = _Users;
                lblNumberOfUsers.Text = dgvUsers.Rows.Count.ToString();
                return;
            }



            if (FilterValue == "UserID" || FilterValue == "PersonID")
                _Users.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterValue, txbSearch.Text.Trim());
            else
                _Users.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterValue, txbSearch.Text.Trim());

            lblNumberOfUsers.Text = dgvUsers.Rows.Count.ToString();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser addnew = new frmAddNewUser();
            addnew.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmAddNewUser addnew = new frmAddNewUser(ID, clsUsers.Find(ID).PersonID);
            addnew.ShowDialog();
            _Refresh();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = 0;
            if (clsUsers.Delete(ID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value)))
            {
                MessageBox.Show($"User with ID {ID} Deleted", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Refresh();
            }
            else
            {
                MessageBox.Show($"We can't delete this user beacuse there is data connected with him", "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelect.Text == "is Active")
            {
                txbSearch.Visible = false;
                cmbActive.Visible = true;

            }
            else
            {
                cmbActive.Visible = false;
                txbSearch.Visible = (cmbSelect.SelectedIndex != 0);
            }


        }

        private void cmbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cmbActive.Text.Trim();
            switch (cmbActive.Text.Trim())
            {
                case "All":

                    break;
                case "Active":
                    FilterValue = "1";
                    break;
                case "Non Active":
                    FilterValue = "0";
                    break;

            }
            if (FilterValue == "All")
                _Users.DefaultView.RowFilter = "";
            else
                _Users.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
        }
    }
}
