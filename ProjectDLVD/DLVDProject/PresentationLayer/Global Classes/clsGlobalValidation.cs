using BusinessLayer;
using System.Text.RegularExpressions;

namespace PresentationLayer.Global_Classes
{
    static class clsGlobalValidation
    {
        static public bool ValidateEmail(string EmailText)
        {
            //khalil98dev@gmail.com
            //
            // use regex with pattern : 
            string Pattern = "^[A-Za-z0-9.-_]+@[A-Za-z0-9]+\\.[A-Za-z]{2,}$";

            Regex reg = new Regex(Pattern);
            return reg.IsMatch(EmailText);
        }

        static public bool ValidateNumber(string NumberText)
        {

            string Pattern = @"^\d+$";

            Regex reg = new Regex(Pattern);
            return reg.IsMatch(NumberText);
        }

        //Check the phone number as 10 digits
        static public bool ValidatePhoneNumber(string PhoneNumnerText)
        {
            string pattern = @"^\d{10}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(PhoneNumnerText);
        }

        static public bool ValidateNationalNumber(string NationalNumber, int PersonID = -1)
        {

            if (PersonID == -1)
            {
                return !clsPerson.isExist(NationalNumber);

            }
            else
            {
                string PersonNationalNumber = clsPerson.Find(PersonID).NationalN.Trim();
                if (NationalNumber != PersonNationalNumber)
                {
                    return !clsPerson.isExist(NationalNumber);
                }


            }

            return true;
        }

    }
}
