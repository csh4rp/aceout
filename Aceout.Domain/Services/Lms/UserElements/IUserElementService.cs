using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Lms.UserElements
{
    public interface IUserElementService
    {
        Task<UserElement> SaveAsync(int userId, int elementId, IEnumerable<AnswerModel> answers, CancellationToken cancellationToken = default(CancellationToken));
    }
}
