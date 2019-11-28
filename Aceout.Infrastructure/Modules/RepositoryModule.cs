using Aceout.Domain.Repositories.Cms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Repositories.Organization;
using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.Repositories.Cms;
using Aceout.Infrastructure.Repositories.Identity;
using Aceout.Infrastructure.Repositories.Lms;
using Aceout.Infrastructure.Repositories.Organization;
using Autofac;
using Microsoft.AspNetCore.Hosting;

namespace Aceout.Infrastructure.Modules
{
    public class RepositoryModule : DependencyModule
    {
        public RepositoryModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordPolicyRepository>().AsImplementedInterfaces();
            builder.RegisterType<MaterialCategoryRepository>().As<IMaterialCategoryRepository>();
            builder.RegisterType<MaterialRepository>().As<IMaterialRepository>();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>();
            builder.RegisterType<GroupUserRepository>().As<IGroupUserRepository>();
            builder.RegisterType<CoursePathRepository>().As<ICoursePathRepository>();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<GroupCourseRepository>().As<IGroupCourseRepository>();
            builder.RegisterType<LessonRepository>().As<ILessonRepository>();
            builder.RegisterType<ElementRepository>().As<IElementRepository>();
            builder.RegisterType<CourseAccessInfoRepository>().As<ICourseAccessInfoRepository>();
            builder.RegisterType<UserCourseRepository>().As<IUserCourseRepository>();
            builder.RegisterType<UserLessonRepository>().As<IUserLessonRepository>();
            builder.RegisterType<UserElementRepository>().As<IUserElementRepository>();
            builder.RegisterType<LessonAccessInfoRepository>().As<ILessonAccessInfoRepository>();
            builder.RegisterType<UserElementInfoRepository>().As<IUserElementInfoRepository>();

            builder.RegisterType<InformationRepository>().As<IInformationRepository>();
            builder.RegisterType<GroupInformationRepository>().As<IGroupInformationRepository>();
        }
    }
}
