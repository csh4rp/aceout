using Aceout.Application.Queries.Infrastructure.Users.Models;
using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Database;
using Aceout.Tools.Data;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Infrastructure.Users.Handlers
{
    public class UserDataSourceQueryHandler : IRequestHandler<UserDataSourceQuery, DataSource<UserDto>>
    {
        private readonly ISession _session;

        public UserDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<UserDto>> Handle(UserDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<User>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.UserName.StartsWith(request.SearchQuery));
            }

            var futureUsers = query.Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName
            })
            .Paginate(request.Pager)
            .ToFuture();

            var futureCount = query.ToFutureValue(x => x.Count());

            var dataSource = new DataSource<UserDto>();
            dataSource.Data = await futureUsers.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }
}
