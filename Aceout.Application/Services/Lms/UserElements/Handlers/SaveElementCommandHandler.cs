using Aceout.Application.Services.Lms.UserElements.Commands;
using Aceout.Application.Services.Lms.UserElements.Results;
using Aceout.Domain.Services.Lms.UserElements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.UserElements.Handlers
{
    public class SaveElementCommandHandler : IRequestHandler<SaveElementCommand, SaveElementResult>
    {
        private readonly IUserElementService _userElementService;

        public SaveElementCommandHandler(IUserElementService userElementService)
        {
            _userElementService = userElementService;
        }

        public async Task<SaveElementResult> Handle(SaveElementCommand request, CancellationToken cancellationToken)
        {
            var userElement = await _userElementService.SaveAsync(request.UserId, request.ElementId, request.Answers);

            return new SaveElementResult();
        }
    }
}
