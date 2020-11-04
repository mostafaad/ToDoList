using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Dataa;
using ToDoList.Dataa.Model;
using ToDoList.Dataa.Models;
using ToDoList.Service.Services;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    public class UserToDoListController : Controller
    {
        public UserToDoListService _UserToDoListService = new UserToDoListService();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllUserToDoList()
        {
            var dbContext = new ApplicationDbContext();
            ApplicationUser user = new ApplicationUser();
            user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var model = _UserToDoListService.GetUserToDoListList().Where(x => x.IsDeleted == false&& x.UserID==user.Id);
            List<UserToDoListViewModel> Result = new List<UserToDoListViewModel>();
            foreach (var item in model)
            {
                Result.Add(new UserToDoListViewModel
                {
                    Title = item.Title,
                    ID = item.ID,
                    date = item.DueDate.ToShortDateString()
                });
            }
            var data = Result.Select(x => new { x.Title, x.ID, x.date });



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {


            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(UserToDoListViewModel model)
        {

            try
            {
               var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                var UserToDoList = new UserToDoList
                {
                    Title = model.Title,
                    DueDate = model.DueDate,
                    UserID=user.Id
                };
                _UserToDoListService.AddUserToDoList(UserToDoList);

                //var idResult = um.Create(user, model.Password);
                return Json(new { data = "Add", Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { data = "Add", Success = false }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var user = _UserToDoListService.GetUserToDoList(id);
            if (user != null)
            {
                UserToDoListViewModel model = new UserToDoListViewModel
                {
                    ID = id,
Title                     = user.Title,
DueDate=user.DueDate
                };

                return View(model);

            }
            else
                return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(UserToDoListViewModel model)
        {
            try
            {

                var user = _UserToDoListService.GetUserToDoList(model.ID);
                if (user != null)
                {

                    user.Title = model.Title;
                    user.DueDate = model.DueDate;
                    _UserToDoListService.UpdateUserToDoList(user);

                }
                return Json(new { data = "Edit", Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { data = "Edit", Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _UserToDoListService.DeleteUserToDoList(id);
            return Json(new { data = "Edit", Success = true }, JsonRequestBehavior.AllowGet);

        }
    }
}