using System.Linq.Expressions;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User? GetUser(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.Get(predicate);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.List();
        }

        public IEnumerable<User> GetUsersWhere(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.List(predicate);
        }
    }
}