using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication8.StaticFile
{
    public static class Helper
    {
        public static  string uploadfile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not provided or is empty.");
            }

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);
            // Create directory if it doesn't exist  
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = $"{Path.GetFileName(file.FileName)}"; 
            string filePath = Path.Combine(directoryPath, fileName);

             using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyTo(stream); // Async file copy  
            }

            return fileName;
        }
        public static void deletefile(string file, string foldername)
        {
            string path= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", foldername,file);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}