namespace TopApplicant.Data.Models
{
    public class Applicant
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Skillset Skillset { get; set; }

        public Applicant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
