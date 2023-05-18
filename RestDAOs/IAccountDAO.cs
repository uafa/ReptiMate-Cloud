using Model;

namespace RestDAOs;

public interface IAccountDAO
{
    Task<Account?> GetAccountAsync(string email);
    Task RegisterAccountAsync(Account account);
}