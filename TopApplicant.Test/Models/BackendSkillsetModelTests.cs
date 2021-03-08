using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class BackendSkillsetModelTests : XUnitTestBase<BackendSkillsetModelTests.Thens>
    {
        [Fact, PositiveTest]
        public void ShouldHaveValidInstance_WhenCreating()
        {
            When(Creating);

            Then.Validator.Should().NotBeNull();
        }

        [Fact, PositiveTest]
        public void ShouldHaveValidModel_WHenValidatingModel_GivenValidModel()
        {
            Given.Model = new BackendSkillsetModel
            {
                CSharp = true,
                DotNetCore = true,
                GoLang = true,
                Java = true,
                Node = true,
                SpringFramework = true
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new BackendSkillsetModel.Validator();

            SetupModel();
        }

        private void SetupModel()
        {
            if (GivensDefined("Model") == false)
            {
                Given.Model = new BackendSkillsetModel();
            }
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public BackendSkillsetModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
