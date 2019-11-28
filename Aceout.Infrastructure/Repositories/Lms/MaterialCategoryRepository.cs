using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class MaterialCategoryRepository : IMaterialCategoryRepository
    {
        private readonly ISession _session;

        public MaterialCategoryRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(MaterialCategory materialCategory)
        {
            _session.Save(materialCategory);
            return _session.FlushAsync();
        }

        public Task<bool> CategoryExistsAsync(string language, string name)
        {
            return _session.Query<MaterialCategory>()
                .AnyAsync(x => x.Language == language && x.Name == name);
        }

        public Task DeleteAsync(int id)
        {

            _session.Query<MaterialCategory>()
                .Where(x => x.Id == id)
                .Delete();

            return _session.FlushAsync();
        }

        public Task<MaterialCategory> GetByIdAsync(int id)
        {
            return _session.Query<MaterialCategory>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<MaterialCategory> GetByNameAsync(string language, string name)
        {
            return _session.Query<MaterialCategory>()
                .FirstOrDefaultAsync(x => 
                    x.Language == language &&
                    x.Name == name);
        }

        public Task<bool> HasAssignedMaterialsAsync(int id)
        {
            return _session.Query<Material>()
                .AnyAsync(x => x.CategoryId == id);
        }

        public Task UpdateAsync(MaterialCategory materialCategory)
        {
            _session.Update(materialCategory);
            return _session.FlushAsync();
        }

    }
}
