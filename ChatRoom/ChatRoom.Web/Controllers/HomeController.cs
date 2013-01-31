using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatRoom.Web.Models;

namespace ChatRoom.Web.Controllers
{
    public class HomeController : Controller
    {
        private static List<string> _names = new List<string>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ClientModel model)
        {
            if (_names.Contains(model.Name))
            {
                ViewBag.DuplicateUser = "This user already logged in. Please try diffent user name";
            }
            else if (ModelState.IsValid)
            {
                _names.Add(model.Name);
                return View("ChatRoom", model);
            }
            return View();
        }

        public ActionResult ChatRoom(ClientModel model)
        {
            return View(model);
        }
    }
}
