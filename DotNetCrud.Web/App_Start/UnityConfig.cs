using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using DotNetCrud.Services;
using DotNetCrud.Web.Data.Models;
using DotNetCrud.Web.Data.Services.EF;
using DotNetCrud.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace DotNetCrud.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IGenericEFDataService<ProductGroup>, GenericEFDataService<ProductGroup>>(new TransientLifetimeManager());
            container.RegisterType<IGenericEFDataService<Product>, GenericEFDataService<Product>>(new TransientLifetimeManager());
            container.RegisterType<IGenericEFDataService<Purchase>, GenericEFDataService<Purchase>>(new TransientLifetimeManager());
            container.RegisterType<IGenericEFDataService<Faq>, GenericEFDataService<Faq>>(new TransientLifetimeManager());


            container.RegisterType<DbContext, ApplicationDbContext>(new TransientLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new TransientLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new TransientLifetimeManager());
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c =>
                    HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<ApplicationSignInManager>(new TransientLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}