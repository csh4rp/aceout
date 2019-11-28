using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Trainings
{
    public class UserMaterialAnswer
    {
        public string Name { get; protected set; }
        public string Content { get; protected set; }
        public string MaterialDataModel { get; protected set; }
        public string MaterialAnswerModel { get; protected set; }
        public TrainingMaterialType MaterialType { get; protected set; }
        public int ElementId { get; protected set; }
        public int LessonId { get; protected set; }
        public int ElementPosition { get; protected set; }
        public int UserLessonId { get; protected set; }
        public string UserAnswerModel { get; protected set; }
        public int? UserId { get; protected set; }

    }
}
