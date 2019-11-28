using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.MaterialCategories.Results
{
    public class CreateCategoryResult
    {
        public MaterialCategory Category { get; }

        public CreateCategoryResult(MaterialCategory materialCategory)
        {
            Category = materialCategory;
        }
    }
}
