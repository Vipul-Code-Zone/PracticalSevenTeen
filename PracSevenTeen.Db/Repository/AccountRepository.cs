using Microsoft.EntityFrameworkCore;
using PracSevenTeen.Db.DatabaseContext;
using PracSevenTeen.Db.Interfaces;
using PracSevenTeen.Models.Enum;
using PracSevenTeen.Models.Models;
using PracticalSevenTeen.ViewModels;

namespace PracSevenTeen.Db.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StudentDbContext _studentDbContext;
        public AccountRepository(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task RegisterUserAsync(RegisterViewModel user)
        {
            _studentDbContext.Users.Add(ChangeViewModelToModel(user));
            await _studentDbContext.SaveChangesAsync();
            User? newUser = await _studentDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (newUser != null)
            {
                _studentDbContext.UserRoles.Add(new UserRole() { RoleId = 2, UserId = newUser.Id });
                await _studentDbContext.SaveChangesAsync();
            }

        }

        public async Task<UserLoginStatus> LoginUserAsync(LoginViewModel model)
        {
            if (model == null)
            {
                return UserLoginStatus.UserNull;
            }

            User? user = await _studentDbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return UserLoginStatus.UserNotFound;
            }

            if (user.Password != model.Password)
            {
                return UserLoginStatus.InvalidPassword;
            }
            else
            {
                return UserLoginStatus.LoginSuccess;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _studentDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<string> GetUserRoles(int id)
        {
            var roles = _studentDbContext.UserRoles
                .Where(x => x.UserId == id)
                .Join(_studentDbContext.Roles, x => x.RoleId, x => x.Id, (user, role) => new { user.UserId, role.RoleName })
                .Select(x => x.RoleName);

            return roles.ToList();
        }

        private User ChangeViewModelToModel(RegisterViewModel model)
        {
            User user = new User()
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
            };



            return user;
        }
    }
}
