using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Cms.Informations.Commands
{
    public class DeleteInformationCommand : IRequest
    {
        public int Id { get; }

        public DeleteInformationCommand(int id)
        {
            Id = id > 0 ? id : throw new ArgumentException(nameof(id));
        }
    }
}
