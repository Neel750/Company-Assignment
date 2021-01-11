using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public interface IUserRepository
    {
        List<string> AuthenticateUser(UserData model);
        List<string> CreateUser(UserData model);
    }
}
