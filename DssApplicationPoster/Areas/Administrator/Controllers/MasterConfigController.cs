using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DssApplicationPoster.Areas.Administrator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DssApplicationPoster.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Administrator/[controller]/[action]")]
    public class MasterConfigController : Controller
    {
        public IActionResult CreateServerDirectory()
        {
            if (HttpContext.Request.Cookies["privilege_access"] != null)
            {
                if (TempData["ModelUserDetail"] is string user)
                {
                    var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                    ViewData["_getUserCode"] = userdetail.UserID;
                    ViewData["_getUserName"] = userdetail.UserName;
                    ViewData["_getFisrtName"] = userdetail.FirstName;
                    ViewData["_getPreferredName"] = userdetail.PreferredName;
                    ViewData["_getLastName"] = userdetail.LastName;

                    TempData.Keep("ModelUserDetail");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }

        public IActionResult CreateNewUser()
        {
            if (HttpContext.Request.Cookies["privilege_access"] != null)
            {
                if (TempData["ModelUserDetail"] is string user)
                {
                    var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                    ViewData["_getUserCode"] = userdetail.UserID;
                    ViewData["_getUserName"] = userdetail.UserName;
                    ViewData["_getFisrtName"] = userdetail.FirstName;
                    ViewData["_getPreferredName"] = userdetail.PreferredName;
                    ViewData["_getLastName"] = userdetail.LastName;

                    TempData.Keep("ModelUserDetail");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }
    }
}