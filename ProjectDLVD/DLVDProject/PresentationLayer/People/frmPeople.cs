using BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmPeople : Form
    {
        static private DataTable _dtAllPeople = clsPerson.GetList();
        static private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                                                                           "SecondName", "ThirdName", "LastName", "DateOfBirth",
                                                                           "Gendor", "Phone", "Email", "CountryName"
                                                                           );

        public frmPeople()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _LoadData(DataTable dt)
        {
            dgvPeople.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                dgvPeople.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);

            }
        }

        private void _Refresh()
        {
            if (cmbSearch.SelectedIndex == 0)
            {
                txbInput.Visible = false;
            }

            _dtAllPeople = clsPerson.GetList();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                                                                               "SecondName", "ThirdName", "LastName", "DateOfBirth",
                                                                               "Gendor", "Phone", "Email", "CountryName"
                                                                               );

            dgvPeople.DataSource = _dtPeople;
            cmbSearch.SelectedIndex = 0;

        }
        private void frmPeople_Load(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 0)
            {
                txbInput.Visible = false;
            }
            dgvPeople.DataSource = _dtPeople;

            int Recordes = 0;
            if ((Recordes = dgvPeople.Rows.Count) > 0)
            {
                lblRecords.Text = Recordes.ToString();
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 100;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 80;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 80;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 80;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 80;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 80;

                dgvPeople.Columns[6].HeaderText = "Date of Birth";
                dgvPeople.Columns[6].Width = 100;

                dgvPeople.Columns[7].HeaderText = "Gendor Caption";
                dgvPeople.Columns[7].Width = 80;

                dgvPeople.Columns[8].HeaderText = "Phone";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Email";
                dgvPeople.Columns[9].Width = 150;

                dgvPeople.Columns[10].HeaderText = "Nationality";
                dgvPeople.Columns[10].Width = 110;
            }

        }
        frmAddUpdatePerson NewPerson = new frmAddUpdatePerson();
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            NewPerson.ShowDialog();
            _Refresh();
        }



        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cmbSearch.SelectedIndex)
            {
                case 0:
                    txbInput.Visible = false;
                    return;


                case 1:
                    txbInput.Visible = true;
                    txbInput.Mask = "000000000";

                    break;
                case 8:
                    txbInput.Visible = true;
                    txbInput.Mask = "0000-00-00-00";

                    break;

                default:
                    if (txbInput.Visible == false)
                        txbInput.Visible = true;
                    txbInput.Mask = "";
                    break;


            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            _LoadData(dataTable);
        }

        private void txbInput_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void _SerachByID()
        {
            if (txbInput.Text != "")
            {

                _LoadData(clsPerson.GetListByID(Convert.ToInt32(txbInput.Text)));

            }
            else
            {
                _LoadData(clsPerson.GetList());
            }

        }



        private void _Serach()
        {

            //dgvPeople.ClearSelection();
            //for (int r = 0; r < dgvPeople.RowCount; r++)
            //    dgvPeople[9, r].Style.BackColor = Color.Aqua;

            string Filter = "";
            /*
             "PersonID","NationalNo","FirstName",
              "SecondName","ThirdName","LastName","DateOfBirth",
               "Gendor","Phone","Email", "CountryName"
            */


            switch (cmbSearch.Text)
            {
                case "None.":
                    Filter = "None";
                    break;
                case "Person ID":
                    Filter = "PersonID";


                    break;

                case "National No":
                    Filter = "NationalNo";



                    break;
                case "First Name":
                    Filter = "FirstName";


                    break;
                case "Second Name":
                    Filter = "SecondName";


                    break;
                case "Third Name":
                    Filter = "ThirdName";


                    break;
                case "Last Name":
                    Filter = "LastName";


                    break;
                case "Gendor":
                    Filter = "Gendor";


                    break;
                case "Phone":
                    Filter = "Phone";


                    break;
                case "Email":
                    Filter = "Email";

                    break;
                case "Nationality":
                    Filter = "CountryName";


                    break;


            }

            if (txbInput.Text.Trim() == "" || Filter == "None")
            {
                dgvPeople.DataSource = _dtPeople;
                cmbSearch.SelectedIndex = 0;
                return;
            }

            if (Filter == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] ={1}", Filter, txbInput.Text.Trim());
            }
            else if (Filter == "Phone")
            {
                string[] PhoneNumber = txbInput.Text.Split('-');
                string result = "";
                foreach (string phone in PhoneNumber)
                {
                    result += phone;
                }
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", Filter, result.Trim());
            }
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", Filter, txbInput.Text.Trim());


            lblRecords.Text = dgvPeople.Rows.Count.ToString();

        }
        private void txbInput_TextChanged(object sender, EventArgs e)
        {

            _Serach();

        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson Update = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            Update.ShowDialog();
            _Refresh();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddUpdatePerson Update = new frmAddUpdatePerson(-1);
            Update.ShowDialog();
            _Refresh();
        }

        private void showInformationsToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            if (clsPerson.Delete(ID))
            {

                MessageBox.Show($"person with ID {ID} Deleted", "Done!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            else

                MessageBox.Show($"We can't delete this person beacuse there is data connected with him",
                    "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            _Refresh();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            dgvPeople.DefaultCellStyle.BackColor = Color.Green;

        }
    }
}
