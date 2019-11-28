using Aceout.Application.Queries.Lms.Categories.Models;
using Aceout.Application.Queries.Lms.Categories.Results;
using Aceout.Domain.Model.Lms;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Lms.Categories.Handlers
{
    public class MaterialCategoryListQueryHandler : IRequestHandler<MaterialCategoryListQuery, IEnumerable<MaterialCategoryDto>>
    {
        private readonly ISession _session;

        public MaterialCategoryListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<MaterialCategoryDto>> Handle(MaterialCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _session.Query<MaterialCategory>()
                .Where(x => x.Language == request.Language)
                .Select(x => new MaterialCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
