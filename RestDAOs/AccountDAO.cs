using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace RestDAOs;

public class AccountDAO : IAccountDAO
{
    private readonly DatabaseContext context;

    public AccountDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<Account?> GetAccountAsync(string email)
    {
        var account = await context.Accounts.Where(account => account.Email == email).FirstOrDefaultAsync();

        return account;
    }

    public async Task RegisterAccountAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        var response = await context.SaveChangesAsync();

        if (response == 0)
        {
            throw new Exception("Account not created!!!!");
        }
    }
}