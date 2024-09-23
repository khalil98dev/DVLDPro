using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BusinessLayer.clsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer
{
    public class clsLicense
    {
        //1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
        public enum eIssueReason { eFirstTime=1, eRenew =2, ReplacementForDamaged = 3,
            ReplacementForLost =4}

        public enum eMode { eAddnewLicense=1,eUpdateLicense=2}
        public eMode Mode { get; set; } 
        public int LicenseID {  get; set; }
        public int ApplicationID { get; set; } 
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public eIssueReason IssueReason { get; set; }   
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserinfo { get; set; }
        public clsDetainLicense DetainedInfo { set; get; }

        public clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, 
            DateTime issueDate, DateTime expirationDate, string notes, float paidFees, bool isActive,
            eIssueReason issueReason,
            int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID) ;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            CreatedByUserinfo = clsUsers.Find(createdByUserID);
            DriverInfo = clsDriver.FindDriverByDriverID(driverID);
            DetainedInfo = clsDetainLicense.FindByLicenseID(licenseID); 
            Mode = eMode.eUpdateLicense;
        }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
           
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = -1;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = -1;
    
            Mode = eMode.eAddnewLicense;
        }

        private bool _AddNewLicense()
        {
            return (this.LicenseID == clsAccessLicensesData.AddnewLicense(ApplicationID, DriverID, LicenseClassID
                , IssueDate, ExpirationDate, Notes, PaidFees, IsActive,(byte) IssueReason, CreatedByUserID)); 
        }

        private bool _Update()
        {
            return clsAccessLicensesData.UpdateLicense(this.LicenseID,ApplicationID,DriverID,LicenseClassID,
                IssueDate,ExpirationDate, Notes,PaidFees,IsActive,(byte) IssueReason,CreatedByUserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eUpdateLicense:
                    return _Update();
                case eMode.eAddnewLicense:
                    if (_AddNewLicense())
                    {
                        Mode = eMode.eAddnewLicense;
                        return true;
                    }
                    else
                        return false; 

            }

            return false;
        }

        static public bool Delete(int LicenseID)
        {
            return clsAccessLicensesData.DeleteLicense(LicenseID);
        }

        public bool Delete() { 
        
            return Delete(this.LicenseID);
        }

        static public int GetActivateLicenseIDByPersonID(int PersonID,int LicenseClassID)
        {
            return clsAccessLicensesData.GetActivateLicenseIDByPersonID(PersonID, LicenseClassID);
        }

        public int GetActivateLicenseIDByPersonID(int PersonID)
        {
            return GetActivateLicenseIDByPersonID(PersonID,this.LicenseClassID);
        }
        
        static public DataTable GetDriverlicense(int DriverID)
        {
            return clsAccessLicensesData.GetDriverLicense(DriverID);
        }
        public Boolean IsLicenseExpired()

        {
            return (ExpirationDate < DateTime.Now); 
        }

        public bool DesActivateCurrentLicense()
        {
            return clsAccessLicensesData.DesActivateCurrentLicense(this.LicenseID);
        }

        public string GetIssueReasonText(eIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case eIssueReason.eFirstTime:
                    return "First Time";
                case eIssueReason.eRenew:
                    return "Renew License";
                case eIssueReason.ReplacementForDamaged:
                    return "Replacement For Damage";
                case eIssueReason.ReplacementForLost:
                    return "Replacement For Lost";
                default:
                    return "Unknow";

            }
        }

       public clsLicense RenewLicense(string Notes,int CreatedByUserID)
        {
            //Create new Application: 
            clsApplications Application = new clsApplications();
           

            Application.PersonID = DriverInfo.PersonID;
            Application.CreatedByUserID = CreatedByUserID;
            Application.ApplicationStatus = clsApplications.eApllicationStatus.eCompeleted;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (byte)clsApplications.eApplicationType.eRenewDrivingLicense;
            Application.PaidFees = clsApplicaionTypes.Find((byte)clsApplications.eApplicationType
                                                                .eRenewDrivingLicense).Fees; 

            if(!Application.Save())
            {
                return null; 
            }

            clsLicense License = new clsLicense();

            License.ApplicationID = Application.ApplicationID; 
            License.CreatedByUserID= CreatedByUserID;
            License.DriverID = this.DriverID;
            License.IssueDate = DateTime.Now;
            License.IssueReason = eIssueReason.eRenew;
            License.LicenseClassID = this.LicenseClassID;

            int DefaultLicenseLenght = this.LicenseClassInfo.DefaultValidityLenghth; 

            License.ExpirationDate = DateTime.Now.AddYears(DefaultLicenseLenght);
            License.IsActive = true;
            License.PaidFees = this.LicenseClassInfo.ClassFees; 

           License.Notes =Notes;

            if(!License.Save())
            {
                return null; 
            }

            //We need to desactivate old license 
            DesActivateCurrentLicense();

            return License;
        }
       public clsLicense Replace(eIssueReason IssueReason,int CreatedByUserID)
        {
            //Create new Application: 
            clsApplications Application = new clsApplications();


            Application.PersonID = DriverInfo.PersonID;
            Application.CreatedByUserID = CreatedByUserID;
            Application.ApplicationStatus = clsApplications.eApllicationStatus.eCompeleted;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (byte)((IssueReason == eIssueReason.ReplacementForDamaged) ?
                clsApplications.eApplicationType.eReplacementForDamage :
                clsApplications.eApplicationType.eReplacementForLost);
            Application.PaidFees = clsApplicaionTypes.Find(Application.ApplicationID).Fees;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense License = new clsLicense();

            License.ApplicationID = Application.ApplicationID;
            License.CreatedByUserID = CreatedByUserID;
            License.DriverID = this.DriverID;
            License.IssueDate = DateTime.Now;
            License.IssueReason = IssueReason ; 
            License.LicenseClassID = this.LicenseClassID;
            License.ExpirationDate = this.ExpirationDate;
            License.IsActive = true;
            License.PaidFees = 0;//Because of its replacement not a new 


            License.Notes = this.Notes;

            if (!License.Save())
            {
                return null;
            }

            //We need to desactivate old license 
            DesActivateCurrentLicense();

            return License;
        }

       public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainLicense DetainLicense = new clsDetainLicense();
            DetainLicense.FineDetain = FineFees;
            DetainLicense.DetainDate = DateTime.Now;                
            DetainLicense.CreatedByUserID= CreatedByUserID;
            DetainLicense.LicenseID = this.LicenseID;

            if(!DetainLicense.Save())
            {
                return -1;
            }

            return DetainLicense.DetainID;
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID,ref int ApplicationID)
        {
            //Create new Application: 
            clsApplications Application = new clsApplications();


            Application.PersonID = DriverInfo.PersonID;
            Application.CreatedByUserID = ReleasedByUserID;
            Application.ApplicationStatus = clsApplications.eApllicationStatus.eCompeleted;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (byte)clsApplications.eApplicationType.eReleaseDetain; 
                
            Application.PaidFees = clsApplicaionTypes.Find((byte)clsApplications.eApplicationType
                                                                .eReleaseDetain).Fees;

            if (!Application.Save())
            {
                ApplicationID = -1; 
                return false;
            }

            ApplicationID = Application.ApplicationID;
            return this.DetainedInfo.ReleaseDetain(ReleasedByUserID,ApplicationID);




        }
    }
}
