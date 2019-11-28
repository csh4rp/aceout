using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Users.Models
{
    public class UserDataSourceQuery : IRequest<DataSource<UserDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<UserDto> Pager { get; set; }
    }
}
