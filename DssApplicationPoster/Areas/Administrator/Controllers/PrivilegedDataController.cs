using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DssApplicationPoster.Areas.Administrator.Models;
using DssApplicationPoster.DatabaseManagement;
using DssApplicationPoster.MailConfig;
using DssApplicationPoster.SecurityService;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MySql.Data.MySqlClient;

namespace DssApplicationPoster.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Administrator/[controller]/[action]")]
    public class PrivilegedDataController : Controller
    {
        [HttpPost]
        // POST: Administrator/AddUserRegisterWithoutPrivileged
        public async Task<ActionResult> AddRegisterUser(UserDetails userDetails)
        {
            if (userDetails != null)
            {
                UserDetails result = await MysqlHelper<UserDetails>.ExecuteProcedureSingleAsync(QueryProcedureHelper.SP_ADD_USERS, new
                {
                    p_FirstName = userDetails.FirstName,
                    P_Lastname = userDetails.LastName,
                    p_PreferredName = userDetails.PreferredName,
                    p_PhoneNumber = userDetails.Phone,
                    p_BirthDate = userDetails.BirthDate,
                    P_Username = userDetails.UserName,
                    p_Email = userDetails.Email,
                    p_Password = AESSecurity.EncryptPassword(userDetails.Password)
                }) as UserDetails;

                if (result.Email != null && result.UserName != null)
                {
                    SendMailActivationAccount(result);
                }
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult GetUserLogin(string txtPrivilegedInfo, string txtPassword)
        {
            List<UserLoginModel> listUser = new List<UserLoginModel>();
            UserLoginModel user = new UserLoginModel();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();

                string regexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                string regexUsername = @"@[a-zA-Z0-9]+";
                string replaceText = string.Empty;
                string passUser = AESSecurity.EncryptPassword(txtPassword);
                bool emailIsValid = Regex.IsMatch(txtPrivilegedInfo, regexEmail, RegexOptions.IgnoreCase);
                bool usernameIsValid = Regex.IsMatch(txtPrivilegedInfo, regexUsername, RegexOptions.IgnoreCase);

                if (emailIsValid == true)
                {
                    replaceText = txtPrivilegedInfo;

                    cmd.Connection = conn;
                    cmd.CommandText = QueryProcedureHelper.SP_CHECK_ACCOUNT_LOGIN_VERIFY_EMAIL_ADDRESS;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.p_EmailAddress, replaceText);
                    cmd.Parameters.AddWithValue(Constants.p_Password, passUser);
                    conn.Open();
                }
                else if (usernameIsValid == true)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = QueryProcedureHelper.SP_CHECK_ACCOUNT_LOGIN_VERIFY_USERNAME;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.p_Username, txtPrivilegedInfo);
                    cmd.Parameters.AddWithValue(Constants.p_Password, passUser);
                    conn.Open();
                }
                else
                {
                    throw new Exception("Data is not match.");
                }
                

                var checkAccount = cmd.ExecuteScalar();
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                cmd.Parameters.Clear();
                // ConfirmationEmail
                if (Convert.ToInt32(checkAccount) == 1)
                {
                    using (MySqlConnection conn2 = new MySqlConnection(Constants.ConnsStrings))
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand())
                        {
                            if (emailIsValid == true)
                            {
                                replaceText = txtPrivilegedInfo;

                                cmd2.Connection = conn2;
                                cmd2.CommandText = QueryProcedureHelper.SP_USER_LOGIN;
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue(Constants.p_Username, txtPrivilegedInfo.Equals(string.Empty));
                                cmd2.Parameters.AddWithValue(Constants.p_EmailAddress, replaceText);
                                cmd2.Parameters.AddWithValue(Constants.p_Password, passUser);
                                conn2.Open();
                                txtPrivilegedInfo = string.Empty;
                            }
                            else if (usernameIsValid == true)
                            {
                                cmd2.Connection = conn2;
                                cmd2.CommandText = QueryProcedureHelper.SP_USER_LOGIN;
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue(Constants.p_Username, txtPrivilegedInfo);
                                cmd2.Parameters.AddWithValue(Constants.p_EmailAddress, replaceText.Equals(string.Empty));
                                cmd2.Parameters.AddWithValue(Constants.p_Password, passUser);
                                conn2.Open();
                            }
                            else
                            {
                                throw new Exception("Data is null.");
                            }

                            using (MySqlDataReader reader = cmd2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        if (txtPrivilegedInfo != "" || replaceText != "" && txtPassword != "")
                                        {
                                            DateTime? date = null;
                                            user = new UserLoginModel()
                                            {
                                                UserID = reader["UserID"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(reader["UserID"]),
                                                UserName = reader["UserName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["UserName"]),
                                                FirstName = reader["FirstName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["FirstName"]),
                                                PreferredName = reader["PreferredName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["PreferredName"]),
                                                LastName = reader["LastName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["LastName"]),
                                                BirthDay = reader["BirthDay"].Equals(DBNull.Value) == true ? date : Convert.ToDateTime(reader["BirthDay"]),
                                                PhoneNumber = reader["PhoneNumber"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["PhoneNumber"]),
                                                Email = reader["Email"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["Email"]),
                                                IsVerified = reader["IsVerified"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["IsVerified"]),
                                                Password = reader["Password"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["Password"]),
                                                UserActive = reader["UserActive"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(reader["UserActive"])
                                            };

                                            listUser.Add(user);
                                        }
                                    }
                                }
                                reader.Close();
                            }
                            conn2.Close();
                        }
                    }

                    if (user.UserName == txtPrivilegedInfo && user.Password == passUser)
                    {
                        string createPrivilegedAccess = AESSecurity.EncryptPassword(user.UserID.ToString()) + "_" + AESSecurity.EncryptPassword(user.UserName) + "_" + AESSecurity.EncryptPassword(user.Password);

                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.HttpOnly = false;
                        cookieOptions.Expires = DateTime.Now.AddSeconds(180);
                        Response.Cookies.Append("privilege_access", createPrivilegedAccess, cookieOptions);

                        var modelObj = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                        TempData["ModelUserDetail"] = modelObj;

                        return RedirectToAction("Home", "Dashboard");
                    }
                    else if (user.Email == replaceText && user.Password == passUser)
                    {
                        string createPrivilegedAccess = AESSecurity.EncryptPassword(user.UserID.ToString()) + "_" + AESSecurity.EncryptPassword(user.UserName) + "_" + AESSecurity.EncryptPassword(user.Password);

                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.HttpOnly = false;
                        cookieOptions.Expires = DateTime.Now.AddSeconds(180);
                        Response.Cookies.Append("privilege_access", createPrivilegedAccess, cookieOptions);

                        var modelObj = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                        TempData["ModelUserDetail"] = modelObj;

                        return RedirectToAction("Home", "Dashboard");
                    }
                    else
                    {
                        throw new Exception("Username, Email or Password is wrong, please check again.");
                    }
                }
                else if (Convert.ToInt32(checkAccount) == 0)
                {
                    string accNotVerify = "Account Unverified.";
                    string passText = Newtonsoft.Json.JsonConvert.SerializeObject(accNotVerify);
                    TempData["_ifAccountCannotVerify"] = passText;

                    return RedirectToAction("ConfirmationEmail", "Privileged");
                }
                else
                {
                    throw new Exception("Your Account not been verified.");
                }
            }
            catch (MySqlException sqlEx)
            {
                ViewBag.SqlExceptionMessage = "Your Exception : " + sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                ViewBag.ArgumentExceptionMessage = "Your Exception : " + arEx.Message.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Check your data. " + ex.Message);
            }

            return StatusCode(200);
        }

        [HttpGet]
        public ActionResult SingOutPrivileged()
        {
            if (HttpContext.Request.Cookies["privilege_access"] != null && HttpContext.Request.Cookies["privilege_access"].Length > 0)
            {
                Response.Cookies.Delete("privilege_access", new CookieOptions()
                {
                    Secure = true
                });

                foreach (var cookieKey in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookieKey);
                }
            }

            return RedirectToAction("Login", "Privileged");
        }

        [HttpGet]
        public ActionResult AccountActivation(int accountCode, string confirmAccount)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = QueryProcedureHelper.SP_CHECK_ACCOUNT;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Constants.p_UserID, accountCode);
                conn.Open();

                var checkAccount = cmd.ExecuteScalar();
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                cmd.Parameters.Clear();
                if (Convert.ToInt32(checkAccount) == 0)
                {
                    MySqlConnection conn2 = new MySqlConnection(Constants.ConnsStrings);
                    MySqlCommand cmd2 = new MySqlCommand();

                    //activationCode = Guid.Parse(confirmAccount.ToString());

                    cmd2.Connection = conn2;
                    cmd2.CommandText = QueryProcedureHelper.SP_ACTIVATE_ACCOUNT;
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue(Constants.p_IsVerified, confirmAccount);
                    cmd2.Parameters.AddWithValue(Constants.p_UserID, accountCode);
                    conn2.Open();
                    cmd2.ExecuteNonQuery();

                    if (conn2.State == System.Data.ConnectionState.Open)
                        conn2.Close();

                    return RedirectToAction("Login", "Privileged");
                }
                else
                {
                    return RedirectToAction("Register", "Privileged");
                }
            }
            catch (MySqlException MysqlEx)
            {
                ViewBag.MysqlEx = MysqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult CheckForgotPassword(string userName, string email)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = QueryProcedureHelper.SP_CHECK_FORGOT_PASSWORD;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Constants.p_Username, userName);
                cmd.Parameters.AddWithValue(Constants.p_EmailAddress, email);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            forgotPassword = new ForgotPassword()
                            {
                                UserId = reader["UserId"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(reader["Username"])
                            };
                        }
                    }
                    reader.Close();
                }

                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                if (forgotPassword.UserId == 0 && forgotPassword.Username == 0)
                {
                    throw new Exception("Username and Password does not exists.");
                }
                else
                {
                    SendMailForgotPassword(forgotPassword.UserId.ToString(), userName, email);

                    return Ok();

                }
            }
            catch (MySqlException MysqlEx)
            {
                ViewBag.MysqlEx = MysqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return Ok();
        }

        [HttpGet]
        public ActionResult ConfirmForgotPassword(string userCode, string usernameCode, string emailCode)
        {
            try
            {
                if (userCode != null && usernameCode != null)
                {
                    string userID = AESSecurity.DecryptPassword(userCode);
                    string username = AESSecurity.DecryptPassword(usernameCode);
                    //string email = AESSecurity.DecryptPassword(emailCode.Replace("+", " "));

                    var modelObj = Newtonsoft.Json.JsonConvert.SerializeObject(userID);
                    TempData["_ForgotPassword"] = modelObj;

                    return RedirectToAction("CreateNewPassword", "Privileged");
                }
            }
            catch (MySqlException MysqlEx)
            {
                ViewBag.MysqlEx = MysqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult ResetPassword(string password)
        {
            try
            {
                if (TempData["_ForgotPassword"] is string userID)
                {
                    var forgotPassword = Newtonsoft.Json.JsonConvert.DeserializeObject(userID).ToString();

                    MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Connection = conn;
                    cmd.CommandText = QueryProcedureHelper.SP_CREATE_NEW_PASSWORD;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.p_NewPassword, AESSecurity.EncryptPassword(password));
                    cmd.Parameters.AddWithValue(Constants.p_UserID, forgotPassword);
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();

                    return StatusCode(200);
                }
            }
            catch (MySqlException MysqlEx)
            {
                ViewBag.MysqlEx = MysqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return Ok();
        }

        private void SendMailActivationAccount(UserDetails userModel)
        {
            try
            {
                string fullName = userModel.PreferredName == null ? userModel.FirstName : userModel.PreferredName != null ? userModel.PreferredName : string.Empty;
                string activateCode = AESSecurity.EncryptPassword(userModel.Email);

                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("noreply@pemkabbekasi.com", EmailConstant.EmailAddress));
                emailMessage.To.Add(new MailboxAddress(fullName, userModel.Email));
                emailMessage.Subject = "Acativation Your Account.";

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = EmailConstant.AcctivationAccount(fullName, HttpContext.Request.Scheme, HttpContext.Request.Host.Value, userModel.UserID, activateCode)
                };

                using (SmtpClient mailClient = new SmtpClient())
                {
                    mailClient.Connect(EmailConstant.SmtpServer, EmailConstant.SmtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    mailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    mailClient.Authenticate(EmailConstant.SmtpUsername, EmailConstant.SmtpPassword);
                    mailClient.Send(emailMessage);
                    mailClient.Disconnect(true);
                    mailClient.Dispose();
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
        }

        private void SendMailForgotPassword(string userId, string userName, string email)
        {
            try
            {

                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("noreply@pemkabbekasi.com", EmailConstant.EmailAddress));
                emailMessage.To.Add(new MailboxAddress(userName, email));
                emailMessage.Subject = "Reset Password.";

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = EmailConstant.ForgotPassword(userName, HttpContext.Request.Scheme, HttpContext.Request.Host.Value, AESSecurity.EncryptPassword(userId), AESSecurity.EncryptPassword(userName), AESSecurity.EncryptPassword(email))
                };

                using (SmtpClient mailClient = new SmtpClient())
                {
                    mailClient.Connect(EmailConstant.SmtpServer, EmailConstant.SmtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    mailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    mailClient.Authenticate(EmailConstant.SmtpUsername, EmailConstant.SmtpPassword);
                    mailClient.Send(emailMessage);
                    mailClient.Disconnect(true);
                    mailClient.Dispose();
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
        }
    }
}