namespace InventarioSaaS.Web.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; } = "";

    public required DateTime Expiracion { get; set; }

    public required string Role { get; set; }
}