using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class CompanyModel
    {
        /// <summary>
        /// Company Name
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }

        /// <summary>
        /// Company Description
        /// </summary>
        [DataMember]
        public string CompanyDesc { get; set; }

        /// <summary>
        /// Job Postings
        /// </summary>
        [DataMember]
        public List<JobPostModel> JobPostings = new List<JobPostModel>();

        /// <summary>
        /// Creates Job Post for Company
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobTitle"></param>
        /// <param name="jobDesc"></param>
        public void CreateJobPost(long jobId, string jobTitle, string jobDesc)
        {
            JobPostings.Add(new JobPostModel(jobId, jobTitle, jobDesc));
        }

        /// <summary>
        /// Updates Skillset Requirement For Job Post
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="requiredSkillset"></param>
        public void CreateJobPostRequirements(long jobId, SkillsetModel requiredSkillset)
        {
            JobPostings.Find(x => x.JobId == jobId).RequiredSkillset = requiredSkillset;
        }

        /// <summary>
        /// Posts New Applicant To Job Post
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="applicant"></param>
        public void PostJobApplicant(long jobId, ApplicantModel applicant)
        {
            JobPostings.Find(x => x.JobId == jobId).Applicants.Add(applicant);
        }

        public CompanyModel(string companyName, string companyDesc)
        {
            CompanyName = companyName;
            CompanyDesc = companyDesc;
        }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<CompanyModel>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyName).NotEmpty();
                RuleFor(x => x.CompanyDesc).NotEmpty();
            }
        }
    }
}
