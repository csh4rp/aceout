using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Organization.Groups.Results
{
    public class UpdateGroupResult
    {
        public Group Group { get; }

        public UpdateGroupResult(Group group)
        {
            Group = group;
        }
    }
}
