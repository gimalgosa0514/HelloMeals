using mealplan.domain.users.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan.domain.users.repository
{
    internal interface IUserRepository
    {

        bool InsertUser(User user);
        bool RemoveUser(User user);
        User SelectUserByLoginId(string loginId);
        List<User> SelectAllUser();

    }
}
