using Aceout.Application.Queries.Cms.Informations.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Cms.Informations.Models
{
    public class InformationListQuery : IRequest<IEnumerable<InformationDto>>
    {
        public int UserId { get; set; }
        public int Count { get; set; }
        public int PageNumber { get; set; }
    }
}
