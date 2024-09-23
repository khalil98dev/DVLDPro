using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public  class clsAccessTests
    {
        static public SqlConnection Connection = new SqlConnection(clsDBSettings.Connection); 
        static public int AddNewTest(int TestAppoinID,byte Result,string Notes,int CreatedByUserID)
        {
            int TestID = -1; 
            string Qeury = "insert into Tests values(@TestAppoinID,@TestResult,@Notes,@CreatedByUserID);SELECT SCOPE_IDENTITY()";
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestAppoinID", TestAppoinID);
            Command.Parameters.AddWithValue("@TestResult", Result);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar(); 
                if(result != null)
                {
                    int.TryParse(result.ToString(), out TestID);
                }

            }
            catch { }
            finally { Connection.Close();  }
            return TestID; 
        }

        static public bool Find(int TestID, ref int TestAppoinID,ref  byte Result, ref string Notes,ref int CreatedByUserID)
        {
            bool isExist = false;
            string Qeury = "SELECT * FROM Tests where TestID = @TestID";
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                while(Reader.Read())
                {
                    TestAppoinID = Convert.ToInt32(Reader["TestAppointmentID"]);
                    Result = (byte)Reader["TestResult"];
                    Notes = (Reader["Notes"] != DBNull.Value) ? Reader["Notes"].ToString(): string.Empty;
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    isExist = true;

                }
                Reader.Close(); 

            }
            catch { }
            finally { Connection.Close(); }
            return isExist;

        }

        static public bool FindLastTestPerPersonIDAndLicenseClass(int PeronID,int LicenseClassID,byte TestTypeID,
            ref int TestID, ref int TestAppoinID, ref byte Result, ref string Notes, ref int CreatedByUserID)
        {
            bool isExist = false;
            string Qeury = @"select top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID

                        from Tests inner join TestAppointments on TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                        inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                        inner join Applications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                        inner join People on People.PersonID = Applications.ApplicantPersonID 
                        inner join LicenseClasses on LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
                        
                        where LicenseClasses.LicenseClassID = @LicenseClassID
                        and PersonID=@PersonID and 
                        TestAppointments.TestTypeID=@TestTypeID";



            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@PersonID", PeronID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    TestID = Convert.ToInt32(Reader["TestID"]);
                    TestAppoinID = Convert.ToInt32(Reader["TestAppointmentID"]);
                    Result = (byte)Reader["TestResult"];
                    Notes = (Reader["Notes"] != DBNull.Value) ? Reader["Notes"].ToString() : string.Empty;
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    isExist = true;

                }
                Reader.Close();

            }
            catch { }
            finally { Connection.Close(); }
            return isExist;

        }


        public static DataTable GetAllTests()
        {

            DataTable dt = new DataTable();
            

            string query = "SELECT * FROM Tests order by TestID";

            SqlCommand command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;

        }

        static public bool FindByAppointID(int TestAppoinID, ref int TestID, ref byte Result, ref string Notes, ref int CreatedByUserID)
        {
            bool isExist = false;
            string Qeury = "SELECT * FROM Tests where TestAppointmentID = @TestAppointmentID";
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppoinID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    TestID = Convert.ToInt32(Reader["TestID"]);
                    Result = (byte)Reader["TestResult"];
                    Notes = (Reader["Notes"] != DBNull.Value) ? Reader["Notes"].ToString() : string.Empty;
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    isExist = true;

                }
                Reader.Close();

            }
            catch { }
            finally { Connection.Close(); }
            return isExist;

        }

        static public bool Update(int TestID,int TestAppoinID,byte Result,string Notes,int CreatedByUserID)
        {
            bool Updated = false;

            string Query = @"update Tests set 
                            TestAppointmentID = @TestAppoinID ,
                            Notes = @notes, 
                            CreatedByUserID=@CreatedByUserID,
                            TestResult = @TestResult
                            where Tests.TestID = @TestID"; 
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
            Command.Parameters.AddWithValue("@TestAppoinID", TestAppoinID);
            Command.Parameters.AddWithValue("@TestResult", Result);
            Command.Parameters.AddWithValue("@notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result > 0)
                    Updated = true;

            }catch { } 
            finally { Connection.Close(); } 
            return Updated;
        }

        static public byte GetPassedTests(int LocalDriningLicenseApplicationID)
        {
            byte Passedtests = 0; 
            string Query = @"select count(Tests.TestID) as PassedTests
                from Tests inner join TestAppointments on TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                inner join LocalDrivingLicenseApplications on
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                
                where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDLAppID 
                        and TestResult=1;SELECT SCOPE_IDENTITY();"; 
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDLAppID", LocalDriningLicenseApplicationID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar(); 
                if(result != null)
                {
                    byte.TryParse(result.ToString(), out Passedtests);
                }
            }catch { }  
            finally { Connection.Close(); } 
            return Passedtests;


        }

    }
}
