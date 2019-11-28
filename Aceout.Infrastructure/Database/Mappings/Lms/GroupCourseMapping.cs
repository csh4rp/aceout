using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Organization;
using Aceout.Domain.Model.Trainings;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class GroupCourseMapping : ClassMap<GroupCourse>
    {
        public GroupCourseMapping()
        {
            this.Id(x => x.AttemptCount);
            this.CompositeId().KeyProperty(x => x.CourseId).KeyProperty(x => x.GroupId);
            this.Table("GroupCourse");
            this.Map(x => x.AttemptCount);
            this.Map(x => x.FromDate);
            this.Map(x => x.ToDate);
            this.References(x => x.Group).Column("GroupId").Not.Insert().Not.Update().Cascade.None();
            this.References(x => x.Course).Column("CourseId").Not.Insert().Not.Update().Cascade.None();

        }
    }
}
