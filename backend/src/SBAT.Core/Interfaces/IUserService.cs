using System.Linq.Expressions;
using SBAT.Core.Entities;

namespace SBAT.Core.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        User? GetUserById(int id);
        User? GetUser(Expression<Func<User, bool>> predicate);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersWhere(Expression<Func<User, bool>> predicate);
    }
}