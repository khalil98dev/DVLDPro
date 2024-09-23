using BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmManageTestType : Form
    {
        public frmManageTestType()
        {
            InitializeComponent();
        }
        void _LoadData()
        {
            DataTable dataTable = clsTestTypes.GetTestTypes();
            dgvTest.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                dgvTest.Rows.Add(row[0], row[1], row[2], row[3]);
            }
        }




        void _Refresh()
        {
            _LoadData();
            lblRecords.Text = dgvTest.RowCount.ToString();


        }
        private void frmManageTestType_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void editTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvTest.CurrentRow.Cells[0].Value;
            frmUpdateTestType uTestType = new frmUpdateTestType(ID);
            uTestType.ShowDialog();
            _Refresh();
        }
    }
}
