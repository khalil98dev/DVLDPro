using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public  class clsAccessLicensesData
    {
        static private SqlConnection Connection = new SqlConnection();

        static public int AddnewLicense(int ApplicationID,int DriverID,int LicenseClassID,DateTime IssueDate,
            DateTime ExpirationDate,string Notes,float PaidFees,bool isActive,byte IssueReason,int CreatedByUserID)
        {
            int LicenseID = -1; 
            string Query = @"insert into Licenses 
					values (@ApplicationID,@DriverID,@LicenseClasseID,@IssueDate,@ExpirationDate
						,@Notes,@PaidFees,@isActive,@IssueReason,@CreatedByUserID);
						select SCOPE_IDENTITY();"; 

            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClasseID", LicenseClassID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@isActive", isActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar(); 
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out LicenseID);
                }
            }catch (Exception ex) { }
            finally { Connection.Close(); }
            
            return LicenseID;

        }

        static public bool DeleteLicense(int LicenseID)
        {
            bool Deleted = false;
            string Query = "delete from Licenses where LicenseID = @LicenseID;";
            SqlCommand command = new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                Connection.Open();
                int Result = command.ExecuteNonQuery(); 
                if (Result >0)
                {
                    Deleted = true; 
                }
            }catch(Exception ex) { }
            finally { Connection.Close(); } 
            return Deleted;
        }

        static public bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PaidFees, bool isActive, byte IssueReason, int CreatedByUserID)
        {
            bool Updated = false;
            string Query = @"update Licenses set 
						ApplicationID=@ApplicationID,
						DriverID=@DriverID,
						LicenseClass=@LicenseClasseID,
						IssueDate=@IssueDate,
						ExpirationDate=@ExpirationDate,
						Notes=@Notes,
						PaidFees=@PaidFees,
						IsActive=@isActive,
						IssueReason=@IssueReason,
						CreatedByUserID=@CreatedByUserID
						where LicenseID = @LicenseID;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClasseID", LicenseClassID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@isActive", isActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        
        static public bool GetLicenseInfoByID(int LicenseID,ref int ApplicationID,ref int DriverID,
            ref int LicenseClassID,ref DateTime IssueDate,
            ref DateTime ExpirationDate,ref string Notes,ref float PaidFees,ref bool isActive,
            ref byte IssueReason,ref int CreatedByUserID)
        {
            bool Founded = false;

            string Query = " select * from Licenses where LicenseID=@LicenseID;";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                Connection.Open(); 
                SqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    PaidFees = (float)Reader["PaidFees"];
                     ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    Notes = Reader["Notes"].ToString();
                    isActive = (bool)Reader["IsActive"];
                    IssueReason = (byte)Reader["IssueReason"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                }
                Reader.Close();

            }catch (Exception ex) { }
            finally { Connection.Close(); }


            return Founded;
        }

        
        static public DataTable GetAllLicense()
        {
            DataTable dt = new DataTable();
            string Query = " select * from Licenses ;";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);

                }

                reader.Close(); 



            }catch(Exception ex) { }
            finally { Connection.Close(); }
            return dt;

        }

        static public int GetActivateLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            string Query = @"select Licenses.LicenseID from 
                            Licenses inner join Drivers  on Drivers.DriverID = Licenses.DriverID
                            inner join People on People.PersonID = Drivers.DriverID 
                            where Drivers.PersonID =@PersonID and LicenseClass= @LicenseClass;
                            select SCOPE_IDENTITY();"; 
            SqlCommand Command = new SqlCommand(@Query, Connection);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar(); 

                if(result != null)
                {
                    int.TryParse(result.ToString(), out LicenseID);
                }
            }catch(Exception ex) { }    
            finally { Connection.Close(); } 

            return LicenseID;    

        }

        static public DataTable GetDriverLicense(int DriverID)
        {
            DataTable dt = new DataTable();
            string Query = @"select * from Licenses 
                where DriverID = @DriverID;";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader(); 
                if(reader.HasRows)
                {
                    dt.Load(reader);    
                }
                reader.Close();


            }catch (Exception ex) { }
            finally { Connection.Close(); }
            return dt;
        }

        static public bool DesActivateCurrentLicense(int LicenseID)
        {
            bool Updated = false;
            string Query = @"update Licenses set 
						
						IsActive=0
						
						where LicenseID = @LicenseID;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
           try
            {
                Connection.Open(); 
                int result = Command.ExecuteNonQuery();
                if(result > 0)
                {
                    Updated = true; 
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Updated;
        }

    }
}
