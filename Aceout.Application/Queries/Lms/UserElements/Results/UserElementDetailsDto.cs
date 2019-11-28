using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserElements.Results
{
    public class UserElementDetailsDto
    {
        public int LessonId { get; set; }
        public TrainingMaterialType MaterialType { get; set; }
        public int ElementId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialContent { get; set; }
        public int Position { get; set; }
        public IEnumerable<DataModel> MaterialDataModels { get; set; }
        public IEnumerable<AnswerModel> UserAnswerModels { get; set; }
        public IEnumerable<AnswerModel> CorrectAnswerModels { get; set; }
    }
}
