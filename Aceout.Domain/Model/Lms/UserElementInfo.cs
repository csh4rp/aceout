using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class UserElementInfo
    {
        public virtual  int CourseId { get; set; }
        public virtual bool IsCourseActive { get; set; }
        public  virtual bool RequireLessonOrder { get; set; }
        public  virtual  int LessonId { get; set; }
        public virtual int UserLessonId { get; set; }
        public virtual  bool IsLessonActive { get; set; }
        public virtual  int LessonPosition { get; set; }
        public virtual  DateTime? FromDate { get; set; }
        public  virtual DateTime? ToDate { get; set; }
        public virtual int ElementId { get; set; }
        public  virtual  bool IsElementActive { get; set; }
        public virtual int ElementPosition { get; set; }
        public virtual  int ElementScale { get; set; }
        public  virtual  bool IsMaterialActive { get; set; }
        public  virtual  TrainingMaterialType MaterialType { get; set; }
        public  virtual string MaterialName { get; set; }
        public  virtual string MaterialContent { get; set; }
        public  virtual  string MaterialDataModel { get; set; }
        public  virtual  string MaterialAnswerModel { get; set; }
        public virtual string UserAnswerModel { get; set; }
        public virtual  int LessonAttempt { get; set; }
        public  virtual int CourseAttempt { get; set; }
        public virtual  int UserId { get; set; }
        public virtual bool AllowAnswerCheck { get; set; }
        public virtual DateTime? LessonStartedDate { get; set; }
        public virtual DateTime? LessonCompletedDate { get; set; }
        public virtual DateTime? CourseStartedDate { get; set; }
        public virtual DateTime? CourseCompletedDate { get; set; }


    }
}
