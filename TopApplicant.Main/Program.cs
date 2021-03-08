using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TopApplicant.Data.Models;
using TopApplicant.Service.Rules;

namespace TopApplicant.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var companies = InitializeAllCompanyData();
            var topApplicantRule = new TopApplicantPercentageRule();

            Console.WriteLine("What is your first name?");
            var firstName = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("And last name?");
            var lastName = Console.ReadLine();

            var applicant = new ApplicantModel(firstName, lastName);

            applicant.Skillset = IdentifyApplicantSkillset();
            Console.WriteLine();

            ApplyToJobPostings(companies, applicant, topApplicantRule);
        }

        #region Initialize All Company Data

        public static List<CompanyModel> InitializeAllCompanyData()
        {

            var Google = new CompanyModel("Google", "Specializes in Internet-related Services and Products.");
            var LinkedIn = new CompanyModel("LinkedIn", "Specializes in Employment-oriented Online Services.");
            var Amazon = new CompanyModel("Amazon", "Specializes in E-commerce, Cloud Computing, and Digital Streaming.");

            Google.CreateJobPost(297, "UI Developer", "Creating UI Components in Angular for the Google Fiber Product Team.");
            Google.CreateJobPost(921, "Data Engineer", "Creating Storage Solutions for large scale applications on the Google Cloud Team.");
            LinkedIn.CreateJobPost(207, "Senior Developer", "Ability to work in a rapid fire test-driven development environment.");
            LinkedIn.CreateJobPost(722, "React Developer", "Ability to work with RESTful APIs within our LinkedIn Learning Team.");
            Amazon.CreateJobPost(884, ".NET Developer", "Help us build the next set of microservices for our Amazon digital products.");
            Amazon.CreateJobPost(983, "Associate Software Engineer", "Must have 1 year of experience. Technical Support for Amazon Delivery Support Teams.");

            Google.CreateJobPostRequirements(297, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true, Angular = true, AngularJS = true },
                BackendSkillset = new BackendSkillsetModel(),
                DatabaseSkillset = new DatabaseSkillsetModel()
            });

            Google.CreateJobPostRequirements(921, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() { Python = true, Django = true },
                BackendSkillset = new BackendSkillsetModel() { GoLang = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MongoDB = true, MSSQL = true, TransactSQL = true }
            });

            LinkedIn.CreateJobPostRequirements(207, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() {  Angular = true, React = true, Vue = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true, Java = true, SpringFramework = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MongoDB = true, PostgresSQL = true }
            });

            LinkedIn.CreateJobPostRequirements(722, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true, React = true, TypeScript = true },
                BackendSkillset = new BackendSkillsetModel() { Node = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { PostgresSQL = true }
            });

            Amazon.CreateJobPostRequirements(884, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() {  HTML = true, CSS = true, JavaScript = true, Vue = true, TypeScript = true },
                BackendSkillset = new BackendSkillsetModel() { CSharp = true, DotNetCore = true },
                DatabaseSkillset = new DatabaseSkillsetModel() { MSSQL = true, TransactSQL = true }
            });

            Amazon.CreateJobPostRequirements(983, new SkillsetModel() {
                FrontendSkillset = new FrontendSkillsetModel() { HTML = true, CSS = true, JavaScript = true },
                BackendSkillset = new BackendSkillsetModel() { Node = true },
                DatabaseSkillset = new DatabaseSkillsetModel()
            });

            return InitializeJobApplicants(new List<CompanyModel>() { Google, LinkedIn, Amazon });
        }

        public static List<CompanyModel> InitializeJobApplicants(List<CompanyModel> companies)
        {
            var applicants = new List<ApplicantModel>()
            {
                new ApplicantModel("David", "Padilla"),
                new ApplicantModel("Don", "Draper"),
                new ApplicantModel("Rick", "Grimes"),
                new ApplicantModel("Sarah", "Connor"),
                new ApplicantModel("Jon", "Snow"),
                new ApplicantModel("Walter", "White"),
                new ApplicantModel("Ragnar", "Lothbrok"),
                new ApplicantModel("Dwayne", "Johnson"),
                new ApplicantModel("Sabrina", "Spellman"),
                new ApplicantModel("Daphne", "Bridgerton")
            };

            foreach (var applicant in applicants)
            {
                applicant.Skillset = Randomizer();

                foreach (var company in companies)
                {
                    foreach (var jobPost in company.JobPostings)
                    {
                        jobPost.Applicants.Add(applicant);
                    }
                }
            }

            SkillsetModel Randomizer()
            {
                var skillset = new SkillsetModel()
                {
                    FrontendSkillset = new FrontendSkillsetModel(),
                    BackendSkillset = new BackendSkillsetModel(),
                    DatabaseSkillset = new DatabaseSkillsetModel()
                };

                foreach (var lang in skillset.FrontendSkillset.GetType().GetProperties())
                    lang.SetValue(skillset.FrontendSkillset, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                foreach (var lang in skillset.BackendSkillset.GetType().GetProperties())
                    lang.SetValue(skillset.BackendSkillset, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                foreach (var lang in skillset.DatabaseSkillset.GetType().GetProperties())
                    lang.SetValue(skillset.DatabaseSkillset, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                return skillset;
            }

            return companies;
        }


        #endregion Initialize All Company Data

        #region Console Interaction Methods

        public static SkillsetModel IdentifyApplicantSkillset()
        {
            var skillset = new SkillsetModel()
            {
                FrontendSkillset = new FrontendSkillsetModel(),
                BackendSkillset = new BackendSkillsetModel(),
                DatabaseSkillset = new DatabaseSkillsetModel()
            };

            SkillsetQuestionaire("front-end", skillset.FrontendSkillset);
            SkillsetQuestionaire("back-end", skillset.BackendSkillset);
            SkillsetQuestionaire("database", skillset.DatabaseSkillset);

            return skillset;

            void SkillsetQuestionaire(string skillsetName, dynamic skillsetClass)
            {
                Console.WriteLine();
                Console.WriteLine($"Do you have any experience working with {skillsetName} technology?");
                Console.WriteLine("NOTE: Please key in y or n for each question.");

                var userSkillsetValidation = Console.ReadKey();
                Console.WriteLine();


                if (userSkillsetValidation.Key == ConsoleKey.Y)
                    foreach (var lang in skillsetClass.GetType().GetProperties())
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Have you worked with {lang.Name} before?");

                        lang.SetValue(skillsetClass, Console.ReadKey().Key == ConsoleKey.Y ? true : false);
                        Console.WriteLine();
                    }
            }
        }

        public static void ApplyToJobPostings(List<CompanyModel> companies, ApplicantModel applicant, TopApplicantPercentageRule rule)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Job Postings by Company:");
            Console.WriteLine();

            var jobDictionary = new Dictionary<long, string>();

            foreach(var company in companies)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("——————————————————————————————————————————————————————————————");
                Console.WriteLine($"{company.CompanyName}");
                Console.WriteLine($"{company.CompanyDesc}");
                Console.WriteLine("——————————————————————————————————————————————————————————————");
                Console.WriteLine();

                foreach (var jobPost in company.JobPostings)
                {
                    jobDictionary.Add(jobPost.JobId, company.CompanyName);

                    var topApplicantPercentage = rule.RetrieveTopApplicantPercentage(applicant.Skillset, jobPost.Applicants, jobPost.RequiredSkillset);

                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    Console.WriteLine(topApplicantPercentage != 0 ?
                        $"You’re in the top {topApplicantPercentage}% of {jobPost.Applicants.Count} applicants based on your skillset!"
                        : $"{jobPost.Applicants.Count} applicants");

                    Console.WriteLine($"JOBID_{jobPost.JobId} —— {jobPost.JobTitle} ({company.CompanyName})");
                    Console.WriteLine($"JOBID_{jobPost.JobId} —— {jobPost.JobDesc}");
                    Console.WriteLine($"Actively recruiting");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("NOTE: Please type in the JOBID (ex: 123) for the job you would like to apply for.");
            Console.WriteLine();

            var appliedJobId = long.TryParse(Console.ReadLine(), out long jobId) ? jobId : 0;
            var companyData = companies.FirstOrDefault(x => jobDictionary.TryGetValue(appliedJobId, out var companyName) && x.CompanyName == companyName);
            var companyDataRetrievalResponse = companyData != null ? true : false;

            if (companyDataRetrievalResponse)
            {
                companies.Find(x => x.CompanyName == jobDictionary[appliedJobId]).PostJobApplicant(appliedJobId, applicant);
                Console.WriteLine();
                Console.WriteLine($"Your application has been received Mr/Ms/Mrs {applicant.FirstName} {applicant.LastName}.");
                Console.WriteLine();
                Console.WriteLine("Type any key to continue or q to quit.");

                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    ApplyToJobPostings(companies, applicant, rule);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"An error occured. Your application was not received. Please try again.");
                Console.WriteLine();
                Console.WriteLine("Type any key to continue or q to quit.");

                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine();

                    Console.WriteLine();
                    ApplyToJobPostings(companies, applicant, rule);
                    Console.WriteLine();
                }
            }
        }

        #endregion Console Interaction Methods
    }
}
