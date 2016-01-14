using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMonkeys
{
    public interface IFileHelper
    {
        string FilePath { get; }
        string CopyFileToAppDirectory(string itemId, string filePath);
        string GetLocalFilePath(string itemId, string fileName);
        void DeleteLocalFile(Microsoft.WindowsAzure.MobileServices.Files.MobileServiceFile file);
    }
}
