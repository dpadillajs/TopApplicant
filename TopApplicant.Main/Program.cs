using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TopApplicant.Data.Models;

namespace TopApplicant.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Company> companies = InitializeAllCompanyData();

            Console.WriteLine("What is your first name?");
            string firstName = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("And last name?");
            string lastName = Console.ReadLine();

            var applicant = new Applicant(firstName, lastName);

            applicant.Skillset = IdentifyApplicantSkillset();
            Console.WriteLine();

            ApplyToJobPostings(companies, applicant);
        }

        #region Initialize All Company Data

        public static List<Company> InitializeAllCompanyData()
        {

            var Google = new Company("Google", "Specializes in Internet-related Services and Products.");
            var LinkedIn = new Company("LinkedIn", "Specializes in Employment-oriented Online Services.");
            var Amazon = new Company("Amazon", "Specializes in E-commerce, Cloud Computing, and Digital Streaming.");

            Google.CreateJobPost(297, "UI Developer", "Creating UI Components in Angular for the Google Fiber Product Team.");
            Google.CreateJobPost(921, "Data Engineer", "Creating Storage Solutions for large scale applications on the Google Cloud Team.");
            LinkedIn.CreateJobPost(207, "Senior Developer", "Ability to work in a rapid fire test-driven development environment.");
            LinkedIn.CreateJobPost(722, "React Developer", "Ability to work with RESTful APIs within our LinkedIn Learning Team.");
            Amazon.CreateJobPost(884, ".NET Developer", "Help us build the next set of microservices for our Amazon digital products.");
            Amazon.CreateJobPost(983, "Associate Software Engineer", "Must have 1 year of experience. Technical Support for Amazon Delivery Support Teams.");

            Google.CreateJobPostRequirements(297, new Skillset() {
                FrontendSkills = new FrontendSkills() { HTML = true, CSS = true, JavaScript = true, Angular = true, AngularJS = true },
                BackendSkills = new BackendSkills(),
                DatabaseSkills = new DatabaseSkills()
            });

            Google.CreateJobPostRequirements(921, new Skillset() {
                FrontendSkills = new FrontendSkills() { Python = true, Django = true },
                BackendSkills = new BackendSkills() { GoLang = true },
                DatabaseSkills = new DatabaseSkills() { MongoDB = true, MSSQL = true, TransactSQL = true }
            });

            LinkedIn.CreateJobPostRequirements(207, new Skillset() {
                FrontendSkills = new FrontendSkills() {  Angular = true, React = true, Vue = true },
                BackendSkills = new BackendSkills() { CSharp = true, DotNetCore = true, Java = true, SpringFramework = true },
                DatabaseSkills = new DatabaseSkills() { MongoDB = true, PostgresSQL = true }
            });

            LinkedIn.CreateJobPostRequirements(722, new Skillset() {
                FrontendSkills = new FrontendSkills() { HTML = true, CSS = true, JavaScript = true, React = true, TypeScript = true },
                BackendSkills = new BackendSkills() { Node = true },
                DatabaseSkills = new DatabaseSkills() { PostgresSQL = true }
            });

            Amazon.CreateJobPostRequirements(884, new Skillset() {
                FrontendSkills = new FrontendSkills() {  HTML = true, CSS = true, JavaScript = true, Vue = true, TypeScript = true },
                BackendSkills = new BackendSkills() { CSharp = true, DotNetCore = true },
                DatabaseSkills = new DatabaseSkills() { MSSQL = true, TransactSQL = true }
            });

            Amazon.CreateJobPostRequirements(983, new Skillset() {
                FrontendSkills = new FrontendSkills() { HTML = true, CSS = true, JavaScript = true },
                BackendSkills = new BackendSkills() { Node = true },
                DatabaseSkills = new DatabaseSkills()
            });

            return InitializeJobApplicants(new List<Company>() { Google, LinkedIn, Amazon });
        }

        public static List<Company> InitializeJobApplicants(List<Company> companies)
        {
            var applicants = new List<Applicant>()
            {
                new Applicant("David", "Padilla"),
                new Applicant("Don", "Draper"),
                new Applicant("Rick", "Grimes"),
                new Applicant("Sarah", "Connor"),
                new Applicant("Jon", "Snow"),
                new Applicant("Walter", "White"),
                new Applicant("Ragnar", "Lothbrok"),
                new Applicant("Dwayne", "Johnson"),
                new Applicant("Sabrina", "Spellman"),
                new Applicant("Daphne", "Bridgerton")
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

            Skillset Randomizer()
            {
                var skillset = new Skillset()
                {
                    FrontendSkills = new FrontendSkills(),
                    BackendSkills = new BackendSkills(),
                    DatabaseSkills = new DatabaseSkills()
                };

                foreach (var lang in skillset.FrontendSkills.GetType().GetProperties())
                    lang.SetValue(skillset.FrontendSkills, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                foreach (var lang in skillset.BackendSkills.GetType().GetProperties())
                    lang.SetValue(skillset.BackendSkills, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                foreach (var lang in skillset.DatabaseSkills.GetType().GetProperties())
                    lang.SetValue(skillset.DatabaseSkills, Convert.ToBoolean(Math.Round(new Random().NextDouble())));

                return skillset;
            }

            return companies;
        }


        #endregion Initialize All Company Data

        #region Console Interaction Methods

        public static Skillset IdentifyApplicantSkillset()
        {
            var skillset = new Skillset()
            {
                FrontendSkills = new FrontendSkills(),
                BackendSkills = new BackendSkills(),
                DatabaseSkills = new DatabaseSkills()
            };

            SkillsetQuestionaire("front-end", skillset.FrontendSkills);
            SkillsetQuestionaire("back-end", skillset.BackendSkills);
            SkillsetQuestionaire("database", skillset.DatabaseSkills);

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

        public static void ApplyToJobPostings(List<Company> companies, Applicant applicant)
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

                    // Algorithm will be implemented later.
                    // RunTopApplicantPercentageRetrieval(applicant.Skillset, jobPost.Applicants, jobPost.RequiredSkillset);

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine($"{jobPost.Applicants.Count} applicants");
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
                    ApplyToJobPostings(companies, applicant);
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
                    ApplyToJobPostings(companies, applicant);
                    Console.WriteLine();
                }
            }
        }

        #endregion Console Interaction Methods

        #region The Top Applicant Algorithm

        public static void RunTopApplicantPercentageRetrieval(Skillset applicantSkillset, List<Applicant> contenders, Skillset requiredSkillset)
        {
            // Step One: Add Required Skills, Matching Applicant Skills, And Contender Skills To Seperate Lists
            var employerSkillsetRequirements = new List<string>();
            var applicantMatchedSkillset = new List<string>();
            var contenderMatchingSkillsets = new List<List<string>>();

            foreach (var contender in contenders)
            {
                var contenderMatchingSkillset = new List<string>();

                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.FrontendSkills, contender.Skillset.FrontendSkills, requiredSkillset.FrontendSkills, contenderMatchingSkillset);
                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.BackendSkills, contender.Skillset.BackendSkills, requiredSkillset.FrontendSkills, contenderMatchingSkillset);
                AddMatchingSkillsToApplicantAndContenders(applicantSkillset.DatabaseSkills, contender.Skillset.DatabaseSkills, requiredSkillset.FrontendSkills, contenderMatchingSkillset);

                contenderMatchingSkillsets.Add(contenderMatchingSkillset);
            }

            void AddMatchingSkillsToApplicantAndContenders(dynamic applicantSkillset, dynamic contenderSkillset, dynamic requiredSkillset, List<string> contenderMatchingSkillset)
            {
                foreach (var conSkill in contenderSkillset.GetType().GetProperties())
                {
                    foreach (var appSkill in applicantSkillset.GetType().GetProperties())
                    {
                        foreach (var reqSkill in requiredSkillset.GetType().GetProperties())
                        {
                            if (reqSkill.GetValue(requiredSkillset) && !employerSkillsetRequirements.Contains(reqSkill.Name))
                                employerSkillsetRequirements.Add(reqSkill.Name);
                            if (appSkill.Name == reqSkill.Name && appSkill.GetValue(applicantSkillset) && reqSkill.GetValue(requiredSkillset) && !applicantMatchedSkillset.Contains(appSkill.Name))
                                applicantMatchedSkillset.Add(appSkill.Name);
                            if (conSkill.Name == reqSkill.Name && conSkill.GetValue(contenderSkillset) && reqSkill.GetValue(requiredSkillset) && !contenderMatchingSkillset.Contains(conSkill.Name))
                                contenderMatchingSkillset.Add(conSkill.Name);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
