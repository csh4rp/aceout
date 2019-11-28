using Aceout.Application.Services.Lms.MaterialCategories.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.MaterialCategories.Commands
{
    public class CreateCategoryCommand : IRequest<CreateCategoryResult>
    {
        public string Language { get; }
        public string Name { get; }

        public CreateCategoryCommand(string language, string name)
        {
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentException(nameof(language));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            Language = language;
            Name = name;
        }
    }
}
