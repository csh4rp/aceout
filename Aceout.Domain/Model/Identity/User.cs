using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Identity
{
    public class User
    {
            public virtual int Id { get; set; }
            public virtual string UserName { get; set; }
            public virtual string Email { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string PhoneNumber { get; set; }
            public virtual DateTime ModifiedDate { get; set; }
            public virtual DateTime CreatedDate { get; set; }
            public virtual string PasswordHash { get; set; }
            public virtual bool IsEmailConfirmed { get; set; }
            public virtual bool IsPhoneNumberConfirmed { get; set; }
            public virtual DateTime? LockoutEndDate { get; set; }
            public virtual bool IsLockoutEnabled { get; set; }
            public virtual int AccessFailedCount { get; set; }
            public virtual string ActivationToken { get; set; }

            public virtual ISet<UserRole> UserRoles { get; set; }
            public virtual ISet<GroupUser> UserGroups { get; set; }

            public User() { }
        
    }
}
