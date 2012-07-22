using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AdminOnly]
    public class SettingController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(Context.Settings.ToList());
        }

        public ActionResult Edit(int id)
        {
            return View(SettingEditViewModel.Load(Context.Settings.Find(id)));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SettingEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Setting setting = Context.Settings.Find(vm.Id);

            Mapper.Map(vm, setting);

            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Mode(string mode)
        {
            if (string.IsNullOrWhiteSpace(mode)) return RedirectToAction("Mode");

            Setting modeSetting = Context.Settings.SingleOrDefault(s => s.Key == "system-mode");

            if (modeSetting == null) return RedirectToAction("Mode");

            modeSetting.Value = mode;

            Context.SaveChanges();

            ViewData["system-mode"] = mode;

            return RedirectToAction("Mode");
        }

        public ActionResult Mode()
        {
            return View();
        }
    }
}