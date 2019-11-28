//using Aceout.Domain.Factories.Trainings;
//using Aceout.Domain.Model.Materials;
//using Aceout.Domain.Model.Materials.SingleChoice;
//using Aceout.Domain.Model.Trainings;
//using Aceout.Domain.Services.Materials.Converters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Xunit;

//namespace Aceout.Tests.Domain.Services.Materials.Converters
//{
//	public class SingleChoiceConverterTests
//	{
//		[Fact]
//		public void ConvertAnswerModel_SimpleData_ModelCreated()
//		{
//			var answerModels = new[]
//			{
//				new AnswerModel
//				{
//					Id = 1
//				}
//			};

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var answerModel = converter.ConvertAnswerModel(answerModels);

//			Assert.IsType<SingleChoiceAnswerModel>(answerModel);
//			Assert.Equal(1, (answerModel as SingleChoiceAnswerModel).Id);
//		}

//		[Fact]
//		public void ConvertDataModel_SimpleData_ModelCreated()
//		{
//			var dataModels = new[]
//			{
//				new DataModel
//				{
//					Id = 1,
//					Content = "Name"
//				},
//				new DataModel
//				{
//					Id = 2,
//					Content = "Name"
//				}
//			};

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var dataModel = converter.ConvertDataModel(dataModels);

//			Assert.IsType<SingleChoiceDataModel>(dataModel);
//			Assert.Equal(1, (dataModel as SingleChoiceDataModel).Elements.First().Id);
//		}

//		[Fact]
//		public void ConvertUserAnswerModel_SimpleData_ModelCreated()
//		{
//			var answerModels = new[]
//			{
//				new AnswerModel
//				{
//					Id = 1
//				}
//			};

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var dataModel = converter.ConvertUserAnswerModel(answerModels);

//			Assert.IsType<SingleChoiceAnswerModel>(dataModel);
//			Assert.Equal(1, (dataModel as SingleChoiceAnswerModel).Id);
//		}

//		[Fact]
//		public void ConvertModelToAnswers_SimpleData_ModelCreated()
//		{
//			var material = new TrainingMaterial("pl", "Name", 1);

//			var dataModel = new SingleChoiceDataModel
//			{
//				Elements = new[]
//				{
//					new SingleChoiceElement
//					{
//						Id = 1,
//						Content = "Content1"
//					},
//					new SingleChoiceElement
//					{
//						Id = 2,
//						Content = "Content2"
//					}
//				}
//			};

//			var answerModel = new SingleChoiceAnswerModel
//			{
//				Id = 1
//			};

//			material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var answers = converter.ConvertAnswerModel(material);

//			Assert.Equal(1, answers.First().Id);
//			Assert.Single(answers);
//		}

//		[Fact]
//		public void ConvertModelToData_SimpleData_ModelCreated()
//		{
//			var material = new TrainingMaterial("pl", "Name", 1);

//			var dataModel = new SingleChoiceDataModel
//			{
//				Elements = new[]
//				{
//					new SingleChoiceElement
//					{
//						Id = 1,
//						Content = "Content1"
//					},
//					new SingleChoiceElement
//					{
//						Id = 2,
//						Content = "Content2"
//					}
//				}
//			};

//			var answerModel = new SingleChoiceAnswerModel
//			{
//				Id = 1
//			};

//			material.SetMaterialModel(dataModel, answerModel, TrainingMaterialType.SingleChoice);

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var data = converter.ConvertDataModel(material);

//			Assert.Equal(1, data.First().Id);
//			Assert.Equal(2, data.Count());
//		}

//		[Fact]
//		public void ConvertModelToUserAnswer_SimpleData_ModelCreated()
//		{
//			var element = new UserElement(1, 1);
		
//			var answerModel = new SingleChoiceAnswerModel
//			{
//				Id = 1
//			};

//			element.SetUserAnswer(answerModel);

//			var converter = new MaterialModelConverterFactory().Create(TrainingMaterialType.SingleChoice);
//			var answers = converter.ConvertUserAnswerModel(element);

//			Assert.Equal(1, answers.First().Id);
//			Assert.Single(answers);
//		}
//	}
//}
