using Aceout.Application.Queries.Organization.Groups.Results;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Organization.Groups.Models
{
    public class GroupDataSourceQuery : IRequest<DataSource<GroupDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<GroupDto> Pager { get; set; }
    }
}
