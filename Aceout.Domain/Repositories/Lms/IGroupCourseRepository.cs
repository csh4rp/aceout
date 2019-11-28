using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IGroupCourseRepository
    {
        Task AddAsync(IEnumerable<GroupCourse> entities, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteForCourseAsync(int courseId, CancellationToken cancellationToken = default(CancellationToken));
        Task<GroupCourse> GetGroupForCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
