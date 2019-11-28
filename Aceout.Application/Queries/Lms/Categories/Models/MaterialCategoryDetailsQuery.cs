using Aceout.Application.Queries.Lms.Categories.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Categories.Models
{
    public class MaterialCategoryDetailsQuery : IRequest<MaterialCategoryDto>
    {
        public int Id { get; set; }
    }
}
