using System.Collections.Generic;

namespace TopApplicant.Data.Models
{
    public class Company
    {
        public string CompanyName { get; set; }

        public string CompanyDesc { get; set; }

        public List<JobPost> JobPostings = new List<JobPost>();

        /// <summary>
        /// Creates Job Post for Company
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobTitle"></param>
        /// <param name="jobDesc"></param>
        public void CreateJobPost(long jobId, string jobTitle, string jobDesc)
        {
            JobPostings.Add(new JobPost(jobId, jobTitle, jobDesc));
        }

        /// <summary>
        /// Updates Skillset Requirement For Job Post
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="requiredSkillset"></param>
        public void CreateJobPostRequirements(long jobId, Skillset requiredSkillset)
        {
            JobPostings.Find(x => x.JobId == jobId).RequiredSkillset = requiredSkillset;
        }

        /// <summary>
        /// Posts New Applicant To Job Post
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="applicant"></param>
        public void PostJobApplicant(long jobId, Applicant applicant)
        {
            JobPostings.Find(x => x.JobId == jobId).Applicants.Add(applicant);
        }

        public Company(string companyName, string companyDesc)
        {
            CompanyName = companyName;
            CompanyDesc = companyDesc;
        }
    }
}
