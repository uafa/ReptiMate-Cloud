using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class AccountServiceRest : IAccountServiceRest
{
    private IRestAccountDAO restAccountDao;

    public AccountServiceRest(IRestAccountDAO restAccountDao)
    {
        this.restAccountDao = restAccountDao;
    }
    public async Task<Account?> GetAccountAsync(string email)
    {
        return await restAccountDao.GetAccountAsync(email);
    }

    public async Task RegisterAccountAsync(Account account)
    { 
        await restAccountDao.RegisterAccountAsync(account);
    }
}