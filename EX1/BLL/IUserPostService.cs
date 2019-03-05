using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DbServices
{
    public interface IUserPostService
    {
        Task<int> Save(UserPost post);
        Task<UserPost> Get(int id);
        Task<bool> Update(UserPost post);
        Task<bool> Delete(int id);
        Task<bool> Exists(UserPost post);
        Task<IEnumerable<UserPost>> All();
        Task<IEnumerable<UserPost>> GetUserPosts(string userId);
    }
}
