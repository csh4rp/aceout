using Aceout.Application.Queries.Cms.Informations.Results;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Cms.Informations.Models
{
    public class InformationDataSourceQuery : IRequest<DataSource<InformationDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<InformationDto> Pager { get; set; }
    }
}
