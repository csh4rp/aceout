using Aceout.Application.Queries.Infrastructure.Roles.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Roles.Models
{
    public class RoleDetailsQuery : IRequest<RoleDetailsDto>
    {
        public int Id { get; set; }
    }
}
