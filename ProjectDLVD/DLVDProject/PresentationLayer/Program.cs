using PresentationLayer.Application.LocalDrivingLicenseApplication;
using PresentationLayer.Applications.LocalDrivingLicenseApplication;
using PresentationLayer.Forms;
using System;
using System.Windows.Forms; 
namespace PresentationLayer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()

        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new frmManageLoacalDrivingLicenseApplications());

        }


    }
}
