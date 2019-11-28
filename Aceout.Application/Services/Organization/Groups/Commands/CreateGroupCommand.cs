using Aceout.Application.Services.Organization.Groups.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Organization.Groups.Commands
{
    public class CreateGroupCommand : IRequest<CreateGroupResult>
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }
}
