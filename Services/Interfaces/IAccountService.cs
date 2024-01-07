using System.Security.Claims;
using DecaBlogMVC.Models;

namespace DecaBlogMVC.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<GetAllAccountViewModel>> GetAllAccountsAsync();
    Task<AccountDetailsViewModel> AccountsDetailsAsync(string id);
    bool IsLoggedInAsync(ClaimsPrincipal user);
}