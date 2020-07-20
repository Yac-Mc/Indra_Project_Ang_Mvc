using DataAccess_Indra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Indra.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        IEnumerable<UserResponseModel> GetAllUsersFilter(string userName, int respMin, int respMax);
        void SaveResponse(UserResponseModel userResponse);
        bool DeleteRegister(int idRegister);
    }
}
