using Aceout.Application.Queries.Lms.UserElements.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserElements.Models
{
    public class UserElementDetailsQuery : IRequest<UserElementDetailsDto>
    {
        public int UserId { get; set; }
        public int ElementId { get; set; }
    }
}
