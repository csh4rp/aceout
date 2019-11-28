using Aceout.Application.Queries.Cms.Informations.Models;
using Aceout.Application.Queries.Cms.Informations.Results;
using Aceout.Application.Services.Cms.Informations.Commands;
using Aceout.WebUI.Areas.Cms.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Cms.Mappings
{
    public class CmsProfile : Profile
    {
        public CmsProfile()
        {
            CreateMap<CreateInforationModel, CreateInformationCommand>();

            CreateMap<UpdateInformationModel, UpdateInformationCommand>();

            CreateMap<InformationsFilter, InformationDataSourceQuery>()
                .ForMember(x => x.Pager, s => s.MapFrom(c => c.GetPager<InformationDto>()));

        }
    }
}
