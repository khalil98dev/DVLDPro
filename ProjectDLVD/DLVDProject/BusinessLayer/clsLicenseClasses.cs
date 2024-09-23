using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicenseClasses
    {
        public enum eMode { eAddNew=0, eUpdate=1} 
        public int ClassID { get; set; }
        public string ClassName {  get; set; }  
        public string ClassDescription { get; set; }    
        public byte MinimumAllowedAge { get; set; }    
        public byte DefaultValidityLenghth { get; set; }   
        public float ClassFees { get; set; }
        public eMode Mode { get; set; } 
        public clsLicenseClasses()
        {
            ClassID = -1;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = 18; 
            DefaultValidityLenghth = 10;
            ClassFees = -1;
            Mode = eMode.eAddNew;   
        }

        public clsLicenseClasses(int classID, string className, 
            string classDescription, byte minimumAllowedAge,
            byte defaultValidityLenghth, float classFees)
        {
            ClassID = classID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLenghth = defaultValidityLenghth;
            ClassFees = classFees;
            Mode = eMode.eUpdate;
        }

        static public DataTable GetAllClasses()

        {
            return clsAccessLicenseClassess.GetLicensesClasses();
           
        }    

        static public clsLicenseClasses Find(int ClassID)
        {
        
           string ClassName = "";
           string ClassDescription = "";
           byte MinimumAllowedAge = 18;
           byte DefaultValidityLenghth = 10;
           float ClassFees = -1;

            if (clsAccessLicenseClassess.GetLicenseClassinfoByID(ClassID, ref ClassName, ref ClassDescription,
                                 ref MinimumAllowedAge,ref DefaultValidityLenghth, ref ClassFees))
            {
                return new clsLicenseClasses(ClassID, ClassName, ClassDescription, MinimumAllowedAge,
                                              DefaultValidityLenghth, ClassFees);
            }
            else
                return null;


        }

        static public clsLicenseClasses Find(string LicenseClassName)
        {

            int LicenseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 18;
            byte DefaultValidityLenghth = 10;
            float ClassFees = -1;

            if (clsAccessLicenseClassess.GetLicenseClassinfoByClassName(LicenseClassName, ref LicenseClassID, ref ClassDescription,
                                 ref MinimumAllowedAge, ref DefaultValidityLenghth, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, LicenseClassName, ClassDescription, MinimumAllowedAge,
                                              DefaultValidityLenghth, ClassFees);
            }
            else
                return null;


        }
        bool _AddNewLicense()
        {
            return (this.ClassID =  clsAccessLicenseClassess.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                                                    this.MinimumAllowedAge, this.DefaultValidityLenghth, this.ClassFees))!=-1;
        }

        bool _UpdateLicense()
        {
            return clsAccessLicenseClassess.UpdateLicenseClassinfo(this.ClassID, this.ClassName, this.ClassDescription,
                                                    this.MinimumAllowedAge, this.DefaultValidityLenghth, this.ClassFees);
        }

        public bool Save()
        {
            switch(Mode)
            {

                case eMode.eUpdate:
                    return _UpdateLicense();
                case eMode.eAddNew: 
                    if(_AddNewLicense())
                    {
                        Mode = eMode.eUpdate;
                        return true;
                    }
                    break;
                     
            }

            return false;
        }

    }
}
