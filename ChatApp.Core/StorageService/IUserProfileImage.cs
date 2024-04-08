using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.StorageService
{
    public interface IUserProfileImage
    {
        Task<string> UploadImageAsync(IFormFile Image);


        Task<string> UpdateImageAsync(IFormFile NewImage, string imageId);

        Stream? GetImage(string PathName);

        bool RemoveImageAsync(string ImagePath);
    }
}
