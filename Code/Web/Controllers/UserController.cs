using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using Services;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AllowOnly(Roles.Admin)]
    [Authorize]
    public class UserController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(UserViewModel.LoadList(Context.Users));
        }

        public ActionResult Create()
        {
            return View(new UserEditViewModel());
        }

        [HttpPost]
        public ActionResult Create(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (Context.Users.Any(u => u.Username == vm.Username))
            {
                ModelState.AddModelError("Username", "Username already exists");
                return View(vm);
            }

            User user = Mapper.Map<UserEditViewModel, User>(vm);

            user.Roles = Roles.None;
            if (vm.Admin) user.Roles += (int)Roles.Admin;
            if (vm.Manager) user.Roles += (int)Roles.Manager;
            if (vm.Referee) user.Roles += (int)Roles.Referee;
            if (vm.Reader) user.Roles += (int)Roles.Reader;

            Context.Users.Add(user);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user = Context.Users.Find(id);

            if (user == null) return Oops<User>(id);

            var vm = UserEditViewModel.Load(user);

            vm.Admin = user.IsIn(Roles.Admin);
            vm.Manager = user.IsIn(Roles.Manager);
            vm.Referee = user.IsIn(Roles.Referee);
            vm.Reader = user.IsIn(Roles.Reader);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            User user = Context.Users.Find(vm.Id);

            Mapper.Map(vm, user);

            user.Roles = Roles.None;
            if (vm.Admin) user.Roles += (int)Roles.Admin;
            if (vm.Manager) user.Roles += (int)Roles.Manager;
            if (vm.Referee) user.Roles += (int)Roles.Referee;
            if (vm.Reader) user.Roles += (int)Roles.Reader;

            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            User user = Context.Users.Find(id);

            if (user == null) return Oops<User>(id);

            new UserManager().Delete(user);

            return RedirectToAction("Index");
        }

        private ActionResult Oops<T>(int id)
        {
            return Content(string.Format("{0} can't be found by ID {1}", typeof (T).Name, id));
        }
    }
}