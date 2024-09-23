using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public class clsAccessApplicationTypes
    {
        static public DataTable GetApplicationtypes()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            string Query = "select * from ApplicationTypes"; 

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                if (Reader.HasRows) 
                {
                    dt.Load(Reader); 
                }
                Reader.Close(); 
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return dt; 

        }

        static public bool Update(int ID, string Title,double Fees)
        {
            bool Updated = false;
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            string Query = "update ApplicationTypes set ApplicationTypeTitle =@Title,ApplicationFees=@Fees where ApplicationTypeID = @ID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Title", Title); 
            Command.Parameters.AddWithValue("@Fees", Fees); 
            Command.Parameters.AddWithValue("@ID", ID); 

            try
            {
                Connection.Open();
                int Result  = Command.ExecuteNonQuery();
                if (Result > 0)
                {
                    Updated = true;
                }
             
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Updated;
        }

        static public bool Find(int ApptTypeID,ref string Title,ref double Fees)
        {
            bool isExist = false;
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            string Query = "select * from ApplicationTypes where ApplicationTypeID =@ID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ApptTypeID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fees = Convert.ToDouble(reader["ApplicationFees"]);
                    isExist = true;
                }
                reader.Close();

            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return isExist; 
        }
    }
}
