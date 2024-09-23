using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.clsUsers;

namespace BusinessLayer
{
    public class clsTestTypes
    {
        public enum eTestTypes { eVisionTest=1,eWrittenTes=2,eStreetTest=3}
        public enum eMode { eAddnew=1,eUpdate=2} 
        public eMode Mode { get; set; } 
        public eTestTypes TestTypeID { get; set; }  
       public string TestTitle { get; set; }  
        public string TestDescription { get; set; }
        public float TestFees { get; set; }

        public clsTestTypes(eTestTypes TestTypeID, string testTitle, string testDescription, float testFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTitle = testTitle;
            this.TestDescription = testDescription;
            this.TestFees = testFees;
        }

        public clsTestTypes() {

            this.TestTypeID = eTestTypes.eVisionTest;
            this.TestTitle = string.Empty;
            this.TestDescription = string.Empty;
            this.TestFees = -1;


        }
         static public clsTestTypes Find(eTestTypes TestTypeID)
        {
            string TestTitle = "";
            string TestDesciption = ""; 
            float TestFees = 0;

            if (clsAccessTestType.Find((byte)TestTypeID, ref TestTitle, ref TestDesciption, ref TestFees))
            {
                return new clsTestTypes(TestTypeID, TestTitle, TestDesciption, TestFees);
            }
            else
                return null; 

        }

        private bool Update()
        {
            return clsAccessTestType.Update((byte)this.TestTypeID, this.TestTitle, this.TestDescription, this.TestFees);

        }

        static public DataTable GetTestTypes()
        {
            return clsAccessTestType.GetTestTypes();
        }



        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.TestTypeID = (clsTestTypes.eTestTypes) clsAccessTestType.AddNewTestType(this.TestTitle, this.TestDescription, this.TestFees);

            return (byte)(this.TestTypeID) != 0;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case eMode.eAddnew:
                    if (_AddNewTestType())
                    {

                        Mode = eMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eMode.eUpdate:

                    return Update();

            }

            return false;
        }

    }
}
