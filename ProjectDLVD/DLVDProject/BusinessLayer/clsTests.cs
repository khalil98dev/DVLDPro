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
    public class clsTests

    {
        public enum eMode { eAddnew=1,eUpdate=2} 
        public eMode Mode { get; set; }
         
        public int TestID { get; set; }
        public int TestAppointID { get; set; }
        public clsTestAppointements TestAppointementsInfos { get; set; }
        public byte TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTests(int TestID, int TestAppointID, byte TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointID = TestAppointID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestAppointementsInfos = clsTestAppointements.Find(TestAppointID);

        }


        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointID = -1 ;
            this.TestResult =0;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
        }

        private bool _AddNewtest()
        {
            return (TestID = clsAccessTests.AddNewTest(TestAppointID, TestResult, Notes, CreatedByUserID)) != -1;
        }

        static public clsTests Find(int TestID)
        {
           
            int TestAppointID = -1;

            byte TestResult = 1;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            return (clsAccessTests.Find(TestID, ref TestAppointID, ref TestResult, ref Notes, ref CreatedByUserID)) ? new clsTests(TestID, TestAppointID, TestResult, Notes, CreatedByUserID) : null;

        }

        static public clsTests FindbyAppoinID(int AppointID)
        {

            int TestID = -1;

            byte TestResult =0;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            return (clsAccessTests.FindByAppointID(AppointID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID)) ? new clsTests(AppointID, TestID, TestResult, Notes, CreatedByUserID) : null;

        }
        private bool _UpdateTest()
        {
            return clsAccessTests.Update(this.TestID, this.TestAppointID, this.TestResult, this.Notes, this.CreatedByUserID);
        }
        public bool Save()
        {
           switch(Mode)
            {
                case eMode.eAddnew: 
                    if(_AddNewtest())
                    {
                        Mode = eMode.eAddnew;
                        return true;
                    }
                    break;
                case eMode.eUpdate: 
                    return _UpdateTest();   
            }
            return false;
        }

        static public clsTests FindLastTestPerPersonIDAndLicenseClassID(int PersonID,int LicenseClassID,clsTestTypes.eTestTypes TestTypeID)
        {
            int TestID = -1, TestAppointID = -1, CreatedByUserID = -1;
            byte TestResult = 0;
            string Notes = "";
            if(clsAccessTests.FindLastTestPerPersonIDAndLicenseClass(PersonID, LicenseClassID,  
                (byte)  TestTypeID,ref TestID
                ,ref TestAppointID,ref TestResult,ref Notes,ref CreatedByUserID))
            
            {
                return new clsTests(TestID, TestAppointID, TestResult, Notes, CreatedByUserID);

            
            }else 
                return null;    
            
        }

        public DataTable GetAlltests()
        {
            return clsAccessTests.GetAllTests();
        }

        static public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsAccessTests.GetPassedTests(LocalDrivingLicenseApplicationID); 
        }

        static public bool PassAllTest(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID)==3;
        }
    }
}
