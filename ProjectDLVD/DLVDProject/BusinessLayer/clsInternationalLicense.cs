using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInternationalLicense : clsApplications 
    {
        public enum eMode { eAddNewILicense =1,eUpdateILicense=2}
        public eMode Mode { get; set; }

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssueUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers UserInfo { get; set; }  
        public clsDriver DriverInfo { get; set; }

        public string DriverFullName
        { 
            get
            {
                return DriverInfo.DriverFullName; 
            }
        }


        public clsInternationalLicense( int ApplicationID,int ApplicatPersonID,
            DateTime ApplicationDate, eApllicationStatus ApplicationStatus,
            DateTime LastStatusDate,float PaidFess,int CreatedByUserID,
            int InternationalLicenseID, int IssueUsingLocalLicenseID, DateTime IssueDate,
            int DriverID,DateTime ExpiredDate,bool IsActive
          )
        {
            //For the base Class: 
            base.ApplicationID = ApplicationID;
            base.ApplicationStatus = ApplicationStatus;
            base.ApplicationTypeID = (byte)clsApplications.eApplicationType.eNewInterNationalLicense; 
            base.CreatedByUserID = CreatedByUserID;
            base.PersonID = ApplicatPersonID;
            base.ApplicationDate = ApplicationDate;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFess;




            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssueUsingLocalLicenseID = IssueUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpiredDate = ExpiredDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUsers.Find(CreatedByUserID); 
            this.DriverInfo = clsDriver.FindDriverByDriverID(DriverID);


            Mode = eMode.eUpdateILicense; 
        }

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssueUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpiredDate = DateTime.Now;

            this.IsActive = false;
            this.CreatedByUserID = -1;

            Mode = eMode.eAddNewILicense; 
        }


        private bool _AddNewInterNationalLicense()
        {
            
            return (this.InternationalLicenseID = clsInterNationalLicenseData.AddNewInterLicense(ApplicationID, DriverID, IssueUsingLocalLicenseID
                , IssueDate, ExpiredDate, IsActive, CreatedByUserID))!=-1; 
        }

        private bool _UpdateInterNationalLicense()
        {
            return clsInterNationalLicenseData.UpdateInterNationalLicense(InternationalLicenseID, ApplicationID,
                DriverID, IssueUsingLocalLicenseID, IssueDate, ExpiredDate, IsActive, CreatedByUserID);  
        }

        static new public clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID=-1, DriverID=-1,CreatedByuserID=-1, IssueUsingLocalLicenseID=-1;
            DateTime IssueDate=DateTime.Now, ExpiredDate=DateTime.Now;
            bool IsActive = false;

            if (clsInterNationalLicenseData.GetInterNationalLicenseInformationsByID(InternationalLicenseID,
              ref ApplicationID, ref DriverID, ref IssueUsingLocalLicenseID, ref IssueDate, ref ExpiredDate,
              ref IsActive, ref CreatedByuserID))
            {
                clsApplications Application = clsApplications.Find(ApplicationID);


                return new clsInternationalLicense(ApplicationID,Application.PersonID,
                    Application.ApplicationDate,Application.ApplicationStatus,Application.LastStatusDate,
                    (float)Application.PaidFees,Application.CreatedByUserID,InternationalLicenseID,
                    IssueUsingLocalLicenseID,IssueDate,DriverID,ExpiredDate,IsActive);
            }
            else
                return null;

        }

        static public clsInternationalLicense FindByDriverID(int DriverID)
        {
            int ApplicationID = -1, InternationalLicenseID = -1, CreatedByuserID = -1, IssueUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now, ExpiredDate = DateTime.Now;
            bool IsActive = false;

            if (clsInterNationalLicenseData.GetInterNationalLicenseInformationsByDriverID(DriverID,
              ref ApplicationID, ref IssueUsingLocalLicenseID, ref InternationalLicenseID, ref IssueDate, ref ExpiredDate,
              ref IsActive, ref CreatedByuserID))
            {
                clsApplications Application = clsApplications.Find(ApplicationID);


                return new clsInternationalLicense(ApplicationID, Application.PersonID,
                    Application.ApplicationDate, Application.ApplicationStatus, Application.LastStatusDate,
                    (float)Application.PaidFees, Application.CreatedByUserID, InternationalLicenseID,
                    IssueUsingLocalLicenseID, IssueDate, DriverID, ExpiredDate, IsActive);
            }
            else
                return null;

        }


        public new bool Save()
        {
            //We must convert the base mode to the same this class mode 
            base.Mode = (clsApplications.eMode)this.Mode;
            
            // Save the base class 
            if(!base.Save())
                return false;


            switch(Mode)
            {
                case eMode.eUpdateILicense:
                    return _UpdateInterNationalLicense();
                case eMode.eAddNewILicense:
                    
                        if(_AddNewInterNationalLicense())
                        {
                            Mode = eMode.eUpdateILicense; 
                            return true;
                        }
                    return false;   
                default:
                    return false;

            }
        }


        static public DataTable GetAllInternationalLicense()
        {
            return clsInterNationalLicenseData.GetAllInterNationalLicense();
        }

        static public DataTable GetDriverInternationalLicense(int DriverID)
        {
            return clsInterNationalLicenseData.GetAllDriverInterNationalLicense(DriverID);

        }

        static public int GetActiveDriverInternationalLicense(int DriverID)
        {
            return clsInterNationalLicenseData.GetActiveInternationalLiceseByDriverID(DriverID);

        }

        
    }
}
