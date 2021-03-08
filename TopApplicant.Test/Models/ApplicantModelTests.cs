using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class ApplicantModelTests : XUnitTestBase<ApplicantModelTests.Thens>
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
            Given.FirstName = "John";
            Given.LastName = "Doe";

            Given.Model = new ApplicantModel(Given.FirstName, Given.LastName)
            {
                Skillset = new SkillsetModel()
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new ApplicantModel.Validator();

            SetupModel();
        }

        private void SetupModel()
        {
            if (GivensDefined("Model") == false)
            {
                Given.Model = new ApplicantModel("John", "Doe");
            }
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public ApplicantModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
