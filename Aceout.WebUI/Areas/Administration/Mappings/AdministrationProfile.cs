using Aceout.Application.Queries.Infrastructure.Roles.Models;
using Aceout.Application.Queries.Infrastructure.Roles.Results;
using Aceout.Application.Queries.Infrastructure.Users.Models;
using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using Aceout.WebUI.Areas.Administration.Models.Roles;
using Aceout.WebUI.Areas.Administration.Models.Users;
using AutoMapper;

namespace Aceout.WebUI.Areas.Administration.Mappings
{
    public class AdministrationProfile : Profile
    {
        public AdministrationProfile()
        {
            CreateMap<UserDetailsDto, UserViewModel>()
                .ForSourceMember(x => x.UserRoles, s => s.DoNotValidate())
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Password, s => s.Ignore())
                .ForMember(x => x.UserRoles, s => s.Ignore());

            CreateMap<UserViewModel, User>()
                .ForMember(x => x.CreatedDate, s => s.Ignore())
                .ForMember(x => x.PasswordHash, s => s.Ignore())
                .ForMember(x => x.ModifiedDate, s => s.Ignore())
                .ForMember(x => x.LockoutEndDate, s => s.Ignore())
                .ForMember(x => x.ActivationToken, s => s.Ignore())
                .ForMember(x => x.AccessFailedCount, s => s.Ignore())
                .ForMember(x => x.UserRoles, s => s.Ignore());

            CreateMap<Role, RoleViewModel>()
                .ForSourceMember(x => x.Permissions, s => s.DoNotValidate())
                .ForSourceMember(x => x.RolePermissions, s => s.DoNotValidate())
                .ForSourceMember(x => x.UserRoles, s => s.DoNotValidate())
                .ReverseMap();

            CreateMap<RoleDto, RoleViewModel>()
                .ReverseMap();

            CreateMap<RoleFilter, RoleDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<RoleDto>()));
                

            CreateMap<UserFilter, UserDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<UserDto>()));

        }
    }
}
