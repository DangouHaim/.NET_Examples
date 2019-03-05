using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using DAL.Entities;
using DAL.UserPostRepository;

namespace BLL.DbServices
{
    public class UserPostService : IUserPostService
    {
        private readonly IUserPostRepository _repository;

        public UserPostService(IUserPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserPost>> All()
        {
            var res = await _repository.All();
            return await Task.Run(() => res?.ToList());
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<UserPost> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<UserPost>> GetUserPosts(string userId)
        {
            var res = from x in await _repository.All()
                      where x.UserId == userId
                      select x;
            return await Task.Run(() => res?.ToList());
        }

        public async Task<int> Save(UserPost post)
        {
            var r = from x in await
                    _repository.All()
                    where x.PostId == post.PostId &&
                    x.UserId == post.UserId &&
                    x.PostType == post.PostType
                    select x;
            if(r.Any())
            {
                throw new InsertException("The such post is already exists.");
            }
            return await _repository.Save(post);
        }

        public async Task<bool> Update(UserPost post)
        {
            return await _repository.Update(post);
        }

        public async Task<bool> Exists(UserPost post)
        {
            return (from x in await _repository.All()
                    where x.PostId == post.PostId 
                    && x.UserId == post.UserId
                    select x).Any();
        }
    }
}
