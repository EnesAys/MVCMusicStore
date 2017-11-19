using MVCMusicStore.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVCMusicStore.Models
{
    public class UserDetail : EntityBase
    {
        public UserDetail()
        {
            IsLocked = false;
        }
        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "UserDetail_UserName"), StringLength(20, ErrorMessage = "Allowed character count is 20 for User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password cannot be empty"), Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First Name cannot be empty"), StringLength(20, ErrorMessage = "Allowed character count is 20 for First Name")]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "Allowed character count is 20 for Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email cannot be empty"), StringLength(100, ErrorMessage = "Allowed character count is 100 for Email"), EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone cannot be empty"), StringLength(20, ErrorMessage = "Allowed character count is 20 for Phone")]
        public string Phone { get; set; }
        [StringLength(300, ErrorMessage = "Allowed character count is 300 for Address")]
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }
        public bool IsLocked { get; set; }

        //Mapping
        public virtual List<Order> Orders { get; set; }
    }
}
