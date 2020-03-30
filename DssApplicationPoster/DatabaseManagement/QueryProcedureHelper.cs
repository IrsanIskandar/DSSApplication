using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DssApplicationPoster.DatabaseManagement
{
    public class QueryProcedureHelper
    {
        public static readonly string SP_ADD_USERS = "sp_AddUser";
        public static readonly string SP_USER_LOGIN = "sp_LoginUser";
        public static readonly string SP_CREATE_TOKEN = "sp_CreateToken";
        public static readonly string SP_PRIVILEGED_LOGIN = "sp_PrivilegedLoginUser";
        public static readonly string SP_CHECK_ACCOUNT = "sp_CheckAccount";
        public static readonly string SP_ACTIVATE_ACCOUNT = "sp_UpdateAccountActivate";
        public static readonly string SP_CHECK_ACCOUNT_LOGIN_VERIFY = "sp_CheckAccountVerify";
        public static readonly string SP_UPLOAD_IMAGES = "sp_UploadImages";
        public static readonly string SP_UPLOAD_VIDEOS = "sp_UploadVideos";
        public static readonly string SP_ADD_TEXT_RUNNING = "sp_AddNewTextRunning";
        public static readonly string SP_GET_LIST_IMAGES = "sp_GetListImages";
        public static readonly string SP_GET_LIST_VIDEOS = "sp_GetListVideos";
        public static readonly string SP_GET_LIST_TEXT_RUN = "sp_GetListTextRunning";
        public static readonly string SP_UPDATE_IMAGE = "dss_application.sp_LiveDisplayImage";
        public static readonly string SP_UPDATE_VIDEO = "dss_application.sp_LiveDisplayVideo";
        public static readonly string SP_UPDATE_TEXT_RUNNING = "dss_application.sp_LiveDisplayTextRunning";
        public static readonly string SP_GET_MEDIA_CONTENT_IMAGE = "dss_application.sp_GetMediaContentImage";
        public static readonly string SP_GET_MEDIA_CONTENT_VIDEO = "dss_application.sp_GetMediaContentVideo";
        public static readonly string SP_GET_MEDIA_CONTENT_TEXTRUN = "dss_application.sp_GetMediaContentTextRun";
        public static readonly string SP_CHECK_FORGOT_PASSWORD = "dss_application.sp_CheckForgotPassword";
        public static readonly string SP_CREATE_NEW_PASSWORD = "dss_application.sp_CreateNewPassword";
    }
}