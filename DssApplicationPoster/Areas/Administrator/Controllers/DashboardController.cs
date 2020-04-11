using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DssApplicationPoster.Areas.Administrator.Models;
using DssApplicationPoster.DatabaseManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DssApplicationPoster.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Administrator/[controller]/[action]")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Home()
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

                var dataTableDataTextRun = MediaContentModel.GetListTextRunning();
                List<TextRunning> resultDataTextRun = new List<TextRunning>();
                if (dataTableDataTextRun.Rows.Count > 0)
                {
                    resultDataTextRun = MediaContentModel.SetDataListTextRunning(dataTableDataTextRun);
                }

                var dataTableCountUsers = MediaContentModel.GetCountUsers();
                GetCountUsers resultCountUser = new GetCountUsers();
                if (dataTableCountUsers.Rows.Count > 0)
                {
                    resultCountUser = MediaContentModel.SetDataCountUsers(dataTableCountUsers);
                }

                var dataTableCountImage = MediaContentModel.GetCountMediaContentImage();
                GetCountContentImage resultCountImage = new GetCountContentImage();
                if (dataTableCountImage.Rows.Count > 0)
                {
                    resultCountImage = MediaContentModel.SetDataCountContentImage(dataTableCountImage);
                }

                var dataTableCountVideo = MediaContentModel.GetCountMediaContentVideo();
                GetCountContentVideo resultCountVideo = new GetCountContentVideo();
                if (dataTableCountVideo.Rows.Count > 0)
                {
                    resultCountVideo = MediaContentModel.SetCountContentVideo(dataTableCountVideo);
                }

                var dataTableCountTextRun = MediaContentModel.GetCountMediaContentTextRunning();
                GetCountContentTextRun resultCountTextRun = new GetCountContentTextRun();
                if (dataTableCountTextRun.Rows.Count > 0)
                {
                    resultCountTextRun = MediaContentModel.SetCountContentTextRun(dataTableCountTextRun);
                }

                CounterMediaContent result = new CounterMediaContent()
                {
                    GetDataTextRun = resultDataTextRun,
                    GetCountUser = resultCountUser,
                    GetCountImage = resultCountImage,
                    GetCountVideo = resultCountVideo,
                    GetCountTextRun = resultCountTextRun
                };

                return View(result);
            }
            else if (TempData["_ifAccountCannotVerify"] is string accNotVerify)
            {
                var userUnverify = Newtonsoft.Json.JsonConvert.DeserializeObject(accNotVerify);
                ViewData["_getUserCode"] = userUnverify;

                return RedirectToAction("ConfirmationEmail", "Privileged");
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }

        [HttpGet]
        public IActionResult UploadImage()
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

        [HttpGet]
        public IActionResult UploadVideo()
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

                var dataTable = MediaContentModel.GetListVideo();
                List<VideoList> result = new List<VideoList>();
                if (dataTable.Rows.Count > 0)
                {
                    result = MediaContentModel.SetDataListVideo(dataTable);
                }

                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }

        [HttpGet]
        public IActionResult GetListImages()
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

                var dataTable = MediaContentModel.GetListImages();
                List<ImageList> result = new List<ImageList>();
                if (dataTable.Rows.Count > 0)
                {
                    result = MediaContentModel.SetDataImageList(dataTable);
                }

                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }

        [HttpGet]
        public IActionResult GetListVideos()
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

                var dataTable = MediaContentModel.GetListVideo();
                List<VideoList> result = new List<VideoList>();
                if (dataTable.Rows.Count > 0)
                {
                    result = MediaContentModel.SetDataListVideo(dataTable);
                }

                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Privileged");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTextRun(string txtRun, string drpIsDisplayFormText)
        {
            if (TempData["ModelUserDetail"] is string user)
            {
                var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                ViewData["_getUserCode"] = userdetail.UserID;

                TempData.Keep("ModelUserDetail");
            }

            string userCode = ViewData["_getUserCode"].ToString();

            List<TextRunning> result = await MysqlHelper<TextRunning>.ExecuteProcedureAsync(QueryProcedureHelper.SP_ADD_TEXT_RUNNING,
                new
                {
                    p_TextName = txtRun.ToUpper(),
                    p_UserCode = userCode,
                    p_IsDisplay = drpIsDisplayFormText,
                    p_CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ttt")
                }) as List<TextRunning>;

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> LiveDisplayTextRun(int drpIsDisplayText, int numTxtlbl)
        {
            if (TempData["ModelUserDetail"] is string user)
            {
                var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                ViewData["_getUserCode"] = userdetail.UserID;

                TempData.Keep("ModelUserDetail");
            }

            string userCode = ViewData["_getUserCode"].ToString();

            List<TextRunning> result = await MysqlHelper<TextRunning>.ExecuteProcedureAsync(QueryProcedureHelper.SP_UPDATE_TEXT_RUNNING,
                new
                {
                    p_TextDisplay = drpIsDisplayText,
                    p_UserCode = userCode,
                    p_UpdateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    p_TextRunID = numTxtlbl
                }) as List<TextRunning>;

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> LiveDisplayImage(int drpIsDisplay, int numImglbl)
        {
            if (TempData["ModelUserDetail"] is string user)
            {
                var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                ViewData["_getUserCode"] = userdetail.UserID;

                TempData.Keep("ModelUserDetail");
            }

            string userCode = ViewData["_getUserCode"].ToString();

            List<ImageList> result = await MysqlHelper<ImageList>.ExecuteProcedureAsync(QueryProcedureHelper.SP_UPDATE_IMAGE,
                new
                {
                    p_LiveDisplayImage = drpIsDisplay,
                    p_UserCode = userCode,
                    p_UpdateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    p_ImageID = numImglbl
                }) as List<ImageList>;

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> LiveDisplayVideo(int drpIsVidDisplay, int numVidlbl)
        {
            if (TempData["ModelUserDetail"] is string user)
            {
                var userdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLoginModel>(user);
                ViewData["_getUserCode"] = userdetail.UserID;

                TempData.Keep("ModelUserDetail");
            }

            string userCode = ViewData["_getUserCode"].ToString();

            List<VideoList> result = await MysqlHelper<VideoList>.ExecuteProcedureAsync(QueryProcedureHelper.SP_UPDATE_VIDEO,
                new
                {
                    p_LiveDisplayVideo = drpIsVidDisplay,
                    p_UserCode = userCode,
                    p_UpdateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    p_VideoID = numVidlbl
                }) as List<VideoList>;

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync()
        {
            try
            {
                var fileImage = HttpContext.Request.Form.Files;

                if (fileImage == null || fileImage.Count() == 0)
                {
                    return  Content("file cannot be empty.");
                }
                else
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

                    foreach (var item in fileImage)
                    {
                        var dirInfo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        string currentPath = Path.Combine(dirInfo, "ImageUploads");
                        bool folderExists = System.IO.Directory.Exists(currentPath);
                        string fileName = item.FileName.Trim();
                        string fullPath = currentPath + "\\" + fileName;
                        string userCode = ViewData["_getUserCode"].ToString();

                        if (!folderExists)
                        {
                            Directory.CreateDirectory(currentPath);
                        }

                        FileInfo fileInfo = new FileInfo(fullPath);
                        var stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);

                        await item.CopyToAsync(stream);

                        MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                        MySqlCommand cmd = new MySqlCommand();

                        cmd.Connection = conn;
                        cmd.CommandText = QueryProcedureHelper.SP_UPLOAD_IMAGES;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.p_FileName, item.FileName.Trim());
                        cmd.Parameters.AddWithValue(Constants.p_FileSize, fileInfo.Length);
                        cmd.Parameters.AddWithValue(Constants.p_FilePath, currentPath);
                        cmd.Parameters.AddWithValue(Constants.p_UserID, userCode);
                        conn.Open();

                        int result = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return StatusCode(200);
        }

        [HttpPost]
        [RequestSizeLimit(6_442_450_944)] // 6GB Limit Uploads
        public async Task<IActionResult> UploadVideoAsync()
        {
            try
            {
                var fileVideo = HttpContext.Request.Form.Files;

                if (fileVideo == null || fileVideo.Count() == 0)
                {
                    return Content("file cannot be empty.");
                }
                else
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

                    foreach (var file in fileVideo)
                    {
                        var dirInfo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        string currentPath = Path.Combine(dirInfo, "VideoUploads");
                        bool folderExists = System.IO.Directory.Exists(currentPath);
                        string fileName = file.FileName;
                        string fullPath = currentPath + "\\" + fileName;
                        string userCode = ViewData["_getUserCode"].ToString();

                        if (!folderExists)
                        {
                            Directory.CreateDirectory(currentPath);
                        }

                        FileInfo fileInfo = new FileInfo(fullPath);
                        var stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);

                        await file.CopyToAsync(stream);

                        MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                        MySqlCommand cmd = new MySqlCommand();

                        cmd.Connection = conn;
                        cmd.CommandText = QueryProcedureHelper.SP_UPLOAD_VIDEOS;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.p_FileName, fileName);
                        cmd.Parameters.AddWithValue(Constants.p_FileSize, fileInfo.Length);
                        cmd.Parameters.AddWithValue(Constants.p_FilePath, currentPath);
                        cmd.Parameters.AddWithValue(Constants.p_UserID, userCode);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return StatusCode(200);
        }
    }
}