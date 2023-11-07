namespace ProductCatalog.Domain.Account;
public interface IAuthenticate
{
    Task<bool> AuthenticateAsunc(string email, string password);
    Task<bool> RegisterUserAsync(string email, string password);
    Task LogoutAsync();
}
