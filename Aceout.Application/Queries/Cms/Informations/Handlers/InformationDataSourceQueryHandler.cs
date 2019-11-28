using Aceout.Application.Queries.Cms.Informations.Models;
using Aceout.Application.Queries.Cms.Informations.Results;
using Aceout.Domain.Model.Cms;
using Aceout.Tools.Data;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Cms.Informations.Handlers
{
    public class InformationDataSourceQueryHandler : IRequestHandler<InformationDataSourceQuery, DataSource<InformationDto>>
    {
        private readonly ISession _session;

        public InformationDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<InformationDto>> Handle(InformationDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Information>();

            var infFeature = query.Select(x => new InformationDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Author = x.User.FirstName + " " + x.User.LastName
            })
            .Paginate(request.Pager)
            .ToFuture();

            var countFuture = query.ToFutureValue(x => x.Count());

            return new DataSource<InformationDto>
            {
                Data = await infFeature.GetEnumerableAsync(cancellationToken),
                RowCount = await countFuture.GetValueAsync(cancellationToken)
            };
        }
    }
}
