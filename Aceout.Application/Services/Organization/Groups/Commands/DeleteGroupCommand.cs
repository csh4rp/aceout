using Aceout.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Organization.Groups.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int Id { get; }

        public DeleteGroupCommand(int id)
        {
            Id = id > 0 ? id : throw new InvalidModelException("Id can't be less than 1");
        }
    }
}
