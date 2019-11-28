using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class UserLessonMapping : ClassMap<UserLesson>
    {
        public UserLessonMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Map(x => x.IsPassed).CustomSqlType("BIT");
            this.Map(x => x.LessonId);
            this.Map(x => x.Result);
            this.Map(x => x.StartedDate);
            this.Map(x => x.CompletedDate);
            this.Map(x => x.UserCourseId);
            this.Map(x => x.UserId);
            this.Map(x => x.Attempt).Generated.Always();

            this.References(x => x.User).Column("UserId").Not.Insert().Not.Update().Cascade.None();
            this.References(x => x.Lesson).Column("LessonId").Not.Insert().Not.Update().Cascade.None();
        }
    }
}
