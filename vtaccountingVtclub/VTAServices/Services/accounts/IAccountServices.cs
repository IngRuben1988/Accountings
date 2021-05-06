using System.Collections.Generic;
using System.Security.Claims;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.accounts.model;

namespace VTAworldpass.VTAServices.Services.accounts
{
    public interface IAccountServices
    {
        SessionModel AccountData(LoginViewModel model);
        bool AccountVerifier(LoginViewModel model);
        List<int> AccountCompanies();
        List<int> AccountLeves();
        List<string> AccountPermissions();
        List<string> AccountData();
        int AccountProfile();
        int AccountIdentity();
        string getUserName();
        string AccountToken();
        bool isOpertive();
        bool isInPermission(string permission);
    }
}
