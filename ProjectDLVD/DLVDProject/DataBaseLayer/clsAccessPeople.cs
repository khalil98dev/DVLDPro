using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataBaseLayer
{
    static public class clsAccessPeople
    {
        static public bool GetPersonByID(int PersonID, ref string FirstName, ref string LastName,ref  string ThirdName
            ,ref string FourthName,ref short Gender, ref DateTime DateOfBirth, ref string PhoneNumber,ref string Email,
            ref int CountryID, ref string Addres,ref string NationalN,ref string ImagePath)

        {
            bool isFound= false;
            string Query = "select * from People where  People.PersonID =@PersonID ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command =new SqlCommand(Query,Conn);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Conn.Open();
                SqlDataReader reader = Command.ExecuteReader(); 
                while(reader.Read())
                {
                    NationalN= reader[1].ToString();
                    FirstName = reader[2].ToString();
                    LastName = reader[3].ToString();
                    ThirdName= reader[4].ToString();
                    FourthName=reader[5].ToString();
                   
                    DateOfBirth = Convert.ToDateTime(reader[6]);
                    Gender = Convert.ToInt16( reader[7]);
                    Addres= reader[8].ToString();

                    PhoneNumber= reader[9].ToString();
                    Email= reader[10].ToString();
                    CountryID = Convert.ToInt32( reader[11]);
                    if(reader[12] == DBNull.Value)
                        ImagePath = "";
                    else
                    ImagePath = reader[12].ToString();


                    isFound = true;
                }
                
                reader.Close();
            }catch (Exception ex) { }
            finally { Conn.Close(); }

            return isFound;
        }
        static public int GetIDByNationaln(string NationalN)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.NationalNo =@NationalN ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@NationalN", NationalN);

            try
            {
                Conn.Open();
              
                    object result = Command.ExecuteScalar();
                    if(result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

              
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int GetIDByPhone(string Phone)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.Phone =@Phone ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                Conn.Open();
               
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }
        static public int GetIDByEmail(string Email)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.Email =@Email ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@Email", Email);

            try
            {
                Conn.Open();
                
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

              
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int GetIDByFirstName(string FirstName)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.FirstName =@FirstName ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@Email", FirstName);

            try
            {
                Conn.Open();
               
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int GetIDBySecondname(string SecondName)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.SecondName =@SecondName ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@SecondName", SecondName);

            try
            {
                Conn.Open();
                
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

             
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int GetIDBythirdName(string ThirdName)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.ThirdName =@ThirdName ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);

            try
            {
                Conn.Open();
               
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int GetIDByFourthName(string LastName)
        {

            int ID = -1;
            string Query = "select People.PersonID from People where  People.LastName =@LastName ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);
            Command.Parameters.AddWithValue("@LastName", LastName);

            try
            {
                Conn.Open();
               
                    object result = Command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out ID);
                    }

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;

        }

        static public int AddNewperson(string FirstName,  string LastName,  string ThirdName
            ,  string FourthName,  short Gender,  DateTime DateOfBirth,  string PhoneNumber,  string Email,
             int CountryID,  string Addres,  string NationalN,  string ImagePath)
        {
            int ID = -1; 
        
            string Query = "Insert Into People values(@NationalN,@FirstName,@LastName,@ThirdName,@FourthName,@DateOfBirth,@Gender,@Addres,@PhoneNumber,@Email" +
                ",@CountryID,@ImagePath); SELECT SCOPE_IDENTITY();";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);

            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            Command.Parameters.AddWithValue("@FourthName", FourthName);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            Command.Parameters.AddWithValue("@Addres", Addres);
            Command.Parameters.AddWithValue("@NationalN", NationalN);
           

            if(ImagePath !="")
            {
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }else
                Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);



            try
            {
                Conn.Open(); 
                object Result = Command.ExecuteScalar();
                if(Result != null ) 
                {
                int.TryParse(Result.ToString(), out ID);
                }

                

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return ID;
        }



         static public bool Update(int PersonID,string FirstName,  string LastName,  string ThirdName
            ,  string FourthName,  short Gender,  DateTime DateOfBirth,  string PhoneNumber,  string Email,
             int CountryID,  string Addres,  string NationalN,  string ImagePath)
        {
            int ID = -1; 
            string Query = "Update People set NationalNo =@NationalN, FirstName=@FirstName,SecondName = @LastName,ThirdName=@ThirdName ,LastName=@FourthName,DateOfBirth=@DateOfBirth,Gendor=@Gender,Address=@Addres,Phone=@PhoneNumber,Email=@Email,NationalityCountryID=@CountryID,ImagePath=@ImagePath where PersonID = @PersonID; ";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            Command.Parameters.AddWithValue("@FourthName", FourthName);
            Command.Parameters.AddWithValue("@Gender",Gender);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            Command.Parameters.AddWithValue("@Addres", Addres);
            Command.Parameters.AddWithValue("@NationalN", NationalN);
            if(ImagePath != "")
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
               Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                Conn.Open(); 
                int Result = Command.ExecuteNonQuery();
                if (Result > 0) {
                    ID = Result;
                }
               
                

            }
            catch (Exception ex)
            {
                
            }
            finally { Conn.Close(); }

            return (ID > 0);
        }


        static public bool Delete(int PersonID)
        {
            int ID = -1;
            string Query = "Delete From People where People.PersonID =@PersonID";

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Query, Conn);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            

            try
            {
                Conn.Open();
                int Result = Command.ExecuteNonQuery();
                if (Result > 0) 
                {
                    ID = Result;
                }
                


            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return (ID > 0);
        }

        static public bool isExist(string NationalN)
        {
            bool Exist =false ; 

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection) ;
            string Qeury = "select People.PersonID From People where NationalNo = @NationalN;SELECT SCOPE_IDENTITY();"; 
            SqlCommand Command = new SqlCommand(Qeury, Conn);
            Command.Parameters.AddWithValue("@NationalN", NationalN); 
            try
            {
                Conn.Open();
                object Result = Command.ExecuteScalar(); 
                if(Result != null)
                    Exist = true;   

            }catch (Exception ex) { }
            finally { Conn.Close(); }   



            return (Exist); 
        }

        static public int CountPeople()
        {
            int Number = 0 ;    
            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "select Count(*) From People ";
            SqlCommand Command = new SqlCommand(Qeury, Conn);
      
            try
            {
                Conn.Open();
                object Result = Command.ExecuteScalar();
                if (Result != null)
                    int.TryParse(Result.ToString(), out Number);    

            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return Number ; 
        }

        static public DataTable GetPeopleList()
        {
            DataTable dt = new DataTable();

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            string Qeury = "select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,\r\ncase People.Gendor \r\nwhen  0 Then 'Male'\r\nwhen 1 then 'Female' \r\nEnd \r\nas Gendor,Address,Phone,Email,Countries.CountryName ,ImagePath\r\nFrom People inner join Countries on Countries.CountryID = People.NationalityCountryID; ";
            SqlCommand Command = new SqlCommand(Qeury, Conn);
           
            try
            {
                Conn.Open();
               SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {

                
                    dt.Load(reader);    
                }
                reader.Close();  
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return dt ; 

        }

        static private DataTable GetDataTabel(string Qeury,string PrmString,object value)
        {
            DataTable dt = new DataTable();

            SqlConnection Conn = new SqlConnection(clsDBSettings.Connection);
            SqlCommand Command = new SqlCommand(Qeury, Conn);
            Command.Parameters.AddWithValue(PrmString, value);
            try
            {
                Conn.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { Conn.Close(); }

            return dt;
        }

        static public DataTable SearchByID(int ID)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.PersonID = @PersonID;", "@PersonID", ID);
        }

        static public DataTable SearchByFirstName(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.FirstName like @FirstName;", "@FirstName", "%" + Name + "%");
        }

        static public DataTable SearchBySecondName(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.SecondName like @SecondName;", "@SecondName", "%"+ Name+"%");
        }
        static public DataTable SearchByThirdNameName(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.ThirdName like @ThirdName;", "@ThirdName", "%" + Name + "%");
        }
        static public DataTable SearchByLastName(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.LastName like @LastName;", "@LastName", "%" + Name + "%");
        }
        static public DataTable SearchByNationalN(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.NationalNo like @NationalNo;", "@NationalNo",  Name + "%");
        }


        static public DataTable SearchByPhone(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.Phone like @Phone;", "@Phone", "%" + Name + "%");
        }

        static public DataTable SearchByEmail(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.Email like @Email;", "@Email", "%" + Name + "%");
        }

        static public DataTable SearchByGendor(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.Gendor like @Gendor;", "@Gendor",  Name + "%");
        }

        static public DataTable SearchByCountry(string Name)
        {
            return GetDataTabel("select * from PersonDetails where PersonDetails.CountryName like @Gendor;", "@Gendor","%"+ Name + "%");
        }


    }
}
