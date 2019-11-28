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
    public class MaterialCategoryDetailsQueryHandler : IRequestHandler<MaterialCategoryDetailsQuery, MaterialCategoryDto>
    {
        private readonly ISession _session;

        public MaterialCategoryDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public Task<MaterialCategoryDto> Handle(MaterialCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            return _session.Query<MaterialCategory>()
                .Where(x => x.Id == request.Id)
                .Select(x => new MaterialCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
