namespace Hotel.Services.Data.Picture
{
    using System;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Common.Repositories;

    public class PictureService : IPictureService
    {
        private readonly IDeletableEntityRepository<Hotel.Data.Models.Picture> pictures;

        public PictureService(IDeletableEntityRepository<Hotel.Data.Models.Picture> pictures)
        {
            this.pictures = pictures;
        }

        public async Task<string> AddPictureAsync(string url)
        {
            var picture = new Hotel.Data.Models.Picture() { Url = url };

            await this.pictures.AddAsync(picture);
            var result = await this.pictures.SaveChangesAsync();

            if (result < 0)
            {
                throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionInPictureService);
            }
            else
            {
                return picture.Id;
            }
        }
    }
}
