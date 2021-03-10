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

        [Fact, PositiveTest]
        public void ShouldReturnExpectedCount_WhenCreatingJobPost_GivenJobIdJobTitleAndJobDesc()
        {
            Given.JobId = 1;
            Given.JobTitle = "exampleJobTitle";
            Given.JobDesc = "exampleJobDesc";

            When(CreatingJobPost);

            Then.ExpectedCount.Should().Be(1, "one job post has been added by the company");
        }

        [Fact, PositiveTest]
        public void ShouldReturnExpectedSkillset_WhenCreatingJobPostRequirements_GivenJobIdAndRequiredSkillset()
        {
            Given.JobId = 1;
            Given.RequiredSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MongoDB = true }
            };

            When(CreatingJobPostRequirements);

            Then.ExpectedSkillset.FrontendSkillset.HTML.Should().Be(true, "the company set HTML to be a required skillset");
            Then.ExpectedSkillset.BackendSkillset.CSharp.Should().Be(true, "the company set CSharp to be a required skillset");
            Then.ExpectedSkillset.DatabaseSkillset.MongoDB.Should().Be(true, "the company set MongoDB to be a required skillset");
        }

        [Fact, PositiveTest]
        public void ShouldReturnExpectedCount_WhenPostingJobApplicant_GivenJobIdAndApplicant()
        {
            Given.JobId = 1;
            Given.Applicant = new ApplicantModel("exampleFirstName", "exampleLastName");

            When(PostingJobApplicant);

            Then.ExpectedCount.Should().Be(1, "one applicant has applied to the job post");
        }

        private void CreatingJobPost()
        {
            Then.Target.CreateJobPost(Given.JobId, Given.JobTitle, Given.JobDesc);

            Then.ExpectedCount = Then.Target.JobPostings.Count;
        }

        private void CreatingJobPostRequirements()
        {
            Then.Target.CreateJobPost(Given.JobId, "exampleJobTitle", "exampleJobDesc");

            Then.Target.CreateJobPostRequirements(Given.JobId, Given.RequiredSkillset);

            Then.ExpectedSkillset = Then.Target.JobPostings.Find(x => x.JobId == Given.JobId).RequiredSkillset;
        }

        private void PostingJobApplicant()
        {
            Then.Target.CreateJobPost(Given.JobId, "exampleJobTitle", "exampleJobDesc");

            Then.Target.PostJobApplicant(Given.JobId, Given.Applicant);

            Then.ExpectedCount = Then.Target.JobPostings.Find(x => x.JobId == Given.JobId).Applicants.Count;
        }

        protected override void Creating()
        {
            Then.Validator = new CompanyModel.Validator();

            Then.Target = new CompanyModel("exampleJobTitle", "exampleJobDesc");
        }

        private void ValidatingModel()
        {
            Then.Results = Then.Validator.Validate(Given.Model);
        }

        public sealed class Thens
        {
            public CompanyModel.Validator Validator;
            public ValidationResult Results;
            public CompanyModel Target;
            public SkillsetModel ExpectedSkillset;
            public int ExpectedCount;
        }
    }
}
