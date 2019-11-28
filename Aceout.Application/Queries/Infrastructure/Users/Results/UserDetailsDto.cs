using Aceout.Infrastructure.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Users.Results
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<int> UserRoles { get; set; }

    }
}
