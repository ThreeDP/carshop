namespace CarShopView.Services;

public interface IAuthService {
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}

public class AuthService : IAuthService {
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}