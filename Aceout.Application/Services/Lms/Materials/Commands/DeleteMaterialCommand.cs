using Aceout.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Materials.Commands
{
    public class DeleteMaterialCommand : IRequest
    {
        public int Id { get; }

        public DeleteMaterialCommand(int id)
        {
            Id = id > 0 ? id : throw new InvalidModelException("Id can't be less than 1");
        }
    }
}
