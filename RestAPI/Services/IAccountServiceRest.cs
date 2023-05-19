using Model;

namespace ReptiMate_Cloud.Services;

public interface IAccountServiceRest
{
    Task<Account?> GetAccountAsync(string email);
    Task RegisterAccountAsync(Account account);
}