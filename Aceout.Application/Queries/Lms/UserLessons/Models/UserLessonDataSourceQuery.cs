using Aceout.Application.Queries.Lms.UserLessons.Results;
using Aceout.Tools.Data;
using MediatR;

namespace Aceout.Application.Queries.Lms.UserLessons
{
    public class UserLessonDataSourceQuery : IRequest<DataSource<UserLessonDto>>
    {
        public int UserId { get; set; }
        public Pager<UserLessonDto> Pager { get; set; }
    }
}
