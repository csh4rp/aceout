using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Trainings
{
    public interface IUserMaterialAnswerRepository
    {
        Task<UserMaterialAnswer> GetByPositionAsync(int userLessonId, int position);
    }
}
