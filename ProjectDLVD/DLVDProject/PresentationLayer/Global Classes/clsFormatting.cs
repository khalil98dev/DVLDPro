using System;

namespace PresentationLayer.Global_Classes
{
    static class clsFormatting
    {
        public static string DateFormating(DateTime d1)
        {
            return d1.ToString("dd/mm/yyyy");
        }

        public static string FullNameFormate(string FirstName, string SecondName, string ThirdName,
            string LastName)
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
    }
}
