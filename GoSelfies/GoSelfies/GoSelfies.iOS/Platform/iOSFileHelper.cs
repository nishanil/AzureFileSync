using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(GoSelfies.iOSFileHelper))]
namespace GoSelfies
{
    public class iOSFileHelper : IFileHelper
    {
        private readonly string filePath;
        public iOSFileHelper()
        {
            filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "../Library/GoSelfieFiles");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
        }
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        public string CopyFileToAppDirectory(string itemId, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            string targetPath = GetLocalFilePath(itemId, fileName);

            File.Copy(filePath, targetPath);

            return targetPath;
        }

        public string GetLocalFilePath(string itemId, string fileName)
        {
            string recordFilesPath = Path.Combine(filePath, itemId);

            if (!Directory.Exists(recordFilesPath))
            {
                Directory.CreateDirectory(recordFilesPath);
            }

            return Path.Combine(recordFilesPath, fileName);
        }

        public void DeleteLocalFile(Microsoft.WindowsAzure.MobileServices.Files.MobileServiceFile file)
        {
            string localPath = GetLocalFilePath(file.ParentId, file.Name);

            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
        }

    }
}
