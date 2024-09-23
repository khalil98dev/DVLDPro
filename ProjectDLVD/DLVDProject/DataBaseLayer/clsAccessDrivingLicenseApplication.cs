using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public class clsAccessDrivingLicenseApplication
    {
       static public SqlConnection Connection = new SqlConnection(clsDBSettings.Connection); 

        
       static public bool ReturnLicenseApplication(int DLappID,ref string LicenseClass,ref short Passedtests)
        {
            bool Founded = false; 
            string Query = "select ClassName,PassedTestCount from LocalDrivingLicenseApplications_View where LocalDrivingLicenseApplicationID = @DLappID";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@DLappID", DLappID);    

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                while(Reader.Read())
                {
                    LicenseClass = Reader["ClassName"].ToString();
                    Passedtests = Convert.ToInt16(Reader["PassedTestCount"]);
                    Founded = true; 
                }
                Reader.Close();  
            
            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return Founded; 
        }


    }
}
