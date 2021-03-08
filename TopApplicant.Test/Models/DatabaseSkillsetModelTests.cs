using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class DatabaseSkillsetModelTests : XUnitTestBase<DatabaseSkillsetModelTests.Thens>
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
            Given.Model = new DatabaseSkillsetModel
            {
                MongoDB = true,
                MSSQL = true,
                PostgresSQL = true,
                TransactSQL = true
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new DatabaseSkillsetModel.Validator();

            SetupModel();
        }

        private void SetupModel()
        {
            if (GivensDefined("Model") == false)
            {
                Given.Model = new DatabaseSkillsetModel();
            }
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public DatabaseSkillsetModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
