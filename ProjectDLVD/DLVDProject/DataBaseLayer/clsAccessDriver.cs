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
    static public class clsAccessDriver
    {
        static private SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

        static public int AddNewDriver(int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            int DriverID = -1; 
            string Query = @"insert into Drivers 
                    values (@PersonID,@CreatedByUserID,@CreatedDate) ;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("PersonID", PersonID);
            Command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("CreatedDate", CreatedDate);
            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out DriverID);
                }
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return DriverID;

        }

        static public bool FindByDriverID(int DriverID,ref int PersonID,ref int CreatedByUserID,
            ref DateTime CreatedDate)
        {
            bool isFound = false;

            string Query = @"select * from Drivers where Drivers.DriverID = @DriverID";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime) reader["CreatedDate"];
                    isFound = true;
                }



                reader.Close();
            }catch (Exception ex) { }
            finally { Connection.Close(); } 




            return isFound;
        }

        static public bool FindByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            string Query = @"select * from Drivers where Drivers.PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    isFound = true;
                }



                reader.Close();
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }




            return isFound;
        }

        static public DataTable GetAllDriver()
        {
            DataTable dt = new DataTable(); 
            string Query = "select * from Drivers"; 
            SqlCommand command = new SqlCommand(Query,Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader(); 
                if(Reader.HasRows)
                {
                    dt.Load(Reader); 
                }

                Reader.Close();
            }catch (Exception ex) { }  
            finally { Connection.Close(); } 

            return dt;  
        }

        static public bool UpdateDriver(int DriverID,  int PersonID,  int CreatedByUserID,  DateTime CreatedDate)
        {
            bool Updated = false;
            string Query = @"update Drivers set 
                            PersonID =@PersonID ,
                            CreatedByUserID = @CreatedByUserID,
                            CreatedDate =@CreatedDate where DriverID = @DriverID"; 
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedDate);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                int result = Command.ExecuteNonQuery(); 
                if(result > 0) { Updated = true;  }
            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return Updated;





            return Updated; 
        }



    }
}
