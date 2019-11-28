using Aceout.Domain.Repositories.Lms;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Materials.Categories
{
    public class MaterialCategoryValidator : IMaterialCategoryValidator
    {
        #region Fields
        private readonly IMaterialCategoryRepository _repository;
        #endregion

        #region Ctor
        public MaterialCategoryValidator(IMaterialCategoryRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<ValidationResult> ValidateCreationAsync(string language, string name)
        {
            if (await _repository.CategoryExistsAsync(language, name))
            {
                return ValidationResult.Invalid("Category with specified name already exists");
            }

            return ValidationResult.Valid();
        }

        public async Task<ValidationResult> ValidateDeletionAsync(int id)
        {
            if(await _repository.HasAssignedMaterialsAsync(id))
            {
                return ValidationResult.Invalid("Category has assigned courses and cannot be deleted");
            }

            return ValidationResult.Valid();
        }

        public async Task<ValidationResult> ValidateUpdateAsync(int id, string language, string name)
        {
            var category = await _repository.GetByNameAsync(language, name);

            if (category != null && category.Id != id)
            {
                return ValidationResult.Invalid("Category with specified name already exists");
            }

            if (category != null) return ValidationResult.Valid();
            category = await _repository.GetByIdAsync(id);

            return category == null ? ValidationResult.Invalid("Category with specified ID does not exist") : ValidationResult.Valid();
        }
        #endregion
    }
}
