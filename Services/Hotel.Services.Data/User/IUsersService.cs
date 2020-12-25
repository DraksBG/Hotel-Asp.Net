using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.Services.Data.User
{
    public interface IUsersService
    {
        Task<string> GetUserIdAsync(ClaimsPrincipal claims);

        Task<string> GetUserPhoneNumberAsync(ClaimsPrincipal claims);
    }
}
