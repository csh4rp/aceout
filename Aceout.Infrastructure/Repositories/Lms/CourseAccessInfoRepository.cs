using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class CourseAccessInfoRepository : ICourseAccessInfoRepository
    {
        private readonly ISession _session;

        public CourseAccessInfoRepository(ISession session)
        {
            _session = session;
        }

        public Task<CourseAccessInfo> GetLastestAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<CourseAccessInfo>()
                .Where(x => x.UserId == userId &&
                       x.CourseId == courseId)
                .FirstOrDefaultAsync(cancellationToken);

        }
    }
}
