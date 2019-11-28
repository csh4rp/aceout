using Aceout.Application.Queries.Lms.Materials.Results;
using Aceout.Domain.Model.Materials;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Materials.Models
{
    public class MaterialDataSourceQuery : IRequest<DataSource<MaterialDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<MaterialDto> Pager { get; set; }
    }
}
