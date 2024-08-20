using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo3MVC.PL.Helpers
{
    public static class DocumentSettings
    {
        //Upload Function
        public static string UploadFile(IFormFile File , string FolderName)
        {
            //The Steps To Create Upload file 
            // 1. Get Located Folder Path
            // but not all users have the same path
            //"D:\front end developer\Route course FullStack\Back End\Assignments\MVC\MVC Project\Demo3 MVC Solution\Demo3MVC.PL\wwwroot\Files\Images\"
            //Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + FolderName
            
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            
            // 2. Get File Name and Make it Unique
            // Why unique because i can have many files by the same name but not the same content
            
            //string FileName = $"{Guid.NewGuid()}{File.FileName}"; //=> This with FileMode.Create
            string FileName = $"{Guid.NewGuid()}{File.FileName}"; //=> This with FileMode.CreateNew

            // 3. Get File Path [Folder Path + FileName]
            string FilePath = Path.Combine(FolderPath, FileName);
            // 4. Save Files As Streams
            using var FileStream = new FileStream(FilePath,FileMode.Create);
            File.CopyTo(FileStream);
            // 5. Return File Name
            return FileName;

        }

        //Delete Function
        public static void DeleteFile(string FileName, string FolderName)
        {
            // 1. Get file Path

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
            // 2. Check If File Exist Or Not
            if(File.Exists(FilePath))
            {
                // if Exist Remove It
                File.Delete(FilePath);
            }
        }
    }
}
