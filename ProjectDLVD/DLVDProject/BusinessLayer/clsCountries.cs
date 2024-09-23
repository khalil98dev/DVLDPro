using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCountries

    {

      static public DataTable GetContries()
        {
            return clsAccessCountries.GetAllCountries();
        }

        static public int CountryID(string Name)
        {
            return clsAccessCountries.GetCountruID(Name);
        }

        static public string CountryName(int ID)
        {
            return clsAccessCountries.GetCountruName(ID);
        }

    }
}
