using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Services.Materials.Converters
{
    public interface IMaterialModelConverter
    {
        object ConvertDataModel(IEnumerable<DataModel> dataModels);
        object ConvertAnswerModel(IEnumerable<AnswerModel> answerModel);
        object ConvertUserAnswerModel(IEnumerable<AnswerModel> userAnswers);

		IEnumerable<DataModel> ConvertDataModel(Material material);
		IEnumerable<AnswerModel> ConvertAnswerModel(Material material);
		IEnumerable<AnswerModel> ConvertUserAnswerModel(UserElement userElement);

        IEnumerable<DataModel> ConvertDataModel(string model);
        IEnumerable<AnswerModel> ConvertAnswerModel(string model);
        IEnumerable<AnswerModel> ConvertUserAnswerModel(string model);
    }
}
