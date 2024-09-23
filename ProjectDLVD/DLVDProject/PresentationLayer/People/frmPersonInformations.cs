using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.People
{
    public partial class frmPersonInformations : Form
    {
        private int _PersonID {  get; set; }    

        public frmPersonInformations(int personID)
        {
            InitializeComponent();
            _PersonID = personID;   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonInformations_Load(object sender, EventArgs e)
        {

            personCard1.LoadPersonData(_PersonID);
        }
    }
}
