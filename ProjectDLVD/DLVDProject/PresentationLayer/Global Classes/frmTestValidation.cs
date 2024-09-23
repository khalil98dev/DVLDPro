using System;
using System.Windows.Forms;

namespace PresentationLayer.Global_Classes
{
    public partial class frmTestValidation : Form
    {
        public frmTestValidation()
        {
            InitializeComponent();
        }

        private void btnCheckEmail_Click(object sender, EventArgs e)
        {



        }


        string _FilePath = string.Empty;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdImages = new OpenFileDialog();

            fdImages.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            fdImages.FilterIndex = 1;
            fdImages.RestoreDirectory = true;
            DialogResult result = fdImages.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    txbPath.Text = fdImages.FileName.ToString();
                    _FilePath = fdImages.FileName;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Load Image: " + ex.Message);
                }

            }

            _Refresh();

        }

        private void txbFileInformation_TextChanged(object sender, EventArgs e)
        {

        }

        void _Refresh()
        {
            if (_FilePath != string.Empty)
            {
                pcB.Load(_FilePath);
                lblPath.Text = _FilePath;
            }

        }
        private void btnGuidName_Click(object sender, EventArgs e)
        {


            if (clsUtil.CopyImageToProjectImagesFolder(ref _FilePath))
            {
                MessageBox.Show("Image copied with successed");


            }
            else
            {
                MessageBox.Show("Error");
            }

            _Refresh();
        }

        private void frmTestValidation_Load(object sender, EventArgs e)
        {

        }
    }
}
