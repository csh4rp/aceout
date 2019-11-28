using Aceout.Domain;
using Aceout.Domain.Model;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.DataModel.Identity
{
    public class PasswordPolicy : Entity<int>
    {
        public virtual int? MinLength { get; set; }
        public virtual int? MaxLength { get; set; }
        public virtual bool RequireSmallAndBigLetters{ get; set; }
        public virtual bool RequireSpecialCharacters { get; set; }
        public virtual bool RequireNumbers { get; set; }
    }
}
