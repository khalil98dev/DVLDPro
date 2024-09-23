using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDrivingLicenseApplicationInfos
    {

        public int DrivinfLicenseApplicationID { get; set; }    
        public string LicenseClass { get; set; }    
        public short PassedTests {  get; set; }

        public clsDrivingLicenseApplicationInfos(int drivinfLicenseApplicationID, string licenseClass, short passedTests)
        {
            DrivinfLicenseApplicationID = drivinfLicenseApplicationID;
            LicenseClass = licenseClass;
            PassedTests = passedTests;
        }

        public clsDrivingLicenseApplicationInfos() {
            this.DrivinfLicenseApplicationID = -1;
            this.LicenseClass = string.Empty;    
            this.PassedTests = -1;    


        }

        static public clsDrivingLicenseApplicationInfos Find(int DLAppID)
        {
            string LicenseClass = string.Empty;
            short PassedTests = -1;

            if (clsAccessDrivingLicenseApplication.ReturnLicenseApplication(DLAppID, ref LicenseClass, ref PassedTests))
            {

                return new clsDrivingLicenseApplicationInfos(DLAppID, LicenseClass, PassedTests);

            }
            else
                return null; 
        }


    }
}
