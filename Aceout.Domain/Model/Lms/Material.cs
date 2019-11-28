using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class Material : Entity<int>
    {
        public virtual string DataModel { get; protected set; }
        public virtual string AnswerModel { get; protected set; }
        public virtual TrainingMaterialType Type { get; protected set; }
        public virtual int CategoryId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Content { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual MaterialCategory MaterialCategory { get; set; }
        public virtual ISet<Element> Elements { get; set; }

        protected Material() { }

        public Material(string name, int categoryId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (categoryId < 1)
                throw new ArgumentException(nameof(categoryId));

            Name = name;
            CategoryId = categoryId;
        }

        public virtual void SetCategory(int categoryId)
        {
            if(categoryId < 1)
                throw new ArgumentException(nameof(categoryId));
            
            CategoryId = categoryId;
        }

        public virtual void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }

        public virtual void SetMaterialModel(object dataModel, object answerModel, TrainingMaterialType type)
        { 
            DataModel = JsonConvert.SerializeObject(dataModel);
            AnswerModel = JsonConvert.SerializeObject(answerModel);
            Type = type;
        }


        public virtual TModel GetDataModel<TModel>()
        {
            return JsonConvert.DeserializeObject<TModel>(DataModel);
        }

        public virtual TModel GetAnswerModel<TModel>()
        {
            return JsonConvert.DeserializeObject<TModel>(AnswerModel);
        }

    }
}
