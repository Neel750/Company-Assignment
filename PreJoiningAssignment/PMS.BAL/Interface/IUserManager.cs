using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BAL.Interface
{
    public interface IUserManager
    {
        List<string> AuthenticateUser(UserData model);
        List<string> CreateUser(UserData model);
    }
}
