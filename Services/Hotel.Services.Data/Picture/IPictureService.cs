using System.Threading.Tasks;

namespace Hotel.Services.Data.Picture
{
    public interface IPictureService
    {
        Task<string> AddPictureAsync(string url);
    }
}
