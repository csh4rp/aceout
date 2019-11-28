using Aceout.Domain.Model.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class UserElement
    {
        public virtual int ElementId { get; protected set; }
        public virtual int UserLessonId { get; protected set; }
        public virtual string UserAnswerModel { get; protected set; }
        public virtual decimal? Result { get; set; }
        public virtual Element Element { get; set; }

        protected UserElement() { }

        public UserElement(int userLessonId, int elementId)
        {
            if (userLessonId <= 0)
                throw new ArgumentException(nameof(userLessonId));

            if (elementId <= 0)
                throw new ArgumentException(nameof(elementId));

            UserLessonId = userLessonId;
            ElementId = elementId;
        }

        public virtual void SetUserAnswer(object answerModel, decimal? result)
        {
            UserAnswerModel = JsonConvert.SerializeObject(answerModel);
            Result = result;
        }

        public virtual TAnswer GetUserAnswer<TAnswer>()
        {
            return JsonConvert.DeserializeObject<TAnswer>(UserAnswerModel);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserElement);
        }

        public virtual bool Equals(UserElement userElement)
        {
            if (userElement == null) return false;

            return this.ElementId == userElement.ElementId && this.UserLessonId == userElement.UserLessonId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1211552702;
            hashCode = hashCode * -1521134295 + UserLessonId.GetHashCode();
            hashCode = hashCode * -1521134295 + ElementId.GetHashCode();
            return hashCode;
        }
    
    }

}
