using System.Collections.Generic;

namespace Aceout.Application.Queries.Organization.Groups.Results
{
    public class GroupDetailsDto : GroupDto
    {

        public IEnumerable<GroupUserDto> Users { get; set; }
    }
}
