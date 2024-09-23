using BusinessLayer;
using PresentationLayer.Global_Classes;
using PresentationLayer.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace PresentationLayer.People
{
    public partial class PersonCard : UserControl
    {
      
        private int _PersonID { get; set; }
        public enum eGendor { eMale = 0, eFemale = 1 }

        private clsPerson _Person;

        public PersonCard()
        {
            InitializeComponent();
        }

        private void _LoadImage()
        {
            if (_Person.ImagePath != "")
            {

                if (File.Exists(_Person.ImagePath))
                {
                    try
                    {
                        pbImage.Load(_Person.ImagePath);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("We can't load the image", "Load Image Err =>" + ex.Message, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Faild found image", "Search image", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            else
            {
                if (_Person.Gender == (short)eGendor.eMale)
                {
                    pbImage.Image = Resources.Artboard_1;
                    lblGender.Text = "Male";
                }
                else
                {

                    pbImage.Image = Resources.Artboard_1_copy1;
                    lblGender.Text = "Female";
                }
            }
        }

        public int LoadPersonData(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            _FillData();
            return PersonID;
        }

        public int LoadPersonData(string NationalNo)
        {
            _Person = clsPerson.FindByNationalN(NationalNo);

            if (_Person == null)
            {
                MessageBox.Show("Person not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1 ;
            }

            _FillData();
            return _Person.PersonID;
        }

         
         private void _FillData()
        {

            if (_Person == null)
            {
                MessageBox.Show("Person not found!");
                return;
            }



            _PersonID = _Person.PersonID;
            lblID.Text = _PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblCountry.Text = clsCountries.CountryName(_Person.CountryID);
            lblDateOfBirth.Text = clsFormatting.DateFormating(_Person.DateOfBirth);
            lblEmail.Text = _Person.Email;
            lblID.Text = _Person.PersonID.ToString();
            lblNationalNumber.Text = _Person.NationalN.ToString();
            lblPhone.Text = _Person.PhoneNumber.ToString();
            lblAdress.Text = _Person.Address.ToString();
            lblID.Text = _Person.PersonID.ToString();
            lblGender.Text = _Person.Gender.ToString();

            _LoadImage();

        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (_PersonID == 0)
            {
                MessageBox.Show("Please select the person First", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmAddUpdatePerson frmperson = new frmAddUpdatePerson(_PersonID);
            frmperson.ShowDialog();
            _Person = clsPerson.Find(_PersonID);
            _FillData();
        }

       
        public void Clear()
        {
            lblFullName.Text = "[????]";
            lblCountry.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblEmail.Text = "[????]";
            lblID.Text = "[????]";
            lblNationalNumber.Text = "[????]";
            lblPhone.Text = "[????]";
            lblAdress.Text = "[????]";
            lblID.Text = "[????]";
            lblGender.Text = "[????]"; 

        }

        private void PersonCard_Load(object sender, EventArgs e)
        {

        }
    }
}
