using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public class clsAccessTestType
    {
       static SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
        static public DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            string Query = "select * from TestTypes";

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
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return dt;

        }

        static public bool Update(int ID, string TestTitle, string TestDescription,double Fees)
        {
            bool Updated = false;
           

            string Query = "update TestTypes set TestTypeTitle =@Title,TestTypeDescription=@TestDescp,TestTypeFees =@Fees   where TestTypeID   = @ID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Title", TestTitle);
            Command.Parameters.AddWithValue("@TestDescp", TestDescription);
            Command.Parameters.AddWithValue("@Fees", Fees);
            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                int Result = Command.ExecuteNonQuery();
                if (Result > 0)
                {
                    Updated = true;
                }

            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Updated;
        }

        static public bool Find(int TestID, ref string Title, ref string Description,ref float Fees)
        {
            bool isExist = false;
           

            string Query = "select * from TestTypes where TestTypeID =@ID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", TestID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    Title = reader["TestTypeTitle"].ToString();
                    Fees = Convert.ToSingle(reader["TestTypeFees"]);
                    Description = reader["TestTypeDescription"].ToString(); 
                    isExist = true;
                }
                reader.Close();

            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return isExist;
        }

        static public   int AddNewTestType(string TestTitle,string TestDescription,float TestFees)
        {
            int TestTypeID = -1; 
            string Qeury = @"Insert into TestTypes values
                (@TestTitle,@TestDesciption,@TestFees);select SCOPE_IDENTITY();";
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestTitle", TestTitle);
            Command.Parameters.AddWithValue("@TestDesciption", TestDescription);
            Command.Parameters.AddWithValue("@TestFees", TestFees);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    int.TryParse(result.ToString(), out TestTypeID); 
            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return TestTypeID;

        }

    }
}
