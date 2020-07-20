using DataAccess_Indra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Indra.User
{
    public interface IUserBL
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        IEnumerable<UserResponseModel> GetAllUsersFilter(string userName, int respMin, int respMax);
        int SaveRegisterUser(UserRegister userRegister);
        bool DeleteRegister(int idRegister);
    }
}
