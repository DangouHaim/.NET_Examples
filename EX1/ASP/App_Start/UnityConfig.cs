using System.Web.Mvc;
using BLL.DbServices;
using BLL.HelpersAndUtils;
using BLL.YoutubeLogic;
using BLL.VKLogic;
using DAL.Entities;
using DAL.UserPostRepository;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;
using Unity.Resolution;
using Unity.Injection;
using BLL.MessagerIdentity;
using BLL.TelegramLogic;

namespace PandaWiki
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here 
            // it is NOT necessary to register your controllers 
            container.RegisterType<IAppConfiguration, AppConfiguration>(new HierarchicalLifetimeManager());
            container.RegisterType<IYoutubeService, YoutubeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserPostRepository, UserPostRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserPostService, UserPostService>(new HierarchicalLifetimeManager());
            container.RegisterType<IVkService, VkService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMessagerIdentity, TelegramIdentity>(new HierarchicalLifetimeManager());
            container.RegisterType<ITelegramService, TelegramService>(new HierarchicalLifetimeManager());
            //container.RegisterType<WcfServices.ITelegramService, WcfServices.TelegramServiceClient>(new HierarchicalLifetimeManager());


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}