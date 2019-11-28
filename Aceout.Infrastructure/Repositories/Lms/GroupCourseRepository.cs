using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Organization;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class GroupCourseRepository : IGroupCourseRepository
    {
        private readonly ISession _session;

        public GroupCourseRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(IEnumerable<GroupCourse> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var item in entities)
            {
                _session.Save(item);
            }

            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteForCourseAsync(int courseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<GroupCourse>()
                .Where(x => x.CourseId == courseId)
                .DeleteAsync(cancellationToken);
        }

        public Task<GroupCourse> GetGroupForCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<GroupCourse>().Where(x => x.CourseId == courseId)
                .Join(_session.Query<GroupUser>().Where(x => x.UserId == userId),
                c => c.GroupId,
                u => u.GroupId,
                (c, u) => c)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
