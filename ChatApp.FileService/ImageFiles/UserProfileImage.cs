
using ChatApp.Core.StorageService;
using ChatApp.Service.Exceptions;
using Microsoft.AspNetCore.Http;


namespace ChatApp.StorageService.ImageFiles
{
    public class UserProfileImage : IUserProfileImage
    {
        public Stream? GetImage(string PathName)
        {
            var imagePath = Path.Combine(@"..\ChatApp.Api\wwwroot\UserProfileImage", PathName);

            if (System.IO.File.Exists(imagePath))
                return System.IO.File.OpenRead(imagePath);

            else
                return null;

        }
        public async Task<string> UploadImageAsync(IFormFile Image)
        {
            return await SaveImageAsync(Image);
        }

        public async Task<string> UpdateImageAsync(IFormFile NewImage, string ImagePathName)
        {
            RemoveImageAsync(ImagePathName);
            return await SaveImageAsync(NewImage);
        }

        public bool RemoveImageAsync(string imagePathName)
        {
            string filePath = Path.Combine(@"..\ChatApp.Api\wwwroot\UserProfileImage", imagePathName);
            if (System.IO.File.Exists(filePath))//dosya varmı diye kontrol ediyor
            {
                System.IO.File.Delete(filePath);//varsa siliyor
                return true;
            }
            else
                return false;
        }



        private async Task<string> SaveImageAsync(IFormFile Image)
        {
            if (!Image.ContentType.StartsWith("image/"))
            { throw new ClientSideException("Sadece resim dosyası gönderilebilir."); }

            if (!(Image.Length > 0 && Image.Length <= 5242880))
            { throw new ClientSideException("Resim boyutu maksimum 5mb olabilir."); }


            //dosya uzantısı çekiyoruz
            var extent = Path.GetExtension(Image.FileName);
            //yeni dosya adını uzantısıyla birlikte veriyoruz.
            var randomName = $"{Guid.NewGuid()}{extent}";
            
            var path = Path.Combine(@"..\ChatApp.Api\wwwroot\UserProfileImage", randomName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            return randomName;

        }


    }
}
