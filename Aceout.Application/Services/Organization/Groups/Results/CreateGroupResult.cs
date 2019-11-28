using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Organization.Groups.Results
{
    public class CreateGroupResult
    {
        public Group Group { get; }

        public CreateGroupResult(Group group)
        {
            Group = group;
        }
    }
}
