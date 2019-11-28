using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface ICourseAccessInfoRepository
    {
        Task<CourseAccessInfo> GetLastestAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
