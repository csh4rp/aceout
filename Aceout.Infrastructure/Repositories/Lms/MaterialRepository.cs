using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ISession _session;

        public MaterialRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(Material material, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(material);
            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
             _session.Query<Material>()
                .Delete();

            return _session.FlushAsync(cancellationToken);
        }

        public Task<Material> GetByElementIdAsync(int elementId, CancellationToken cancellationToken = default(CancellationToken))
        {
            //return _session.Query<Material>()
            //    .Join(_session.Query<Element>().Where(x => x.Id == elementId),
            //    m => m.Id,
            //    e => e.MaterialId,
            //    (m, e) => m)
            //    .FirstOrDefaultAsync(cancellationToken);

            return _session.Query<Element>().Where(x => x.Id == elementId)
                .Select(x => x.Material)
                .FirstOrDefaultAsync(cancellationToken);

            //return (from m in _session.Query<Material>()
            //        join e in (from el in _session.Query<Element>()
            //                   where el.Id == elementId
            //                   select el)
            //        on m.Id equals e.MaterialId
            //        select m)
            //        .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<Material> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Material>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<Material> GetForUserAsync(int userLessonId, int number, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (from ul in _session.Query<UserLesson>()
                    where ul.Id == userLessonId
                    join l in _session.Query<Lesson>()
                    on ul.LessonId equals l.Id
                    join e in _session.Query<Element>()
                    on l.Id equals e.LessonId
                    join m in _session.Query<Material>()
                    on e.MaterialId equals m.Id
                    orderby e.Position descending
                    select m)
                   .Skip(number)
                   .Take(1)
                   .FirstOrDefaultAsync(cancellationToken);
                   
        }

        public Task UpdateAsync(Material material, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(material);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
