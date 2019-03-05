using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    public class UserService : IUserService
    {
        private readonly BLL.IServices.IUserService _service;

        public UserService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.UserService(new EntityUserRepository(context), new EntityProcedureManager(context));
        }

        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        public User Get(int id)
        {
            return User.FromEntity(_service.Get(id));
        }

        public IEnumerable<User> GetAll()
        {
            List<User> l = new List<User>();
            foreach (var user in _service.GetAll())
            {
                l.Add(User.FromEntity(user));
            }
            return l;
        }

        public int Save(User user)
        {
            return _service.Save(user.ToEntity());
        }

        public bool Update(User user)
        {
            return _service.Update(user.ToEntity());
        }

        public IEnumerable<User> Find(string emailOrLogin)
        {
            List<User> l = new List<User>();
            foreach (var user in _service.Find(emailOrLogin))
            {
                l.Add(User.FromEntity(user));
            }
            return l;
        }

        public IEnumerable<GetUserCart_Result3> GetCart(int uid)
        {
            List<GetUserCart_Result3> l = new List<GetUserCart_Result3>();
            foreach (var r in _service.GetCart(uid))
            {
                l.Add(GetUserCart_Result3.FromEntity(r));
            }
            return l;
        }
    }
}
