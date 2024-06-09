using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            return users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
