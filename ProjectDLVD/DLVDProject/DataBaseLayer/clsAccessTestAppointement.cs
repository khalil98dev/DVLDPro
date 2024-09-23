using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseLayer
{
    static public class clsAccessTestAppointement
    {
        private static SqlConnection Connection = new SqlConnection(clsDBSettings.Connection); 

        static public bool FindTestAppointementByAppID(int AppointementID,ref byte TestTypeID,ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate,ref float PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool Founded = false;

            string Query  = @"select* from TestAppointments 
                        where TestAppointments.TestAppointmentID = @TestAppointID;";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@TestAppointID", AppointementID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader(); 

                if(Reader.Read())
                {
                    TestTypeID = (byte)Reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(Reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    IsLocked = (bool)Reader["IsLocked"];
                    RetakeTestApplicationID = (Reader["RetakeTestApplicationID"] != DBNull.Value) ? 
                                        Convert.ToInt32( Reader["RetakeTestApplicationID"]) : -1;
                }
                Reader.Close(); 
            }catch (Exception ex) { }
            finally { Connection.Close(); }




            return Founded;
        }

        static public int AddTestAppointement(byte TestTypeID,  int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,  float PaidFees,  int CreatedByUserID,  bool IsLocked,  int RetakeTestApplicationID)
        {
            int TestID = -1;
  
            string Query = @"insert into TestAppointments values 
            (@TestTypeID,@LDLicenseAppID,@AppointementDate,@PaidFees,@UserID,0,@RetakeTestID);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDLicenseAppID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointementDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", CreatedByUserID);
           if(RetakeTestApplicationID !=-1)
            command.Parameters.AddWithValue("@RetakeTestID", RetakeTestApplicationID);
           else
                command.Parameters.AddWithValue("@RetakeTestID", DBNull.Value);
            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    int.TryParse(result.ToString(), out TestID);

                }





            }
            catch (Exception ex) { }
            finally { Connection.Close(); }


            return TestID;
        }

        static public bool UpdateTestAppointement(byte TestTypeID, int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            bool isUpdated = false;
           

            string Query = @"update  TestAppointments set 
                    TestTypeID = @TestTypeID,
                    LocalDrivingLicenseApplicationID=@LocalDLAppID,

                    AppointmentDate =@AppointementDate ,
                    IsLocked=@Locked 
                    PaidFees=@PaidFees,
                    CreatedByUserID=@CreatedByUserID,
                    RetakeTestApplicationID=@RetakeTestID
                    where TestAppointmentID=@AppoinID;  ";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if(RetakeTestApplicationID != -1) 
            command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID",DBNull.Value);



            try
            {
                Connection.Open();
                int result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    isUpdated = true;
                }

            }
            catch { }
            finally { Connection.Close(); }


            return isUpdated;


        }

        static public DataTable GetAllTestAppointement()
        {
            DataTable dt = new DataTable();
            string Query = @"select* from TestAppointments_View"; 
            SqlCommand Command = new SqlCommand(Query, Connection); 
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                    dt.Load(Reader);
                Reader.Close();

            }
            catch (Exception ex) { }
            finally { Connection.Close();}
            return dt;
                    
                
                }

        static public bool FindLastTestAppointement(int LocalDrivingLicenseID, byte TestTypeID, ref int AppointementID, 
             ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID,
             ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool Founded = false;

            string Query = @"select top 1 * 
			from TestAppointments
			where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseID
			and TestTypeID=@TestTypeID
			Order by TestAppointmentID desc; ";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                if (Reader.Read())
                {
                    TestTypeID = (byte)Reader["TestTypeID"];
                    
                    AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    AppointementID = Convert.ToInt32(Reader["TestAppointmentID"]);
                    IsLocked = (bool)Reader["IsLocked"];
                    RetakeTestApplicationID = (Reader["RetakeTestApplicationID"] != DBNull.Value) ?
                                        Convert.ToInt32(Reader["RetakeTestApplicationID"]) : -1;
                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }




            return Founded;
        }

        static public DataTable GetAllTestsAppointementPerTestType(int LocalDrivingLicenseApplication,
                                                                        byte TestTypeID)
        {
            DataTable dt = new DataTable();
            string Query = @"select * from TestAppointments 
			where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseID
			and TestTypeID = @TestTypeID ; ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseApplication);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close(); 

            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return dt;
        }
        static public int GetTestID(int TestAppointementID)
        {
            int TestID = -1;

            string Qeury = @"select Tests.TestID 
                            from Tests 
                            where TestAppointmentID = @TestAppointID;select SCOPE_IDENTITY(); ";
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestAppointID", TestAppointementID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    int.TryParse(result.ToString(), out TestID); 

            }catch (Exception ex) { }   
            finally { Connection.Close(); }

            return TestID;
        }

        
    }
}
