using Aceout.Application.Services.Lms.UserCourses.Commands;
using Aceout.Application.Services.Lms.UserCourses.Validators;
using Aceout.Application.Services.Lms.UserLessons.Commands;
using Aceout.Application.Services.Lms.UserLessons.Validators;
using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.Modules;
using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;

namespace Aceout.Application
{
    public class ServiceModule : DependencyModule
    {
        public ServiceModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartLessonCommandValidator>().As<IValidator<StartLessonCommand>>();
            builder.RegisterType<StartCourseCommandValidator>().As<IValidator<StartCourseCommand>>();
        }
    }
}
