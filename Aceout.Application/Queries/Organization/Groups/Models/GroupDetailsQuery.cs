using Aceout.Application.Queries.Organization.Groups.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Organization.Groups.Models
{
    public class GroupDetailsQuery : IRequest<GroupDetailsDto>
    {
        public int Id { get; set; }
    }
}
