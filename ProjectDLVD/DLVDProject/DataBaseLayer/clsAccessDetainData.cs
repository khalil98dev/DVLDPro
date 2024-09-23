using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public  class clsAccessDetainData
    {
       static private SqlConnection Connection = new SqlConnection( clsDBSettings.Connection) ;
        //int detainID, int licenseID,
        //    DateTime detainDate, float fineDetain, int createdByUserID,
        //    bool isReleased, DateTime releaseDate, int releasedApplicationID,
        //    int releasedByUserID
        static public int AddNewDetain(int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;
            string Query = @"insert into DetainedLicenses 
						values (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased,
						@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID); "; 
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
             
            try
            {
                Connection.Open(); 
                object result = Command.ExecuteScalar();
                if (result != null)
                {
                    int.TryParse(result.ToString(), out DetainID);

                }

            }catch (Exception ex) { }
            finally { Connection.Close(); }

           


            return DetainID ;
        }

        static public bool UpdateDetainLicense(int DetainID,int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            bool Updated = false; 
            string Query = @"Update DetainedLicenses set 
	                            LicenseID = @LicenseID,
	                            DetainDate =@DetainDate , 
	                            FineFees=@FineFees,
	                            CreatedByUserID=@CreatedByUserID
	                            where DetainID = @DetainID; ";
            SqlCommand Command  = new SqlCommand (Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();
                int reult = Command.ExecuteNonQuery(); 
                if (reult > 0) {
                    Updated = true;
                }

            }catch  (Exception ex) { }  
            finally { Connection.Close(); } 
            return Updated; 
        }

        static public bool ReleaseDetainLicense(int DetainID,int ReleasedByUserID,
                                                int ReleasedApplicationID)
        {
            bool Released = false;

            string Query = @"Update DetainedLicenses set 
	                    ReleaseDate = @ReleaseDate,
	                    ReleasedByUserID =@ReleasedByUserID , 
	                    ReleaseApplicationID =@ReleaseApplicationID
	                    where DetainID = @DetainID; "; 

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID); 
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleasedApplicationID); 
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID); 
            Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now); 
           
            try
            {
                Connection.Open();
                int reult = Command.ExecuteNonQuery();
                if (reult > 0)
                {
                    Released = true;
                }
            }catch(Exception ex) { }
            finally { Connection.Close(); }
            return Released;
        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            bool Detained = false;
            string Query = @"select IsDetaineted = 1 from 
                            DetainedLicenses 
                            where DetainedLicenses.LicenseID = @LicenseID
                            and IsReleased=0 ;"; 

            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar(); 
                if (result != null)
                {
                    Detained = Convert.ToBoolean(result); 
                }
            }catch      (Exception ex) { }  
            finally { Connection.Close(); } 
            return Detained;    


        }

        static public bool GetDetainLicenseInformationByDetainID(int DetainID,ref int LicenseID,
            ref DateTime DetainDate,ref  float FineDetain,ref int CreatedByUserID,
            ref bool IsReleased,ref DateTime ReleaseDate,ref int ReleasedApplicationID,
           ref int ReleasedByUserID)
        {
            bool Found = false;

            string Query = @"select * from DetainedLicenses 
                            where DetainID = @DetainID ";
            SqlCommand Command = new SqlCommand(@Query,Connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader(); 
                if(Reader.Read())
                {
                     Found = true;
                    LicenseID = (int)Reader["LicenseID"];
                    DetainDate = Convert.ToDateTime(Reader["DetainDate"]);
                    FineDetain = Convert.ToSingle(Reader["FineFees"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    IsReleased = (bool)Reader["IsReleased"];
                    if(Reader["ReleaseDate"] ==DBNull.Value)
                    {
                        ReleaseDate = DateTime.MaxValue; 
                    }else 

                    ReleaseDate = Convert.ToDateTime(Reader["ReleaseDate"]);


                    if (Reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1; 
                    else 
                    ReleasedByUserID = (int)Reader["ReleasedByUserID"];

                    if (Reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleasedApplicationID = -1; 
                    else
                        ReleasedApplicationID = (int)Reader["ReleaseApplicationID"];

                }
                Reader.Close();
            }
            catch(Exception ex) { }
            finally { Connection.Close(); }



            return Found;

        }

        static public bool GetDetainLicenseInformationByLicenseID(int LicenseID, ref int DetainID,
           ref DateTime DetainDate, ref float FineDetain, ref int CreatedByUserID,
           ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedApplicationID,
          ref int ReleasedByUserID)
        {
            bool Found = false;

            string Query = @"select * from DetainedLicenses 
                            where LicenseID = @LicenseID ";
            SqlCommand Command = new SqlCommand(@Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    Found = true;
                    DetainID = (int)Reader["DetainID"];
                    DetainDate = Convert.ToDateTime(Reader["DetainDate"]);
                    FineDetain = Convert.ToSingle(Reader["FineFees"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    IsReleased = (bool)Reader["IsReleased"];
                    if (Reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = DateTime.MaxValue;
                    }
                    else

                        ReleaseDate = Convert.ToDateTime(Reader["ReleaseDate"]);


                    if (Reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)Reader["ReleasedByUserID"];

                    if (Reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleasedApplicationID = -1;
                    else
                        ReleasedApplicationID = (int)Reader["ReleaseApplicationID"];

                }
                Reader.Close();
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }



            return Found;

        }

        static public DataTable GetAllDetainLicense()
        {
            DataTable dt = new DataTable();

            string Query = "select * from DetainedLicenses ";

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
            }
            catch {  } finally { Connection.Close(); }
            return dt;
        }

    }
}
