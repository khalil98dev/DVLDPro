using System;
using System.IO;
using System.Windows.Forms;

namespace PresentationLayer.Global_Classes
{
    static class clsUtil

    {

        static public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        static public bool CreateFolderDoesNotExist(string FolderPath)
        {

            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Create Folser" + ex.Message);
                }
                return false;

            }
            return true;
        }

        static public string ReplaceFileNameWithGuid(string FileSource)
        {
            // Get File Extenstion by Create new file infromration with FileInfo class
            return GenerateGuid() + new FileInfo(FileSource).Extension;
        }

        static public bool CopyImageToProjectImagesFolder(ref string FileSource)
        {
            if (FileSource == null)
                return false;
            if (clsProjectSettings.ProjectImagesFolder == null)
            {
                string DistinationFolder = "C:\\DVLD-People-ImagesTest";
                clsProjectSettings.ProjectImagesFolder = DistinationFolder;
            }
            string FileFolderDistination = clsProjectSettings.ProjectImagesFolder;
            string FileDistination = FileFolderDistination + "\\" + ReplaceFileNameWithGuid(FileSource);

            if (!CreateFolderDoesNotExist(clsProjectSettings.ProjectImagesFolder))
            {
                return false;
            }

            try
            {
                File.Copy(FileSource, FileDistination, true);


            }
            catch (IOException IOEx)
            {
                MessageBox.Show("Error" + IOEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FileSource = FileDistination;
            return true;
        }


    }
}
