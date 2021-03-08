using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class FrontendSkillsetModelTests : XUnitTestBase<FrontendSkillsetModelTests.Thens>
    {
        [Fact, PositiveTest]
        public void ShouldHaveValidInstance_WhenCreating()
        {
            When(Creating);

            Then.Validator.Should().NotBeNull();
        }

        [Fact, PositiveTest]
        public void ShouldHaveValidModel_WhenValidatingModel_GivenValidModel()
        {
            Given.Model = new FrontendSkillsetModel
            {
                Angular = true,
                AngularJS = true,
                CSS = true,
                Django = true,
                HTML = true,
                JavaScript = true,
                jQuery = true,
                Python = true,
                React = true,
                TypeScript = true,
                Vue = true
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new FrontendSkillsetModel.Validator();

            SetupModel();
        }

        private void SetupModel()
        {
            if (GivensDefined("Model") == false)
            {
                Given.Model = new FrontendSkillsetModel();
            }
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public FrontendSkillsetModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
