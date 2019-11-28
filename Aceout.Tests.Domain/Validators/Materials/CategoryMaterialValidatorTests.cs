using Xunit;
using Moq;
using System.Threading.Tasks;
using Aceout.Domain.Services.Materials.Categories;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Model.Lms;

namespace Aceout.Tests.Domain.Validators.Materials
{
    public class CategoryMaterialValidatorTests
    {
        [Fact]
        public async void ValidateCategoryCreation_UniqueName_Valid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.CategoryExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(false));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateCreationAsync("pl", "Category");
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async void ValidateCategoryCreation_NonUniqueName_Invalid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.CategoryExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(true));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateCreationAsync("pl", "Category");
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async void ValidateCategoryUpdate_UniqueName_Valid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new MaterialCategory("Category1", "pl")));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateUpdateAsync(0, "pl", "Category");
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async void ValidateCategoryUpdate_NonUniqueName_Invalid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new MaterialCategory("Category", "pl")));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateUpdateAsync(1, "pl", "Category");
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }


        [Fact]
        public async void ValidateCategoryDeletion_NonAssignedCourses_Valid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.HasAssignedMaterialsAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(false));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateDeletionAsync(0);
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async void ValidateCategoryDeletion_AssignedCourses_Invalid()
        {
            var repoMock = new Mock<IMaterialCategoryRepository>();
            repoMock.Setup(x => x.HasAssignedMaterialsAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(true));

            var repo = repoMock.Object;
            var validator = new MaterialCategoryValidator(repo);

            var result = await validator.ValidateDeletionAsync(0);
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }
    }



}
