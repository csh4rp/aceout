using Aceout.Application.Queries.Infrastructure.Roles.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Roles.Models
{
    public class RoleDataSourceQuery : IRequest<DataSource<RoleDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<Role> Pager { get; set; }
    }
}
