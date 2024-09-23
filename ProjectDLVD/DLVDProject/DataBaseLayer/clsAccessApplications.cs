using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
namespace DataBaseLayer
{
    static public class clsAccessApplications
    {

        static readonly SqlConnection Connection = new SqlConnection(clsDBSettings.Connection); 
        
        static public DataTable GetLicenseClasses()
        {
            DataTable dtClasses = new DataTable();

            string Query = "select * from LicenseClasses"; 

            SqlCommand Command = new SqlCommand(Query, Connection); 

            try
            {
                Connection.Open(); 

                SqlDataReader reader = Command.ExecuteReader(); 
                if (reader.HasRows) 
                
                {
                    dtClasses.Load(reader); 

                }
                reader.Close(); 


            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); } 






            return dtClasses;   
        }

        static public int AddNewApplication(int PersonID,DateTime ApplicationDate,short ApplicationType,
            short ApplicationStatus, DateTime lastStatusDate, double PaidFees,int CreatedByUserID)
        {
            int ID = -1;

            string Query = @"insert into Applications values(@PersonID,@ApplicationDate,@ApplicationType,@ApplicationStatus,@lastStatusDate,@PaidFees,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID); 
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate); 
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType); 
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus); 
            command.Parameters.AddWithValue("@lastStatusDate", lastStatusDate); 
            command.Parameters.AddWithValue("@PaidFees", PaidFees); 
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID); 

            try
            {
                Connection.Open();

                object result = command.ExecuteScalar(); 
                if (result != null) 
                {
                    int.TryParse(result.ToString(), out ID);
                }



            }catch (Exception ex) {
              
            }   
            finally { Connection.Close(); } 

            return ID;  
        }

        static public bool UpdateApplicationStatus(int ApplicationID,short ApplicationStatus, DateTime lastStatusDate)
        {
            bool isUpdateed = false;

            string Query = "Update Applications set ApplicationStatus =@AppStatus ,LastStatusDate= @LastUpdateDate where ApplicationID = @AppID ; ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@AppStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastUpdateDate", lastStatusDate);
            Command.Parameters.AddWithValue("@AppID", ApplicationID);

            try
            {
                Connection.Open(); 
                int result = Command.ExecuteNonQuery();

                if ( result != 0 )
                {
                    isUpdateed = true;  
                }
                

            }catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); } 
            return isUpdateed; 
        }

        static public bool UpdateApplication(int ApplicationID, int ApplicantPersonID,int ApplicationTypeID,short ApplicationStatus,DateTime LastStatusDate,double PaidFees,int CreatedByUserID)
        {
            bool isUpdated = false;
            string Query = "Update Applications set \r\nApplicantPersonID =@PersonID , \r\n\r\nApplicationTypeID = @ApplicationTypeID, \r\nApplicationStatus = @ApplicationStatus, \r\nLastStatusDate = @LastStatusDate, \r\nPaidFees = @PaidFees, \r\nCreatedByUserID = @CreatedByUserID \r\nwhere ApplicationID = @ApplicationID  ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@PersonID", ApplicantPersonID);
            

            try
            {
                Connection.Open();
                int result = Command.ExecuteNonQuery();

                if (result != 0)
                {
                    isUpdated = true;
                }


            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); }
            return isUpdated;

        }
        static public DataTable GetApplicationInfos()
        {
            DataTable dt = new DataTable(); 
            string Qeury = "select * from LocalDrivingLicenseApplications_View"; 

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                if(Reader.HasRows)
                {
                    dt.Load(Reader); 
                }
                Reader.Close(); 

            }catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); }     
            return dt;  


        }
        
       

        static public int  GetActiveApplicationForLicenseClass(int personId,int LicenseClassID,short ApplicationTypeID)
        {
            int ApplicationID = -1;
            string Query = @"select LocalDrivingLicenseApplications.ApplicationID  
                        from LocalDrivingLicenseApplications  inner join
                        Applications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                        where 
                    Applications.ApplicantPersonID =@PersonID  
                and ApplicationStatus = 1
                and ApplicationTypeID=@ApplicationTypeID 
                    and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID ;
                    Select SCOPE_IDENTITY() ;";
           
             SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@PersonID", personId);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();    
                if(result != null )
                {
                    int.TryParse(result.ToString(), out ApplicationID);
                }
                
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally 
            { 
                Connection.Close();                     
            }
            return ApplicationID;   
        }

        static public int GetApplicationID(int LocalApplicationID)
        {
            int ID = -1;

            string Query = "select LocalDrivingLicenseApplications.ApplicationID from LocalDrivingLicenseApplications where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalApplicationID ;SELECT SCOPE_IDENTITY();";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalApplicationID", LocalApplicationID);
           

            try
            {
                Connection.Open();
                object  result = Command.ExecuteScalar();

                if (result != null)
                {
                    int.TryParse(result.ToString(), out ID); 

                }


            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); }
            return ID;
        }

        static public int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
    
        {
            int ActiveApplicationID = -1;

            string Query = @"select Applications.ApplicationID as ActiveApplicationID 
                                from Applications 
                            where ApplicationTypeID=@ApplicationTypeID
                            and ApplicantPersonID= @PersonID 
                            and ApplicationStatus=1; ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID); 
            Command.Parameters.AddWithValue("PersonID", PersonID); 

            try
            {
                Connection.Open(); 
                object Result = Command.ExecuteScalar();
                if(Result != null)
                {
                    int.TryParse(Result.ToString(), out ActiveApplicationID);
                }


            }catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
            finally { Connection.Close(); }


            return ActiveApplicationID;
        }
        
        static public bool DoesPersonHasActiveApplication(int PersonID,int AplicationTypeID)
        {
            return GetActiveApplicationID(PersonID, AplicationTypeID) != -1;
        }

        static public bool isApplicationExist(int ApplicationID)
        {
            int Founed = -1;

            string Query = @"select ApplicationID from Applications
                                where ApplicationID = @ApplicationID ; 
                                select SCOPE_IDENTITY(); ";
            SqlCommand Command = new SqlCommand(Query, Connection);    

            Command.Parameters.AddWithValue(Query, ApplicationID);

            try
            {
                Connection.Open() ;

                object Result = Command.ExecuteScalar();
                if(Result != null  )
                {
                    int.TryParse(Result.ToString().Trim(), out Founed);
                }

            }catch (Exception ex)
            {

            }finally { Connection.Close(); }


            return Founed !=-1; 
        }




        static public bool DelApplication(int ApplicationID) 
        {
            bool Deleted = false;
            string Query = "delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplications.ApplicationID =@ApplicationID;delete from applications where ApplicationID=@ApplicationID;"; 
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID); 

            try
            {
                Connection.Open();
                int result = command.ExecuteNonQuery(); 
                if (result != 0)
                {
                    Deleted = true; 
                }
            }catch  (Exception ex) {
               
            }  
            finally { Connection.Close(); } 
            return Deleted;


        }

        static public bool Find(int ApplicationID, ref int PersonID,ref int CreatedByUserID,ref DateTime ApplicationDate,
            ref short ApplicationTypeID,ref short ApplicationStatus,ref  DateTime LastStatusDate,ref double PaidFees)
        {
            bool isExist = false;

            string Query = "select * from applications where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader    = command.ExecuteReader();  

                while (Reader.Read())
                {
                    PersonID = (int)Reader["ApplicantPersonID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    ApplicationTypeID = Convert.ToInt16( Reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToInt16(Reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    PaidFees = Convert.ToDouble (Reader["PaidFees"]);
                    isExist = true;
                }
                Reader.Close(); 
            }
            catch (Exception ex) 
            {
            Console.WriteLine(ex.Message.ToString());
                
            }
            finally { Connection.Close(); }
            return isExist;

        }


    }
}
