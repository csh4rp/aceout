using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Identity
{
    public class UserMapping :  ClassMap<User>
    {
        public UserMapping()
        {

            this.Id(x => x.Id).GeneratedBy.Identity();
            this.HasMany(x => x.UserRoles).KeyColumn("UserId").Inverse().Cascade.AllDeleteOrphan();
            this.HasMany(x => x.UserGroups).KeyColumn("UserId").Inverse().Cascade.AllDeleteOrphan();
            this.Table("User");
            this.Map(x => x.UserName);
            this.Map(x => x.Email);
            this.Map(x => x.FirstName);
            this.Map(x => x.LastName);
            this.Map(x => x.PhoneNumber);
            this.Map(x => x.ModifiedDate);
            this.Map(x => x.CreatedDate);
            this.Map(x => x.PasswordHash);
            this.Map(x => x.IsEmailConfirmed).CustomSqlType("BIT");
            this.Map(x => x.IsPhoneNumberConfirmed).CustomSqlType("BIT");
            this.Map(x => x.IsLockoutEnabled).CustomSqlType("BIT");
            this.Map(x => x.AccessFailedCount);
            this.Map(x => x.ActivationToken);
        }
    }

}
