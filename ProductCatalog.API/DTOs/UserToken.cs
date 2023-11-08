namespace ProductCatalog.API.DTOs;

public class UserTokenDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
