namespace Hotel.Services.Data.User
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<string> GetUserIdAsync(ClaimsPrincipal claims);

        Task<string> GetUserPhoneNumberAsync(ClaimsPrincipal claims);
    }
}
