using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Model.Materials.SingleChoice;
using Aceout.Domain.Model.Trainings;
using Newtonsoft.Json;

namespace Aceout.Domain.Services.Materials.Converters
{
    public class SingleChoiceModelConverter : IMaterialModelConverter
    {
        public SingleChoiceModelConverter()
        {
        }

        public object ConvertAnswerModel(IEnumerable<AnswerModel> answerModel)
        {
            if (!answerModel.Any())
            {
                return null;
            }

            return new SingleChoiceAnswerModel
            {
                Id = answerModel.First().Id
            };
        }

        public IEnumerable<AnswerModel> ConvertAnswerModel(Material material)
        {
            var answerModel = material.GetAnswerModel<SingleChoiceAnswerModel>();

            yield return new AnswerModel
            {
                Id = answerModel.Id
            };
        }

        public IEnumerable<AnswerModel> ConvertAnswerModel(string model)
        {
            var answerModel = JsonConvert.DeserializeObject<SingleChoiceAnswerModel>(model);

            yield return new AnswerModel
            {
                Id = answerModel.Id
            };
        }

        public object ConvertDataModel(IEnumerable<DataModel> dataModels)
        {

            var elements = dataModels.Select(x => new SingleChoiceElement
            {
                Id = x.Id,
                Content = x.Content
            });

			return new SingleChoiceDataModel
			{
				Elements = elements
			};
        }

        public IEnumerable<DataModel> ConvertDataModel(Material material)
        {
            return material.GetDataModel<SingleChoiceDataModel>().Elements
                .Select(x => new DataModel
                {
                    Id = x.Id,
                    Content = x.Content
                });
        }

        public IEnumerable<DataModel> ConvertDataModel(string model)
        {
            return JsonConvert.DeserializeObject<SingleChoiceDataModel>(model).Elements
                .Select(x => new DataModel
                {
                    Id = x.Id,
                    Content = x.Content
                });
        }

        public object ConvertUserAnswerModel(IEnumerable<AnswerModel> userAnswers)
        {
            return ConvertAnswerModel(userAnswers);
        }

        public IEnumerable<AnswerModel> ConvertUserAnswerModel(UserElement userElement)
        {
            var answerModel = userElement.GetUserAnswer<SingleChoiceAnswerModel>();
            yield return new AnswerModel
            {
                Id = answerModel.Id,
            };
        }

        public IEnumerable<AnswerModel> ConvertUserAnswerModel(string model)
        {
            if (string.IsNullOrEmpty(model)) yield break;

            var answerModel = JsonConvert.DeserializeObject<SingleChoiceAnswerModel>(model);
            yield return new AnswerModel
            {
                Id = answerModel.Id,
            };
        }
    }
}
