using Aceout.Application.Services.Lms.UserCourses.Commands;
using Aceout.Domain.Repositories.Lms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserCourses.Validators
{
    public class StartCourseCommandValidator : AbstractValidator<StartCourseCommand>
    {
        public StartCourseCommandValidator()
        {
            this.RuleFor(x => x.CourseId).GreaterThan(0);
            this.RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
