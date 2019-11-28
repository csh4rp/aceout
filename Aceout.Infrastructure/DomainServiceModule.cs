using Aceout.Domain.Factories.Lms;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Services.Lms.Access;
using Aceout.Domain.Services.Lms.UserElements;
using Aceout.Domain.Services.Lms.UserLessons;
using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.Modules;
using Autofac;
using Microsoft.AspNetCore.Hosting;

namespace Aceout.Infrastructure
{
    public class DomainServiceModule : DependencyModule
    {
        public DomainServiceModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnswerProcessorFactory>().As<IAnswerProcessorFactory>();
            builder.RegisterType<LmsAccessService>().As<ILmsAccessService>();
            builder.RegisterType<MaterialValidatorFactory>().As<IMaterialValidatorFactory>();
            builder.RegisterType<MaterialModelConverterFactory>().As<IMaterialModelConverterFactory>();
            builder.RegisterType<UserElementService>().As<IUserElementService>();
            builder.RegisterType<UserLessonService>().As<IUserLessonService>();
        }
    }

}
