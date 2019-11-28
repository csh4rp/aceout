using Aceout.Application.Model.Lms;
using Aceout.Application.Services.Lms.Courses.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Application.Services.Lms.Courses.Validators
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            this.RuleFor(x => x.CoursePathId).GreaterThan(0);
            this.RuleFor(x => x.Name).NotNull().NotEmpty();
            this.RuleFor(x => x.Language).NotNull().NotEmpty();
            this.RuleFor(x => x.Groups).Custom(ValidateGroups);
        }

        protected void ValidateGroups(IEnumerable<CourseGroupModel> groupIds, FluentValidation.Validators.CustomContext context)
        {
            if (groupIds != null && groupIds.Any(x => x.Id <= 0))
            {
                context.AddFailure("Group Id can't be less than 1");
            }
        }
    }
}
