using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class UserLoginModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PreferredName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IsVerified { get; set; }
        public string Password { get; set; }
        public string UserActive { get; set; }

        public UserLoginModel(int userID, string userName, string preferredName, string firstName, string lastName, DateTime? birthDay, string phoneNumber, string email, string isVerified, string password, string userActive)
        {
            UserID = userID;
            UserName = userName;
            PreferredName = preferredName;
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            PhoneNumber = phoneNumber;
            Email = email;
            IsVerified = isVerified;
            Password = password;
            UserActive = userActive;
        }

        public UserLoginModel() { }
    }
}
