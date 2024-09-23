using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDriver
    {
        public enum eMode { eAddNewDriver=1,eUpdateDriver=2}
        public eMode Mode { get; set; } 

        public int DriverID { get; set; }

        public int PersonID { get; set; }
        public clsPerson Driverinfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo { get; set; }
        public DateTime CreatedDate { get; set; }

        public string DriverFullName
        {
            get
            {
                return Driverinfo.FullName; 
            }
        }

        public clsDriver(int driverID, int personID, int createdByUserID,  DateTime createdDate)
        {
            Mode = eMode.eUpdateDriver;
            DriverID = driverID;
            PersonID = personID;
            Driverinfo = clsPerson.Find(personID);
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUsers.Find(createdByUserID);
            CreatedDate = createdDate;
        }

        public clsDriver() {

            Mode = eMode.eAddNewDriver;
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
        }

        private bool _AddNewDriver()
        {
            return (this.DriverID = clsAccessDriver.AddNewDriver( this.PersonID,this.CreatedByUserID,
                this.CreatedDate))!=-1;
        }

        private bool _UpdateDriver()
        {
            return clsAccessDriver.UpdateDriver(this.DriverID, PersonID, CreatedByUserID, 
                CreatedDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eUpdateDriver:
                    return _UpdateDriver();
                case eMode.eAddNewDriver:
                    if(_AddNewDriver())
                    {
                        Mode = eMode.eUpdateDriver;
                        return true;    
                    }
                    break; 
                    

            }
            return false;
        }

        static public DataTable GetAllDrivers()
        {
            return clsAccessDriver.GetAllDriver(); 
        }

        static public clsDriver FindDriverByDriverID(int DriverID)
        {
            int PersonID =-1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsAccessDriver.FindByDriverID(DriverID, ref PersonID, ref CreatedByUserID,
                ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

            }
            else
                return null; 
        }

        static public clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsAccessDriver.FindByPersonID(PersonID, ref DriverID, ref CreatedByUserID,
                ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

            }
            else
                return null;
        }

        static public DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverlicense(DriverID); 
        }



    }
}
