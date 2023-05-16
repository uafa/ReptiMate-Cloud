using Model;

namespace ReptiMate_Cloud.Services;

public interface IAccountService
{
    Task<Account?> GetAccountAsync(string email);
    Task RegisterAccountAsync(Account account);
}