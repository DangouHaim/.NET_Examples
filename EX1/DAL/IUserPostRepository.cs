using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserPostRepository
{
    public interface IUserPostRepository
    {
        Task<int> Save(UserPost post);
        Task<UserPost> Get(int id);
        Task<bool> Update(UserPost post);
        Task<bool> Delete(int id);
        Task<IQueryable<UserPost>> All();
    }
}
