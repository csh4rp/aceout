using Aceout.Application.Queries.Lms.Materials.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Materials.Models
{
    public class MaterialDetailsQuery : IRequest<MaterialDetailsDto>
    {
        public int Id { get; set; }
    }
}
