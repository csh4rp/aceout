using Aceout.Application.Queries.Organization.Groups.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Organization.Groups.Models
{
    public class GroupAutocompleteQuery : IRequest<IEnumerable<GroupDto>>
    {
        public string SearchQuery { get; set; }
        public string Language { get; set; }
    }
}
