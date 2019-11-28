using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface ICoursePathRepository
    {
        Task AddAsync(CoursePath coursePath);
        Task UpdateAsync(CoursePath coursePath);
        Task DeleteAsync(int id);
        Task<CoursePath> GetByIdAsync(int id);
    }
}
