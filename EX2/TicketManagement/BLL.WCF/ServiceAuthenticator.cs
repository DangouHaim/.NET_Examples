using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    public class ServiceAuthenticator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            // Validate arguments
            //throw new ArgumentNullException("userName");
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            // Check the user name and password
            if (userName != "test" || password != "test")
            {
                bool result = false;
                DAL.TicketManagementContext context = new DAL.TicketManagementContext();
                var service = new ManagerServices.UserService(new EntityUserRepository(context), new EntityProcedureManager(context));
                var users = service.Find(userName);
                if(users.Any())
                {
                    var user = users.First();
                    if(user.Password == password)
                    {
                        result = true;
                    }
                }
                if(!result)
                {
                    throw new SecurityTokenException("Unknown username or password.");
                }
            }
        }
    }
}
