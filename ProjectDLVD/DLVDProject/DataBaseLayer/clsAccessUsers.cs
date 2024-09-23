using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    static public class clsAccessUsers
    {
        static public int isUserExist(string UserName,string Password)
        {
            int isExist = -1;

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "select * from Users where UserName = @UserName and Password = @Password ;SELECT SCOPE_IDENTITY();"; 
            SqlCommand command = new SqlCommand(Qeury,Connection);

            command.Parameters.AddWithValue("@UserName", UserName);  
            command.Parameters.AddWithValue("@Password", Password);  

            try
            {
                Connection.Open();
                object Result = command.ExecuteScalar(); 
                if (Result !=null ) 
                {
                    int.TryParse(Result.ToString(), out isExist); 
                }
            }catch (Exception ex) { }
            finally { Connection.Close(); } 
            return isExist; 
        }





        // Find By User NAme 
        static public bool FindByUserName( string UserName ,ref int UserID, ref string Pass,ref int PersonID, ref bool isActive)
        {
            bool isEist = false;
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = @"select * from Users where UserName  = @UserName;";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {


                    UserID = Convert.ToInt32(Reader["UserID"]);
                    Pass = Reader["Password"].ToString();
                    PersonID = Convert.ToInt32(Reader["PersonID"]);
                   

                    if (Reader["IsActive"].ToString() == "False")
                        isActive = false;
                    else
                        isActive = true;
                    isEist = true;

                }
                Reader.Close();


            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isEist;
        }

        static public bool FindByUserID(int UserID, ref string UserName, ref string Pass, ref int PersonID,ref bool isActive)
        {
            bool isEist = false;
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = @"select * from Users where UserID  = @UserID;";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {


                    UserID = Convert.ToInt32(Reader["UserID"]);
                    Pass = Reader["Password"].ToString();
                    PersonID = Convert.ToInt32(Reader["PersonID"]);
                    UserName = Reader["UserName"].ToString();

                    if (Reader["IsActive"].ToString() == "False")
                        isActive = false;
                    else
                        isActive = true;
                    isEist = true;

                }
                Reader.Close();


            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isEist;
        }


        static public bool IsExist(string UserName)
        {
            bool isEist = false;
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = @"select * from Users where Users.UserName =@UserName ;Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserName ", UserName);

            try
            {
                Connection.Open();
               object Res = command.ExecuteScalar();    
                if(Res != null) 
                {
                    int.TryParse(Res.ToString(), out int NumberOfRows);
                    isEist = NumberOfRows > 0;  

                }
                


            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isEist;
        }
        static public bool isUserActive(int UserID) 
        {

            bool isExist = false;

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "select * from Users where UserID =@UserID ;";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();
                int Result = command.ExecuteNonQuery();
                if (Result > 0)
                {
                    isExist  = true;    
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isExist;
        }

        static public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable(); 
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "select Users.UserID,Users.PersonID,People.FirstName +' '+ People.LastName as FullName, Users.UserName,Users.IsActive\r\nfrom Users inner join People \r\non Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.HasRows) 
                {
                    dt.Load(reader);
                }
       
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return dt;  
        }

        static public int AddNewUser(int PersonID,string UserName,string Password,bool isActive)
        {
            int isExist = -1;

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "insert into Users values(@PersonID,@UserName,@Password,@isActive);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            if(!isActive)
            command.Parameters.AddWithValue("@isActive", 0);
            else
                command.Parameters.AddWithValue("@isActive", 1);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out isExist);
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isExist;
        }

        static public bool DeleteUser(int UserID)
        {
            bool Deleted = false;

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "delete from Users where UserID =@UserID ;";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                Connection.Open();
                int Result = command.ExecuteNonQuery();
                if (Result > 0)
                {
                    Deleted = true;
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Deleted;
        }

        static public bool Updateuserinfo(int UserID,string UserName, string Password, bool isActive)
        {
            bool Updated = false;

            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "update Users set UserName =@UserName,Password =@Password,IsActive=@isActive where UserID = @UserID;";
            SqlCommand command = new SqlCommand(Qeury, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            if(!isActive)
            command.Parameters.AddWithValue("@isActive", 0);
            else
                command.Parameters.AddWithValue("@isActive", 1);

            try
            {
                Connection.Open();
                int Result = command.ExecuteNonQuery();
                if (Result > 0)
                {
                    Updated = true;
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return Updated;
        }

       //Filter Methodes ... -- this methodes retrive all users by : 
     

       
        //UserName 
        static public DataTable GetUsersInfoByUserName(string  UserName) 
        {
            DataTable dt = new DataTable();

            string Qeury = @"select * from UsersPlus where UserName like @UserName  ;";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@UserName", "%" + UserName + "%");


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
        //User ID
        static public DataTable GetUsersInfoByUserID(int UserID)
        {
            DataTable dt = new DataTable();

            string Qeury = @"select * from UsersPlus where UserID like @UserID  ;";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID +"%");

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
        //Person ID
        static public DataTable GetUsersInfoByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();

            string Qeury = "select * from UsersPlus where PersonID like @PersonID  ;";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID +"%");

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
        //Active Status
        static public DataTable GetUsersInfoByisActiveStatus(bool isActive)
        {
            DataTable dt = new DataTable();

            string Qeury = "select * from UsersPlus where IsActive = @IsActive  ;";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);
            if(isActive)
            Command.Parameters.AddWithValue("@IsActive", 1);
            else
                Command.Parameters.AddWithValue("@IsActive", 0);


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

        // By Full Name  UsersWithFullNames

        static public DataTable GetUsersInfoByFullName(string FullName)
        {
            DataTable dt = new DataTable();

            string Qeury = @"select * from UsersPlus where FullName like @FullName  ;";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@FullName", "%"+FullName +"%");


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

        static public int Countusers()
        {
            string Qeury = @"select Count(*) from Users ; SELECT SCOPE_IDENTITY();";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            int id = -1;
            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();    
                if(Result !=null) {
                int.TryParse(Result.ToString(), out  id);
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }

            return id;
        }

        static public bool isExistWithPersonID(int PersonID)
        {
            bool isExist = false;

            string Qeury = "select PersonID from Users where PersonID = @PersonID ;select SCOPE_IDENTITY();";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object Res  = Command.ExecuteScalar();  
                if(Res !=null)
                {
                    int.TryParse(Res.ToString(), out  int ID);
                    (isExist) = (ID > 0);   
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isExist;
        }


        
        // Log in Users 

        static public int isExistUserWithuserNameAndPassword(string UserName,string Password)
        {
            int isExist = -1;

            string Qeury = "select UserID from Users  where UserName =@UserName and Password =@Password;  ;select SCOPE_IDENTITY();";
            SqlConnection Connection = new SqlConnection(clsDBSettings.Connection);

            SqlCommand Command = new SqlCommand(Qeury, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();
                object Res = Command.ExecuteScalar();
                if (Res != null)
                {
                    int.TryParse(Res.ToString(), out isExist);
                  
                    
                }
            }
            catch (Exception ex) { }
            finally { Connection.Close(); }
            return isExist;
        }

    }
}
