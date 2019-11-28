using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class CourseRepository :  ICourseRepository
    {
        private readonly ISession _session;

        public CourseRepository(ISession session)
        {
            _session = session;
        }

        public Task<Course> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Course>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Course entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(entity);
            return _session.FlushAsync(cancellationToken);
        }

        public Task AddAsync(Course entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(entity);
            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Query<Course>()
                .Where(x => x.Id == id)
                .Delete();

            return _session.FlushAsync(cancellationToken);
        }
    }
}
