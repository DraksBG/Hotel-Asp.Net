namespace Hotel.Services.Data.Picture
{
    using System.Threading.Tasks;

    public interface IPictureService
    {
        Task<string> AddPictureAsync(string url);
    }
}
