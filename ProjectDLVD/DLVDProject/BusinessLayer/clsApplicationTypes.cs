using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsApplicaionTypes
    {
       public int ID { get; set; }  
        public string Title {  get; set; }  
        public double Fees { get; set; }


      public clsApplicaionTypes (int ID, string Title, double Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        static public DataTable GetApplicationTypes()
        {
            return clsAccessApplicationTypes.GetApplicationtypes();
        }



        public bool Update()
        {
            return clsAccessApplicationTypes.Update(this.ID, this.Title, this.Fees);   
        }

        static public clsApplicaionTypes Find(int AppTypeID)
        {
            
            string Title = "";
            double Fess = 0;
            if (clsAccessApplicationTypes.Find(AppTypeID, ref Title,ref Fess))
            {
                return new clsApplicaionTypes(AppTypeID, Title, Fess);

            }
            else
                return null;


        }

    }
}
