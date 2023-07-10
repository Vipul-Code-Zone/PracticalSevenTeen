using PracSevenTeen.Models.Enum;
using PracSevenTeen.Models.Models;
using PracticalSevenTeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracSevenTeen.Db.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        List<string> GetUserRoles(int id);
        Task<UserLoginStatus> LoginUserAsync(LoginViewModel model);
        Task RegisterUserAsync(RegisterViewModel user);
    }
}
