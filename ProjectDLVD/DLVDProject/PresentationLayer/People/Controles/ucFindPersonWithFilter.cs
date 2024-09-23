using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;

namespace PresentationLayer.People.Controles
{
    public partial class ucFindPersonWithFilter : UserControl
    {
        private int _PersonID = -1; 
        public event Action<int> PersonSelector;

        protected virtual void OnPersonSelect(int PersonID)
        {
            Action<int> Handler = PersonSelector;
            if (Handler != null)
            {
                Handler(PersonID);
            }
        }

       



        public ucFindPersonWithFilter()
        {
            InitializeComponent();
        }

        private void _FillNow()
        {
            if(!ValidateChildren())
            {
                //Messenger: 
                MessageBox.Show("Some Fildes is requered,put curson on red icon", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return; 
            }

            switch (cmbFilter.Text)
            {
                case "Person ID.":
                    _PersonID =  personCard1.LoadPersonData(int.Parse(txbSearch.Text.ToString()));
                    
                    break;
                case "National No.":
                    _PersonID = personCard1.LoadPersonData(txbSearch.Text.ToString());
                    break;
                default:
                    break;
            }

            OnPersonSelect(_PersonID);


        }

        public void LoadDataToPersonWithID(int ID)

        {
            cmbFilter.Text = "Person ID.";
            txbSearch.Text=ID.ToString();
            _FillNow();
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            _FillNow();
        }

        private void txbSearch_Validating(object sender, CancelEventArgs e)
        {
            if(txbSearch.Text =="")
            {
                e.Cancel = true;
                errProvider.SetError(txbSearch, "This field is required, Please full it"); 

            }else
            {
                e.Cancel = false;
                errProvider.SetError(txbSearch, null);
            }
        }

        private void ucFindPersonWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0; 
        }

        public void Clear()
        {
            txbSearch.Clear();  
             personCard1.Clear();   
        }
        private void cmbFilter_TextChanged(object sender, EventArgs e)
        {
             txbSearch.Text = "";
             txbSearch.Focus();
        }
    }

       
    }
