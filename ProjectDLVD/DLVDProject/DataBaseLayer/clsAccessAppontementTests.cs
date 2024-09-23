using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseLayer
{
    public static class clsAccessAppontementTests
    {
        static public SqlConnection Connection = new SqlConnection(clsDBSettings.Connection); 
    
        static public int AddAppointementTest(int TestTypeID,int LDLicenseAppID,DateTime AppointementDate,double PaidFees,int UserID,bool IsLocked=false)
        {
            int TestID = -1;
            int Locked = (IsLocked) ? 1 : 0;
            string Query = "insert into TestAppointments values (@TestTypeID,@LDLicenseAppID,@AppointementDate,@PaidFees,@UserID,@IsLocked);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query,Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDLicenseAppID", LDLicenseAppID);
            command.Parameters.AddWithValue("@AppointementDate", AppointementDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@IsLocked", Locked);
            try
            {
                Connection.Open(); 
                object result = command.ExecuteScalar();
                if(result != null )
                {
                    int.TryParse(result.ToString(), out TestID);    

                }





            }catch(Exception ex) { }
            finally { Connection.Close(); } 


            return TestID;
        }

        static public bool UpdateAppointement(DateTime AppointementDate,int AppintID ,bool IsLocked=false )
        {
            bool isUpdated = false;
            int Locked = (IsLocked) ? 1 : 0;

            string Query = "update  TestAppointments set AppointmentDate =@AppointementDate , IsLocked=@Locked where TestAppointmentID=@AppoinID;  ";
            SqlCommand command= new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@Locked", Locked); 
            command.Parameters.AddWithValue("@AppointementDate", AppointementDate); 
            command.Parameters.AddWithValue("@AppoinID", AppintID); 

            try
            {
                Connection.Open();
                int result = command.ExecuteNonQuery(); 
                if( result != 0 )
                {
                    isUpdated = true; 
                }

            }catch {  } finally { Connection.Close(); }


            return isUpdated; 


        }

       static public DataTable GetShudeleInfos(int DLAppID,int TestTypeID)
        {
            DataTable dt = new DataTable();

            string Query = "select * from TestAppointments where TestTypeID=@TestTypeID and LocalDrivingLicenseApplicationID = @DlApplicationID";
            SqlCommand command = new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@DlApplicationID", DLAppID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader(); 
                if(Reader.HasRows)
                {
                    dt.Load(Reader); 

                }
                Reader.Close();
            }
            catch { }
            finally { Connection.Close(); }    


            return dt;
        }

        static public bool Find(int AppoinID, ref int LDAppID, ref string TestTypeTitle,ref string ClasseName
            ,ref  DateTime AppointDate,ref double PaidFees,ref string FullName,ref short isLock)
        {
            bool isExist= false;    
            string Qeury = "select * from TestAppointments_View  where TestAppointmentID = @AppoinID";

            SqlCommand Command = new SqlCommand(Qeury,Connection);
            Command.Parameters.AddWithValue("@AppoinID", AppoinID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                while(Reader.Read())
                {
                    
                    LDAppID = Convert.ToInt32(Reader["LocalDrivingLicenseApplicationID"]);
                    TestTypeTitle = Reader["TestTypeTitle"].ToString();
                    ClasseName = Reader["ClassName"].ToString();
                    AppointDate = Convert.ToDateTime( Reader["AppointmentDate"]);
                    PaidFees = Convert.ToDouble( Reader["PaidFees"]);
                    FullName =  Reader["FullName"].ToString();
                    isLock =  Convert.ToInt16(Reader["IsLocked"]);
                    isExist =true;

                }
                Reader.Close();
            }catch { }finally { Connection.Close(); }   

            return isExist;
        }
    }
}
