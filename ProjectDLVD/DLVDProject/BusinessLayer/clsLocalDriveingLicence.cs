using DataBaseLayer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLocalDriveingLicenceApplications : clsApplications 
    {
        public new enum eMode { eAddNew=1,eUpdate=2} 
        public new  eMode Mode { get; set; } 

        public int LocalDrivingLicenseApplicationID { get; set; } 
        public int LicenseClassID { get; set; }
        
        public clsLicenseClasses LicenseClassInfo { get; set; } 

     

        public clsLocalDriveingLicenceApplications() 
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassInfo =new clsLicenseClasses();
            LicenseClassID = -1;
            this.Mode = eMode.eAddNew; 
        }
        public clsLocalDriveingLicenceApplications(int LocalDrivingLicenseApplicationID, 
            int ApplicationID, int LicenseClassID,int PersonID,int CreatedByUserID,DateTime ApplicationDate,
           eApplicationType ApplicationType, eApllicationStatus ApplicationStatus,DateTime LastStatusDate,
           double PaidFees)

        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsers.Find(CreatedByUserID);

            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (byte)ApplicationType; 
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.ApplicationStatus = ApplicationStatus;



            this.Mode = eMode.eUpdate; 
        }   

        

        public bool AddNew()
        {
            
                if ((this.LocalDrivingLicenseApplicationID = clsAccessLocalDrivingLicenseApplications.
                AddNewLocalDrivingLicenceApplication(this.ApplicationID, this.LicenseClassID)) != -1)
                {
                    return true;

                }
                else
                { return false; }
        
        }

        public bool _Update()
        {
            return clsAccessLocalDrivingLicenseApplications.UpdateLocalaDrivingLicenseApplication(LocalDrivingLicenseApplicationID,this.ApplicationID,
                this.LicenseClassID);
        }

        public static new  clsLocalDriveingLicenceApplications Find(int LocalDrivingLicenseID)
        {
            int ApplicationID =-1,LicenceClassID =-1;
            if(clsAccessLocalDrivingLicenseApplications.GetLocalDrivingAppInfoByID(LocalDrivingLicenseID,
               ref ApplicationID,ref LicenceClassID))
            {
                clsApplications Application = clsApplications.Find(ApplicationID);
                return new clsLocalDriveingLicenceApplications(LocalDrivingLicenseID, ApplicationID, LicenceClassID,
                    Application.PersonID, Application.CreatedByUserID, Application.ApplicationDate,
                    (eApplicationType)Application.ApplicationID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees);
            }else 
                return null;    

        }

        public static clsLocalDriveingLicenceApplications FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseID = -1, LicenceClassID = -1;
            if (clsAccessLocalDrivingLicenseApplications.GetLocalDrivingAppInfoByApplicationID(ApplicationID,
               ref LocalDrivingLicenseID, ref LicenceClassID))
            {
                clsApplications Application = clsApplications.Find(ApplicationID);
                return new clsLocalDriveingLicenceApplications(LocalDrivingLicenseID, ApplicationID, LicenceClassID,
                    Application.PersonID, Application.CreatedByUserID, Application.ApplicationDate,
                    (eApplicationType)Application.ApplicationID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees);
            }
            else
                return null;

        }

        public new bool Save()
        {
            //Cause of inheritance we call the base class firstly 


            //check that the Mode of the base class is the same of the child class

            base.Mode = (clsApplications.eMode) this.Mode;

            //Save New/Update data to the base class firstly and if not save return false

            if(!base.Save())
                return false;

            switch(this.Mode)
            {

                case eMode.eAddNew:
                    if (AddNew())
                    {
                        this.Mode = eMode.eUpdate;
                        return true;
                    }
                      
                    break;
                case eMode.eUpdate: 
                    if(_Update())
                    {
                        return true;
                    }
                    break;
                    
            }
            return false;   
        }

        public new bool Delete()
        {
            //Firstly we delete the local application 

            if(!clsAccessLocalDrivingLicenseApplications.DeleteLocalaApplication(this.ApplicationID))
            {
                return false;
            }

            //delete the Application from base class 

            return base.Delete(); 
        }

        public bool DoesPassTest(clsTestTypes.eTestTypes TestType)
        {
            return DoesPassTest(this.LocalDrivingLicenseApplicationID, TestType); 
        }
        
        static public bool DoesPassTest(int LocalDrivingLicenseApplication,clsTestTypes.eTestTypes TestTypes)
        {
            return clsAccessLocalDrivingLicenseApplications.CheckPersonPassTest(LocalDrivingLicenseApplication,(byte) TestTypes);
        }

        public bool DoesPassPrevsiousTest(clsTestTypes.eTestTypes CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestTypes.eTestTypes.eVisionTest:
                    //The first test 
                    return true;
                case clsTestTypes.eTestTypes.eStreetTest:

                    return DoesPassTest(clsTestTypes.eTestTypes.eWrittenTes); 
                case clsTestTypes.eTestTypes.eWrittenTes:
                    return DoesPassTest(clsTestTypes.eTestTypes.eVisionTest); 
                   
                default: return false;

            }
        }

        public bool DoesAttendTest(clsTestTypes.eTestTypes TestType)
        {
            return clsAccessLocalDrivingLicenseApplications.CheckAttendTest(this.LocalDrivingLicenseApplicationID,(byte) TestType);
        }

        static bool DoesAttendTest(int LocalDrivingLicenseApplication,clsTestTypes.eTestTypes TestTypeID)
        {
            return clsAccessLocalDrivingLicenseApplications.CheckAttendTest(LocalDrivingLicenseApplication, (byte)TestTypeID);
        }

        static public byte TotalTrilaPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.eTestTypes TestTypeID)
        {
            return clsAccessLocalDrivingLicenseApplications.GetTotalTrialPerTest(LocalDrivingLicenseApplicationID,(byte) TestTypeID);
        }

        public byte TotalTrilaPerTest(clsTestTypes.eTestTypes TestTypeID)
        {
            return TotalTrilaPerTest(this.LocalDrivingLicenseApplicationID, TestTypeID); 
        }

        static public bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.eTestTypes TestTypeID)
        {
            return clsAccessLocalDrivingLicenseApplications.GetTotalTrialPerTest(LocalDrivingLicenseApplicationID, (byte)TestTypeID)>0;
        }

        public bool AttendedTest(clsTestTypes.eTestTypes TestTypeID)
        {
            return AttendedTest(this.LocalDrivingLicenseApplicationID , TestTypeID);
        }

         static public bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplication,clsTestTypes.eTestTypes TestTypeID)
        {
            return clsAccessLocalDrivingLicenseApplications.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplication,(byte) TestTypeID);

        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.eTestTypes TestTypeID)
        {
            return IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, TestTypeID); 
        }

        public clsTests GetTheLastTestPerTestType(clsTestTypes.eTestTypes TestTypeID)
        {
            return clsTests.FindLastTestPerPersonIDAndLicenseClassID(this.PersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        static public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTests.PassAllTest(this.LocalDrivingLicenseApplicationID);
        }
        static public bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.PassAllTest(LocalDrivingLicenseApplicationID);
        }


        public int GetActiveLicenseID()
        {
            return clsLicense.GetActivateLicenseIDByPersonID(this.PersonID, this.LicenseClassID); 
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }


    }
}
