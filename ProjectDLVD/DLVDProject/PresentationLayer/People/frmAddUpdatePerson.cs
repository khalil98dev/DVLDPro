using BusinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Global_Classes;
using PresentationLayer.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmAddUpdatePerson : Form
    {
        public enum eMode { eAddNew = 1, eUpdate = 2 }
        public eMode Mode { get; set; }
        public enum eGendor { eMale = 0, eFemale = 1 }
        public eGendor Gendor { get; set; }

        int _PersonID { get; set; }
        clsPerson _Person;


        public delegate void DelagateCloseFrom(object sender, int PersonID);
        public event DelagateCloseFrom delegateHandler;

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            Mode = eMode.eUpdate;
            _PersonID = PersonID;


        }
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            Mode = eMode.eAddNew;
        }
        //private int _GetCountryID()
        //{
        //    return clsCountries.CountryID(cmbCountries.SelectedItem.ToString());

        //}

        private void _LoadContries()
        {
            DataTable dt = clsCountries.GetContries();
            foreach (DataRow row in dt.Rows)
            {
                cmbCountries.Items.Add(row[1].ToString());
            }



        }




        private void frmModePerson_Load(object sender, EventArgs e)
        {
            _ResetToDefaultData();

            if (Mode == eMode.eUpdate)
                _SetData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;


            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                try
                {
                    pbImage.Load(dialog.FileName);

                    pbImage.ImageLocation = dialog.FileName;

                    lnkSetImage.LinkVisited = true;
                    lnkRemove.Visible = lnkSetImage.LinkVisited;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Can't load this image please try again,Error [ {ex.Message} ]");
                }


            }
            else
            {
                MessageBox.Show("We can't found the correct Image");
            }
        }


        private void lnkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbImage.ImageLocation = null;

            lnkSetImage.LinkVisited = false;

            lnkRemove.Visible = lnkSetImage.LinkVisited;
            _LoadDefaultProfilPicture();
        }

        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _LoadDefaultProfilPicture();

        }
        private DateTime DateBeforeNYears(int Value)
        {
            DateTime dt = DateTime.Now;
            return dt.AddYears(-Value);
        }

        private void _LoadDefaultProfilPicture()
        {
            if (pbImage.ImageLocation != null)
                return;
            if (ckbFemale.Checked)
            {
                pbImage.Image = Resources.Artboard_1_copy1;

            }
            else
            {
                pbImage.Image = Resources.Artboard_1;
            }

        }
        private void _PersonMode()
        {

            switch (Mode)
            {
                case eMode.eAddNew:
                    lblMode.Text = "Add New Person";
                    btnSave.Image = Resources.Save;
                    ckbMale.Checked = true;
                    cmbCountries.SelectedIndex = 2;
                    break;
                case eMode.eUpdate:
                    _Person = clsPerson.Find(_PersonID);
                    _SetData();
                    lblMode.Text = "Update  Person";
                    lblID.Text = _Person.PersonID.ToString();
                    btnSave.Image = Resources.Update;




                    break;
                default:
                    break;
            }
        }
        private void _ResetToDefaultData()
        {
            _LoadContries();

            if (Mode == eMode.eAddNew)
            {
                _Person = new clsPerson();
                lblMode.Text = "Add New Person";
                btnSave.Image = Resources.Save;
            }
            else
            {
                lblMode.Text = "Update  Person";
                btnSave.Image = Resources.Update;
            }

            //if (pbImage.ImageLocation != "")
            //    lnkSetImage.LinkVisited = true;

            //lnkRemove.Visible = lnkSetImage.LinkVisited;

            lnkRemove.Visible = pbImage.ImageLocation != "";
            dtpDateOfBirth.MaxDate = DateBeforeNYears(18);
            dtpDateOfBirth.MinDate = DateBeforeNYears(100);
            pbImage.ImageLocation = null;
            txbAddress.Text = "";
            txbEmail.Text = "";
            txbFirstName.Text = "";
            txbLastName.Text = "";
            txbThirdName.Text = "";
            txbFourthName.Text = "";
            txbPhone.Text = "";
            txbNationalN.Text = "";
            ckbMale.Checked = true;
            cmbCountries.SelectedIndex = cmbCountries.FindString("Algeria");




        }




        private bool _NationalnNCheck()
        {

            switch (Mode)
            {
                case eMode.eAddNew:
                    if (clsPerson.isExist(txbNationalN.Text))
                    {
                        errPrController.SetError(txbNationalN, "This Person is Already exist,Please Enter another National No.");
                        txbNationalN.Focus();
                        return false;
                    }
                    else
                    {
                        errPrController.Clear();
                        return true;
                    }



                case eMode.eUpdate:
                    string CurrentNationalNumber = _Person.NationalN;

                    if (txbNationalN.Text == CurrentNationalNumber)
                    {
                        return true;

                    }
                    else
                    {
                        if (clsPerson.isExist(txbNationalN.Text))
                        {
                            errPrController.SetError(txbNationalN, "This Person is Already exist,Please Enter another National No.");
                            txbNationalN.Focus();
                            return false;
                        }
                        else
                        {
                            errPrController.Clear();
                            return true;
                        }
                    }
                default: return false;








            }




        }




        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private short GetGender()
        {
            return (ckbMale.Checked) ? (short)eGendor.eMale : (short)eGendor.eFemale;
        }

        private void _LoadDataToPerson()
        {



        }



        private void _SetCountryID()
        {
            _LoadContries();
            cmbCountries.SelectedIndex = cmbCountries.FindString(clsCountries.CountryName(_Person.CountryID));
        }

        private void _SetData()
        {
            if (Mode == eMode.eUpdate)
            {
                _Person = clsPerson.Find(_PersonID);
                //txbFirstName.Focus();
                lblID.Text = _Person.PersonID.ToString();
                txbFirstName.Text = _Person.FirstName;
                txbLastName.Text = _Person.LastName;
                txbThirdName.Text = _Person.ThirdName;
                txbFourthName.Text = _Person.FourthName;
                if (_Person.Gender == (short)eGendor.eMale)
                {
                    ckbMale.Checked = true;
                }
                else
                    ckbFemale.Checked = true;

                txbAddress.Text = _Person.Address;
                txbEmail.Text = _Person.Email.ToString();
                txbPhone.Text = _Person.PhoneNumber;
                try
                {
                    dtpDateOfBirth.Value = _Person.DateOfBirth;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Date", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                txbNationalN.Text = _Person.NationalN;
                _SetCountryID();

                if (_Person.ImagePath != string.Empty)
                {

                    pbImage.ImageLocation = _Person.ImagePath;

                }
                else
                {
                    _LoadDefaultProfilPicture();
                }

            }

        }

        //private void _MoveImage()
        //{

        //        string FileName = "";
        //        string FileSource = pbImage.ImageLocation;
        //        string Distination = "";

        //        switch (Mode)
        //        {
        //            case eMode.eUpdate:
        //                if (_Person.ImagePath != "")
        //                {
        //                    if (_Person.ImagePath == pbImage.ImageLocation)
        //                        return;
        //                //E:\khalilProjects\C# Projects\c#\Couse19-DLLV\ProjectDLVD\DLVDProject\PresentationLayer\Images\Icones\7d6516d6-fe4e-484b-9961-5ec81edf8228.png
        //                    Distination = _Person.ImagePath;
        //                    File.Delete(Distination);
        //                    File.Copy(FileSource, Distination);


        //                //E:\khalilProjects\C# Projects\c#\Couse19-DLLV\ProjectDLVD\DLVDProject\PresentationLayer\Images\Icones\46fbc5f7-51bd-4298-9944-715643943ac9.png
        //                return;
        //                }
        //                else
        //                {
        //                    FileName = Guid.NewGuid().ToString();
        //                    Distination = @"C:\DLVD\" + FileName + Path.GetExtension(FileSource);
        //                    File.Copy(FileSource, Distination);
        //                }
        //                break;
        //            case eMode.eAddNew:
        //                FileName = Guid.NewGuid().ToString();
        //                Distination = @"C:\DLVD\" + FileName + Path.GetExtension(FileSource);
        //                File.Copy(FileSource, Distination);
        //                break;
        //        }




        //        _Person.ImagePath = Distination;


        //}
        //private bool  _CheckEmpty()
        // { 
        //     if(txbFirstName.Text == string.Empty)
        //     {
        //         txbFirstName.Focus();
        //         return false;
        //     }
        //     if (txbLastName.Text == string.Empty)
        //     {
        //         txbLastName.Focus();
        //         return false;
        //     }
        //     if (txbThirdName.Text == string.Empty)
        //     {
        //         txbThirdName.Focus();
        //         return false;
        //     }

        //     if (txbFourthName.Text == string.Empty)
        //     {
        //         txbFourthName.Focus();
        //         return false;
        //     }

        //     if (txbNationalN.Text == string.Empty)
        //     {
        //         txbNationalN.Focus();
        //         return false;
        //     }

        //     if (txbPhone.Text == string.Empty)
        //     {
        //         txbPhone.Focus();
        //         return false;
        //     }
        //     if (txbAddress.Text == string.Empty)
        //     {
        //         txbAddress.Focus();
        //         return false;
        //     }



        //     return true;
        // }

        private bool _HandelPersonImage()
        {
            //Check if the person has a Picture to delete the old picture

            if (_Person.ImagePath != pbImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        if(File.Exists(_Person.ImagePath))  
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Error to delete old picture", "Delete old pic " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                //Copy image to th new path (project Folder) 
                if (pbImage.ImageLocation != null)
                {
                    string NewImagePath = pbImage.ImageLocation;
                    if (clsUtil.CopyImageToProjectImagesFolder(ref NewImagePath))
                    {
                        pbImage.ImageLocation = NewImagePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error cannot to copy the image", "Copy pic ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }



                }
            }

            return true;






        }
        private void btnSave_Click(object sender, EventArgs e)
        {


            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are required!,Please put curson on [red] icon to show error message", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon
                    .Error);
                return;
            }

            if (!_HandelPersonImage())
                return;


            int NationalituID = clsCountries.CountryID(cmbCountries.Text);
            _Person.FirstName = txbFirstName.Text;
            _Person.LastName = txbLastName.Text;
            _Person.ThirdName = txbThirdName.Text;
            _Person.FourthName = txbFourthName.Text;
            _Person.Gender = GetGender();
            _Person.Address = txbAddress.Text;
            _Person.Email = txbEmail.Text;
            _Person.PhoneNumber = txbPhone.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.CountryID = NationalituID;
            _Person.NationalN = txbNationalN.Text;
            //_Person.CountryID = _GetCountryID();

            if (pbImage.ImageLocation != null)
            {
                _Person.ImagePath = pbImage.ImageLocation.ToString();
            }
            else
                _Person.ImagePath = "";



            if (_Person.Save())
            {
                Mode = eMode.eUpdate;
                lblID.Text = _Person.PersonID.ToString();
                lblMode.Text = "Update Person";
                MessageBox.Show($"Person Saved with Seccess", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show($"Person can't be save", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            delegateHandler?.Invoke(sender, _Person.PersonID);


        }


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool _CheckEmailFormat(string Email)
        {
            return clsGlobalValidation.ValidateEmail(Email);
        }








        private void Fi(object sender, EventArgs e)
        {
            Guna2TextBox txb = (Guna2TextBox)sender;
            txb.Text = string.Empty;
        }
        //private void _Reset()
        //{
        //    lblID.Text = "[????]";  
        //    txbEmail.Clear();
        //      txbFirstName.Clear();
        //    txbLastName.Clear();
        //     txbThirdName.Clear();
        //    txbFourthName.Clear();
        //    txbAddress.Clear(); 
        //    txbNationalN.Clear();
        //    txbPhone.Clear();
        //    dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;


        //}
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Mode = eMode.eAddNew;
            _ResetToDefaultData();

        }





        private void EmptyTextBoxValidate(object sender, CancelEventArgs e)
        {
            Guna2TextBox Temp = (Guna2TextBox)sender;
            if (Temp.Text == string.Empty)
            {
                e.Cancel = true;
                errPrController.SetError(Temp, "This field is required!");
            }
            else
            {
                errPrController.SetError(Temp, null);
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                MessageBox.Show("Valide");
            }
            else
                MessageBox.Show("Non Valide!");
        }

        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txbEmail.Text == string.Empty)
            {
                return;
            }

            if (!clsGlobalValidation.ValidateEmail(txbEmail.Text))
            {
                e.Cancel = true;
                errPrController.SetError(txbEmail, "Email not valid,Please enter correct format \\'Exp:exemple@email.com'");

            }
            else
            {
                errPrController.SetError(txbEmail, null);
            }

        }

        private void txbNationalN_Validating(object sender, CancelEventArgs e)
        {
            if (txbNationalN.Text == string.Empty)
            {
                e.Cancel = true;
                errPrController.SetError(txbNationalN, "This field is required!");
                return;
            }


            switch (this.Mode)
            {
                case eMode.eUpdate:
                    if (!clsGlobalValidation.ValidateNationalNumber(txbNationalN.Text.Trim(), _PersonID))
                    {
                        e.Cancel = true;
                        errPrController.SetError(txbNationalN, "National number is already exist!");
                        return;

                    }
                    break;
                case eMode.eAddNew:
                    if (!clsGlobalValidation.ValidateNationalNumber(txbNationalN.Text.Trim()))
                    {
                        e.Cancel = true;
                        errPrController.SetError(txbNationalN, "National number is already exist!");
                        return;
                    }
                    break;
                default: break;
            }
            errPrController.SetError(txbNationalN, null);

        }
    }
}
