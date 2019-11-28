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
    public class UserElementInfoRepository : IUserElementInfoRepository
    {
        private readonly ISession _session;

        public UserElementInfoRepository(ISession session)
        {
            _session = session;
        }

        public Task<UserElementInfo> GetAsync(int userId, int elementId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = _session.Query<UserElementInfo>().ToList();

            return _session.Query<UserElementInfo>()
                .Where(x => x.UserId == userId && x.ElementId == elementId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
