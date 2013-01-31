using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatRoom.Web.Models;
using ChatRoom.Web.Properties;

namespace ChatRoom.Web.Controllers
{
    /// <summary>
    /// The only controller to serve the appplication
    /// </summary>
    public class HomeController : Controller
    {
        //not a thread safe way
        private static List<string> _names = new List<string>();

        /// <summary>
        /// Lets user enter textbox
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Checks for valid user name and redirects user to chat room
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ClientModel model)
        {
            if (_names.Contains(model.Name))
            {
                ViewBag.DuplicateUser = Resources.DuplicateUser;
            }
            else if (ModelState.IsValid)
            {
                _names.Add(model.Name);
                return View("ChatRoom", model);
            }
            return View();
        }

        /// <summary>
        /// Displays chatroom
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ChatRoom(ClientModel model)
        {
            return View(model);
        }
    }
}
