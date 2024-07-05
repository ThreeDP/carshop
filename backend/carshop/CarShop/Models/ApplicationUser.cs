using Microsoft.AspNetCore.Identity;

namespace CarShop.Models;

public class ApplicationUser : IdentityUser {
    public string? RefreshToken { get; set; }
    private DateTime _rtet = DateTime.Now;
    public DateTime RefreshTokenExpiryTime { 
        get {
            return _rtet;
        } 
        set { value.ToUniversalTime(); }
    }
}