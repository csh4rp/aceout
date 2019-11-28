using Aceout.Application.Queries.Cms.Informations.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Cms.Informations.Models
{
    public class InformationDetailsQuery : IRequest<InformationDetailsDto>
    {
        public int Id { get; set; }
    }
}
