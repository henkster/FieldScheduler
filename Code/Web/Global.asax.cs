using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Web.Controllers;
using Web.Models;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MapModels();
        }

        private void MapModels()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<User, UserEditViewModel>();
            Mapper.CreateMap<UserEditViewModel, User>();
            Mapper.CreateMap<Field, FieldViewModel>();
            Mapper.CreateMap<Slot, SlotViewModel>();
            Mapper.CreateMap<Setting, SettingEditViewModel>();
            Mapper.CreateMap<SettingEditViewModel, Setting>();
            Mapper.CreateMap<Game, GameViewModel>();
        }
    }
}