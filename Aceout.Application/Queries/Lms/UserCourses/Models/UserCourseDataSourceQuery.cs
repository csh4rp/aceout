using Aceout.Application.Queries.Lms.UserCourses.Results;
using Aceout.Tools.Data;
using MediatR;

namespace Aceout.Application.Queries.Lms.UserCourses.Models
{
    public class UserCourseDataSourceQuery : IRequest<DataSource<UserCourseDto>>
    {
        public int UserId { get; set; }
        public int? CoursePathId { get; set; }
        public Pager<UserCourseDto> Pager { get; set; }
    }
}
