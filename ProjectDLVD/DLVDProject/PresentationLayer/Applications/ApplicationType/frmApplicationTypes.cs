using BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace PresentationLayer.Forms
{
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        void _LoadData()
        {
            DataTable dt = clsApplicaionTypes.GetApplicationTypes();
            dgvApplicationTypes.Rows.Clear();
            foreach (DataRow Row in dt.Rows)
            {
                dgvApplicationTypes.Rows.Add(Row[0], Row[1], Row[2]);
            }

        }

        void _Refresh()
        {
            _LoadData();
            lblRecordes.Text = dgvApplicationTypes.RowCount.ToString();


        }
        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            frmUpdateApplicationTypes appUpdate = new frmUpdateApplicationTypes(ID);
            appUpdate.ShowDialog();
            _Refresh();
        }
    }
}
