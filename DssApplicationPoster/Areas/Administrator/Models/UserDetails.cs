using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string PreferredName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IsVerified { get; set; }
        public string Password { get; set; }
        public string UserActive { get; set; }

        public UserDetails(int userID, string userName, string firstName, string preferredName, DateTime? birthDate, string phone, string lastName, string email, string isVerified, string password, string userActive)
        {
            UserID = userID;
            UserName = userName;
            FirstName = firstName;
            PreferredName = preferredName;
            BirthDate = birthDate;
            Phone = phone;
            LastName = lastName;
            Email = email;
            IsVerified = isVerified;
            Password = password;
            UserActive = userActive;
        }

        public UserDetails() { }
    }
}
