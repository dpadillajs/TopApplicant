using System.Collections.Generic;

namespace TopApplicant.Data.Models
{
    public class JobPost
    {
        public long JobId { get; set; }

        public string JobTitle { get; set; }

        public string JobDesc { get; set; }

        public Skillset RequiredSkillset { get; set; }

        public List<Applicant> Applicants = new List<Applicant>();

        public JobPost(long jobId, string jobTitle, string jobDesc)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            JobDesc = jobDesc;
        }
    }
}
