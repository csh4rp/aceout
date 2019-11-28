using Aceout.Application.Queries.Infrastructure.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Users.Models
{
    public class UserDetailsQuery : IRequest<UserDetailsDto>
    {
        public int Id { get; set; }
    }
}
