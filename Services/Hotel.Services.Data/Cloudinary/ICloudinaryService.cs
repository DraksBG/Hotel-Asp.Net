using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hotel.Services.Data.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<string> UploadPhotoAsync(IFormFile picture, string name, string folderName);
    }
}
