using mealplan.domain.users.model;
using mealplan.domain.users.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.users.service
{
    internal interface IUserService
    {
        User GetUserById(string loginId);
        List<User> GetAllUser();
        bool Login(string inputId, string inputPassword);
        bool Logout(string inputId);
        bool Withdraw();

        bool Regist(User user);
    }
}
