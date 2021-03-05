using System.Collections.Generic;

namespace TopApplicant.Data.Models
{
    public class JobPost
    {
        public long JobId;

        public string JobTitle;

        public string JobDesc;

        public Skillset RequiredSkillset;

        public List<Applicant> Applicants = new List<Applicant>();

        public JobPost(long jobId, string jobTitle, string jobDesc)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            JobDesc = jobDesc;
        }
    }
}
