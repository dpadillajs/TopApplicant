using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using Xunit;

namespace TopApplicant.Test.Models
{
    public sealed class JobPostModelTests : XUnitTestBase<JobPostModelTests.Thens>
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
            Given.JobId = 1;
            Given.JobTitle = "Front End Developer";
            Given.JobDescription = "Enhancing Retail Website Design";

            Given.Model = new JobPostModel(Given.JobId, Given.JobTitle, Given.JobDescription)
            {
                Applicants = new List<ApplicantModel>()
                {
                    new ApplicantModel("John", "Doe")
                    {
                        Skillset = new SkillsetModel()
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
                        }
                    }
                },
                RequiredSkillset = new SkillsetModel()
                {
                    FrontendSkillset = new FrontendSkillsetModel
                    {
                        CSS = true,
                        HTML = true,
                        JavaScript = true,
                        React = true,
                        TypeScript = true
                    }
                }
            };

            When(ValidatingModel);

            Then.Results.Should().NotBeNull();
            Then.Results.IsValid.Should().BeTrue();
        }

        protected override void Creating()
        {
            Then.Validator = new JobPostModel.Validator();
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public JobPostModel.Validator Validator;
            public ValidationResult Results;
        }
    }
}
