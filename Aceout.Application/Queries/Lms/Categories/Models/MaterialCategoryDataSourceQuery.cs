using Aceout.Application.Queries.Lms.Categories.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Categories.Models
{
    public class MaterialCategoryDataSourceQuery : IRequest<DataSource<MaterialCategoryDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<MaterialCategoryDto> Pager { get; set; }
    }
}
