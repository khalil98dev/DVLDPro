using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public class clsInterNationalLicenseData
    {
        static private SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

        static public int AddNewInterLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InterNationalLicenseID = -1;

            string Query = @"insert into InternationalLicenses
			                values
			                (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,
			                @ExpirationDate,@IsActive,@CreatedByUserID);
			                select SCOPE_IDENTITY(); "; 
            SqlCommand Command = new SqlCommand(Query, Connection); 
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open(); 
                object result = Command.ExecuteScalar();
                if (result != null)
                {
                    int.TryParse(result.ToString(), out InterNationalLicenseID); 
                }
            }catch  (Exception ex) { }
            finally { Connection.Close(); }
            return InterNationalLicenseID;

        }

        static public bool UpdateInterNationalLicense(int InternationalLicenseID,int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            bool Updated= false;

            string Query = @"Update InternationalLicenses set 
									ApplicationID				=@ApplicationID,
									DriverID					=@DriverID,
									IssuedUsingLocalLicenseID	=@IssuedUsingLocalLicenseID,
									IssueDate					=@IssueDate,
									ExpirationDate				=@ExpirationDate,
									IsActive					=@IsActive,
									CreatedByUserID				=@CreatedByUserID
							where InternationalLicenseID=
							@InternationalLicenseID;"; 
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID); 
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID); 
            Command.Parameters.AddWithValue("@DriverID", DriverID); 
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID); 
            Command.Parameters.AddWithValue("@IssueDate", IssueDate); 
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate); 
            Command.Parameters.AddWithValue("@IsActive", IsActive); 
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID); 

            try
            {
                Connection.Open(); 

                int result = Command.ExecuteNonQuery();
                if (result > 0)
                {
                    Updated = true; 
                }


            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return Updated;
            
        }

        static public bool GetInterNationalLicenseInformationsByID(int InternationalLicenseID,
            ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,
         ref   DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,
         ref int CreatedByUserID)
        {
            bool Founded = false;

            string Query = @"select * from InternationalLicenses 
                            where InternationalLicenseID= @InternationalLicenseID;"; 

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader  reader = Command.ExecuteReader();
                if(reader.Read())
                {
                    Founded = true;
                    ApplicationID = (int)reader.GetOrdinal("ApplicationID");
                    DriverID = (int)reader.GetOrdinal("DriverID");
                    CreatedByUserID = (int)reader.GetOrdinal("CreatedByUserID");
                    IssuedUsingLocalLicenseID = (int)reader.GetOrdinal("IssuedUsingLocalLicenseID");
                    IssueDate = Convert.ToDateTime( reader.GetOrdinal("IssueDate"));
                    ExpirationDate = Convert.ToDateTime( reader.GetOrdinal("ExpirationDate"));
                    IsActive = Convert.ToBoolean( reader.GetOrdinal("IsActive"));
                   

                }
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return Founded;
        }

        static public bool GetInterNationalLicenseInformationsByLocalLicenseID(int IssuedUsingLocalLicenseID,
           ref int ApplicationID, ref int DriverID, ref int InternationalLicenseID,
        ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,
        ref int CreatedByUserID)
        {
            bool Founded = false;

            string Query = @"select * from InternationalLicenses 
                            where IssuedUsingLocalLicenseID= @IssuedUsingLocalLicenseID;";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    Founded = true;
                    ApplicationID = (int)reader.GetOrdinal("ApplicationID");
                    DriverID = (int)reader.GetOrdinal("DriverID");
                    CreatedByUserID = (int)reader.GetOrdinal("CreatedByUserID");
                    InternationalLicenseID = (int)reader.GetOrdinal("InternationalLicenseID");
                    IssueDate = Convert.ToDateTime(reader.GetOrdinal("IssueDate"));
                    ExpirationDate = Convert.ToDateTime(reader.GetOrdinal("ExpirationDate"));
                    IsActive = Convert.ToBoolean(reader.GetOrdinal("IsActive"));


                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Founded;
        }

        static public bool GetInterNationalLicenseInformationsByDriverID(int DriverID,
               ref int ApplicationID, ref int IssuedUsingLocalLicenseID, ref int InternationalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,
            ref int CreatedByUserID)
        {
            bool Founded = false;

            string Query = @"select * from InternationalLicenses 
                            where DriverID= @DriverID;";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    Founded = true;
                    ApplicationID = (int)reader.GetOrdinal("ApplicationID");
                    IssuedUsingLocalLicenseID = (int)reader.GetOrdinal("IssuedUsingLocalLicenseID");
                    CreatedByUserID = (int)reader.GetOrdinal("CreatedByUserID");
                    InternationalLicenseID = (int)reader.GetOrdinal("InternationalLicenseID");
                    IssueDate = Convert.ToDateTime(reader.GetOrdinal("IssueDate"));
                    ExpirationDate = Convert.ToDateTime(reader.GetOrdinal("ExpirationDate"));
                    IsActive = Convert.ToBoolean(reader.GetOrdinal("IsActive"));


                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Founded;
        }

        static public DataTable GetAllInterNationalLicense()
        {
            DataTable dt = new DataTable();

            string Query = @"select * from InternationalLicenses 
                            order by IsActive, ExpirationDate desc"; 

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close(); 
            } catch (Exception ex) { }
            finally { Connection.Close(); }
            return dt;


        }

        static public bool DesactivateInternationalLicense(int InternationalLicenseID)
        {
            bool DesActivated = false;
            string Query = @"Update InternationalLicenses set 
							IsActive = 0,							
							where InternationalLicenseID=
							@InternationalLicenseID;";
            SqlCommand Command = new SqlCommand(@Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();
                if(RowsEffected > 0)
                {
                    DesActivated = true;
                }

            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return DesActivated;
        }

        static public int GetActiveInternationalLiceseByDriverID(int DriverID)
        {
            int ActiveInterNationalLicenseID = -1;

            string Query = @"select top 1 InternationalLicenseID
                                from InternationalLicenses 
                                where 
                                DriverID = @DriverID
                                and 
                                GETDATE() between IssueDate and ExpirationDate 
                                order by ExpirationDate desc"; 
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID); 
            try
            {
                Connection.Open(); 
                object result = Command.ExecuteScalar();
                if(result != null)
                {
                    int.TryParse(result.ToString(), out ActiveInterNationalLicenseID);
                }

            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return ActiveInterNationalLicenseID;



        }

        static public DataTable GetAllDriverInterNationalLicense(int DriverID)
        {
            DataTable dt = new DataTable();

            string Query = @"select * from InternationalLicenses where 
                            DriverID = @DriverID
                             order by IsActive, ExpirationDate desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID); 
            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return dt;


        }



    }
}
