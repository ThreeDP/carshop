using System.Threading.Tasks;
using Refit;
using CarShopView.Models;

namespace CarShopView.Repositories;

public interface IUserRepository {

    [Post("/api/auth/login")]
    Task<ApiResponse<TokenModel>> Login([Body] IUser user);
}