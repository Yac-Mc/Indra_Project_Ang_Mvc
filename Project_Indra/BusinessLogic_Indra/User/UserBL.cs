using DataAccess_Indra.Model;
using DataAccess_Indra.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Indra.User
{
    public class UserBL : IUserBL
    {

        private readonly IUserRepository _iUserRepository;
        public UserBL(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }

        public bool DeleteRegister(int idRegister)
        {
            return _iUserRepository.DeleteRegister(idRegister);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _iUserRepository.GetAllUsers();
        }

        public IEnumerable<UserResponseModel> GetAllUsersFilter(string userName, int respMin, int respMax)
        {
            return _iUserRepository.GetAllUsersFilter(userName, respMin, respMax);
        }

        public int SaveRegisterUser(UserRegister userRegister)
        {
            int result = CalculateResult(userRegister.Limit);

            UserResponseModel userResponse = new UserResponseModel()
            {
                IdUser = userRegister.IdUser,
                Response = result,
                DateResponse = DateTime.Now
            };

            _iUserRepository.SaveResponse(userResponse);

            return result;
        }

        private int CalculateResult(int limit)
        {
            List<int> lstMult3 = new List<int>();
            List<int> lstMult5 = new List<int>();

            for (int i = 0; i < limit; i++)
            {
                if (i % 3 == 0)
                {
                    lstMult3.Add(i);
                }
                if (i % 5 == 0)
                {
                    lstMult5.Add(i);
                }
            }

            return (lstMult3.Sum(x => x) + lstMult5.Sum(x => x));
        }
    }
}
