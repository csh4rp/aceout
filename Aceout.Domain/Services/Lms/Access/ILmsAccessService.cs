using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Lms.Access
{
    public interface ILmsAccessService
    {
        Task CheckCourseStartAccessAsync(CourseAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task CheckCourseStartAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task CheckCourseFinishAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task CheckLessonStartAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));
        Task CheckLessonFinishAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));

        Task CheckElementAccessAsync(UserElementInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken));
    }
}
