using System.Collections.Generic;
using FluentAssertions;
using GwtUnit.XUnit;
using TopApplicant.Data.Models;
using TopApplicant.Service.Rules;
using Xunit;

namespace TopApplicant.Test.Service.Rules
{
    public sealed class TopApplicantPercentageRuleTests : XUnitTestBase<TopApplicantPercentageRuleTests.Thens>
    {

        [Fact, PositiveTest]
        public void ShouldReturnTop10Percentage_WhenProcessing_GivenApplicantSkillset()
        {
            Given.ApplicantSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MSSQL = true, TransactSQL = true }
            };

            When(Processing);

            Then.Percentage.Should().Be(10, "the applicant has enough required skills to be in the Top 10% of applicants");
        }

        [Fact, PositiveTest]
        public void ShouldReturnTop25Percentage_WhenProcessing_GivenApplicantSkillset()
        {
            Given.ApplicantSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                DatabaseSkillset = new DatabaseSkillsetModel()
            };

            When(Processing);

            Then.Percentage.Should().Be(25, "the applicant has enough required skills to be in the Top 25% of applicants");
        }

        [Fact, PositiveTest]
        public void ShouldReturnTop50Percentage_WhenProcessing_GivenApplicantSkillset()
        {
            Given.ApplicantSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true },
                DatabaseSkillset = new DatabaseSkillsetModel()
            };

            When(Processing);

            Then.Percentage.Should().Be(50, "the applicant has enough required skills to be in the Top 50% of applicants");
        }

        [Fact, PositiveTest]
        public void ShouldReturnNoTopPercentage_WhenProcessing_GivenApplicantSkillset()
        {
            Given.ApplicantSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel(),
                BackendSkillset = new BackendSkillsetModel(),
                DatabaseSkillset = new DatabaseSkillsetModel()
            };

            When(Processing);

            Then.Percentage.Should().Be(0, "the applicant does not have any of the required skills");
        }

        private void Processing()
        {
            Given.RequiredSkillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MSSQL = true, TransactSQL = true }
            };

            Given.ListOfContenders = InitializeContenders();

            Then.Percentage = Then.Target.RetrieveTopApplicantPercentage(Given.ApplicantSkillset, Given.ListOfContenders, Given.RequiredSkillset);
        }

        private List<ApplicantModel> InitializeContenders()
        {
            return new List<ApplicantModel>()
            {
                new ApplicantModel("Test_01", "Subject_01") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_02", "Subject_02") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_03", "Subject_03") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_04", "Subject_04") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_05", "Subject_05") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_06", "Subject_06") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_07", "Subject_07") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel() { CSharp = true },
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_08", "Subject_08") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel() { CSharp = true },
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_09", "Subject_09") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                    DatabaseSkillset = new DatabaseSkillsetModel() }
                },
                new ApplicantModel("Test_10", "Subject_10") { Skillset = new SkillsetModel() {
                    FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                    BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                    DatabaseSkillset = new DatabaseSkillsetModel() { MSSQL = true } }
                }
            };
        }

        protected override void Creating()
        {
            Then.Target = new TopApplicantPercentageRule();
        }

        public sealed class Thens
        {
            public TopApplicantPercentageRule Target;
            public int Percentage;
        }
    }
}
