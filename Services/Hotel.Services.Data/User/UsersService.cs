namespace Hotel.Services.Data.User
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(IDeletableEntityRepository<ApplicationUser> context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<string> GetUserIdAsync(ClaimsPrincipal claims)
        {
            ApplicationUser uID = await userManager.GetUserAsync(claims);

            return uID.Id;
        }

        public async Task<string> GetUserPhoneNumberAsync(ClaimsPrincipal claims)
        {
            ApplicationUser uID = await userManager.GetUserAsync(claims);

            return uID.PhoneNumber;
        }
    }
}
