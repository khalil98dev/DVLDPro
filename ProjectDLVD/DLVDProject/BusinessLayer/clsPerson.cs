using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsPerson
    {
        public enum eMode { eAddNew = 0, eUpdateNew = 2 }

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public short Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string Address { get; set; }
        public string NationalN { get; set; }
        public string ImagePath { get; set; }

        public eMode Mode { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName +" " + ThirdName +" " + " "+ FourthName;
            }
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.ThirdName = "";
            this.FourthName = "";
            this.Gender = -1;
            this.DateOfBirth = DateTime.Now;
            this.PhoneNumber = "";
            this.Email = "";
            this.CountryID = 0;
            this.Address = "";
            this.NationalN = "";
            this.ImagePath = "";
            this.Mode = eMode.eAddNew;

        }

        public clsPerson(int PersonID, string FirstName, string LastName, string ThirdName, string FourthName, short Gender,
            DateTime DateOfBirth, string PhoneNumber, string Email, int CountryID, string Addres, string NationalN, string ImagePath)
        {

            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ThirdName = ThirdName;
            this.FourthName = FourthName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.CountryID = CountryID;
            this.Address = Addres;
            this.NationalN = NationalN;
            this.ImagePath = ImagePath;
            this.Mode = eMode.eUpdateNew;

        }


        static public clsPerson Find(int ID)
        {
            int PersonID = -1;
            string FirstName = "";
            string LastName = "";
            string ThirdName = "";
            string FourthName = "";
            short Gender = -1;
            DateTime DateOfBirth = DateTime.Now;
            string PhoneNumber = "";
            string Email = "";
            int CountryID = 0;
            string Address = "";
            string NationalN = "";
            string ImagePath = "";
            if (clsAccessPeople.GetPersonByID(ID, ref FirstName, ref LastName, ref ThirdName, ref FourthName, ref Gender,
                ref DateOfBirth, ref PhoneNumber, ref Email, ref CountryID, ref Address, ref NationalN, ref ImagePath))
            {

                return new clsPerson(ID, FirstName, LastName, ThirdName, FourthName, Gender, DateOfBirth, PhoneNumber, Email,
                    CountryID, Address, NationalN, ImagePath);
            }
            else
                return null;

        }

        static public clsPerson FindByNationalN(string NationalN)
        {
            return clsPerson.Find(clsAccessPeople.GetIDByNationaln(NationalN)); 
        }

        static public clsPerson FindByPhone(string Phone)
        {
            return clsPerson.Find(clsAccessPeople.GetIDByPhone(Phone));
        }

        static public clsPerson FindByEmail(string Email)
        {
            return clsPerson.Find(clsAccessPeople.GetIDByEmail(Email));
        }

        static public clsPerson FindByFirstName(string FirstName)
        {
            return clsPerson.Find(clsAccessPeople.GetIDByFirstName(FirstName));
        }

        static public clsPerson FindBySecondName(string Second)
        {
            return clsPerson.Find(clsAccessPeople.GetIDBySecondname(Second));
        }

        static public clsPerson FindByThirdName(string Third)
        {
            return clsPerson.Find(clsAccessPeople.GetIDBythirdName(Third));
        }

        static public clsPerson FindByLastName(string Last)
        {
            return clsPerson.Find(clsAccessPeople.GetIDByFourthName(Last));
        }



        public clsPerson Find()
        {
            return clsPerson.Find(this.PersonID);
        }

        private bool _AddNew()
        {
            return (this.PersonID = clsAccessPeople.AddNewperson(FirstName,LastName, ThirdName, FourthName, Gender, DateOfBirth, PhoneNumber,
                Email, CountryID, Address, NationalN,ImagePath))!=-1 ;
        }

        private bool _Update()
        {
            return clsAccessPeople.Update(PersonID,FirstName, LastName, ThirdName, FourthName, Gender, DateOfBirth, PhoneNumber,
                Email, CountryID, Address, NationalN, ImagePath);
        }

         public bool Delete()
        {
            return clsAccessPeople.Delete(this.PersonID);
        }
        static public bool Delete(int PersonID)
        {
            return clsAccessPeople.Delete(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eAddNew:
                   if (_AddNew())
                    {
                        Mode = eMode.eUpdateNew;
                        return true;
                        
                    }
                    else return false;
                        

                 
                case eMode.eUpdateNew:
                   if(_Update())
                    {
                        return true;
                    }else
                        return false;
                  
                   
                default:
                    return false;

            } 
        }

        static public bool isExist(string NationalNumber)
        {
            return clsAccessPeople.isExist(NationalNumber);    
        }

        static public int CountPeople()
        {
            return clsAccessPeople.CountPeople();
        }

        static public DataTable GetList()
        {
            return clsAccessPeople.GetPeopleList();
        }

        static public DataTable GetListByID(int ID)
        {
        return clsAccessPeople.SearchByID(ID);  
        }

        static public DataTable GetListByFirstName(string Name)
        {
            return clsAccessPeople.SearchByFirstName(Name);
        }

        static public DataTable GetListByLastName(string Name)
        {
            return clsAccessPeople.SearchByLastName(Name);
        }

        static public DataTable GetListByThirdName(string Name)
        {
            return clsAccessPeople.SearchByThirdNameName(Name);
        }

        static public DataTable GetListBySecondName(string Name)
        {
            return clsAccessPeople.SearchBySecondName(Name);
        }


        static public DataTable GetListByNationalNe(string Name)
        {
            return clsAccessPeople.SearchByNationalN(Name);
        }

        static public DataTable GetListByPhone(string Name)
        {
            return clsAccessPeople.SearchByPhone(Name);
        }

        static public DataTable GetListByEmail(string Name)
        {
            return clsAccessPeople.SearchByEmail(Name);
        }

        static public DataTable GetListByGendor(string Name)
        {
            return clsAccessPeople.SearchByGendor(Name);
        }

        static public DataTable GetListByCountry(string Name)
        {
            return clsAccessPeople.SearchByCountry(Name);
        }


    }
}
