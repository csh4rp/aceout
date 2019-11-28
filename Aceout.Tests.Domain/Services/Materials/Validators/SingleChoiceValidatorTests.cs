using Aceout.Domain.Factories.Lms;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Materials.Converters;
using Moq;
using System.Linq;
using Xunit;

namespace Aceout.Tests.Domain.Services.Materials.Validators
{
    public class SingleChoiceValidatorTests
    {
        [Fact]
        public async void ValidateData_NotEnoughData_IsInvalid()
        {
            var repoMock = new Mock<IMaterialRepository>();

            var repo = repoMock.Object;
            var validator = new MaterialValidatorFactory(repo).CreateValidator(TrainingMaterialType.SingleChoice);
            var material = GetMaterialWithNotEnoughData( "Name");

            var result = await validator.ValidateAsync(material);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, s => s == "At least two answers are required");
        }

        [Fact]
        public async void ValidateAnswer_InvalidAnswerId_IsInvalid()
        {
            var repoMock = new Mock<IMaterialRepository>();

            var repo = repoMock.Object;
            var validator = new MaterialValidatorFactory(repo).CreateValidator(TrainingMaterialType.SingleChoice);
            var material = GetMaterialWithInvalidAnswer("Name");

            var result = await validator.ValidateAsync(material);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, s => s == "Answer ID doesn't match data model answer ids");
        }

        [Fact]
        public async void ValidateData_WithoutAnswer_IsInvalid()
        {
            var repoMock = new Mock<IMaterialRepository>();

            var repo = repoMock.Object;
            var validator = new MaterialValidatorFactory(repo).CreateValidator(TrainingMaterialType.SingleChoice);
            var material = GetMaterialWithoutAnswer("Name");

            var result = await validator.ValidateAsync(material);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, s => s == "Answer model is required");
        }

        [Fact]
        public async void ValidateData_MultipleErrors_IsInvalid()
        {
            var repoMock = new Mock<IMaterialRepository>();

            var repo = repoMock.Object;
            var validator = new MaterialValidatorFactory(repo).CreateValidator(TrainingMaterialType.SingleChoice);
            var material = GetInvalidMaterial("Name");

            var result = await validator.ValidateAsync(material);

            Assert.False(result.IsValid);
            Assert.Equal(3, result.Errors.Count());
            Assert.Contains(result.Errors, s => s == "At least two answers are required");
            Assert.Contains(result.Errors, s => s == "Answer model is required");
            Assert.Contains(result.Errors, s => s == "Material with specified name already exists");
        }


        Material GetValidMaterial(string name, int categoryId = 1)
        {
            var material = new Material(name, categoryId);

            var answers = new[]
            {
                new AnswerModel
                {
                    Id = 1,
                }
            };

            var datas = new[]
            {
                new DataModel
                {
                    Id = 1,
                    Content = "Content1"
                },
                new DataModel
                {
                    Id = 2,
                    Content = "Content2"
                }
            };

            var modelConverter = new SingleChoiceModelConverter();
            var answerModel = modelConverter.ConvertAnswerModel(answers);
            var dataModel = modelConverter.ConvertDataModel(datas);

            material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

            return material;
        }

        Material GetMaterialWithInvalidAnswer(string name, int categoryId = 1)
        {
            var material = new Material(name, categoryId);

            var answers = new[]
            {
                new AnswerModel
                {
                    Id = 3,
                }
            };

            var datas = new[]
            {
                new DataModel
                {
                    Id = 1,
                    Content = "Content1"
                },
                new DataModel
                {
                    Id = 2,
                    Content = "Content2"
                }
            };

            var modelConverter = new SingleChoiceModelConverter();
            var answerModel = modelConverter.ConvertAnswerModel(answers);
            var dataModel = modelConverter.ConvertDataModel(datas);

            material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

            return material;
        }

        Material GetMaterialWithNotEnoughData(string name, int categoryId = 1)
        {
            var material = new Material(name, categoryId);

            var answers = new[]
            {
                new AnswerModel
                {
                    Id = 1,
                }
            };

            var datas = new[]
            {
                new DataModel
                {
                    Id = 1,
                    Content = "Content1"
                }
            };

            var modelConverter = new SingleChoiceModelConverter();
            var answerModel = modelConverter.ConvertAnswerModel(answers);
            var dataModel = modelConverter.ConvertDataModel(datas);

            material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

            return material;
        }

        Material GetMaterialWithoutAnswer(string name, int categoryId = 1)
        {
            var material = new Material(name, categoryId);

            var answers = new AnswerModel[0];


            var datas = new[]
            {
                new DataModel
                {
                    Id = 1,
                    Content = "Content1"
                },
                new DataModel
                {
                    Id = 2,
                    Content = "Content2"
                }
            };

            var modelConverter = new SingleChoiceModelConverter();
            var answerModel = modelConverter.ConvertAnswerModel(answers);
            var dataModel = modelConverter.ConvertDataModel(datas);

            material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

            return material;
        }

        Material GetInvalidMaterial(string name, int categoryId = 1)
        {
            var material = new Material(name, categoryId);

            var answers = new AnswerModel[0];

            var datas = new[]
            {
                new DataModel
                {
                    Id = 1,
                    Content = "Content1"
                }
            };

            var modelConverter = new SingleChoiceModelConverter();
            var answerModel = modelConverter.ConvertAnswerModel(answers);
            var dataModel = modelConverter.ConvertDataModel(datas);

            material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

            return material;
        }
    }

}
