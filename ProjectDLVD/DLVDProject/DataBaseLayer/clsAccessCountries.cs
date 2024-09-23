using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public  class clsAccessCountries
    {
        static public DataTable GetAllCountries()
        {
            string Query = " select * from Countries";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            DataTable dt = new DataTable();
            try
            {
                Conn.Open();

                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.HasRows)
                {
                    dt.Load(Reader);

                }

                Reader.Close();
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return dt;



        }

        static public int GetCountruID(string country)
        {
            string Query = " select * from Countries where Countries.CountryName = @CountryName";
            int ID = -1; 
            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@CountryName",country);
                       
            try
            {
                Conn.Open();

               object Res  = Command.ExecuteScalar();   
                if(Res != null) 
                {
                int.TryParse(Res.ToString(), out  ID);   
                }
              
                
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;
        }

        static public string GetCountruName(int ID)
        {
            string Query = " select * from Countries where Countries.CountryID = @ID";
            string Name = "";
            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Conn.Open();

               SqlDataReader reader = Command.ExecuteReader();  
                if (reader.Read())
                {
                    Name = reader["CountryName"].ToString();   
                }


            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return Name;
        }

    }


}
