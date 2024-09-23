using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsUsers
    {
       public  enum enMode { eAddNew = 0, eUpdateNew = 1 }
      public  enMode Mode { get; set; }
        public int    UserID     { get; set; } 
        public string UserName   { get; set; }
        public string Password   { get; set; }    
        public int    PersonID   {  get; set; }  
        public bool   isActive   { get; set; }


       public clsUsers() 
        {
            UserID = -1;
            UserName    ="";
            Password    ="";
            PersonID    =-1;
            isActive    =false;
            Mode = enMode.eAddNew;  

        }

        public clsUsers(int userID,string UserName,string Password,int PersonID,bool isActive)

        {
            this.UserID = userID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.isActive = isActive;
            Mode = enMode.eUpdateNew;   

        }

        static public clsUsers Find(int UserID)
        {
           
           string UserName = "";
            string Password ="";
           int PersonID = -1;
            bool isActive = false;

            if (clsAccessUsers.FindByUserID(UserID, ref UserName, ref Password, ref PersonID, ref isActive))
            {
                return new clsUsers(UserID, UserName, Password, PersonID, isActive);
            }
            else
                return null;


        }

        static public clsUsers Find(string UserName) 
        {
            int UserID = -1;    
            
            string Password = "";
            int PersonID = -1;
            bool isActive = false;

            if (clsAccessUsers.FindByUserName(UserName, ref UserID,  ref Password,ref PersonID, ref isActive))
            {
                return new clsUsers(UserID, UserName, Password, PersonID, isActive);
            }
            else
                return null;
        }

        static public bool IsEXistUser(string UserName)
        {
            return clsAccessUsers.IsExist(UserName);
        }

        static public bool IsEist(int persID)
        {
            return clsAccessUsers.isExistWithPersonID(persID);   
        }
        //Crud 

        private bool _AddNewuser()
        {
            return (this.UserID = clsAccessUsers.AddNewUser(this.PersonID,this.UserName,this.Password,this.isActive)) != -1;
        }

        public bool _Delete()
        {
            return clsAccessUsers.DeleteUser(this.UserID);    
        }

        static public bool Delete(int UserID)
        {
            return clsAccessUsers.DeleteUser(UserID);
        }

        private bool _Update()
        {
            return clsAccessUsers.Updateuserinfo(this.UserID,this.UserName, this.Password,this.isActive);   
        }

        public bool Save()
        {
            bool Saved = false; 
         switch(Mode) {
                case enMode.eAddNew :
                    if(_AddNewuser()) 
                    {
                        Saved = true;
                        Mode = enMode.eUpdateNew;
                    }
                    break ;
                case enMode.eUpdateNew: 
                    if(_Update())
                    {
                        Saved = true;
                    }
                    break ;

                    default: return false;  
            }   

            return Saved;   
        }


        //Get user info by Filter ....
       public  static DataTable GetUsersByUserID(int keyID)
        {
            return clsAccessUsers.GetUsersInfoByUserID(keyID);  
        }

       public static DataTable GetUsersByPersonID(int keyID)
        {
            return clsAccessUsers.GetUsersInfoByPersonID(keyID);
        }

       public static DataTable GetUsersByFullName(string keyFullName)
        {
            return clsAccessUsers.GetUsersInfoByFullName(keyFullName);
        }

       public static DataTable GetUsersByUserName(string keyUserName)
        {
            return clsAccessUsers.GetUsersInfoByUserName(keyUserName);
        }

       public static DataTable GetUsersByIsActive(bool activeSatatus)
        {
            return clsAccessUsers.GetUsersInfoByisActiveStatus(activeSatatus);
        }


       public static   int Count()
        {
            return clsAccessUsers.Countusers();
        }

       static public DataTable GetAllUsers()
        {
            return clsAccessUsers.GetAllUsers();
        }

        static public int Login(string UserName,string Password)
        {
            return clsAccessUsers.isExistUserWithuserNameAndPassword(UserName, Password);   
        }


    }
}
