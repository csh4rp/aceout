using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IMaterialCategoryRepository
    {
        Task<bool> CategoryExistsAsync(string language, string name);
        Task<MaterialCategory> GetByNameAsync(string language, string name);
        Task<bool> HasAssignedMaterialsAsync(int id);
        Task<MaterialCategory> GetByIdAsync(int id);
        Task AddAsync(MaterialCategory materialCategory);
        Task UpdateAsync(MaterialCategory materialCategory);
        Task DeleteAsync(int id);
    }
}
