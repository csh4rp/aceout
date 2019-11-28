using Aceout.Application.Queries.Infrastructure.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Users.Models
{
    public class UserAutocompleteQuery : IRequest<IEnumerable<UserDto>>
    {
        public string Language { get; set; }
        public string SearchQuery { get; set; }
    }
}
