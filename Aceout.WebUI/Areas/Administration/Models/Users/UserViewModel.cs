using Aceout.Infrastructure.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Administration.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsLockoutEnabled { get; set; }
        public IEnumerable<int> UserRoles { get; set; }
    }
}
