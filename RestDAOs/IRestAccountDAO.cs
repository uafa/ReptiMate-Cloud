using Model;

namespace RestDAOs;

public interface IRestAccountDAO
{
    Task<Account?> GetAccountAsync(string email);
    Task RegisterAccountAsync(Account account);
}