using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Materials.Categories
{
    public interface IMaterialCategoryValidator
    {
        Task<ValidationResult> ValidateCreationAsync(string language, string name);
        Task<ValidationResult> ValidateUpdateAsync(int id, string language, string name);
        Task<ValidationResult> ValidateDeletionAsync(int id);
    }
}
