using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class CompanyModelTests : XUnitTestBase<CompanyModelTests.Thens>
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
            Given.CompanyName = "Best Buy";
            Given.CompanyDescription = "Retail supplier of consumer eletronics";

            Given.Model = new CompanyModel(Given.CompanyName, Given.CompanyDescription)
            {
                JobPostings = new List<JobPostModel>()
                {
                    new JobPostModel(1, "Front End Developer", "Enhancing Retail Website Design")
                    {
                        Applicants = new List<ApplicantModel>()
                        {
                            new ApplicantModel("John", "Doe")
                        },
                        RequiredSkillset = new SkillsetModel()
                        {
                            FrontendSkillset = new FrontendSkillsetModel(),
                            BackendSkillset = new BackendSkillsetModel(),
                            DatabaseSkillset = new DatabaseSkillsetModel()
                        }
                    }
                }
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new CompanyModel.Validator();

            SetupModel();
        }

        private void SetupModel()
        {
            if (GivensDefined("Model") == false)
            {
                Given.Model = new CompanyModel("Best Buy", "Retail supplier of consumer eletronics");
            }
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public CompanyModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
