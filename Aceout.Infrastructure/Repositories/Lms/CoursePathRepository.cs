using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Repositories.Trainings;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class CoursePathRepository : Repository<CoursePath, int>, ICoursePathRepository
    {
        public CoursePathRepository(ISession session) : base(session)
        {
        }
    }
}
