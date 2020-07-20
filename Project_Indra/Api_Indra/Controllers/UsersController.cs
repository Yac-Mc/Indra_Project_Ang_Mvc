using BusinessLogic_Indra.User;
using DataAccess_Indra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api_Indra.Controllers
{
    [RoutePrefix("api/User")]
    public class UsersController : ApiController
    {
        private readonly IUserBL _iUserBL;
        public UsersController(IUserBL iUserBL)
        {
            _iUserBL = iUserBL;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _iUserBL.GetAllUsers();
        }

        [HttpGet]
        [Route("GetUsersFilter/{userName}/{respMin}/{respMax}")]
        public IEnumerable<UserResponseModel> GetUsersFilter(string userName, int respMin, int respMax)
        {
            return _iUserBL.GetAllUsersFilter(userName, respMin, respMax);
        }

        [HttpPost]
        [Route("SaveRegisterUser")]
        public int SaveRegisterUser(UserRegister register)
        {
            return _iUserBL.SaveRegisterUser(register);
        }

        [HttpGet]
        [Route("DeleteRegister/{idRegister}")]
        public bool DeleteRegister(int idRegister)
        {
            return _iUserBL.DeleteRegister(idRegister);
        }
    }
}
