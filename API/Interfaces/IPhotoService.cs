using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        public  Task<string> UploadPhoto(IFormFile file,string username);
        public  bool DeletePhoto(string fileName);//url=fileName
        
      //  public  Task<string> UploadPhotoFromRequest(IFormFile Photo);
      //  public  Task<List<string>> UploadPhotosFromRequest(List<IFormFile> Photos);
     //   public string UploadPhoto(string file);
    }
}