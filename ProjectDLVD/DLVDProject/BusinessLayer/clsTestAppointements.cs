using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTestAppointements
    {

        public enum eMode { eAddNewAppointement=0,eUpdateAppointement=1}
        public eMode Mode { get; set; } 
        public int TestAppointementID { get; set; }
        public clsTestTypes.eTestTypes TestTypeID { get; set; }
        public int LocalDrivingLicenseID { get; set; }
        public clsLocalDriveingLicenceApplications LocalDrivingLicenseAplicationInfo { get; set; }
        public DateTime AppointementDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool isLocked { get; set; }
        public int RetakeTestID { get; set; }
        
        public int TestID
        {
            get { return _GetTestID(); }
        }

        public clsTestAppointements(int TestAppointID,clsTestTypes.eTestTypes TestTypeID, int LocalDrivingLicenseID,
            DateTime AppointementDate, float PaidFees, int CreatedByUSerID, bool isLocked, int RetakeTestID)
        {
            this.TestAppointementID = TestAppointID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseID = LocalDrivingLicenseID;
            this.LocalDrivingLicenseAplicationInfo = clsLocalDriveingLicenceApplications.Find(LocalDrivingLicenseID);
            this.AppointementDate = AppointementDate; 
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUSerID; 
            this.isLocked  = isLocked;
            this.RetakeTestID = RetakeTestID;
           
        }
        public clsTestAppointements()
        {
            this.TestAppointementID = -1;
            this.TestTypeID = 0;
            this.LocalDrivingLicenseID = -1;
            this.LocalDrivingLicenseAplicationInfo = null;
            this.AppointementDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.isLocked = false;
            this.RetakeTestID = -1;

            Mode = eMode.eAddNewAppointement; 

        }
        static public clsTestAppointements Find(int AppointementID)
        {
            byte TestTypeID = 0;
            int LocalDrivingLicenseID = -1, RetakeTest = -1, CreatedByUserID = -1;
            DateTime AppointementDate = DateTime.Now;
            float PaidFees = -1;
            
            bool isLocked = false;
            if (clsAccessTestAppointement.FindTestAppointementByAppID(AppointementID, ref TestTypeID, ref LocalDrivingLicenseID,
                    ref AppointementDate, ref PaidFees, ref CreatedByUserID, ref isLocked, ref RetakeTest))
            {
                return new clsTestAppointements(AppointementID, (clsTestTypes.eTestTypes)TestTypeID, LocalDrivingLicenseID,
                    AppointementDate, PaidFees, CreatedByUserID, isLocked, RetakeTest);
            }
            else
                return null; 


        }

        private bool _AddNewAppointement()
        {
            return (this.TestAppointementID = clsAccessTestAppointement.AddTestAppointement((byte)this.TestTypeID,
                LocalDrivingLicenseID, AppointementDate, PaidFees, CreatedByUserID, isLocked, RetakeTestID)) != -1;
        }

        private bool _UpdateTestAppointement()
        {
            return clsAccessTestAppointement.UpdateTestAppointement((byte)this.TestTypeID, LocalDrivingLicenseID,
                AppointementDate, PaidFees, CreatedByUserID, isLocked, RetakeTestID); 
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eAddNewAppointement:
                    if(_AddNewAppointement())
                    {
                        Mode = eMode.eUpdateAppointement;
                        return true;
                    }
                       
                    break;
                case eMode.eUpdateAppointement:
                    return _UpdateTestAppointement();
                    
                   
            }

            return false;
        }
        static public clsTestAppointements FindLastAppointement(int LocalDrivingLicenseID, byte TestTypeID)
        {
          
            int RetakeTest = -1, CreatedByUserID = -1,TestAppointID=-1;

            DateTime AppointementDate = DateTime.Now;
            float PaidFees = -1;

            bool isLocked = false;
            if (clsAccessTestAppointement.FindLastTestAppointement(LocalDrivingLicenseID, TestTypeID,
                ref TestAppointID,ref AppointementDate, ref PaidFees,ref CreatedByUserID,ref isLocked,
                ref RetakeTest))
            {
                return new clsTestAppointements(TestAppointID, (clsTestTypes.eTestTypes)TestTypeID,
                    LocalDrivingLicenseID, AppointementDate, PaidFees, CreatedByUserID, isLocked, RetakeTest);
            }else
                return null;

        }

        public clsTestAppointements FindLastAppointement(byte TestTypeID)
        {
            return FindLastAppointement(this.LocalDrivingLicenseID, TestTypeID);
        }

        static public DataTable  GetAllTestAppointement()
        {
            return clsAccessTestAppointement.GetAllTestAppointement();
        }
        private int _GetTestID()
        {
            return clsAccessTestAppointement.GetTestID(this.TestAppointementID);  
        }



    }
}
