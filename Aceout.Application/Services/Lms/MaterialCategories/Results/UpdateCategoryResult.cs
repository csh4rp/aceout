using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.MaterialCategories.Results
{
    public class UpdateCategoryResult
    {
        public MaterialCategory Category { get; }

        public UpdateCategoryResult(MaterialCategory category)
        {
            Category = category;
        }
    }
}
