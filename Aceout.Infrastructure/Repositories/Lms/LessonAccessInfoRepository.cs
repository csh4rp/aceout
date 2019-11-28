using Aceout.Domain.Model.Lms;
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
    public class LessonAccessInfoRepository : ILessonAccessInfoRepository
    {
        private readonly ISession _session;

        public LessonAccessInfoRepository(ISession session)
        {
            _session = session;
        }

        public Task<LessonAccessInfo> GetAsync(int userId, int lessonId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<LessonAccessInfo>()
                .FirstOrDefaultAsync(x => x.UserId == userId &&
                                          x.LessonId == lessonId, cancellationToken);
        }

        public Task<LessonAccessInfo> GetForElementAsync(int userId, int elementId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Element>()
                .Where(x => x.Id == elementId)
                .Join(_session.Query<LessonAccessInfo>()
                        .Where(l => l.UserId == userId),
                e => e.LessonId,
                a => a.LessonId,
                (e, a) => a)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
