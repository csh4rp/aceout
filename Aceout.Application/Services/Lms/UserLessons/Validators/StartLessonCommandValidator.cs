using Aceout.Application.Services.Lms.UserLessons.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserLessons.Validators
{
    public class StartLessonCommandValidator : AbstractValidator<StartLessonCommand>
    {
        public StartLessonCommandValidator()
        {
            this.RuleFor(x => x.LessonId).GreaterThan(0);
            this.RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
