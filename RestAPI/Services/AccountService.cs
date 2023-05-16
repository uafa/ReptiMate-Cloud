using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class AccountService : IAccountService
{
    private IAccountDAO accountDao;

    public AccountService(IAccountDAO accountDao)
    {
        this.accountDao = accountDao;
    }
    public async Task<Account?> GetAccountAsync(string email)
    {
        return await accountDao.GetAccountAsync(email);
    }

    public async Task RegisterAccountAsync(Account account)
    { 
        await accountDao.RegisterAccountAsync(account);
    }
}