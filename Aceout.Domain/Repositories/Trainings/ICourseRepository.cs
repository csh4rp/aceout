using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Aceout.Domain.Repositories.Lms
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(Course entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(Course entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
