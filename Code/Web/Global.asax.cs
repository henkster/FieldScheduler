﻿using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Web.Models;
using Web.Models.AccountModels;

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
                "Game Activity",
                "Game/Schedule",
                new { controller = "Game", action = "Activity" });

            routes.MapRoute(
                "Game Size",
                "Game/Schedule/{activity}",
                new { controller = "Game", action = "Size" },
                new { activity = "^friendly|training|state-league$"});

            routes.MapRoute(
                "Game Date",
                "Game/Schedule/{activity}/{size}",
                new {controller = "Game", action = "Date"},
                new {activity = "^friendly|training|state-league$", size = "^11v11|8v8|6v6$" });

            routes.MapRoute(
                "Game Slot",
                "Game/Schedule/{activity}/{size}/{date}",
                new {controller = "Game", action = "Slot"},
                new {activity = "^friendly|training|state-league$", size = "^11v11|8v8|6v6$", date = @"\d{8}"});

            routes.MapRoute(
                "Game Slot Select",
                "Game/Schedule/{activity}/{size}/{date}/{slotId}",
                new { controller = "Game", action = "Select" },
                new { activity = "^friendly|training|state-league$", size = "^11v11|8v8|6v6$", date = @"\d{8}", slotId = @"\d+" });

            //routes.MapRoute(
            //    "Create Team",
            //    "ExternalTeam/Create/{returnTo}",
            //    new { controller = "ExternalTeam", action = "Create", returnTo = UrlParameter.Optional });

            //routes.MapRoute(
            //    "Create Club",
            //    "Club/Create/{returnTo}",
            //    new { controller = "Club", action = "Create", returnTo = UrlParameter.Optional });

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
            Mapper.CreateMap<User, AccountEditViewModel>();
            Mapper.CreateMap<AccountEditViewModel, User>();
        }
    }
}