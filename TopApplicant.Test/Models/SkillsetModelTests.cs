using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class SkillsetModelTests : XUnitTestBase<SkillsetModelTests.Thens>
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
            Given.Model = new SkillsetModel
            {
                FrontendSkillset = new FrontendSkillsetModel
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
                },
                BackendSkillset = new BackendSkillsetModel
                {
                    CSharp = true,
                    DotNetCore = true,
                    GoLang = true,
                    Java = true,
                    Node = true,
                    SpringFramework = true
                },
                DatabaseSkillset = new DatabaseSkillsetModel
                {
                    MongoDB = true,
                    MSSQL = true,
                    PostgresSQL = true,
                    TransactSQL = true
                }
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new SkillsetModel.Validator();
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public SkillsetModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
