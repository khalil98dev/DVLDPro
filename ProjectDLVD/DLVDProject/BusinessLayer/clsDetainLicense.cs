using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class clsDetainLicense
    {
        public enum eMode { eAddNew=1,eUpdate=2}; 
        public eMode Mode { get; set; }    

        public int DetainID { get; set; }
        public int LicenseID { get; set; }  
        public clsLicense Licenseinfo { get; set; } 
        public DateTime DetainDate { get; set; }    
        public float FineDetain {  get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo { get; set; }   

        
        //Release detain 
        public bool isReleased { get; set; }
        public DateTime ReleaseDate {  get; set; }  
        public int ReleasedApplicationID { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUsers ReleasedByUserInfo { get; set; }

        //Constractor 
        public clsDetainLicense (int detainID, int licenseID,
            DateTime detainDate, float fineDetain, int createdByUserID, 
            bool isReleased, DateTime releaseDate, int releasedApplicationID, 
            int releasedByUserID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
      
            DetainDate = detainDate;
            FineDetain = fineDetain;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUsers.Find(CreatedByUserID); 
            this.isReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedApplicationID = releasedApplicationID;
            ReleasedByUserID = releasedByUserID;
            ReleasedByUserInfo = clsUsers.Find(ReleasedByUserID); 
            Mode = eMode.eUpdate; 

        }

        public clsDetainLicense()
        {

            this.DetainDate = DateTime.Now;  
            this.FineDetain = 0; 
            this.CreatedByUserID = -1;
            this.DetainID = -1;
            this.LicenseID = -1;

            this.isReleased = false;
            this.ReleasedByUserID = -1;
            this.ReleasedApplicationID = -1;
            this.ReleaseDate = DateTime.Now;

            Mode = eMode.eAddNew;
        }
        
       static public clsDetainLicense Find(int DetainID)
        {
            int LicenseID = -1, ReleasedByUserID = -1, ReleasedApplicationID = -1,
                CreatedByUserID=-1;

            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineDetain = -1; 
            bool IsReleased = false;
            if (clsAccessDetainData.GetDetainLicenseInformationByDetainID(DetainID, ref LicenseID,
                ref DetainDate, ref FineDetain, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedApplicationID, ref ReleasedByUserID))
            {
                return new clsDetainLicense(DetainID, LicenseID, DetainDate, FineDetain, CreatedByUserID, IsReleased,
                    ReleaseDate, ReleasedApplicationID, ReleasedByUserID);
            }
            else
                return null; 
        }
       static public clsDetainLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, ReleasedByUserID = -1, ReleasedApplicationID = -1,
                CreatedByUserID = -1;

            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineDetain = -1;
            bool IsReleased = false;
            if (clsAccessDetainData.GetDetainLicenseInformationByLicenseID(LicenseID, ref DetainID,
                ref DetainDate, ref FineDetain, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedApplicationID, ref ReleasedByUserID))
            {
                return new clsDetainLicense(DetainID, LicenseID, DetainDate, FineDetain, CreatedByUserID, IsReleased,
                    ReleaseDate, ReleasedApplicationID, ReleasedByUserID);
            }
            else
                return null;
        }


        private bool _AddNewDetain()
        {
            return (this.DetainID = clsAccessDetainData.AddNewDetain(LicenseID, DetainDate, FineDetain,
                CreatedByUserID)) != -1;
        }

        private bool _UpdateDetain()
        {
            return clsAccessDetainData.UpdateDetainLicense(DetainID, LicenseID, DetainDate, FineDetain,
                CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eUpdate:
                    return _UpdateDetain();
                case eMode.eAddNew:

                    if (_AddNewDetain())
                    {
                        Mode = eMode.eUpdate;
                        return true;
                    }
                    return false;
                default: return false;
            }
        }
      
        static public DataTable GetAllDetainLincese()
        {
            return clsAccessDetainData.GetAllDetainLicense(); 
        }

        public bool ReleaseDetain(int ReleasedByUserID, int ReleasedApplicationID)
        {
            return clsAccessDetainData.ReleaseDetainLicense(this.DetainID, ReleasedByUserID, ReleasedApplicationID); 
        }

        public static bool ReleaseDetain(int DetainID,int ReleasedByUserID,int ReleasedApplicationID)
        {
            return clsAccessDetainData.ReleaseDetainLicense(DetainID, ReleasedByUserID, ReleasedApplicationID);
        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            return clsAccessDetainData.IsLicenseDetained(LicenseID); 
        }

        public bool IsThisLicenseDetained()
        {
            return IsLicenseDetained(this.LicenseID);
        }
    }
}
