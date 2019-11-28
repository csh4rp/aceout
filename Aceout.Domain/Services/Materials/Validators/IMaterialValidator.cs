using Aceout.Domain.Model.Lms;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Materials.Validators
{
    public interface IMaterialValidator
    {
        Task<ValidationResult> ValidateAsync(Material material);
    }
}
