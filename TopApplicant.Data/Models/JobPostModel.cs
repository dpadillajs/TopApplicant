using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation;

namespace TopApplicant.Data.Models
{
    [DataContract]
    public class JobPostModel
    {
        /// <summary>
        /// Job ID
        /// </summary>
        [DataMember]
        public long JobId { get; set; }

        /// <summary>
        /// Job Title
        /// </summary>
        [DataMember]
        public string JobTitle { get; set; }

        /// <summary>
        /// Job Description
        /// </summary>
        [DataMember]
        public string JobDesc { get; set; }

        /// <summary>
        /// Required Skillset
        /// </summary>
        [DataMember]
        public SkillsetModel RequiredSkillset { get; set; }

        /// <summary>
        /// List of Applicants
        /// </summary>
        [DataMember]
        public List<ApplicantModel> Applicants = new List<ApplicantModel>();

        public JobPostModel(long jobId, string jobTitle, string jobDesc)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            JobDesc = jobDesc;
        }

        /// <summary>
        /// Validation Logic
        /// </summary>
        public class Validator : AbstractValidator<JobPostModel>
        {
            public Validator()
            {
                RuleFor(x => x.JobId).GreaterThan(0);
                RuleFor(x => x.JobTitle).NotEmpty();
                RuleFor(x => x.JobDesc).NotEmpty();
            }
        }
    }
}
