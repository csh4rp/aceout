using Aceout.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.MaterialCategories.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; }

        public DeleteCategoryCommand(int id)
        {
            Id = id > 0 ? id : throw new InvalidModelException("Id can't be less than 1");
        }
    }
}
