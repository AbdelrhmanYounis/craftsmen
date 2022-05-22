using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;

        private string[] ImageExtentions = new string[] { "gif", "jpg", "png", "jpeg", "jfif" };
        public PhotoService(DataContext context)
        {
            _context = context;

        }

        public async Task<string> UploadPhoto(IFormFile file, string username)
        {
            if (file == null || file.Length == 0)
                return ("Please select profile picture");

            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
            if (ImageExtentions.Contains(extention.ToLower()))
            {
               var filePath=getFilePath();

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var date = DateTime.Now;
                string uniqueID = String.Format(
                                "{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                                date.Year, date.Month, date.Day,
                                date.Hour, date.Minute, date.Second, date.Millisecond
                                );

                var uniqueFileName = username + uniqueID + "." + file.FileName.Split('.').LastOrDefault();

                using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return $"Data/{uniqueFileName}";
            }

            return null;
        }
         public bool DeletePhoto(string fileName)
        { 
            var filePath=Path.Combine(getFilePath(),fileName.Substring(5)) ;
            if (File.Exists(filePath))
                { 
                    System.IO.File.Delete(filePath);
                    return true;
                }
              
                   
            return false;
        }
       private string getFilePath(){
            var folderName = Path.Combine("src","assets","Data");
                var filePath = Path.Combine(Directory.GetCurrentDirectory().Replace("API","client"), folderName);
                return filePath;
       }
    }

}