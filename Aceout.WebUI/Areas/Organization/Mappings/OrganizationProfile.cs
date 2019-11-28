using Aceout.Application.Queries.Organization.Groups;
using Aceout.Application.Queries.Organization.Groups.Models;
using Aceout.Application.Queries.Organization.Groups.Results;
using Aceout.Application.Services.Organization.Groups.Commands;
using Aceout.Domain.Model.Organization;
using Aceout.Tools.Helpers;
using Aceout.WebUI.Areas.Organization.Models.Groups;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Organization.Mappings
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Group, UpdateGroupModel>()
                .ForSourceMember(s => s.Language, x => x.DoNotValidate())
                .ForMember(s => s.UserIds, x => x.Ignore())
                .ReverseMap();

            CreateMap<GroupFilter, GroupDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<GroupDto>()));


            CreateMap<CreateGroupModel, CreateGroupCommand>()
                .ForMember(x => x.Language, s => s.MapFrom(m => AppHelper.CurrentLanguage));

            CreateMap<UpdateGroupModel, UpdateGroupCommand>();

            CreateMap<Group, GroupViewModel>()
                .ForMember(x => x.UserIds, s => s.Ignore());
        }
    }
}
