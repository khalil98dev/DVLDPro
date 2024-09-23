using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataBaseLayer
{
    static public class clsAccessBasicApplicationInfos
    {
        static public SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

        static public bool GetApplicationInfo(int ApplicationID,ref string status,
            ref double fees,ref string applicationType, ref string applicantName,ref DateTime date, 
            ref DateTime statusDate, ref string createdByUserName)
        {
            bool isExist = false;
            string Qeury = "select * from BasicApplciationInfos_view where ApplicationID = @ApplicationID"; 
            SqlCommand Command = new SqlCommand(Qeury,Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID); 
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                while(Reader.Read())
                {
                    status = Reader["Satutus"].ToString();
                    fees = Convert.ToDouble( Reader["Fees"]);
                    applicationType = Reader["ApplicationType"].ToString();
                    applicantName = Reader["ApplicantName"].ToString();
                    date =(DateTime) Reader["Date"];
                    statusDate = (DateTime) Reader["statusDate"];
                    createdByUserName = Reader["CreatedByUserName"].ToString(); 
                    isExist = true; 
                }
                Reader.Close(); 
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return isExist;
        }

    }
}
