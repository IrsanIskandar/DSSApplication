using System;
using System.Configuration;

namespace DssApplicationPoster.DatabaseManagement
{
    public class Constants
    {
        public static string ConnsStrings { get; set; }

        public static string Authority { get; set; }

        // Query Parameter
        public static readonly string p_UserID = "@p_UserId";
        public static readonly string p_Username = "@p_Username";
        public static readonly string p_EmailAddress = "@p_EmailAddress";
        public static readonly string p_Password = "@p_Password";
        public static readonly string p_IsVerified = "@p_IsVerified";
        public static readonly string p_Verification = "@p_Verification";
        public static readonly string p_FileName = "@p_FileName";
        public static readonly string p_FileSize = "@p_FileSize";
        public static readonly string p_FilePath = "@p_FilePath";

        //Time on GMT+0 Timezone
        public static readonly DateTime GMT_TIME =
            TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
    }
}