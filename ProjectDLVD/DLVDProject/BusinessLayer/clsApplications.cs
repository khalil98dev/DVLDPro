using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer
{
    public class clsApplications
    {
        //Application status from database description : 1-New 2-Cancelled 3-Completed
        public enum eApllicationStatus { eNew=1,eCancelled=2,eCompeleted=3} 
         public enum eApplicationType   { eNewDrivingLicense=1,eRenewDrivingLicense=2,eReplacementForLost=3,
                                        eReplacementForDamage=4,eReleaseDetain=5,eNewInterNationalLicense=6,
                                        eRetakeTest=7}
         
        public enum eMode { eAddNew=1, eUpdate=2}
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public clsPerson Personinfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public short ApplicationTypeID { get; set; }
        public clsApplicaionTypes ApplicationTypeInfo { get; set; } 
       public DateTime LastStatusDate { get; set; }
        public double PaidFees { get; set; }
        public eApllicationStatus ApplicationStatus {  get; set; } 

        public string ApplicantFullName
        {
            get
            {
                return Personinfo.FullName; 
            }
        }
        public string StatusText
        {
            get 
            {
                switch (ApplicationStatus)
                {
                    case eApllicationStatus.eNew:
                        return "New"; 
                        
                    case eApllicationStatus.eCompeleted:
                        return "Compeleted";
                        
                    case eApllicationStatus.eCancelled:
                        return "Cancelled";
                    default:
                        return "Unknow"; 
                }
            }
        }
        public eMode Mode { get; set; } 


        public clsApplications(int ApplicationID,int PersonID, int CreatedByUserID, DateTime ApplicationDate, 
            eApplicationType ApplicationTypeID,
            eApllicationStatus ApplicationStatus, DateTime LastStatusDate, double PaidFees)
        {
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.Personinfo = clsPerson.Find(PersonID); 
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsers.Find(CreatedByUserID); 
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID =(byte) ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicaionTypes.Find((byte)ApplicationTypeID); 
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            Mode = eMode.eUpdate;

        }
        public clsApplications()
        {
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.Personinfo = new clsPerson(); 
            this.CreatedByUserID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = eApllicationStatus.eNew ;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = -1;
            Mode = eMode.eAddNew;
        }


        private bool AddNewApplication()
        {
            return (this.ApplicationID = clsAccessApplications.AddNewApplication(PersonID, ApplicationDate, ApplicationTypeID,
                (byte)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)) != -1;
        }


        static public DataTable GetLocalDrivingLicenceInfo()
        {
            return clsAccessApplications.GetApplicationInfos();
        }
       
       
       
        public bool DoesPersonHasActiveApplication(int PersonID)
        {
            return DoesPersonHasActiveApplication(PersonID, this.ApplicationID);
        }

        static public bool DoesPersonHasActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return clsAccessApplications.DoesPersonHasActiveApplication(PersonID, ApplicationTypeID);
        }


        private bool _Update()
        {
            return clsAccessApplications.UpdateApplication(this.ApplicationID, this.PersonID, this.ApplicationTypeID
                ,(byte)ApplicationStatus, this.ApplicationDate, this.PaidFees, this.CreatedByUserID);
        }
              
       
        public bool Save()
        {
            switch(Mode)
            {
                case eMode.eUpdate:
                    if(_Update())
                    {
                        return true;
                    }
                    break;
                    case eMode.eAddNew:
                    if (AddNewApplication())
                    {
                        Mode = eMode.eUpdate;
                        return true;
                    }
                    break; 
                    
            }
            return false;
        }

        static public int FindApplicationID(int LocalApplicationID)
        {
            return clsAccessApplications.GetApplicationID(LocalApplicationID); 
        }

        static public bool Delete(int ApplicationID)
        {
            return clsAccessApplications.DelApplication(ApplicationID); 
        }
         public bool Delete()
        {
            return clsAccessApplications.DelApplication(this.ApplicationID);
        }

        static public  clsApplications Find(int AppID)
        {

            
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now;
            short ApplicationTypeID = -1;
            short ApplicationStatus = -1;
            DateTime LastStatusDate = DateTime.Now;
            double PaidFees = -1;

            if (clsAccessApplications.Find(AppID, ref PersonID, ref CreatedByUserID, ref ApplicationDate, 
                ref ApplicationTypeID, ref ApplicationStatus,ref LastStatusDate, ref PaidFees))
            {

                return new clsApplications( AppID, PersonID, CreatedByUserID, ApplicationDate, (eApplicationType)ApplicationTypeID,
             (eApllicationStatus)ApplicationStatus, LastStatusDate, PaidFees);
            }
            else
                return null; 


        }

        public bool SetCompeted()
        {
            return clsAccessApplications.UpdateApplicationStatus(this.ApplicationID,
                                     (byte)eApllicationStatus.eCompeleted,DateTime.Now);
        }

        public bool SetCancelled()
        {
            return clsAccessApplications.UpdateApplicationStatus(this.ApplicationID,
                                    (byte)eApllicationStatus.eCancelled , DateTime.Now);
        }

        static public int GetActiveApplicationID(int PeronID,int ApplicationTypeID)
        {
            return clsAccessApplications.GetActiveApplicationID(PeronID, ApplicationTypeID); 
        }

        public int GetActiveApplicationID(int ApplicationTypeID)
        {
            return GetActiveApplicationID(this.PersonID, ApplicationTypeID);
        }


        static public bool isExistApplication(int ApplicationID)
        {
            return clsAccessApplications.isApplicationExist(ApplicationID);
        }

        static public int GetAactiveApplicationIDForLicenseClas(int PersonID,int LicenseClassID,eApplicationType ApplicationType)

        {
            return clsAccessApplications.GetActiveApplicationForLicenseClass(PersonID, LicenseClassID
                                                                            , (short)ApplicationType);
        }
    }

   


    }





