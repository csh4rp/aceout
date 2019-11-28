using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class UserElementMapping : ClassMap<UserElement>
    {
        public UserElementMapping()
        {
            this.Table("UserElement");
            this.CompositeId().KeyProperty(x => x.ElementId).KeyProperty(x => x.UserLessonId);

            this.Map(m => m.Result);
            this.Map(m => m.UserAnswerModel);
            this.References(x => x.Element).Column("ElementId").Not.Insert().Not.Update().Cascade.None();
        }
    }
}
