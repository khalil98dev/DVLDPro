using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public  class clsAccessLocalDrivingLicenseApplications
    {
        static SqlConnection Connection =new SqlConnection( clsDBSettings.Connection); 

        static public int AddNewLocalDrivingLicenceApplication(int ApplicationID,int LicenceClassID)
        {
            int ID = -1;

            string Query = @"insert into LocalDrivingLicenseApplications values (@AppilcationID,@LicenceClassID);SELECT SCOPE_IDENTITY(); ";
           
            SqlCommand Command = new SqlCommand(Query, Connection); 

            Command.Parameters.AddWithValue("@AppilcationID", ApplicationID);  
            Command.Parameters.AddWithValue("@LicenceClassID", LicenceClassID);  

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if(result != null) 
                { 
                int.TryParse(result.ToString(), out ID);
                }

            }
            catch (Exception ex) { }
            finally { Connection.Close(); } 

            return ID; 



        }

        static public bool UpdateLocalaDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,
            int ApplicationID, int LicenceClassID)
        {
            bool Updated= false;
            string Query = @"update LocalDrivingLicenseApplications set 
                                 
                                ApplicationID = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                                where LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand Commannd = new SqlCommand(Query, Connection);
            Commannd.Parameters.AddWithValue("@LDLAppID", LocalDrivingLicenseApplicationID); 
            Commannd.Parameters.AddWithValue("@ApplicationID", ApplicationID); 
            Commannd.Parameters.AddWithValue("@LicenseClassID", LicenceClassID); 
            try
            {
                Connection.Open(); 
               int result =  Commannd.ExecuteNonQuery();
                if (result>0)

                {
                    Updated = true;
                }
                

            }
            catch (Exception ex) { }   
            finally { Connection.Close(); }

            return Updated;

        }

        static public bool GetLocalDrivingAppInfoByID(int LocalDrivingAppID,ref int ApplicationID,
            ref int LicenceClassID)
        {
            bool founded = false;
            string Query =@"select * from LocalDrivingLicenseApplications 
                        where LocalDrivingLicenseApplicationID = @LocalAplicationID; ";

            SqlCommand Commannd = new SqlCommand( Query, Connection);
            Commannd.Parameters.AddWithValue("@LocalAplicationID", LocalDrivingAppID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Commannd.ExecuteReader();    
                if(reader.Read())
                {
                    founded = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenceClassID = (int)reader["LicenseClassID"]; 

                }

                reader.Close(); 
                
            }catch (Exception ex) { }   
            finally { Connection.Close(); }

            return founded;
        }

        static public bool GetLocalDrivingAppInfoByApplicationID(int ApplicationID, ref int LocalDrivingAppID,
           ref int LicenceClassID)
        {
            bool founded = false;
            string Query = @"select * from LocalDrivingLicenseApplications 
                        where ApplicationID = @ApplicationID; ";

            SqlCommand Commannd = new SqlCommand(Query, Connection);
            Commannd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Commannd.ExecuteReader();
                if (reader.Read())
                {
                    founded = true;
                    LocalDrivingAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenceClassID = (int)reader["LicenseClassID"];

                }

                reader.Close();

            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return founded;
        }

        static public bool DeleteLocalaApplication(int LocalApplicattionID)
        {
            bool Deleted = false;

            string Query = @"delete from LocalDrivingLicenseApplications 
                        where LocalDrivingLicenseApplicationID = @LocalAppID; ";
            SqlCommand Commannd = new SqlCommand( Query, Connection);
            Commannd.Parameters.AddWithValue("@LocalAppID", LocalApplicattionID);
            try
            {
                Connection.Open(); 
                int result = Commannd.ExecuteNonQuery();
                if(result > 0) {
                
                Deleted = true;
                }




            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return Deleted;
        }

        static public bool CheckPersonPassTest(int LocalDrivingApplicationID,byte TestTypeID)
        {
            bool Result = false;

            string Qeury = @"select top 1 TestResult as Reult , LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            = TestAppointments.LocalDrivingLicenseApplicationID
                            where TestAppointments.TestTypeID = @TestTypeID 
                                and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDAppID
                            order by TestAppointments.AppointmentDate desc;";       
            
            SqlCommand command = new SqlCommand( Qeury, Connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDAppID", LocalDrivingApplicationID);
            try
            {
                Connection.Open();
                int _result = command.ExecuteNonQuery();
                if (_result > 0)
                    Result = true; 
            }catch (Exception ex) { }   
            finally { Connection.Close(); }
            return Result;

        }

        static public bool CheckAttendTest(int LocalDrivingLicenseApplication,byte TestTypeID)
        {
            bool Result = false;

            string Qeury = @"select top 1 Found = 1 
                            from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            = TestAppointments.LocalDrivingLicenseApplicationID
                            where TestAppointments.TestTypeID = @TestTypeID 
                                and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDAppID
                            order by TestAppointments.AppointmentDate desc;";

            SqlCommand command = new SqlCommand(Qeury, Connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDAppID", LocalDrivingLicenseApplication);
            try
            {
                Connection.Open();
                int _result = command.ExecuteNonQuery();
                if (_result > 0)
                    Result = true;
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Result;

        }

        static public byte GetTotalTrialPerTest(int LocalDrivingApplicationID, byte TestTypeID)

        {
            byte Trials = 0;

            string Qeury = @"select Count(TestID) as TotalTrialPerTest 
                            from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            = TestAppointments.LocalDrivingLicenseApplicationID
                            where TestAppointments.TestTypeID =@TestTypeID
                    and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LDLAppID", LocalDrivingApplicationID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result == null)
                    byte.TryParse(result.ToString(), out Trials); 
            }catch (Exception ex) { }   
            finally { Connection.Close(); } 
            return Trials;
                
                
                }

        static public bool IsThereAnActiveScheduledTest(int LocalDrivingApplicationID, byte TestTypeID)
        {
            bool isthere = false; 
            string Qeury = @"select  Top 1 TestID 
                            from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            = TestAppointments.LocalDrivingLicenseApplicationID
                            where TestAppointments.TestTypeID = @TestTypeID 
                    and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAppID and 
                    IsLocked =0
                            order by TestAppointments.TestAppointmentID desc"; 
            SqlCommand Command = new SqlCommand(Qeury, Connection);
            Command.Parameters.AddWithValue("@TestTypeID ", TestTypeID); 
            Command.Parameters.AddWithValue("@LDLAppID ", LocalDrivingApplicationID); 

            try
            {
                Connection.Open();
                int result = Command.ExecuteNonQuery(); 
                if(result > 0)
                {
                    isthere = true; 
                }
            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return isthere;
        }
    }


}
