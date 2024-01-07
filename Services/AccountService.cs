using DecaBlogMVC.Models;
using System.Security.Claims;
using DecaBlogMVC.Services.Interfaces;

namespace DecaBlogMVC.Services;

public class AccountService : BaseService
{
    public AccountService(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(
        client, httpContextAccessor, config)
    {
    }
    
    public async Task<AccountDetailsViewModel> AccountsDetailsAsync(string id)
    {
        var viewModel = new AccountDetailsViewModel();

        return viewModel;
    }
}