using Aceout.Application.Queries.Lms.Categories.Models;
using Aceout.Application.Queries.Lms.Categories.Results;
using Aceout.Application.Queries.Lms.CoursePaths.Models;
using Aceout.Application.Queries.Lms.CoursePaths.Results;
using Aceout.Application.Queries.Lms.CourseReports.Models;
using Aceout.Application.Queries.Lms.Courses.Models;
using Aceout.Application.Queries.Lms.Courses.Results;
using Aceout.Application.Queries.Lms.LessonReports.Model;
using Aceout.Application.Queries.Lms.Lessons.Models;
using Aceout.Application.Queries.Lms.Lessons.Results;
using Aceout.Application.Queries.Lms.Materials.Models;
using Aceout.Application.Queries.Lms.Materials.Results;
using Aceout.Application.Queries.Lms.UserCourses.Models;
using Aceout.Application.Queries.Lms.UserCourses.Results;
using Aceout.Application.Services.Lms.CoursePaths.Commands;
using Aceout.Application.Services.Lms.Courses.Commands;
using Aceout.Application.Services.Lms.Lessons.Commands;
using Aceout.Application.Services.Lms.MaterialCategories.Commands;
using Aceout.Application.Services.Lms.Materials.Commands;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Helpers;
using Aceout.WebUI.Areas.Lms.Models.CoursePaths;
using Aceout.WebUI.Areas.Lms.Models.CourseReports;
using Aceout.WebUI.Areas.Lms.Models.Courses;
using Aceout.WebUI.Areas.Lms.Models.LessonReports;
using Aceout.WebUI.Areas.Lms.Models.Lessons;
using Aceout.WebUI.Areas.Lms.Models.MaterialCategory;
using Aceout.WebUI.Areas.Lms.Models.Materials;
using Aceout.WebUI.Areas.Lms.Models.UserCourses;
using Aceout.WebUI.Areas.Lms.Models.UserLessons;
using AutoMapper;

namespace Aceout.WebUI.Areas.Lms.Mappings
{
    public class LmsProfile : Profile
    {
        public LmsProfile()
        {
            #region CoursePaths

            CreateMap<CreateCoursePathModel, CreateCoursePathCommand>()
                .ConstructUsing(x => new CreateCoursePathCommand(AppHelper.CurrentLanguage, x.Name, x.Description)
                {
                    PictureUrl = x.PictureUrl
                });

            CreateMap<UpdateCoursePathModel, UpdateCorusePathCommand>()
                .ConstructUsing(x => new UpdateCorusePathCommand(x.Id, x.Name)
                {
                    PictureUrl = x.PictureUrl,
                    Description = x.Description
                });

            CreateMap<CoursePathFilter, CoursePathDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<CoursePathDto>()));

            CreateMap<CoursePath, CoursePathViewModel>();

            #endregion


            #region Courses

            CreateMap<CreateCourseModel, CreateCourseCommand>()
                .ForMember(x => x.Language, s => s.MapFrom(m => AppHelper.CurrentLanguage));

            CreateMap<UpdateCourseModel, UpdateCourseCommand>();

            CreateMap<CourseFilter, CourseDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<CourseDto>()));

            #endregion

            #region Lessons

            CreateMap<CreateLessonModel, CreateLessonCommand>();

            CreateMap<UpdateLessonModel, UpdateLessonCommand>();

            CreateMap<LessonFilter, LessonDataSourceQuery>()
                .ForMember(x => x.CourseId, s => s.MapFrom(c => c.CourseId))
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<LessonDto>()));

            CreateMap<Lesson, LessonViewModel>();

            #endregion

            #region UserCourses

            CreateMap<UserCoursesFilter, UserCourseDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<UserCourseDto>()));

            #endregion

            #region UserLessons

            CreateMap<UserLesson, StartLessonViewModel>();


            #endregion


            #region MaterialCategories

            CreateMap<CreateMaterialCategoryModel, CreateCategoryCommand>()
                .ConstructUsing(x => new CreateCategoryCommand(AppHelper.CurrentLanguage, x.Name));

            CreateMap<UpdateMaterialCategoryModel, UpdateCategoryCommand>()
                .ConstructUsing(x => new UpdateCategoryCommand(x.Id, x.Name));

            CreateMap<MaterialCategoryFilter, MaterialCategoryDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<MaterialCategoryDto>()));

            CreateMap<MaterialCategory, MaterialCategoryViewModel>();

            #endregion


            #region Materials

            CreateMap<CreateMaterialModel, CreateMaterialCommand>();

            CreateMap<UpdateMaterialModel, UpdateMaterialCommand>();

            CreateMap<MaterialFilter, MaterialDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<MaterialDto>()));

            #endregion

            #region CourseReports

            CreateMap<CourseReportFilter, CourseReportDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<CourseReport>()));

            #endregion

            #region LessonReports

            CreateMap<LessonReportFilter, LessonReportDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<LessonReport>()));

            #endregion
        }
    }
}
