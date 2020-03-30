using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DssApplicationPoster.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Administrator/[controller]/[action]")]
    public class PrivilegedController : Controller
    {
        [HttpGet]
        // GET: Administrator/LoginAccess
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmationEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateNewPassword()
        {
            if (TempData["_ForgotPassword"] is string userID)
            {
                var forgotPassword = Newtonsoft.Json.JsonConvert.DeserializeObject(userID).ToString();
                ViewData["_getUserCode"] = forgotPassword;

                TempData.Keep("_ForgotPassword");
            }

            return View();
        }
    }
}