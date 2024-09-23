using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer
{
    public class clsBasicAppilcationInfo
    {
        public int ApplicationID { get; set; }  
        public string Status { get; set; }
        public double Fees { get; set; }
        public string ApplicationType { get; set; }

        public string ApplicantName { get; set; }   
        public DateTime Date{ get; set; }
        public DateTime StatusDate { get; set; }
        public string CreatedByUserName { get; set; }


        public clsBasicAppilcationInfo()
        {
            ApplicationID = -1;
            Status = string.Empty;
            Fees = 0;
            ApplicationType = string.Empty;
            ApplicantName = string.Empty;
            Date = DateTime.Now;
            StatusDate = DateTime.Now;
            CreatedByUserName = string.Empty;

        }
        public clsBasicAppilcationInfo(int applicationID, string status, double fees, 
            string applicationType, string applicantName, DateTime date, DateTime statusDate,string createdByUserName)
        {
            ApplicationID = applicationID;
            Status = status;
            Fees = fees;
            ApplicationType = applicationType;
            ApplicantName = applicantName;
            Date = date;
            StatusDate = statusDate;
            CreatedByUserName = createdByUserName;
        }

        static public clsBasicAppilcationInfo Find(int ApplicationID)
        {

           string Status = string.Empty;
           double Fees = 0;
           string ApplicationType = string.Empty;
           string ApplicantName = string.Empty;
           DateTime Date = DateTime.Now;
           DateTime StatusDate = DateTime.Now;
           string CreatedByUserName = string.Empty;
            if (clsAccessBasicApplicationInfos.GetApplicationInfo(ApplicationID, ref Status, ref Fees, ref ApplicationType,
                ref ApplicantName, ref Date, ref StatusDate, ref CreatedByUserName))
            {
                return new clsBasicAppilcationInfo(ApplicationID, Status, Fees, ApplicationType,
                    ApplicantName, Date, StatusDate, CreatedByUserName);
            }
            else
                return null; 
        }

    }
}
