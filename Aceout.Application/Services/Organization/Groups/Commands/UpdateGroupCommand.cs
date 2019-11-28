using Aceout.Application.Services.Organization.Groups.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Organization.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<UpdateGroupResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }
}
