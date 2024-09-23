using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
   
    public static class clsAccessLicenseClassess
    {
        static readonly SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

        static public DataTable GetLicensesClasses()
        {
            DataTable LicenseClasse = new DataTable();

            string Query = "select * from LicenseClasses";

            SqlCommand command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                while (Reader.HasRows)
                {
                    LicenseClasse.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                // Console.ReadKey();
            }
            finally { Connection.Close(); }

            return LicenseClasse;

        }

        static public bool GetLicenseClassinfoByID(int LicenseClassID,ref string ClassName,
            ref string ClassDescription,
            ref byte MinimumAllowedAge,ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isExist = false; 
            
            string Query = "select * from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                Connection.Open() ;

                SqlDataReader Reader = command.ExecuteReader();
                if( Reader.Read()) 
                {
                    isExist = true ;
                    ClassName = Reader["ClassName"].ToString();
                    ClassDescription = Reader["ClassDescription"].ToString() ;
                    MinimumAllowedAge =(byte) Reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte) Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }



                Reader.Close();
            }catch (Exception ex) { }
            finally { Connection.Close(); }

            return isExist;


        }

        static public bool GetLicenseClassinfoByClassName(string ClassName, ref int LicenseClassID,
          ref string ClassDescription,
          ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isExist = false;

            string Query = "select * from LicenseClasses where ClassName = @ClassName";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                Connection.Open();

                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isExist = true;
                    LicenseClassID = Convert.ToInt32( Reader["ClassName"]);
                    ClassDescription = Reader["ClassDescription"].ToString();
                    MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }



                Reader.Close();
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return isExist;


        }

        static public int AddNewLicenseClass(string ClassName,
             string ClassDescription,
             byte MinimumAllowedAge,  byte DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1;

            string Query = @"insert into LicenseClasses 
                values (@ClassName,@ClassDescription,@MinimumAllowedAge,@DefaultValidityLenght,@ClassFees);
                select SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(@Query, Connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLenght", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                Connection.Open();
                object Result = command.ExecuteScalar(); 
                if (Result == DBNull.Value)
                {
                    int.TryParse(Result.ToString(), out LicenseClassID); 

                }
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return LicenseClassID;

        }

        static public bool UpdateLicenseClassinfo(int LicenseClassID, string ClassName,
         string ClassDescription,
         byte MinimumAllowedAge,  byte DefaultValidityLength,  float ClassFees)
        {
            bool  isUpdated = false;

            string Query = @"update LicenseClasses set 
                                ClassName = @ClassName ,
                                ClassDescription = @ClassDescription,
                                MinimumAllowedAge = @MinAllowedAge,
                                DefaultValidityLength = @DefaultValidityLength,
                                ClassFees = @ClassFees
                                where LicenseClassID = @LicenseClassID; ";

            SqlCommand command = new SqlCommand(@Query, Connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLenght", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                Connection.Open();
                int Result = command.ExecuteNonQuery();
                if (Result >0)
                {
                    isUpdated= true;

                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isUpdated;

        }

        static public bool DeleteLicenseClass(int LicenseClassID)
        {
            bool Deleted = false;

            string Query = @"delete from LicenseClasses where LicenseClassID = @LicenseClassID ; "; 

            SqlCommand sqlCommand = new SqlCommand(@Query, Connection);

            try
            {
                Connection.Open(); 
                int Result = sqlCommand.ExecuteNonQuery();
                if (Result >0)
                {
                    Deleted = true;
                }
            }catch (Exception ex) { }   
            finally { Connection.Close(); } 
            return Deleted; 
        }

    }
}
