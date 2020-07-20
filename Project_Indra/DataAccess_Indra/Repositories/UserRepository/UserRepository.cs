using DataAccess_Indra.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Indra.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public bool DeleteRegister(int idRegister)
        {
            try
            {
                using (YacEntities context = new YacEntities())
                {
                    var register = new UserResponse { Id = idRegister };
                    context.UserResponse.Attach(register);
                    context.UserResponse.Remove(register);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            try
            {
                using (YacEntities context = new YacEntities())
                {
                    List<User> lstUser = await context.User.AsNoTracking().ToListAsync();
                    List<UserModel> lstUserModel = new List<UserModel>();
                    foreach (var item in lstUser)
                    {
                        UserModel UserModel = new UserModel()
                        {
                            Id = item.Id,
                            UserName = item.UserName
                        };
                        lstUserModel.Add(UserModel);
                    }

                    return lstUserModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<UserResponseModel> GetAllUsersFilter(string userName, int respMin, int respMax)
        {
            try
            {
                List<UserResponseModel> resultQuery = new List<UserResponseModel>();
                using (YacEntities context = new YacEntities())
                {
                    if (string.IsNullOrEmpty(userName) && respMin == 0 && respMax == 0)
                    {
                        resultQuery = (from ur in context.UserResponse
                                       join u in context.User on ur.IdUser equals u.Id
                                       select new UserResponseModel
                                       {
                                           Id = ur.Id,
                                           IdUser = ur.IdUser,
                                           UserName = u.UserName,
                                           Response = ur.Response,
                                           DateResponse = ur.DateResponse
                                       }).ToList();
                    }
                    else if (!string.IsNullOrEmpty(userName) && respMin == 0 && respMax == 0)
                    {
                        resultQuery = (from ur in context.UserResponse
                                       join u in context.User on ur.IdUser equals u.Id
                                       where u.UserName == userName
                                       select new UserResponseModel
                                       {
                                           Id = ur.Id,
                                           IdUser = ur.IdUser,
                                           UserName = u.UserName,
                                           Response = ur.Response,
                                           DateResponse = ur.DateResponse
                                       }).ToList();
                    }
                    else if (string.IsNullOrEmpty(userName) && (respMin > 0 || respMax > 0))
                    {
                        resultQuery = (from ur in context.UserResponse
                                     join u in context.User on ur.IdUser equals u.Id
                                     where (ur.Response >= respMin && ur.Response <= respMax)
                                     select new UserResponseModel
                                     {
                                         Id = ur.Id,
                                         IdUser = ur.IdUser,
                                         UserName = u.UserName,
                                         Response = ur.Response,
                                         DateResponse = ur.DateResponse
                                     }).ToList();
                    }
                    else if(!string.IsNullOrEmpty(userName) && (respMin > 0 || respMax > 0))
                    {
                        resultQuery = (from ur in context.UserResponse
                                       join u in context.User on ur.IdUser equals u.Id
                                       where u.UserName == userName && (ur.Response >= respMin && ur.Response <= respMax)
                                       select new UserResponseModel
                                       {
                                           Id = ur.Id,
                                           IdUser = ur.IdUser,
                                           UserName = u.UserName,
                                           Response = ur.Response,
                                           DateResponse = ur.DateResponse
                                       }).ToList();
                    }

                }
                return resultQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveResponse(UserResponseModel userResponse)
        {
            try
            {
                using (YacEntities context = new YacEntities())
                {
                    var dbUserResponse = context.Set<UserResponse>();
                    dbUserResponse.Add(new UserResponse { IdUser = userResponse.IdUser, DateResponse = userResponse.DateResponse, Response = userResponse.Response});

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
