using PMS.BAL.Interface;
using PMS.DAL.Repository;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BAL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<String> AuthenticateUser(UserData model)
        {
            return _userRepository.AuthenticateUser(model);
        }

        public List<string> CreateUser(UserData model)
        {
            return _userRepository.CreateUser(model);
        }
    }
}
