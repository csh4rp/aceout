using Aceout.Infrastructure.DataModel.Identity;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Identity
{
    public class PasswordPolicyMapping : ClassMap<PasswordPolicy>
    {
        public PasswordPolicyMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Table("PasswordPolicy");
            this.Map(x => x.MaxLength);
            this.Map(x => x.MinLength);
            this.Map(x => x.RequireNumbers).CustomSqlType("BIT");
            this.Map(x => x.RequireSmallAndBigLetters).CustomSqlType("BIT");
            this.Map(x => x.RequireSpecialCharacters).CustomSqlType("BIT");
        }
    }
}
